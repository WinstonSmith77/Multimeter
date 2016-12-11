module AllDisplayedData
    open MeasurementTypes
    open VC_840Decoder
    open Digit
    open TelegramData

     type AllDisplayedData = {
        KindOfCurrent : ACOrDC option
        Value : double option
        Factor : int
        Unit : Unit option
     }

     let GetScalingFromDecimalPoints decimalPointOne decimalPointTwo decimalPointThree decoded =
        let isBitSet = isBitSetInArray decoded
        match(isBitSet decimalPointOne, isBitSet decimalPointTwo, isBitSet decimalPointThree) with
        | (true, false, false) -> Some(0.1)
        | (false, true, false) -> Some(0.01)
        | (false, false, true) -> Some(0.001)
        | _ -> None


     let GetAllData raw =
        let digits = [digitFour; digitThree; digitTwo; digitOne]
        let decoded = Decode raw
        let scalingDueToDecimalPointer = GetScalingFromDecimalPoints decimalPointOne decimalPointTwo decimalPointThree decoded
        let result = {
            Unit = FindUnit decoded unitToPosition
            Factor = FindScaling decoded factorToPosition
            KindOfCurrent = KindOfCurrent decoded currentToPosition
            Value = DecodeAllDigits decoded digits DigitToInt  
                |> Option.map (fun value ->  value * IsNegativeScaling decoded)
                |> Option.map (fun value -> double(value))
                |> Helper.MapTwoOptionsIfBothAreSome  (fun a b -> a * b) scalingDueToDecimalPointer
        }

        result

     let isNotStartByte value =
         value / byte(Bits.Five) <> byte(1)   

     let rec findValidSequence buffer =  
        match buffer with 
        | [] -> None    
        | _  -> match isNotStartByte (List.head buffer) with    
                | false -> match List.length buffer with
                           | length when length >= numberOfBytesInTelegram ->  Some(List.take numberOfBytesInTelegram buffer)
                           | _ -> None
                | true  -> findValidSequence  (List.tail buffer)

     let MergeBuffers a b =
        List.concat [List.ofSeq a; List.ofSeq b]   

     let GetAllDataFromBuffer buffer =
      
       let rec GetAllDataFromBufferInner dataAndBuffer =
          let (dataList, buffer) = dataAndBuffer
         
          let parseFrom = findValidSequence buffer

          match(parseFrom) with
          | Some(x) -> GetAllDataFromBufferInner ((GetAllData x) :: dataList,  List.skipWhile isNotStartByte  buffer |> List.skip numberOfBytesInTelegram )
          | None    -> dataAndBuffer

       let result = GetAllDataFromBufferInner ([], buffer)  
       result