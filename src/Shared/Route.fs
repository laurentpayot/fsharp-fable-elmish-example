module Route

open Elmish.UrlParser


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
