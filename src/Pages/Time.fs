module Pages.Time

open Elmish
open Thoth.Json
open Feliz
open type Html

open View
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
        h2 "Time"
        p [ strong lastTime ]
        p [
            ul [
                P.className "times"
                __ [ for time in model.times -> li [ P.key time; __' time ] ]

            ]
        ]
    ]


let subscriptions (model: Model) : Msg Sub = [ [ "time" ], onEvent "time" GotJsonTime ]
