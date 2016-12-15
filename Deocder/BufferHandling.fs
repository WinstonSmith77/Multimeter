
module BufferHandling
    open Data
    open TelegramData

    let NumberOfBytesInTelegram = 14

    let IsNotStartByte value =
         value / byte(Bits.Five) <> byte(1)   

    let rec FindValidSequence buffer =  
        match buffer with 
        | [] -> None    
        | _  -> match IsNotStartByte (List.head buffer) with    
                | false -> match List.length buffer with
                           | length when length >= NumberOfBytesInTelegram ->  Some(List.take NumberOfBytesInTelegram buffer)
                           | _ -> None
                | true  -> FindValidSequence  (List.tail buffer)

    let MergeBuffers a b =
        Array.concat [Array.ofSeq a; Array.ofSeq b]   

    let GetAllValidBlocksFromBuffer bufferInput =
       let buffer = List.ofSeq bufferInput 
       let rec getAllValidBlockFromBufferInner dataAndBuffer =
          let (dataList, buffer) = dataAndBuffer
         
          let parseFrom = FindValidSequence buffer

          match(parseFrom) with
          | Some(x) -> getAllValidBlockFromBufferInner (x :: dataList,  List.skipWhile IsNotStartByte  buffer |> List.skip NumberOfBytesInTelegram )
          | None    -> dataAndBuffer

       getAllValidBlockFromBufferInner ([], buffer)  

    let GetAllDataFromBuffer buffer =
      let (blocks, remain) = GetAllValidBlocksFromBuffer buffer
      
      (List.map (fun input -> GetAllData input) blocks, remain)

