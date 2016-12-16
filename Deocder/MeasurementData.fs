module MeasurementData

    open Helper

    type Current =
        | AC 
        | DC

    type Unit =
        | Volt
        | Ampere
        | Ohm
        | Farad
        | Hertz

    let UnitToString unit =
        let unitToStringInner _ unit =
            match unit with
            | Volt -> "V"
            | Ampere -> "A"
            | Ohm -> "Ohm"
            | Farad -> "F"
            | Hertz -> "Hz"
       
        Option.fold unitToStringInner "" unit
   
    type Factor =   
        | Nano 
        | Micro 
        | Milli 
        | Kilo
        | Mega 

    let FactorToValue factor =   
        let factorToValueInner _ factor = match factor with
                                          | Nano -> -9 
                                          | Micro -> -6  
                                          | Milli -> -3  
                                          | Kilo -> 3  
                                          | Mega -> 6    

        Option.fold factorToValueInner 0 factor

   

    let FactorToString factor =  
        let factorToStringInner _ factor = match factor with
                                           | Nano -> "n"
                                           | Micro -> "µ"  
                                           | Milli -> "m"  
                                           | Kilo -> "k"  
                                           | Mega -> "M"

        Option.fold factorToStringInner "" factor
                            
               
        