﻿module TelegramData

    open Digit
    open MeasurementData
    
    type DecodedBuffer = {  Buffer:byte array }   

    [<System.FlagsAttribute>]
     type Bits =
           |    One = 1
           |    Two = 2
           |    Three = 4
           |    Four = 8
           |    AllLowerHalf = 15
           |    Five = 16

    [<System.FlagsAttribute>]
     type BytesInTelegram =
           |    One = 0
           |    Two = 1
           |    Three = 2
           |    Four = 3
           |    Five = 4
           |    Six = 5
           |    Seven = 6
           |    Eight = 7
           |    Nine = 8
           |    Ten = 9
           |    Eleven = 10
           |    Twelve = 11
           |    Thirteen = 12
          
     type BinaryPosition = {
        Bit : Bits
        Byte : BytesInTelegram
     }

   
     let postionNegativeSign = {Byte = BytesInTelegram.Two; Bit = Bits.Four}
           
     let digit firstByte secondByte = 
                    [(SevenSegments.Top, {Byte = firstByte; Bit = Bits.One});
                     (SevenSegments.TopRight, {Byte = secondByte; Bit = Bits.One})
                     (SevenSegments.BottomRight, {Byte = secondByte; Bit = Bits.Three})
                     (SevenSegments.Bottom, {Byte = secondByte; Bit = Bits.Four})
                     (SevenSegments.BottomLeft, {Byte = firstByte; Bit = Bits.Three})
                     (SevenSegments.TopLeft, {Byte = firstByte; Bit = Bits.Two})
                     (SevenSegments.Center, {Byte = secondByte; Bit = Bits.Two})   ] 

    let digitOne = digit BytesInTelegram.Eight BytesInTelegram.Nine
    let digitTwo = digit BytesInTelegram.Six BytesInTelegram.Seven
    let digitThree = digit BytesInTelegram.Four BytesInTelegram.Five
    let digitFour = digit BytesInTelegram.Two BytesInTelegram.Three

    let decimalPoint byte = {Byte = byte; Bit = Bits.Four}

    let decimalPointOne = decimalPoint BytesInTelegram.Eight
    let decimalPointTwo = decimalPoint BytesInTelegram.Six
    let decimalPointThree = decimalPoint BytesInTelegram.Four

    let factorToPosition = [
                            (Factor.Kilo, {Byte = BytesInTelegram.Ten; Bit = Bits.Two});
                            (Factor.Nano, {Byte = BytesInTelegram.Ten; Bit = Bits.Three});
                            (Factor.Micro, {Byte = BytesInTelegram.Ten; Bit = Bits.Four});
                            (Factor.Mega, {Byte = BytesInTelegram.Eleven; Bit = Bits.Two});
                            (Factor.Milli, {Byte = BytesInTelegram.Eleven; Bit = Bits.Four})
                         ]

                         
    let unitToPosition = [
                            (Unit.Ampere, {Byte = BytesInTelegram.Thirteen; Bit = Bits.Four});
                            (Unit.Volt, {Byte = BytesInTelegram.Thirteen; Bit = Bits.Three});
                            (Unit.Hertz, {Byte = BytesInTelegram.Thirteen; Bit = Bits.Two});
                            (Unit.Ohm, {Byte = BytesInTelegram.Twelve; Bit = Bits.Three});
                            (Unit.Farad, {Byte = BytesInTelegram.Twelve; Bit = Bits.Four});
                         ]

    let currentToPosition = [
                                (Current.AC, {Byte = BytesInTelegram.One; Bit = Bits.Four});
                                (Current.DC, {Byte = BytesInTelegram.One; Bit = Bits.Three});
                            ]

