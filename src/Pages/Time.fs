module Pages.Time

open Fable.React
open Fable.React.Props
open Fable.Core.JsInterop
open Elmish
open Browser.Dom
open System
open Thoth.Json

type TimeRecord = { time: string; foo: string option }

// type Model = { time: DateTime option }
type Model = { time: string option }

type Msg =
    | Refresh
    // | GotJsonTime of DateTime
    | GotJsonTime of string


let decoder: Decoder<TimeRecord> =
    Decode.object (fun get -> {
        time = get.Required.Field "time" Decode.string
        foo = get.Optional.Field "foo" Decode.string
    })

let init () = { time = None }, Cmd.none

let update msg model =
    match msg with
    | Refresh -> model, Cmd.none
    | GotJsonTime json ->
        match Decode.fromString decoder (json) with
        | Ok timeRecord ->
            {
                model with
                    time = Some timeRecord.time
            },
            Cmd.none
        | Error _ -> model, Cmd.none

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
            (fun event -> dispatch (toMsg <| event?detail)),
            false
        )

        { new IDisposable with
            member _.Dispose() =
                document.removeEventListener (eventName, (fun _ -> ()))
        }

    start

let subscriptions model = [ [ "time" ], onEvent "time" GotJsonTime ]
