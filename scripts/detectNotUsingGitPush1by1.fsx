#!/usr/bin/env -S dotnet fsi

open System
open System.IO
open System.Net.Http

// #r "nuget: Flurl.Http"
#r "nuget: Fsdk, Version=0.6.0--date20230214-0422.git-1ea6f62"
#load "../src/FileConventions/Helpers.fs"

// open Flurl.Http

// task {
//     let! content =
//         let url = "https://api.github.com/repos/realmarv/conventions/commits/ba83f08a0c2d06abf79f8061d0b84f12a139b9a1/check-suites"

//         url.GetStringAsync()

//     do! File.WriteAllTextAsync("./response.html", content)
// }
// |> Async.AwaitTask
// // we run synchronously
// // to allow the fsi to finish the pending tasks
// |> Async.RunSynchronously

task {
    /// note the ***use*** instead of ***let***
    use client = new HttpClient()
    let! response = 
        client.GetStringAsync("https://api.github.com")
    do! File.WriteAllTextAsync("./response.html", response)
    // after the client goes out of scope
    // it will get disposed automatically thanks to the ***use*** keyword
}
|> Async.AwaitTask
// we run synchronously
// to allow the fsi to finish the pending tasks
|> Async.RunSynchronously


// let checkSuites =
//     Fsdk
//         .Process
//         .Execute(
//             {
//                 Command = "curl"
//                 Arguments = "https://api.github.com/repos/realmarv/conventions/commits/ba83f08a0c2d06abf79f8061d0b84f12a139b9a1/check-suites"
//             },
//             Fsdk.Process.Echo.All
//         )
// printfn "%A" checkSuites



// let prCommits =
//     Fsdk
//         .Process
//         .Execute(
//             {
//                 Command = "git"
//                 Arguments = "rev-list master..wip/checkGitPush1by1"
//             },
//             Fsdk.Process.Echo.All
//         ).UnwrapDefault()

// printfn "%A" (prCommits.Trim().Split "\n")


