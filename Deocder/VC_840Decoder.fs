module VC_840Decoder
    open DecoderTypes
    open TelegramTypes  
    open Digit

    let numberOfBytesInTelegram = 14

    let Decode raw =
       let getIndex rawByte = rawByte / byte(Bits.Five)
       let getData rawByte = rawByte &&& byte(Bits.All)
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

    let KindOfCurrent buffer =
        let isAC  =     
            isBitSetInArray buffer positionAC

        let isDC  =     
            isBitSetInArray buffer positionDC

        match (isAC, isDC) with 
        | (true, _) ->  Some(ACOrDC.AC)
        | (_, true) ->  Some(ACOrDC.DC)
        | (_, _) -> None
   

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
              let result =  match (number, acc) with
                            | Some(number), Some(acc) -> Some(timesTenAndAdd number  acc)
                            | _, _ -> None

              result

        let result = List.fold (fun acc digit -> accumlateDigits acc digit) result patternsDigit
        
        result

        
