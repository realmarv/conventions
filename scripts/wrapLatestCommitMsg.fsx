#!/usr/bin/env -S dotnet fsi

open System.IO
open System

#load "../src/FileConventions/Library.fs"

#r "nuget: Fsdk, Version=0.6.0--date20230214-0422.git-1ea6f62"

open Fsdk
open Fsdk.Process

let commitMsg =
    Fsdk
        .Process
        .Execute(
            {
                Command = "git"
                Arguments = "log -1 --format=%B"
            },
            Echo.Off
        )
        .UnwrapDefault()
        .Trim()

let header, body =
    let newLineIndex = commitMsg.IndexOf(Environment.NewLine)

    if newLineIndex > 0 then
        commitMsg.Substring(0, newLineIndex).Trim(),
        commitMsg.Substring(newLineIndex).Trim()
    else
        commitMsg, null

let maxCharsPerLine = 64

let wrappedBody = FileConventions.WrapText body maxCharsPerLine

let newCommitMsg =
    header
    + Environment.NewLine
    + Environment.NewLine
    + wrappedBody
    + Environment.NewLine

File.WriteAllText("wlog.txt", newCommitMsg)
