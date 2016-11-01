module AllDisplayedData
    open DecoderTypes
    open VC_840Decoder

    type AllDisplayedData = {
        KindOfCurrent : ACOrDC option
        Value : double option
    }

     let GetAllData raw =
        let decoded = Decode raw
        {
            KindOfCurrent = KindOfCurrent decoded;
            Value = match IsNegative decoded with   
                    | true -> Some(-1.0)
                    | false -> Some(1.0)
        }