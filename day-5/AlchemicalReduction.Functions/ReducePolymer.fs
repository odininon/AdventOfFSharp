namespace AlchemicalReduction.Functions

open Microsoft.Azure.WebJobs
open FSharp.Control.Tasks
open AlchemicalReduction.API.Reduction
open System.Threading.Tasks

module HelloSequence =
    [<Struct>]
    type Results = { Orignal: int; ReducedMinimum: int }

    [<Struct>]
    type Simplification = { Removal: char; Polymer: string}

    [<FunctionName("ReduceAllPolymers")>]
    let Run([<OrchestrationTrigger>] context : DurableOrchestrationContext,
            [<Blob("inputs/input.txt")>] input: string) = task { 
        let helloTask = context.CallActivityAsync<int>("ReducePolymer", input)
        let simplifcations = List.map (fun c -> context.CallActivityAsync<int>("SimplifyPolymer", {Removal = c; Polymer = input})) ([97..122] |> List.map char)
  
        let! results = Task.WhenAll (List.append [helloTask] simplifcations)
        let hello1 = results |> Seq.head
        return { Orignal = hello1; ReducedMinimum = results |> Seq.tail |> Seq.min }
    }

    [<FunctionName("ReducePolymer")>]
    let ReducePolymerLength([<ActivityTrigger>] polymer) = reducePolymers polymer |> String.length

    [<FunctionName("SimplifyPolymer")>]
    let SimplifyPolymer([<ActivityTrigger>] polymer: Simplification) = 
            let {Removal = a; Polymer = b} = polymer;
            let newLines = b.Replace(a.ToString(), "").Replace(char(int a - 32).ToString(), "")
            reducePolymers newLines |> String.length