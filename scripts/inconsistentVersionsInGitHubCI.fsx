#!/usr/bin/env -S dotnet fsi

open System.IO

#load "../src/FileConventions/Helpers.fs"
#load "../src/FileConventions/Library.fs"

let rootDir =
    Path.Combine(__SOURCE_DIRECTORY__, "../.github/workflows") |> DirectoryInfo

let inconsistentVersionsInGitHubCI =
    FileConventions.DetectInconsistentVersionsInGitHubCI rootDir

if inconsistentVersionsInGitHubCI then
    failwith
        "You shouldn't use inconsistent versions in `uses: foo@bar` or `with: foo-version: bar` statements in GitHubCI files."
