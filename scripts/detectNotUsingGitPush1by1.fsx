#!/usr/bin/env -S dotnet fsi

open System
open System.Net.Http
open System.Net.Http.Headers

#r "nuget: Fsdk, Version=0.6.0--date20230214-0422.git-1ea6f62"

let currentBranch =
    Fsdk
        .Process
        .Execute(
            {
                Command = "git"
                Arguments = "rev-parse --abbrev-ref HEAD"
            },
            Fsdk.Process.Echo.All
        )
        .UnwrapDefault()
        .Trim()

let prCommits =
    Fsdk
        .Process
        .Execute(
            {
                Command = "git"
                Arguments = sprintf "rev-list %s~..%s" currentBranch currentBranch
            },
            Fsdk.Process.Echo.All
        )
        .UnwrapDefault()
        .Trim()
        .Split "\n"

let notUsingGitPush1by1 =
    prCommits
    |> Seq.map(fun commit ->
        let client: HttpClient = new HttpClient()
        client.DefaultRequestHeaders.Accept.Clear()

        client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/vnd.github+json")
        )

        client.DefaultRequestHeaders.Add("User-Agent", ".NET App")
        client.DefaultRequestHeaders.Add("X-GitHub-Api-Version", "2022-11-28")

        let command = sprintf
                        "https://api.github.com/repos/%s/commits/%s/check-suites"
                        (Environment.GetEnvironmentVariable("GITHUB_REPOSITORY"))
                        commit

        printfn "Command: %A" command

        let url =
            sprintf
                "https://api.github.com/repos/%s/commits/%s/check-suites"
                (Environment.GetEnvironmentVariable("GITHUB_REPOSITORY"))
                commit

        let json = client.GetStringAsync(url).Result

        json.Contains "\"check_suites\":[]"
    )
    |> Seq.contains true

if notUsingGitPush1by1 then
    failwith "Please push the commits one by one.\nYou may use the scripts/gitPush1by1.fsx script."
