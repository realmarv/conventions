open System.IO

let EOFIsEOL(path: string) = 
    use sr = new StreamReader (path)
    let filetext = sr.ReadToEnd()
    let eofIsEol = (filetext |> Seq.last = '\n')
    (eofIsEol, path)

printfn "%s" EOFIsEOL "./.gitignore"
// Directory.GetFiles("./") 
// |> Array.map EOFIsEOL 
// |> Array.iter (printfn "%b*%s")
