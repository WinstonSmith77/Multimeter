module VC_840Decoder
    open DecoderTypes
    open TelegramTypes  
    open AllDisplayedData

    let numberOfBytesInTelegram = 14

    let decodeInner raw =
       let getIndex rawByte = rawByte / byte(LowerBits.Five)
       let getData rawByte = rawByte &&& byte(LowerBits.All)
       { Buffer =
            raw 
            |> Seq.map (fun rawByte -> (getIndex rawByte, getData rawByte) )
            |> Seq.sortBy (fun (index, _) -> index)
            |> Seq.map (fun (_, data) -> data)
            |> Seq.toArray
        }

    let Decode raw =
        if Seq.length raw <> numberOfBytesInTelegram then
            failwith "Wrong length"
        else
             decodeInner raw

    let isBitSet input index =
         input &&& (byte index) <> byte(0)

    let isBitSetInArray (input:byte array) indexByte indexBit =
        isBitSet input.[int indexByte]  <| indexBit

    let IsNegative buffer =     
        isBitSetInArray buffer.Buffer BytesInTelegram.Second LowerBits.Four

    let KindOfCurrent buffer =

        let isAC  =     
            isBitSetInArray buffer.Buffer BytesInTelegram.First LowerBits.Four

        let isDC  =     
            isBitSetInArray buffer.Buffer BytesInTelegram.First LowerBits.Three

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

    let GetAllData raw =
        let decoded = Decode raw
        {KindOfCurrent = KindOfCurrent decoded}