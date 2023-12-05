module Route

open Fable.Core.JsInterop
open Browser.Types
open Elmish.UrlParser
open Elmish.Navigation


type Route =
    | Home
    | Counter of int
    | Time

let parser: Browser.Types.Location -> Route option =
    parsePath
    <| oneOf [ map Home top; map Counter (s "counter" </> i32); map Time (s "time") ]

let toString (route: Route) : string =
    match route with
    | Home -> "/"
    | Counter count -> $"/counter/{count}"
    | Time -> "/time"

let goToHref (e: MouseEvent) =
    e.preventDefault ()
    let href: string = !!e.currentTarget?attributes?href?value
    Navigation.newUrl href |> List.map (fun f -> f ignore) |> ignore
