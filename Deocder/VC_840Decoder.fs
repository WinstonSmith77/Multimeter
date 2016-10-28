module VC_840Decoder
    
    let numberOfBytesInTelegram = 14

    [<System.FlagsAttribute>]
     type LowerBits =
           |    One = 1
           |    Two = 2
           |    Three = 4
           |    Four = 8
           |    All = 15
           |    Five = 16
  
    type DecodedBuffer =   
        {
            Buffer:byte array
        }   

    let getIndex rawByte = rawByte / byte(LowerBits.Five)
    let getData rawByte = rawByte &&& byte(LowerBits.All)

    let decodeInner raw =
       { Buffer =
            raw 
            |> Seq.map (fun (rawByte:byte) -> (getIndex rawByte , getData rawByte) )
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
         input &&& byte(index) <> byte(0)

    let isBitSetInArray (input:byte array) indexByte indexBit =
        isBitSet input.[indexByte]  <| indexBit

    let IsNegative buffer =     
        buffer.Buffer.[1] &&& byte(LowerBits.Four) <> byte(0)

    let IsAC buffer =     
        buffer.Buffer.[0] &&& byte(LowerBits.Four) <> byte(0)

    let IsDC buffer =     
        buffer.Buffer.[0] &&& byte(LowerBits.Three) <> byte(0)