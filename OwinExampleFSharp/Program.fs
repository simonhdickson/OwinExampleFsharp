open System
open Owin
open Microsoft.AspNet.SignalR
open Microsoft.Owin.StaticFiles
open Microsoft.Owin.Hosting

open ImpromptuInterface.FSharp

type Move() =
    inherit Hub()
    member this.Action(x: int, y: int) : unit =
        this.Clients.Others?shapemoved(x, y);

type Startup() =
    member x.Configuration(app: Owin.IAppBuilder) =
        let config = new HubConfiguration()

        app.UseStaticFiles("Web") |> ignore
        app.MapSignalR(config) |> ignore

[<EntryPoint>]
let main argv =
    let url = "http://localhost:5000/"
    let disposable = WebApp.Start<Startup>(url)
    Console.WriteLine("Server running on " + url)
    Console.WriteLine("Press Enter to stop.")
    Console.ReadLine() |> ignore
    disposable.Dispose()
    0