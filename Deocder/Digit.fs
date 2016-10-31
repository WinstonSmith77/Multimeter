module Digit
    type SevenSegment=
        | TopCenter
        | Center
        | BottomCenter
        | TopLeft
        | TopRight
        | BottomLeft
        | BottomRight  

    type Digit = {
        Segments : SevenSegment Set
        }

    let One = { Segments = [SevenSegment.BottomRight; SevenSegment.TopRight] |> Set.ofList }
    let Two = { Segments = [SevenSegment.Top; SevenSegment.Bottom; SevenSegment.BottomLeft; SevenSegment.TopRight] |> Set.ofList }

   