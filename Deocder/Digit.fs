module Digit
    type SevenSegment=
        | TopCenter
        | Center
        | BottomCenter
        | TopLeft
        | Topright
        | BottomLeft
        | BottomRight  

    type Digit = {
        Segments : SevenSegment Set
        }

    let One = { Segments = [SevenSegment.BottomRight; SevenSegment.Topright] |> Set.ofList }

   