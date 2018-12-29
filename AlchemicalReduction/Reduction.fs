namespace Alchemical

module Reduction =
    let compose fs = List.reduce (>>) fs

    let removeDuplicate (a:char) (b: string) =
        b.Replace(a.ToString() + char(int a - 32).ToString(), "").Replace(char(int a - 32).ToString() + a.ToString() , "")

    let fs = [97..122] |> List.map(fun i -> (removeDuplicate (char i)))
    let removeDuplicates a = 
        let rec _do _a cycles =
          let reduced = (compose fs _a)
          if _a.Equals reduced then 
            reduced else (_do reduced (cycles + 1))
        _do a 1

    let reducePolymers = removeDuplicates
