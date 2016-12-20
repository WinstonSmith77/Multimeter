module Data
    open MeasurementData
    open Decoding
    open Digit
    open TelegramData
    open Helper

     type AllDisplayedData = {
        Current : Current option
        ValueUnscaled : float option
        Unit : Unit option
        Factor : Factor option
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
        
        let valueUnscaled = DecodeAllDigits decoded digits DigitToInt  
                            |> Option.map (fun value ->  value * IsNegativeScaling decoded)
                            |> Helper.MapTwoOptionsIfBothAreSome  (fun a b -> a * float(b)) scalingDueToDecimalPointer
        {
            Unit = FindUnit decoded unitToPosition
            Current = KindOfCurrent decoded currentToPosition
            ValueUnscaled = valueUnscaled
            Factor = FindFactor decoded factorToPosition
        }

     let ScaledValue unscaled = MapTwoOptionsIfBothAreSome (fun value (factor:Factor) -> value * 10.0** float(factor.ToValue())) unscaled.ValueUnscaled unscaled.Factor 
         
