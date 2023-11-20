module Pages.Time

open Fable.React
open Fable.React.Props
open Elmish

type JsTime = JsTime of string

type Model = { jsTime: JsTime option }

type Msg = | Refresh

let init () = { jsTime = None }, Cmd.none

let update msg model =
    match msg with
    | Refresh -> model, Cmd.none

let view model dispatch =
    div [] [
        h2 [] [ str "Time" ]
        p [] [ str (sprintf "%A" model.jsTime) ]
        p [] [ button [ OnClick(fun _ -> dispatch Refresh) ] [ str "Refresh" ] ]
    ]
