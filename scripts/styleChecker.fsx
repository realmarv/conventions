#!/usr/bin/env -S dotnet fsi

#r "nuget: Fsdk, Version=0.6.0--date20230326-0544.git-5c4f55b"
#load "../src/FileConventions/Helpers.fs"

open System
open System.IO

open Fsdk
open Fsdk.Process

open Helpers

let StyleFSharpFiles() =
    Process
        .Execute(
            {
                Command = "dotnet"
                Arguments = "new tool-manifest --force"
            },
            Echo.Off
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
            Echo.Off
        )
        .UnwrapDefault()
    |> ignore

    Process
        .Execute(
            {
                Command = "dotnet"
                Arguments = "fantomless --recurse ."
            },
            Echo.Off
        )
        .UnwrapDefault()
    |> ignore

let RunPrettier(arguments: string) =
    Process.Execute(
        {
            Command = "pwd"
            Arguments = ""
        },
        Echo.All
    )
    |> ignore
    
    let processResult =
        Process.Execute(
            {
                Command = "npx"
                Arguments = $"prettier {arguments}"
            },
            Echo.All
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
            Echo.Off
        )
        .UnwrapDefault()
    |> ignore

let StyleTypeScriptFiles() =
    RunPrettier "--quote-props=consistent --write ./**/*.ts"

let StyleYmlFiles() =
    RunPrettier "--quote-props=consistent --write ./**/*.yml"

let ContainsFiles (rootDir: DirectoryInfo) (searchPattern: string) =
    Helpers.GetFiles rootDir searchPattern |> Seq.length > 0

let GitDiff() : ProcessResult =
    let processResult =
        Process.Execute(
            {
                Command = "git"
                Arguments = "diff --exit-code"
            },
            Echo.Off
        )

    processResult

let GitRestore() =
    Process.Execute(
        {
            Command = "git"
            Arguments = "restore ."
        },
        Echo.Off
    )
    |> ignore


let PrintProcessResult (processResult: ProcessResult) (suggestion: string) =
    let errMsg =
        sprintf
            "Error when running '%s %s'"
            processResult.Details.Command
            processResult.Details.Args

    match processResult.Result with
    | Success output -> output
    | Error(_, output) ->
        if processResult.Details.Echo = Echo.Off then
            printfn "HERE4"
            output.PrintToConsole()
            Console.WriteLine()
            Console.Out.Flush()

        let fullErrMsg = errMsg + Environment.NewLine + suggestion
        fullErrMsg

    | WarningsOrAmbiguous output ->
        if processResult.Details.Echo = Echo.Off then
            output.PrintToConsole()
            Console.WriteLine()
            Console.Out.Flush()

        let fullErrMsg = sprintf "%s (with warnings?)" errMsg
        fullErrMsg

    |> printfn "%A"
    printfn "HERE2"

let GetProcessExitCode(processResult: ProcessResult) : int =
    match processResult.Result with
    | Success output -> 0
    | _ -> 1

let CheckStyleOfFSharpFiles(rootDir: DirectoryInfo) : int =
    let suggestion =
        "Please style your F# code using: `dotnet fantomless --recurse .`"

    GitRestore()

    let exitCode =
        if ContainsFiles rootDir "*.fs" || ContainsFiles rootDir ".fsx" then
            StyleFSharpFiles()
            let processResult = GitDiff()
            printfn "HERE3:%A" (PrintProcessResult processResult suggestion)
            GetProcessExitCode processResult 
        else
            0

    exitCode

let CheckStyleOfTypeScriptFiles(rootDir: DirectoryInfo) : int =
    let suggestion =
        "Please style your TypeScript code using: `npx prettier --quote-props=consistent --write ./**/*.ts`"

    GitRestore()

    let exitCode =
        if ContainsFiles rootDir "*.ts" then
            StyleTypeScriptFiles()
            let processResult = GitDiff()
            printfn "HERE3:%A" (PrintProcessResult processResult suggestion)
            printfn "HERE1" 
            GetProcessExitCode processResult

        else
            0

    exitCode

let CheckStyleOfYmlFiles(rootDir: DirectoryInfo) : int =
    let suggestion =
        "Please style your YML code using: `npx prettier --quote-props=consistent --write ./**/*.yml`"

    GitRestore()

    let exitCode =
            if ContainsFiles rootDir "*.yml" then
                StyleYmlFiles()
                let processResult = GitDiff()
                PrintProcessResult processResult suggestion
                GetProcessExitCode processResult
            else
                0

    exitCode


let rootDir = Path.Combine(__SOURCE_DIRECTORY__, "..") |> DirectoryInfo

let exitCodes =
    seq {
        // CheckStyleOfFSharpFiles rootDir
        // CheckStyleOfTypeScriptFiles rootDir
        CheckStyleOfYmlFiles rootDir
    }

if exitCodes |> Seq.contains 1 then
    Environment.Exit 1
