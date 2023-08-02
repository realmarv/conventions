module Helpers

open System
open System.IO
open System.Linq

open Fsdk
open Fsdk.Process

let NotInDir (dirName: string) (fileInfo: FileInfo) =
    not(
        fileInfo.FullName.Contains
            $"%c{Path.DirectorySeparatorChar}%s{dirName}%c{Path.DirectorySeparatorChar}"
    )

let GetFiles (maybeRootDirectory: DirectoryInfo) (searchPattern: string) =
    Directory.GetFiles(
        maybeRootDirectory.FullName,
        searchPattern,
        SearchOption.AllDirectories
    )
    |> Seq.map(fun pathStr -> FileInfo pathStr)
    |> Seq.filter(NotInDir "node_modules")
    |> Seq.filter(NotInDir ".git")
    |> Seq.filter(NotInDir "bin")
    |> Seq.filter(NotInDir "obj")
    |> Seq.filter(NotInDir "DummyFiles")

let GetInvalidFiles
    (rootDirectory: DirectoryInfo)
    (searchPattern: string)
    filterFunction
    =
    GetFiles rootDirectory searchPattern |> Seq.filter filterFunction

let PreferLessDeeplyNestedDir
    (dirAandPreferred: DirectoryInfo)
    (dirB: DirectoryInfo)
    =
    let dirSeparatorsOfDirA =
        dirAandPreferred.FullName.Count(fun char ->
            char = Path.DirectorySeparatorChar
        )

    let dirSeparatorsOfDirB =
        dirB.FullName.Count(fun char -> char = Path.DirectorySeparatorChar)

    if dirSeparatorsOfDirB > dirSeparatorsOfDirA then
        dirAandPreferred
    elif dirSeparatorsOfDirA > dirSeparatorsOfDirB then
        dirB
    else
        dirAandPreferred

let AssertNoInvalidFiles (invalidFiles: seq<FileInfo>) (message: string) =
    if Seq.length invalidFiles > 0 then
        let message =
            message
            + Environment.NewLine
            + (invalidFiles
               |> Seq.map(fun fileInfo -> fileInfo.FullName)
               |> String.concat Environment.NewLine)

        failwith message

let InstallFantomlessTool(version: string) =
    let isFantomlessInstalled =
        let installedPackages: string =
            Process
                .Execute(
                    {
                        Command = "dotnet"
                        Arguments = "tool list"
                    },
                    Echo.Off
                )
                .UnwrapDefault()

        installedPackages.Split Environment.NewLine
        |> Seq.map(fun line ->
            line.Contains "fantomless-tool"
            && line.Contains fantomlessToolVersion
        )
        |> Seq.contains true

    if not(isFantomlessInstalled) then
        Process
            .Execute(
                {
                    Command = "dotnet"
                    Arguments = "new tool-manifest --force"
                },
                Echo.Off
            )
            .UnwrapDefault()
        |> ignore

        Process
            .Execute(
                {
                    Command = "dotnet"
                    Arguments =
                        $"tool install fantomless-tool --version {version}"
                },
                Echo.Off
            )
            .UnwrapDefault()
        |> ignore

    Process
        .Execute(
            {
                Command = "dotnet"
                Arguments = "tool restore"
            },
            Echo.Off
        )
        .UnwrapDefault()
    |> ignore
