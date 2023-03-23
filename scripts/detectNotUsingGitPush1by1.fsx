#!/usr/bin/env -S dotnet fsi

open System
open System.IO
open System.Net.Http
open System.Net.Http.Headers
open System.Text.RegularExpressions

#r "nuget: Fsdk, Version=0.6.0--date20230214-0422.git-1ea6f62"
open Fsdk

let githubEventPath = Environment.GetEnvironmentVariable "GITHUB_EVENT_PATH"

if String.IsNullOrEmpty githubEventPath then
    Console.Error.WriteLine
        "This script is meant to be used only within a GitHubCI pipeline"

    Environment.Exit 2

let jsonString = File.ReadAllText(githubEventPath)
let repoRegex = Regex("\"full_name\"\\s*:\\s*\"([^\\s]*)\"", RegexOptions.Compiled)
let gitRepo = (repoRegex.Matches jsonString).[0].Groups.[1].ToString()

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

printfn "currentBranch %A" currentBranch

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

printfn "prCommits %A" prCommits

let notUsingGitPush1by1 =
    prCommits
    |> Seq.map(fun commit ->
        use client = new HttpClient()
        client.DefaultRequestHeaders.Accept.Clear()

        client.DefaultRequestHeaders.Accept.Add(
            MediaTypeWithQualityHeaderValue "application/vnd.github+json"
        )

        client.DefaultRequestHeaders.Add("User-Agent", ".NET App")
        client.DefaultRequestHeaders.Add("X-GitHub-Api-Version", "2022-11-28")

        let url =
            sprintf
                "https://api.github.com/repos/%s/commits/%s/check-suites"
                gitRepo
                commit

        printfn "url %A" url

        let json = (client.GetStringAsync url).Result

        json.Contains "\"check_suites\":[]"
    )
    |> Seq.contains true

if notUsingGitPush1by1 then
    let errMsg =
        sprintf
            "Please push the commits one by one; using this script is recommended:%s%s"
            Environment.NewLine
            "https://github.com/nblockchain/conventions/blob/master/scripts/gitPush1by1.fsx"

    Console.Error.WriteLine errMsg
    Environment.Exit 1
