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

    [<System.FlagsAttribute>]
    type Factor =   
        | Nano = -9
        | Micro = -6
        | Milli = -3
        | Kilo = 3
        | Mega = 6
               
        