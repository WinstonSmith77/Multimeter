module Digit
    type SevenSegment2=
        | Top
        | Center
        | Bottom
        | TopLeft
        | TopRight
        | BottomLeft
        | BottomRight  

    type Digit = {
        Segments : SevenSegment2 Set
        }

    let One = { Segments = [SevenSegment2.BottomRight; SevenSegment2.TopRight] |> Set.ofList }
    let Seven = { Segments = Set.add SevenSegment2.Top One.Segments }
    let Three = { Segments = Set.add SevenSegment2.Bottom Seven.Segments |> Set.add SevenSegment2.Center}

    let Two = { Segments = [SevenSegment2.Top; SevenSegment2.Bottom; SevenSegment2.BottomLeft; SevenSegment2.TopRight; SevenSegment2.Center] |> Set.ofList }
    let Five = { Segments = [SevenSegment2.Top; SevenSegment2.Bottom; SevenSegment2.BottomRight; SevenSegment2.TopLeft; SevenSegment2.Center] |> Set.ofList }
   
    let Eight = { Segments = Set.union Two.Segments Five.Segments }
    let Six = { Segments = Set.remove SevenSegment2.TopRight Eight.Segments }
    let Nine = { Segments = Set.remove SevenSegment2.BottomLeft Eight.Segments }
    let Zero = { Segments = Set.remove SevenSegment2.Center Eight.Segments }

    let Four = { Segments = Set.remove SevenSegment2.Top Nine.Segments |> Set.remove SevenSegment2.Bottom }

    let DigitToInt = [(Zero, 0); (One, 1); (Two, 2); (Three, 3); (Four, 4); (Five, 5); (Six, 6); (Seven, 7); (Eight,8); (Nine, 9)] 

   
   