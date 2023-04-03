#!/usr/bin/env -S dotnet fsi

#r "nuget: Fsdk, Version=0.6.0--date20230326-0544.git-5c4f55b"

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

    Process
        .Execute(
            {
                Command = "git"
                Arguments = "config --global --add safe.directory '*'"
            },
            Process.Echo.Off
        )
        .UnwrapDefault()
    |> ignore

    let processResult =
        Process
            .Execute(
                {
                    Command = "npx"
                    Arguments = $"prettier {arguments}"
                },
                Process.Echo.All
            )
            .Result

    match processResult with
    | ProcessResultState.Success output -> output
    | ProcessResultState.WarningsOrAmbiguous output -> output.StdErr
    | ProcessResultState.Error(exitCode, output) ->
        failwith(
            sprintf
                "Finished with an error of exitcode %i: %s"
                exitCode
                output.StdErr
        )
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
    RunPrettier "--quote-props=consistent --write \"./**/*.ts\""

let StyleYmlFiles() =
    RunPrettier "--quote-props=consistent --write \"./**/*.yml\""

StyleFSharpFiles()
StyleTypeScriptFiles()
StyleYmlFiles()

Process
    .Execute(
        {
            Command = "git"
            Arguments = "diff --exit-code"
        },
        Process.Echo.Off
    )
    .UnwrapDefault()
|> ignore
