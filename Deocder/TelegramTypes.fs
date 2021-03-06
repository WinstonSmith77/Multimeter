﻿module TelegramTypes
    open Digit

    [<System.FlagsAttribute>]
     type Bits =
           |    One = 1
           |    Two = 2
           |    Three = 4
           |    Four = 8
           |    All = 15
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
          
     type BinaryPosition = {
        Bit : Bits
        Byte : BytesInTelegram
     }

     let positionAC = {Byte = BytesInTelegram.One; Bit = Bits.Four}
     let positionDC = {Byte = BytesInTelegram.One; Bit = Bits.Three}
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

