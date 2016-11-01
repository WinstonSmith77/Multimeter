module TelegramTypes
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
           |    Nine = 8
           |    Eight = 7
          
           

     type BinaryPosition = {
        Bit : Bits
        Byte : BytesInTelegram
     }

     let positionAC = {Byte = BytesInTelegram.One; Bit = Bits.Four}
     let positionDC = {Byte = BytesInTelegram.One; Bit = Bits.Three}
     let postionNegativeSign = {Byte = BytesInTelegram.Two; Bit = Bits.Four}
           
     let digitOne = [(SevenSegment.Top, {Byte = BytesInTelegram.Eight; Bit = Bits.One});
                     (SevenSegment.TopRight, {Byte = BytesInTelegram.Nine; Bit = Bits.One})
                     (SevenSegment.BottomRight, {Byte = BytesInTelegram.Nine; Bit = Bits.Three})
                     (SevenSegment.Bottom, {Byte = BytesInTelegram.Nine; Bit = Bits.Four})
                     (SevenSegment.BottomLeft, {Byte = BytesInTelegram.Eight; Bit = Bits.Three})
                     (SevenSegment.TopLeft, {Byte = BytesInTelegram.Eight; Bit = Bits.Two})
                     (SevenSegment.Center, {Byte = BytesInTelegram.Nine; Bit = Bits.Two})   ] 

