
module Helper

 let WithTwoOptions f a b = match (a, b) with
                            | Some(a), Some(b)  -> Some (f a b)
                            | _, _ -> None 

