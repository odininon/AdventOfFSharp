// Learn more about F# at http://fsharp.org
open System
open System.IO
open AlchemicalReduction.API.Reduction

let file = @"../input.txt"
let lines = File.ReadAllLines(file) |> Seq.head

let removeChar (a : char) (b : string) =
    async {
        let newLines = b.Replace(a.ToString(), "").Replace(char(int a - 32).ToString(), "")
        return (reducePolymers newLines).Length
    }

[<EntryPoint>]
let main argv =
    [ 97..122 ]
    |> List.map (fun i -> removeChar (char i) lines)
    |> Async.Parallel
    |> Async.RunSynchronously
    |> Seq.min
    |> printfn "%A"
    printfn "%A" (reducePolymers lines).Length
    0 // return an integer exit code
