module Route

open Fable.React
open Fable.React.Props
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

let linkTo (route: Route) (props: IHTMLProp list) (children: ReactElement list) : ReactElement =
    let href = toString route

    let onClick =
        OnClick(fun (e: MouseEvent) ->
            e.preventDefault ()
            Navigation.newUrl href |> List.map (fun f -> f ignore) |> ignore)

    a (Href href :: onClick :: props) children
