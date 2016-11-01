module AllDisplayedData
    open DecoderTypes
    open VC_840Decoder
    open Digit

    type AllDisplayedData = {
        KindOfCurrent : ACOrDC option
        Value : double option
    }

     let GetAllData raw =
        let decoded = Decode raw
        {
            KindOfCurrent = KindOfCurrent decoded;
            Value = DecodeDigit decoded TelegramTypes.digitOne Digit.DigitToInt  
                |> Option.map double
                |> Option.map (fun value -> if VC_840Decoder.IsNegative decoded then -value else value)
        }