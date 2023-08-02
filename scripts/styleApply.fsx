#!/usr/bin/env -S dotnet fsi

#r "nuget: Fsdk, Version=0.6.0--date20230214-0422.git-1ea6f62"
#load "../src/FileConventions/Helpers.fs"
#load "../src/FileConventions/Library.fs"

open System.IO

open FileConventions

let fantomlessToolVersion = "4.7.997-prerelease"
let prettierVersion = "2.8.3"
let pluginXmlVersion = "v2.2.0"

let rootDir = Path.Combine(__SOURCE_DIRECTORY__, "..") |> DirectoryInfo

StyleFSharpFiles rootDir fantomlessToolVersion
StyleTypeScriptFiles prettierVersion
StyleYmlFiles prettierVersion
StyleCSharpFiles rootDir
StyleXamlFiles prettierVersion pluginXmlVersion
