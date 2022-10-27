open System.IO
open System

// let readLines (filePath:string) = seq {
//     use sr = new StreamReader (filePath)
//     while not sr.EndOfStream do
//         yield sr.ReadLine ()
// }
let EOFIsLinebreak(path: string) = bool {
    use sr = new StreamReader (path)
    let filetext = sr.ReadToEnd()
    (filetext |> Seq.last = '\n')
}

// readLines("/home/zahra/workshop/work/nodeffect/conventions/hello.fsx")
// |> Array.iter (printfn "%s")
let root = "/home/zahra/workshop/work/nodeffect/conventions/"
Directory.GetFiles("./") 
|> Array.map Path.GetFileName 
|> Array.iter (printfn "%s") 
// let path = "/home/zahra/workshop/work/nodeffect/conventions/hello.fsx"
// use lines = StreamReader.ReadToEnd(path)

// printfn "HERE ===> %b" (filetext |> Seq.last = '\n')

// printfn "%s" lines;;
// let content = File.Open(path, FileMode.Open, FileAcces.Read)                
// printfn content
// To check
// let lineLength = lines.Length
// printfn "Lines length: %i" lineLength
// lines |> Seq.iter(fun x -> printfn  "%s end" x) 