open System.IO
#r "nuget: FSharp.Data, Version=5.0.2"
open FSharp.Data

type Simple = JsonProvider<"scripts/checkSuitsType.json">

let value = Simple.Parse(File.ReadAllText "scripts/sample.json")

printfn "value: %A" value
