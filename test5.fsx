open System.IO
// #r "../../../bin/FSharp.Data.dll"
// #r "FSharp.Json.dll"
// open FSharp.Json
// open System.Json
// open FSharp.Data.JsonProvider
open System.Text.RegularExpressions
#r "nuget: FSharp.Data, Version=5.0.2"
open FSharp.Data
// open Newtonsoft.Json.Linq

// let repoRegex = Regex("\"full_name\"\\s*:\\s*\"([^\\s]*)\"", RegexOptions.Compiled)
let jsonString = File.ReadAllText("repo_info.json")

let parsedJson = FSharp.Data.JsonValue.Parse jsonString
printfn "%A" (parsedJson.base.repo.full_name)
// printfn "%A" ((repoRegex.Matches jsonString).[0].Groups.[1])

// let info = JsonValue.Parse(jsonString)