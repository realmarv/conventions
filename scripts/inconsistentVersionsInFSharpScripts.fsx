#!/usr/bin/env -S dotnet fsi

open System.IO

#load "../src/FileConventions/Helpers.fs"
#load "../src/FileConventions/Library.fs"

let rootDir = Path.Combine(__SOURCE_DIRECTORY__, "..") |> DirectoryInfo

let inconsistentVersionsInFsharpScripts =
    FileConventions.DetectInconsistentVersionsInFsharpScripts rootDir

if inconsistentVersionsInFsharpScripts then
    failwith
        "You shouldn't use inconsistent versions in nuget package references of F# scripts."
