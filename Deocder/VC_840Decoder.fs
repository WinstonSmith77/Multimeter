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

    let getIndex rawByte = rawByte / byte(LowerBits.Five)
    let getData rawByte = rawByte &&& byte(LowerBits.All)

    let decodeInner raw =
        raw 
        |> Seq.map (fun (rawByte:byte) -> (getIndex rawByte , getData rawByte) )
        |> Seq.sortBy (fun (index, _) -> index)
        |> Seq.map (fun (_, data) -> data)
        |> Seq.toArray

    let Decode raw =
        if Seq.length raw <> numberOfBytesInTelegram then
            failwith "Wrong length"
        else
             decodeInner raw