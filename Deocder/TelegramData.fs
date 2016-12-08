module TelegramData

    open Digit
    open MeasurementTypes
    
    type DecodedBuffer = {  Buffer:byte array }   

    let numberOfBytesInTelegram = 14

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
          
     type BinaryPosition = {
        Bit : Bits
        Byte : BytesInTelegram
     }

     let positionAC = {Byte = BytesInTelegram.One; Bit = Bits.Four}
     let positionDC = {Byte = BytesInTelegram.One; Bit = Bits.Three}
     let postionNegativeSign = {Byte = BytesInTelegram.Two; Bit = Bits.Four}
           
     let digit firstByte secondByte = 
                    [(SevenSegment.Top, {Byte = firstByte; Bit = Bits.One});
                     (SevenSegment.TopRight, {Byte = secondByte; Bit = Bits.One})
                     (SevenSegment.BottomRight, {Byte = secondByte; Bit = Bits.Three})
                     (SevenSegment.Bottom, {Byte = secondByte; Bit = Bits.Four})
                     (SevenSegment.BottomLeft, {Byte = firstByte; Bit = Bits.Three})
                     (SevenSegment.TopLeft, {Byte = firstByte; Bit = Bits.Two})
                     (SevenSegment.Center, {Byte = secondByte; Bit = Bits.Two})   ] 

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

