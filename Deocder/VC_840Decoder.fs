module VC_840Decoder
    open DecoderTypes
    open TelegramTypes  

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

    let isBitSetInArray (input:byte array) position =
        isBitSet input.[int position.Byte]  position.Bit

    let IsNegative buffer =     
        isBitSetInArray buffer.Buffer postionNegativeSign

    let KindOfCurrent buffer =
        let isAC  =     
            isBitSetInArray buffer.Buffer positionAC

        let isDC  =     
            isBitSetInArray buffer.Buffer positionDC

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
   
