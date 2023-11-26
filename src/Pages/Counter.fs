module Pages.Counter

open Fable.Core.JS
open Fable.Core.JsInterop
open Fable.React
open Fable.React.Props
open Elmish

open Remote

let multiply (a: int) (b: int) : int = importMember "../lib.js"
let catBase64 (text: string) (fontSize: int) : string Promise = importMember "../lib.js"


type Model = { count: int; cat: string Remote }

type Msg =
    | Increment
    | Decrement
    | Double
    | Randomize
    | GetCat
    | GotCat of string
    | GotCatError of exn

let init (initialCount: int) : Model * Msg Cmd =
    { count = initialCount; cat = NotAsked }, Cmd.none

let update (msg: Msg) (model: Model) =
    match msg with
    | Increment -> { model with count = model.count + 1 }, Cmd.none
    | Decrement -> { model with count = model.count - 1 }, Cmd.none
    | Double ->
        {
            model with
                count = multiply model.count 2
        },
        Cmd.none
    | Randomize ->
        {
            model with
                count = System.Random().Next(100)
        },
        Cmd.none
    | GetCat ->
        { model with cat = Loading },
        Cmd.OfPromise.either
            (fun () -> catBase64 (model.count.ToString()) 100)
            ()
            GotCat
            GotCatError
    | GotCat base64 -> { model with cat = Loaded base64 }, Cmd.none
    | GotCatError err -> { model with cat = Error err }, Cmd.none

let subscriptions (model: Model) : Sub<Msg> = []

let view (model: Model) (dispatch: Msg -> unit) =
    div [] [
        h2 [] [ str "Counter" ]
        p [] [
            button [ OnClick(fun _ -> dispatch Decrement) ] [ str "-" ]
            str (sprintf "%A" model.count)
            button [ OnClick(fun _ -> dispatch Increment) ] [ str "+" ]
        ]
        p [] [
            button [ OnClick(fun _ -> dispatch Double) ] [ str "Double" ]
            button [ OnClick(fun _ -> dispatch Randomize) ] [ str "Random" ]
        ]
        p [] [ button [ OnClick(fun _ -> dispatch GetCat) ] [ str "Cat" ] ]
        match model.cat with
        | NotAsked -> p [] [ str "No cat yet" ]
        | Loading -> p [] [ str "Loading…" ]
        | Loaded base64 ->
            p [] [ img [ Src("data:image/png;base64," + base64); Style [ Height "200px" ] ] ]
        | Error err -> p [] [ str err.Message ]
    ]
