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
        let digits = [digitOne; digitTwo; digitThree; digitFour]
        let decoded = Decode raw
        let result = {
            KindOfCurrent = KindOfCurrent decoded;
            Value = DecodeAllDigits decoded digits Digit.DigitToInt  
                |> Option.map (fun value -> if VC_840Decoder.IsNegative decoded then -value else value)
        }

        result

     let GetAllDataFromBuffer oldBuffer newBuffer =
         let completeBuffer = List.concat [List.ofSeq oldBuffer; List.ofSeq newBuffer]
        
         let GetAllDataFromBufferInner dataAndBuffer =
            let (dataList, buffer) = dataAndBuffer
            let isNotStartByte value = value / byte(Bits.Five) <> byte(1)
            let parseFrom = List.skipWhile isNotStartByte  buffer |> List.take numberOfBytesInTelegram
            
            if List.length parseFrom = numberOfBytesInTelegram then
             ((GetAllData parseFrom) :: dataList,  List.skipWhile isNotStartByte  buffer |> List.skip numberOfBytesInTelegram )
            else
             dataAndBuffer
            

         (fst(result), Array.ofSeq (snd(result)))