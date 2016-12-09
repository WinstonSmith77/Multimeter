
module Helper

 let MapTwoOptionsIfBothAreSome f a b = match (a, b) with
                                        | Some(a), Some(b)  -> Some (f a b)
                                        | _, _ -> None 

 let FirstFromOptionTuple option =
        match option with 
        | Some(a, _) -> Some(a)
        | None -> None

 let TrySingle list =
       match list with
        | [first] -> Some(first)
        | _ -> None


