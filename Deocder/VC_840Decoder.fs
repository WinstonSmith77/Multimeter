module VC_840Decoder
    open TelegramData
    open Digit
    open Helper
    open MeasurementTypes

   

    let Decode raw =
       let getIndex rawByte = rawByte / byte(Bits.Five)
       let getData rawByte = rawByte &&& byte(Bits.AllLowerHalf)
       { Buffer =
            raw 
            |> Seq.map (fun rawByte -> (getIndex rawByte, getData rawByte) )
            |> Seq.sortBy (fun (index, _) -> index)
            |> Seq.map (fun (_, data) -> data)
            |> Seq.toArray
        }

    let IsBitSet input index =
         input &&& (byte index) <> byte(0)

    let IsBitSetInArray input position =
        IsBitSet input.Buffer.[int position.Byte]  position.Bit

    let IsNegativeScaling buffer =     
       match IsBitSetInArray buffer postionNegativeSign with
       | true -> -1
       | false -> 1

    let FindInMapping buffer mapping =
        let allFound = List.where (fun (_, position) -> IsBitSetInArray buffer position) mapping

        TrySingle allFound

    let FindFactor buffer factorToPosition =
        FindInMapping buffer factorToPosition |> FirstFromOptionTuple

    let FindUnit buffer unitToPosition =
        let foundUnit = FindInMapping buffer unitToPosition
        FirstFromOptionTuple foundUnit

    let KindOfCurrent buffer currentToBuffer =
        let foundCurrent = FindInMapping buffer currentToBuffer
        FirstFromOptionTuple foundCurrent

    let BufferToString buffer = 
        let innerResult =
            buffer.Buffer
            |> Seq.mapi (fun index data -> byte(index) * byte(16) + data)
            |> Seq.fold (fun acc (input:byte) -> acc +  "0x" + input.ToString("x2") + ", ") ""  
        "new byte[] {" + innerResult.TrimEnd(',') + "}";
   
    let DecodeDigit buffer patternDigit digitToInt =        
        let isBitSet position =  IsBitSetInArray buffer position
        let segmentsSet = List.fold (fun acc (segment, position) -> if isBitSet position then Set.add segment acc else acc) Set.empty patternDigit

        let result = List.tryFind (fun (digit, _) -> digit.Segments = segmentsSet) digitToInt

        Option.bind (fun (_, number) -> Some(number)) result

    let DecodeAllDigits buffer patternsDigit digitToInit =    
        let timesTenAndAdd  b a = a * 10 + b

        let accumlateDigits acc digit = 
              let number = DecodeDigit buffer digit digitToInit

              MapTwoOptionsIfBothAreSome timesTenAndAdd number acc

        List.fold (fun acc digit -> accumlateDigits acc digit) (Some(0)) patternsDigit

        
