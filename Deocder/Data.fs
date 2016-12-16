module Data
    open MeasurementData
    open Decoding
    open Digit
    open TelegramData

     type Factor = { 
                     Value:int
                     Text:string
                   }

     type UnitInfo = { 
                        Text:string
                        Unit:Unit option
                     }


     type AllDisplayedData = {
        Current : Current option
        ValueUnscaled : float option
        Unit : UnitInfo
        Factor : Factor
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
        
        let valueUnscaled = DecodeAllDigits decoded digits DigitToInt  
                            |> Option.map (fun value ->  value * IsNegativeScaling decoded)
                            |> Helper.MapTwoOptionsIfBothAreSome  (fun a b -> a * float(b)) scalingDueToDecimalPointer
        let unit = FindUnit decoded unitToPosition

        {
            Unit =  {Unit = unit; Text = unit |> UnitToString }
            Current = KindOfCurrent decoded currentToPosition
            ValueUnscaled = valueUnscaled
            Factor = { Value = FactorToValue factor;  Text = FactorToString factor }
        }

     let ScaledValue unscaled =
         match unscaled.ValueUnscaled with
         | Some(x) -> Some(x * 10.0** float(unscaled.Factor.Value))    
         | None -> None
