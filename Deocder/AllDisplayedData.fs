module AllDisplayedData
    open DecoderTypes
    open VC_840Decoder
    open Digit
    open TelegramTypes

     type AllDisplayedData = {
        KindOfCurrent : ACOrDC option
        Value : double option
     }

     let GetAllData raw =
        let digits = [digitFour; digitThree; digitTwo; digitOne]
        let decoded = Decode raw
        let result = {
            KindOfCurrent = KindOfCurrent decoded;
            Value = DecodeAllDigits decoded digits Digit.DigitToInt  
                |> Option.map (fun value ->  value * VC_840Decoder.IsNegativeScaling decoded)
                |> Option.map (fun value -> double(value))
        }

        result

     let isNotStartByte value =
         value / byte(Bits.Five) <> byte(1)   

     let rec findValidSequence buffer =  
        match buffer with 
        | [] -> None    
        | _  -> match isNotStartByte (List.head buffer) with    
                | true  -> findValidSequence  (List.tail buffer)
                | false -> Some( List.take (min (List.length buffer) numberOfBytesInTelegram) buffer )

     let MergeBuffers a b =
        List.concat [List.ofSeq a; List.ofSeq b]   

     let GetAllDataFromBuffer buffer =
      
       let rec GetAllDataFromBufferInner dataAndBuffer =
          let (dataList, buffer) = dataAndBuffer
         
          let parseFrom = findValidSequence buffer

          match(parseFrom) with
          | Some(x) -> GetAllDataFromBufferInner ((GetAllData x) :: dataList,  List.skipWhile isNotStartByte  buffer |> List.skip numberOfBytesInTelegram )
          | None    -> dataAndBuffer

       let result = GetAllDataFromBufferInner ([], buffer)  
       result