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
        }