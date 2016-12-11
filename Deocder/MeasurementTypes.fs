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
        | Some(Volt) -> "V"
        | Some(Ampere) -> "A"
        | Some(Ohm) -> "Ohm"
        | Some(Farad) -> "F"
        | Some(Hertz) -> "Hz"
        | None -> ""
   
    type Factor =   
        | Nano 
        | Micro 
        | Milli 
        | Kilo
        | Mega 

    let factorToValue factor =   
        let factorToValueInner factor = match factor with
                                        | Nano -> -9 
                                        | Micro -> -6  
                                        | Milli -> -3  
                                        | Kilo -> 3  
                                        | Mega -> 6    
        match factor with
        | Some(scaling) -> factorToValueInner scaling
        | None -> 1

   

    let factorToString factor = match factor with
                                 | Nano -> "n"
                                 | Micro -> "µ"  
                                 | Milli -> "m"  
                                 | Kilo -> "k"  
                                 | Mega -> "M"
                            
               
        