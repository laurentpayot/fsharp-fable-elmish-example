module Pages.Counter

open Elmish
open Feliz
open type Html

open View
open Remote


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
                count = MyJsLib.multiply model.count 2
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
            (fun () -> MyJsLib.catBase64 (model.count.ToString()) 50)
            ()
            GotCat
            GotCatError
    | GotCat base64 -> { model with cat = Loaded base64 }, Cmd.none
    | GotCatError err -> { model with cat = LoadError err }, Cmd.none


let subscriptions (model: Model) : Msg Sub = []


let view (model: Model) (dispatch: Msg Dispatch) : ReactElement list = [
    h2 "Counter"
    p [

        // TODO: covert to Feliz below!!!!!!!!!!!!!!

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
        p [] [
            img [
                Src("data:image/png;base64," + base64)
                Style [ Height "200px"; Width "200px" ]
            ]
        ]
    | Error err -> p [] [ str err.Message ]
]
