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

let parsedJsonObj = JsonProvider<jsonString>

// let gitRepo =
//     match parsedJsonObj.TryGetProperty "pull_request" with
//     | None -> failwith "Invalid JSON"
//     | Some pull_request ->
//         match pull_request.TryGetProperty "base" with
//         | None -> failwith "Invalid JSON"
//         | Some baseInfo ->
//             match baseInfo.TryGetProperty "repo" with
//             | None -> failwith "Invalid JSON"
//             | Some repo ->
//                 match repo.TryGetProperty "full_name" with
//                 | None -> failwith "Invalid JSON"
//                 | Some full_name -> full_name
    
printfn "%A" parsedJsonObj
// printfn "%A" ((repoRegex.Matches jsonString).[0].Groups.[1])

// let info = JsonValue.Parse(jsonString)