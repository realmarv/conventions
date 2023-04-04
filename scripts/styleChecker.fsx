#!/usr/bin/env -S dotnet fsi

#r "nuget: Fsdk, Version=0.6.0--date20230326-0544.git-5c4f55b"

open System

open Fsdk
open Fsdk.Process

let StyleFSharpFiles() =
    Process
        .Execute(
            {
                Command = "dotnet"
                Arguments = "new tool-manifest --force"
            },
            Process.Echo.Off
        )
        .UnwrapDefault()
    |> ignore

    Process
        .Execute(
            {
                Command = "dotnet"
                Arguments =
                    "tool install fantomless-tool --version 4.7.997-prerelease"
            },
            Process.Echo.Off
        )
        .UnwrapDefault()
    |> ignore

    Process
        .Execute(
            {
                Command = "dotnet"
                Arguments = "fantomless --recurse ."
            },
            Process.Echo.Off
        )
        .UnwrapDefault()
    |> ignore

let RunPrettier(arguments: string) =
    let processResult =
        Process.Execute(
            {
                Command = "npx"
                Arguments = $"prettier {arguments}"
            },
            Process.Echo.All
        )

    let errMsg =
        sprintf
            "Error when running '%s %s'"
            processResult.Details.Command
            processResult.Details.Args

    match processResult.Result with
    | Success output -> output
    | Error(_, output) ->
        if processResult.Details.Echo = Echo.Off then
            output.PrintToConsole()
            Console.WriteLine()
            Console.Out.Flush()

        Console.Error.WriteLine errMsg
        raise <| ProcessFailed errMsg
    | WarningsOrAmbiguous output ->
        if processResult.Details.Echo = Echo.Off then
            output.PrintToConsole()
            Console.WriteLine()
            Console.Out.Flush()

        let fullErrMsg = sprintf "%s (with warnings?)" errMsg
        fullErrMsg
    |> printfn "%A"


    // Since after installing commitlint dependencies package.json file changes, we need to
    // run the following command to ignore package.json file
    Process
        .Execute(
            {
                Command = "git"
                Arguments = "restore package.json"
            },
            Process.Echo.Off
        )
        .UnwrapDefault()
    |> ignore

let StyleTypeScriptFiles() =
    RunPrettier "--quote-props=consistent --write ./**/*.ts"

let StyleYmlFiles() =
    RunPrettier "--quote-props=consistent --write ./**/*.yml"

StyleFSharpFiles()
StyleTypeScriptFiles()
StyleYmlFiles()

let processResult =
        Process.Execute(
            {
                Command = "git"
                Arguments = "diff --exit-code"
            },
            Process.Echo.Off
        )

let errMsg =
    sprintf
        "Error when running '%s %s'"
        processResult.Details.Command
        processResult.Details.Args

let suggestion =
    "Please use the following commands to style your code:"
    + System.Environment.NewLine
    + "Style your F# code using: `dotnet fantomless --recurse .`"
    + System.Environment.NewLine
    + "Style your TypeScript code using: `npx prettier --quote-props=consistent --write ./**/*.ts`"
    + System.Environment.NewLine
    + "Style your YML code using: `npx prettier --quote-props=consistent --write ./**/*.yml`"
    + System.Environment.NewLine

match processResult.Result with
| Success output -> output
| Error(_, output) ->
    if processResult.Details.Echo = Echo.Off then
        output.PrintToConsole()
        Console.WriteLine()
        Console.Out.Flush()

    let fullErrMsg = suggestion + System.Environment.NewLine + errMsg
    Console.Error.WriteLine fullErrMsg
    raise <| ProcessFailed fullErrMsg
| WarningsOrAmbiguous output ->
    if processResult.Details.Echo = Echo.Off then
        output.PrintToConsole()
        Console.WriteLine()
        Console.Out.Flush()

    let fullErrMsg = sprintf "%s (with warnings?)" errMsg
    fullErrMsg
|> printfn "%A"
