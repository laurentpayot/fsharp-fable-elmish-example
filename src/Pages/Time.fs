module Pages.Time

open Fable.React
open Fable.React.Props
open Fable.Core.JsInterop
open Elmish
open Browser.Dom
open System
open Thoth.Json

// type Model = { time: DateTime option }
type Model = { time: string option } //TODO JSON

type Msg =
    | Refresh
    // | GotTime of DateTime
    | GotTime of string //TODO JSON

let init () = { time = None }, Cmd.none

let update msg model =
    match msg with
    | Refresh -> model, Cmd.none
    | GotTime timeStr -> { model with time = Some timeStr }, Cmd.none

let view model dispatch =
    div [] [
        h2 [] [ str "Timer" ]
        p [] [
            str
            <| if model.time = None then
                   "Waiting for time..."
               else
                   model.time.Value
        ]
        p [] [ button [ OnClick(fun _ -> dispatch Refresh) ] [ str "Refresh" ] ]
    ]

let onEvent eventName toMsg =
    let start dispatch =
        document.addEventListener (
            eventName,
            (fun event ->
                let detail = event?detail
                dispatch (toMsg <| detail)),
            false
        )

        { new IDisposable with
            member _.Dispose() =
                document.removeEventListener (eventName, (fun _ -> ()))
        }

    start

let subscriptions model = [ [ "time" ], onEvent "time" GotTime ]
