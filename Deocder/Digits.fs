module Digit
    type SevenSegments=
        | Top
        | Center
        | Bottom
        | TopLeft
        | TopRight
        | BottomLeft
        | BottomRight  

    type Digit = {
        Segments : SevenSegments Set
        }

    let One = { Segments = [SevenSegments.BottomRight; SevenSegments.TopRight] |> Set.ofList }
    let Seven = { Segments = Set.add SevenSegments.Top One.Segments }
    let Three = { Segments = Set.add SevenSegments.Bottom Seven.Segments |> Set.add SevenSegments.Center}

    let Two = { Segments = [SevenSegments.Top; SevenSegments.Bottom; SevenSegments.BottomLeft; SevenSegments.TopRight; SevenSegments.Center] |> Set.ofList }
    let Five = { Segments = [SevenSegments.Top; SevenSegments.Bottom; SevenSegments.BottomRight; SevenSegments.TopLeft; SevenSegments.Center] |> Set.ofList }
   
    let Eight = { Segments = Set.union Two.Segments Five.Segments }
    let Six = { Segments = Set.remove SevenSegments.TopRight Eight.Segments }
    let Nine = { Segments = Set.remove SevenSegments.BottomLeft Eight.Segments }
    let Zero = { Segments = Set.remove SevenSegments.Center Eight.Segments }

    let Four = { Segments = Set.remove SevenSegments.Top Nine.Segments |> Set.remove SevenSegments.Bottom }

    let DigitToInt = [(Zero, 0); (One, 1); (Two, 2); (Three, 3); (Four, 4); (Five, 5); (Six, 6); (Seven, 7); (Eight,8); (Nine, 9)] 

   
   