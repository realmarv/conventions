module FileConventions

open System
open System.IO
open System.Linq

open Mono.Unix.Native

let HasCorrectShebang (fileInfo: FileInfo) =
    false;
