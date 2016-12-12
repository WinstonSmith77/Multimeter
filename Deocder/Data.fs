module Data
    open MeasurementTypes
    open Decoding
    open Digit
    open TelegramData

     type Factor = { 
                     Value:int
                     Text:string
                   }

     type AllDisplayedData = {
        KindOfCurrent : ACOrDC option
        Value : double option
        Factor : Factor
        Unit : string
     }

     let GetScalingFromDecimalPoints decimalPointOne decimalPointTwo decimalPointThree decoded =
        let isBitSet = IsBitSetInArray decoded
        match(isBitSet decimalPointOne, isBitSet decimalPointTwo, isBitSet decimalPointThree) with
        | (true, false, false) -> Some(0.1)
        | (false, true, false) -> Some(0.01)
        | (false, false, true) -> Some(0.001)
        | _ -> None

     let GetAllData raw =
        let digits = [digitFour; digitThree; digitTwo; digitOne]
        let decoded = Decode raw
        let scalingDueToDecimalPointer = GetScalingFromDecimalPoints decimalPointOne decimalPointTwo decimalPointThree decoded
        let factor = FindFactor decoded factorToPosition
        
        {
            Unit = FindUnit decoded unitToPosition  |> UnitToString
            Factor = { Value = FactorToValue factor;  Text = FactorToString factor }
            KindOfCurrent = KindOfCurrent decoded currentToPosition
            Value = DecodeAllDigits decoded digits DigitToInt  
                |> Option.map (fun value ->  value * IsNegativeScaling decoded)
                |> Helper.MapTwoOptionsIfBothAreSome  (fun a b -> a * float(b)) scalingDueToDecimalPointer
        }

    