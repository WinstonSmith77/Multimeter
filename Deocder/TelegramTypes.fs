module TelegramTypes

    [<System.FlagsAttribute>]
     type LowerBits =
           |    One = 1
           |    Two = 2
           |    Three = 4
           |    Four = 8
           |    All = 15
           |    Five = 16

    [<System.FlagsAttribute>]
     type BytesInTelegram =
           |    First = 0
           |    Second = 1
           
