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

     let rec findValidSequnce buffer =  
        match buffer with 
        | [] -> None    
        | _  -> match isNotStartByte (List.head buffer) with    
                | true  -> findValidSequnce  (List.tail buffer)
                | false -> Some( List.take (min (List.length buffer) numberOfBytesInTelegram) buffer )

     let GetAllDataFromBuffer oldBuffer newBuffer =
       let completeBuffer = List.concat [List.ofSeq oldBuffer; List.ofSeq newBuffer]
      
       let rec GetAllDataFromBufferInner dataAndBuffer =
          let (dataList, buffer) = dataAndBuffer
         
          let parseFrom = findValidSequnce buffer

          match(parseFrom) with
          | Some(x) -> GetAllDataFromBufferInner ((GetAllData x) :: dataList,  List.skipWhile isNotStartByte  buffer |> List.skip numberOfBytesInTelegram )
          | None    -> dataAndBuffer

       let result = GetAllDataFromBufferInner ([], completeBuffer)  
       (fst(result), Array.ofSeq (snd(result)))