module Pages.Time

open Fable.React
open Fable.React.Props
open Elmish
open Thoth.Json

open OnEvent


type Model = { times: string list }

type Msg = GotJsonTime of string

type TimeRecord = { time: string; foo: string option }

let decoder: Decoder<TimeRecord> =
    Decode.object (fun get -> {
        time = get.Required.Field "time" Decode.string
        foo = get.Optional.Field "foo" Decode.string
    })


let init () : Model * Msg Cmd = { times = [] }, Cmd.none


let update (msg: Msg) (model: Model) : Model * Msg Cmd =
    match msg with
    | GotJsonTime str ->
        match Decode.fromString decoder str with
        | Ok timeRecord ->
            {
                model with
                    times = timeRecord.time :: model.times
            },
            Cmd.none
        | Error _ -> model, Cmd.none


let view (model: Model) (dispatch: Msg Dispatch) : ReactElement list =
    let lastTime =
        match model.times with
        | [] -> "Waiting for time…"
        | time :: _ -> time

    [
        h2 [] [ str "Time" ]
        p [] [ strong [] [ str lastTime ] ]
        p [] [
            ul [ ClassName "times" ] [ for time in model.times -> li [ Key time ] [ str time ] ]
        ]
    ]


let subscriptions (model: Model) : Msg Sub = [ [ "time" ], onEvent "time" GotJsonTime ]
