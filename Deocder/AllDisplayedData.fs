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