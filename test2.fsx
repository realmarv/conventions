open System.Net.Http
open System.Net.Http.Headers

let client: HttpClient = new HttpClient()
client.DefaultRequestHeaders.Accept.Clear()
client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/vnd.github.v3+json"))
client.DefaultRequestHeaders.Add("User-Agent", ".NET Foundation Repository Reporter")

// await ProcessRepositoriesAsync(client);

let ProcessRepositoriesAsync(client: HttpClient) =
    let json = client.GetStringAsync("https://api.github.com/orgs/dotnet/repos")

    printfn "%A" json

ProcessRepositoriesAsync client