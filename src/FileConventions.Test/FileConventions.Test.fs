module FileConventions.Test

open System
open System.IO

open NUnit.Framework
open NUnit.Framework.Constraints

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
let DetectUnpinnedVersionsInGitHubCI1 () =
    let fileInfo = (FileInfo (Path.Combine(__SOURCE_DIRECTORY__, "DummyFiles", "DummyCIWithLatestTag.yml")))
    Assert.That(DetectUnpinnedVersionsInGitHubCI fileInfo, Is.EqualTo true)


[<Test>]
let DetectUnpinnedVersionsInGitHubCI2 () =
    let fileInfo = (FileInfo (Path.Combine(__SOURCE_DIRECTORY__, "DummyFiles", "DummyCIWithoutLatestTag.yml")))
    Assert.That(DetectUnpinnedVersionsInGitHubCI fileInfo, Is.EqualTo false)


[<Test>]
let DetectAsteriskInPackageReferenceItems1 () =
    let fileInfo = (FileInfo (Path.Combine(__SOURCE_DIRECTORY__, "DummyFiles", "DummyFsprojWithAsterisk.fsproj")))
    Assert.That(DetectAsteriskInPackageReferenceItems fileInfo, Is.EqualTo true)


[<Test>]
let DetectAsteriskInPackageReferenceItems2 () =
    let fileInfo = (FileInfo (Path.Combine(__SOURCE_DIRECTORY__, "DummyFiles", "DummyFsprojWithoutAsterisk.fsproj")))
    Assert.That(DetectAsteriskInPackageReferenceItems fileInfo, Is.EqualTo false)


[<Test>]
let DotnetToolInstallInvocationsWithoutVersionFlag1 () =
    let fileInfo = (FileInfo (Path.Combine(__SOURCE_DIRECTORY__, "DummyFiles", "DummyDotnetToolInstallWithoutVersion.yml")))
    Assert.That(DotnetToolInstallInvocationsWithoutVersionFlag fileInfo, Is.EqualTo true)


[<Test>]
let DotnetToolInstallInvocationsWithoutVersionFlag2 () =
    let fileInfo = (FileInfo (Path.Combine(__SOURCE_DIRECTORY__, "DummyFiles", "DummyDotnetToolInstallWithVersion.yml")))
    Assert.That(DotnetToolInstallInvocationsWithoutVersionFlag fileInfo, Is.EqualTo false)
