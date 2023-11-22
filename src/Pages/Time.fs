module Pages.Time

open Fable.React
open Fable.React.Props
open Elmish
open System

// type JsTime = JsTime of string

type Model = { time: DateTime option }

type Msg =
    | Refresh
    | GotTime of DateTime

let init () = { time = None }, Cmd.none

let update msg model =
    match msg with
    | Refresh -> model, Cmd.none
    | GotTime dateTime -> { model with time = Some dateTime }, Cmd.none

let view model dispatch =
    div [] [
        h2 [] [ str "Time" ]
        p [] [
            str
            <| if model.time = None then
                   "Waiting nnnfor time..."
               else
                   model.time.Value.ToString()
        ]
        p [] [ button [ OnClick(fun _ -> dispatch Refresh) ] [ str "Refresh" ] ]
    ]

let subscriptions model = [ [ "time" ], GotTime ]
