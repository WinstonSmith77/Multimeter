module Digit
    type SevenSegment=
        | Top
        | Center
        | Bottom
        | TopLeft
        | TopRight
        | BottomLeft
        | BottomRight  

    type Digit = {
        Segments : SevenSegment Set
        }

    let One = { Segments = [SevenSegment.BottomRight; SevenSegment.TopRight] |> Set.ofList }
    let Seven = { Segments = Set.add SevenSegment.Top One.Segments }
    let Three = { Segments = Set.add SevenSegment.Bottom Seven.Segments |> Set.add SevenSegment.Center}

    let Two = { Segments = [SevenSegment.Top; SevenSegment.Bottom; SevenSegment.BottomLeft; SevenSegment.TopRight; SevenSegment.Center] |> Set.ofList }
    let Five = { Segments = [SevenSegment.Top; SevenSegment.Bottom; SevenSegment.BottomRight; SevenSegment.TopLeft; SevenSegment.Center] |> Set.ofList }
   
    let Eight = { Segments = Set.union Two.Segments Five.Segments }
    let Six = { Segments = Set.remove SevenSegment.TopRight Eight.Segments }
    let Nine = { Segments = Set.remove SevenSegment.BottomLeft Eight.Segments }
    let Zero = { Segments = Set.remove SevenSegment.Center Eight.Segments }

    let Four = { Segments = Set.remove SevenSegment.Top Nine.Segments |> Set.remove SevenSegment.Bottom }

    let DigitToInt = [(Zero, 0); (One, 1); (Two, 2); (Three, 3); (Four, 4); (Five, 5); (Six, 6); (Seven, 7); (Eight,8); (Nine, 9)] 

   
   