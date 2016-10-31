module DecoderTypes
    type DecodedBuffer =   
        {
            Buffer:byte array 
        }   

    type ACOrDC =
        | AC 
        | DC
        