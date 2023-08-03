#!/usr/bin/env -S dotnet fsi

#r "nuget: Fsdk, Version=0.6.0--date20230214-0422.git-1ea6f62"
#load "../src/FileConventions/Helpers.fs"
#load "../src/FileConventions/Library.fs"
#load "config.fs"

open System.IO

open FileConventions

let rootDir = Path.Combine(__SOURCE_DIRECTORY__, "..") |> DirectoryInfo

StyleFSharpFiles rootDir
StyleTypeScriptFiles()
StyleYmlFiles()
StyleCSharpFiles rootDir
StyleXamlFiles()
