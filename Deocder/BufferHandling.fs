
module BufferHandling
    open Data
    open TelegramData

    let numberOfBytesInTelegram = 14

    let IsNotStartByte value =
         value / byte(Bits.Five) <> byte(1)   

    let rec FindValidSequence buffer =  
        match buffer with 
        | [] -> None    
        | _  -> match IsNotStartByte (List.head buffer) with    
                | false -> match List.length buffer with
                           | length when length >= numberOfBytesInTelegram ->  Some(List.take numberOfBytesInTelegram buffer)
                           | _ -> None
                | true  -> FindValidSequence  (List.tail buffer)

    let MergeBuffers a b =
        List.concat [List.ofSeq a; List.ofSeq b]   

    let GetAllDataFromBuffer buffer =
       let rec getAllDataFromBufferInner dataAndBuffer =
          let (dataList, buffer) = dataAndBuffer
         
          let parseFrom = FindValidSequence buffer

          match(parseFrom) with
          | Some(x) -> getAllDataFromBufferInner ((GetAllData x) :: dataList,  List.skipWhile IsNotStartByte  buffer |> List.skip numberOfBytesInTelegram )
          | None    -> dataAndBuffer

       getAllDataFromBufferInner ([], buffer)  
