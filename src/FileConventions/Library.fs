module FileConventions

open System
open System.IO
open System.Linq
open System.Text.RegularExpressions

let HasCorrectShebang(fileInfo: FileInfo) =
    let fileText = File.ReadLines fileInfo.FullName

    if fileText.Any() then
        let firstLine = fileText.First()

        firstLine.StartsWith "#!/usr/bin/env fsx"
        || firstLine.StartsWith "#!/usr/bin/env -S dotnet fsi"

    else
        false

let MixedLineEndings(fileInfo: FileInfo) =
    use streamReader = new StreamReader(fileInfo.FullName)
    let fileText = streamReader.ReadToEnd()

    let lf = Regex("[^\r]\n", RegexOptions.Compiled)
    let cr = Regex("\r[^\n]", RegexOptions.Compiled)
    let crlf = Regex("\r\n", RegexOptions.Compiled)

    let numberOfLineEndings =
        [
            lf.IsMatch fileText
            cr.IsMatch fileText
            crlf.IsMatch fileText
        ]
        |> Seq.filter(
            function
            | isMatch -> isMatch = true
        )
        |> Seq.length

    numberOfLineEndings > 1

let DetectUnpinnedVersionsInGitHubCI(fileInfo: FileInfo) =
    assert (fileInfo.FullName.EndsWith(".yml"))
    use streamReader = new StreamReader(fileInfo.FullName)
    let fileText = streamReader.ReadToEnd()

    let latestTagInRunsOnRegex =
        Regex("runs-on: .*-latest", RegexOptions.Compiled)

    latestTagInRunsOnRegex.IsMatch fileText

let DetectAsteriskInPackageReferenceItems(fileInfo: FileInfo) =
    assert (fileInfo.FullName.EndsWith "proj")
    use streamReader = new StreamReader(fileInfo.FullName)
    let fileText = streamReader.ReadToEnd()

    let asteriskInPackageReference =
        Regex(
            "<PackageReference.*Version=\".*\*.*\".*/>",
            RegexOptions.Compiled
        )

    asteriskInPackageReference.IsMatch fileText

let DetectMissingVersionsInNugetPackageReferences(fileInfo: FileInfo) =
    assert (fileInfo.FullName.EndsWith ".fsx")

    let fileLines = File.ReadLines fileInfo.FullName

    not(
        fileLines
        |> Seq.filter(fun line -> line.StartsWith "#r \"nuget:")
        |> Seq.filter(fun line -> not(line.Contains ","))
        |> Seq.isEmpty
    )

let HasBinaryContent(fileInfo: FileInfo) =
    let lines = File.ReadLines fileInfo.FullName

    lines
    |> Seq.map(fun line ->
        line.Any(fun character ->
            Char.IsControl character && character <> '\r' && character <> '\n'
        )
    )
    |> Seq.contains true

type EolAtEof =
    | True
    | False
    | NotApplicable

let EolAtEof(fileInfo: FileInfo) =
    if HasBinaryContent fileInfo then
        NotApplicable
    else
        use streamReader = new StreamReader(fileInfo.FullName)
        let filetext = streamReader.ReadToEnd()

        if filetext <> String.Empty then
            if Seq.last filetext = '\n' then
                True
            else
                False
        else
            True

let WrapParagraph (paragraph: string) (count: int) : string =

    let modifyCodeBlocks(text: string) =
        Regex.Replace(
            text,
            @"\s*```\s*",
            Environment.NewLine + "```" + Environment.NewLine
        )

    let newParagraph = modifyCodeBlocks(paragraph)

    let rec splitIntoLines
        (acc: string list)
        (currLine: string)
        (insideCodeBlock: bool)
        (words: string list)
        =
        match words with
        | [] -> currLine :: acc
        | word :: rest ->
            if word = "```" then
                let newInsideCodeBlock = not(insideCodeBlock)
                let newAcc = "```" :: currLine.Trim() :: acc
                let newLine = rest.[0]
                let newWords = words.Tail
                splitIntoLines newAcc newLine newInsideCodeBlock newWords
            else
                let newLineCharacterCount = currLine.Length + word.Length + 1

                let newAcc =
                    if newLineCharacterCount > count then
                        currLine.Trim() :: acc
                    else
                        acc

                let newLine =
                    if newLineCharacterCount > count then
                        word
                    else
                        currLine + " " + word

                splitIntoLines newAcc newLine insideCodeBlock rest

    let words = newParagraph.Split([| ' ' |]) |> Array.toList

    words.Tail
    |> splitIntoLines [] words.Head (words.Tail = "```")
    |> List.rev
    |> String.concat Environment.NewLine
