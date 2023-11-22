module Router

open Elmish.UrlParser

type Route =
    | Counter of int
    | Time

let parser: Browser.Types.Location -> Route option =
    parsePath <| oneOf [ map Counter (s "counter" </> i32); map Time (s "time") ]
