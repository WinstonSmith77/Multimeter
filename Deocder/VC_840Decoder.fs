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

    let isBitSet input index =
         input &&& (byte index) <> byte(0)

    let isBitSetInArray (input:DecodedBuffer) position =
        isBitSet input.Buffer.[int position.Byte]  position.Bit

    let IsNegativeScaling buffer =     
       match isBitSetInArray buffer postionNegativeSign with
       | true -> -1
       | false -> 1

    let findInMapping buffer mapping =
        let allFound = List.where (fun (_, position) -> isBitSetInArray buffer position) mapping

        TryFirst allFound
     

    let FindScaling buffer factorToPosition =
        let foundScaling = findInMapping buffer factorToPosition
        match foundScaling with 
        | Some(factor, _) -> getFactorValue factor
        | None -> 0

    let FindUnit buffer unitToPosition =
        let foundUnit = findInMapping buffer unitToPosition
        FirstFromOptionTuple foundUnit

    let KindOfCurrent buffer currentToBuffer =
        let foundCurrent = findInMapping buffer currentToBuffer
        FirstFromOptionTuple foundCurrent

    let BufferToString buffer = 
        let innerResult =
            buffer.Buffer
            |> Seq.mapi (fun index data -> byte(index) * byte(16) + data)
            |> Seq.fold (fun acc (input:byte) -> acc +  "0x" + input.ToString("x2") + ", ") ""  
        "new byte[] {" + innerResult.TrimEnd(',') + "}";
   
    let DecodeDigit buffer patternDigit digitToInt =        
        let isBitSet position =  isBitSetInArray buffer position
        let segmentsSet = List.fold (fun acc (segment, position) -> if isBitSet position then Set.add segment acc else acc) Set.empty patternDigit

        let result = List.tryFind (fun (digit, number) -> digit.Segments = segmentsSet) digitToInt

        Option.bind (fun (_, number) -> Some(number)) result

    let DecodeAllDigits buffer patternsDigit digitToInit =    
        let result = Some(0)
        let timesTenAndAdd  b a = a * 10 + b

        let accumlateDigits acc digit = 
              let number = DecodeDigit buffer digit digitToInit

              MapTwoOptionsIfBothAreSome timesTenAndAdd number acc

        let result = List.fold (fun acc digit -> accumlateDigits acc digit) result patternsDigit
        
        result

        
