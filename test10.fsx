open System.IO
#r "nuget: FSharp.Data, Version=5.0.2"
open FSharp.Data

[<Literal>]
let ResolutionFolder = __SOURCE_DIRECTORY__
// let checkSuitsType = File.ReadAllText("scripts/checkSuitsType.json")
type Simple = JsonProvider<"scripts/checkSuitsType.json", ResolutionFolder=ResolutionFolder>

let sample = File.ReadAllText("scripts/sample.json")
let value = Simple.Parse sample

printfn "value: %A" value.CheckSuites.[0].Repository.FullName
