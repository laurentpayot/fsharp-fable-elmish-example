module Pages.Time

open Fable.React
open Fable.React.Props
open Elmish

open System
open Fable.Core


type Model = { time: DateTime option }
// type Model = { time: string option } //TODO JSON

type Msg =
    | Refresh
    // | GotTime of string //TODO JSON
    | GotTime of DateTime

let init () = { time = None }, Cmd.none

let update msg model =
    match msg with
    | Refresh -> model, Cmd.none
    | GotTime timeStr -> { model with time = Some timeStr }, Cmd.none

let view model dispatch =
    div [] [
        h2 [] [ str "Time" ]
        p [] [
            str
            <| if model.time = None then
                   "Waiting for time..."
               else
                   model.time.Value.ToString()
        ]
        p [] [ button [ OnClick(fun _ -> dispatch Refresh) ] [ str "Refresh" ] ]
    ]

let timer onTick =
    let start dispatch =
        let intervalId = JS.setInterval (fun _ -> dispatch (onTick DateTime.Now)) 1000

        { new IDisposable with
            member _.Dispose() = JS.clearInterval intervalId
        }

    start

let subscriptions model = [ [ "time" ], timer GotTime ]
