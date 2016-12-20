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
         override this.ToString() = 
           match this with
            | Volt -> "V"
            | Ampere -> "A"
            | Ohm -> "Ohm"
            | Farad -> "F"
            | Hertz -> "Hz"
   
    type Factor =   
        | Nano 
        | Micro 
        | Milli 
        | Kilo
        | Mega 
         override this.ToString() = 
           match this with
                                           | Nano -> "n"
                                           | Micro -> "µ"  
                                           | Milli -> "m"  
                                           | Kilo -> "k"  
                                           | Mega -> "M"
          member this.ToValue() = 
           match this with
                                          | Nano -> -9 
                                          | Micro -> -6  
                                          | Milli -> -3  
                                          | Kilo -> 3  
                                          | Mega -> 6    

                            
               
        