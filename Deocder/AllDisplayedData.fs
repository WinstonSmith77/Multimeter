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
                |> Option.map (fun value -> if VC_840Decoder.IsNegative decoded then -value else value)
        }

        result

     let isNotStartByte value =
      value / byte(Bits.Five) <> byte(1)   

     let rec findValidSequnce buffer =  
        if List.isEmpty buffer then
           buffer 
        elif isNotStartByte (List.head buffer) then
           findValidSequnce  (List.tail buffer)
        else
           List.take (min (List.length buffer) numberOfBytesInTelegram) buffer 

     let GetAllDataFromBuffer oldBuffer newBuffer =
       let completeBuffer = List.concat [List.ofSeq oldBuffer; List.ofSeq newBuffer]
      
       let rec GetAllDataFromBufferInner dataAndBuffer =
          let (dataList, buffer) = dataAndBuffer
         
          let parseFrom = findValidSequnce buffer
          
          if List.length parseFrom = numberOfBytesInTelegram then
              GetAllDataFromBufferInner ((GetAllData parseFrom) :: dataList,  List.skipWhile isNotStartByte  buffer |> List.skip numberOfBytesInTelegram )
          else
              dataAndBuffer
          

       let result = GetAllDataFromBufferInner ([], completeBuffer)  
       (fst(result), Array.ofSeq (snd(result)))