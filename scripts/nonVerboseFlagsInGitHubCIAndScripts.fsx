#!/usr/bin/env -S dotnet fsi

open System
open System.IO

#r "nuget: Fsdk, Version=0.6.0--date20230214-0422.git-1ea6f62"
#load "../src/FileConventions/Helpers.fs"
#load "../src/FileConventions/Library.fs"

let rootDir = Path.Combine(__SOURCE_DIRECTORY__, "..") |> DirectoryInfo

let validExtensions =
    seq {
        ".yml"
        ".fsx"
        ".fs"
        ".sh"
    }

let invalidFiles =
    validExtensions
    |> Seq.map(fun ext ->
        Helpers.GetInvalidFiles
            rootDir
            ("*" + ext)
            FileConventions.NonVerboseFlags
    )
    |> Seq.concat

let message = "Please don't use non-verbose flags in the following files:"

Helpers.AssertNoInvalidFiles invalidFiles message
