#!/usr/bin/env -S dotnet fsi

open System
open System.Net.Http
open System.Net.Http.Headers

#r "nuget: Fsdk, Version=0.6.0--date20230214-0422.git-1ea6f62"
open Fsdk

let currentBranch =
    Process
        .Execute(
            {
                Command = "git"
                Arguments = "rev-parse --abbrev-ref HEAD"
            },
            Process.Echo.Off
        )
        .UnwrapDefault()
        .Trim()

let prCommits =
    Process
        .Execute(
            {
                Command = "git"
                Arguments =
                    sprintf "rev-list %s~..%s" currentBranch currentBranch
            },
            Process.Echo.Off
        )
        .UnwrapDefault()
        .Trim()
        .Split "\n"
    |> Seq.tail

if Seq.length prCommits > 1 then
    Environment.Exit 0
else
    let prCommit = Seq.nth 0 prCommits
    printfn "%A" prCommits
    