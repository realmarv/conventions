module FileConventions.Test

open System
open System.IO

open NUnit.Framework
open NUnit.Framework.Constraints
open Fsdk
open Fsdk.Process

open FileConventions

[<SetUp>]
let Setup () =
    ()

let dummyFilesDirectory = DirectoryInfo (Path.Combine(__SOURCE_DIRECTORY__, "DummyFiles"))

[<Test>]
let HasCorrectShebangTest1 () =
    let fileInfo = (FileInfo (Path.Combine(dummyFilesDirectory.FullName, "DummyWithoutShebang.fsx")))
    Assert.That(HasCorrectShebang fileInfo, Is.EqualTo false)


[<Test>]
let HasCorrectShebangTest2 () =
    let fileInfo = (FileInfo (Path.Combine(dummyFilesDirectory.FullName, "DummyWithShebang.fsx")))
    Assert.That(HasCorrectShebang fileInfo, Is.EqualTo true)


[<Test>]
let HasCorrectShebangTest3 () =
    let fileInfo = (FileInfo (Path.Combine(dummyFilesDirectory.FullName, "DummyWithWrongShebang.fsx")))
    Assert.That(HasCorrectShebang fileInfo, Is.EqualTo false)


[<Test>]
let HasCorrectShebangTest4() =
    let fileInfo = (FileInfo (Path.Combine(dummyFilesDirectory.FullName, "DummyEmpty.fsx")))
    Assert.That(HasCorrectShebang fileInfo, Is.EqualTo false)


[<Test>]
let MixedLineEndingsTest1 () =
    let fileInfo = (FileInfo (Path.Combine(dummyFilesDirectory.FullName, "DummyWithMixedLineEndings")))
    Assert.That(MixedLineEndings fileInfo, Is.EqualTo true)


[<Test>]
let MixedLineEndingsTest2 () =
    let fileInfo = (FileInfo (Path.Combine(dummyFilesDirectory.FullName, "DummyWithLFLineEndings")))
    Assert.That(MixedLineEndings fileInfo, Is.EqualTo false)


[<Test>]
let MixedLineEndingsTest3 () =
    let fileInfo = (FileInfo (Path.Combine(__SOURCE_DIRECTORY__, "DummyFiles", "DummyWithCRLFLineEndings")))
    Assert.That(MixedLineEndings fileInfo, Is.EqualTo false)


[<Test>]
let IsExecutableTest1 () =
    let filePath = Path.Combine(__SOURCE_DIRECTORY__, "DummyFiles", "DummyExecutable.fsx")
    Fsdk.Process
        .Execute(
            {
                Command = "chmod"
                Arguments = sprintf "+x %s" filePath
            },
            Echo.All
        )
        .UnwrapDefault()
    |> ignore<string>
    let fileInfo = (FileInfo filePath)
    Assert.That(IsExecutable fileInfo, Is.EqualTo true)


[<Test>]
let IsExecutableTest2 () =
    let fileInfo = (FileInfo (Path.Combine(__SOURCE_DIRECTORY__, "DummyFiles", "DummyNotExecutable.fs")))
    Assert.That(IsExecutable fileInfo, Is.EqualTo false)
