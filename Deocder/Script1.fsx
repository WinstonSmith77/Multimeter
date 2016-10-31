

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
    let Two = { Segments = [SevenSegment.Top; SevenSegment.Bottom; SevenSegment.BottomLeft; SevenSegment.TopRight] |> Set.ofList }

    let dummy = { Segments = Set.add SevenSegment.Center One.Segments}
    let dummy2 = { Segments = Set.remove SevenSegment.Center dummy.Segments}