module MeasurementTypes

    type ACOrDC =
        | AC 
        | DC

    type Unit =
        | Volt
        | Ampere
        | Ohm
        | Farad
        | Hertz

    let unitToString unit =
        match unit with
        | Some(Volt) -> "Volt"
        | Some(Ampere) -> "Ampere"
        | Some(Ohm) -> "Ohm"
        | Some(Farad) -> "Farad"
        | Some(Hertz) -> "Hertz"
        | None -> ""
   
    type Factor =   
        | Nano 
        | Micro 
        | Milli 
        | Kilo
        | Mega 

    let getFactorValue factor = match factor with
                                | Nano -> -9 
                                | Micro -> -6  
                                | Milli -> -3  
                                | Kilo -> 3  
                                | Mega -> 6
                            
               
        