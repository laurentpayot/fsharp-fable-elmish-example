module Main

open Fable.Core.JsInterop
open Fable.React
open Elmish
open Elmish.React
open Elmish.Navigation
open Elmish.Debug
open Elmish.HMR // must be after all the other Elmish imports

open Route

importSideEffects "./index.css"


type Page =
    | Counter of Pages.Counter.Model
    | Time of Pages.Time.Model
    | NotFound

type Msg =
    | CounterMsg of Pages.Counter.Msg
    | TimeMsg of Pages.Time.Msg

type Model = { page: Page }

type Flags = { count: int }


let urlUpdate (routeOpt: Route option) (model: Model) : Model * Msg Cmd =
    match routeOpt with
    | Some(Home) ->
        let pageModel, pageCmd = Pages.Counter.init 42

        { model with page = Counter pageModel }, Cmd.map CounterMsg pageCmd


    | Some(Route.Counter count) ->
        let pageModel, pageCmd = Pages.Counter.init count

        { model with page = Counter pageModel }, Cmd.map CounterMsg pageCmd

    | Some(Route.Time) ->
        let pageModel, pageCmd = Pages.Time.init ()

        { model with page = Time pageModel }, Cmd.map TimeMsg pageCmd

    // no matching route
    | None -> { model with page = NotFound }, Cmd.none


let init (flags: Flags) (routeOpt: Route option) : Model * Msg Cmd =
    let pageModel, _ = Pages.Counter.init flags.count

    urlUpdate routeOpt { page = Counter pageModel }


let update (msg: Msg) (model: Model) : Model * Msg Cmd =
    match msg, model.page with

    | CounterMsg pageMsg, Counter pageModel ->
        let newPageModel, newPageCmd = Pages.Counter.update pageMsg pageModel

        {
            model with
                page = Counter newPageModel
        },
        Cmd.map CounterMsg newPageCmd


    | TimeMsg pageMsg, Time pageModel ->
        let newPageModel, newPageCmd = Pages.Time.update pageMsg pageModel

        { model with page = Time newPageModel }, Cmd.map TimeMsg newPageCmd

    | _ -> model, Cmd.none


let subscriptions (model: Model) : Msg Sub =
    Sub.batch [
        match model.page with
        | Time pageModel -> Sub.map "time" TimeMsg <| Pages.Time.subscriptions pageModel
        | _ -> Sub.none
    ]

let pageView (model: Model) (dispatch: Msg Dispatch) : ReactElement list =
    match model.page with
    | Counter pageModel -> Pages.Counter.view pageModel (CounterMsg >> dispatch)
    | Time pageModel -> Pages.Time.view pageModel (TimeMsg >> dispatch)
    | NotFound -> Pages.NotFound.view

let view (model: Model) (dispatch: Msg Dispatch) : ReactElement =
    div [] [
        header [] [
            h1 [] [ str "F# Fable Elmish example" ]
            nav [] [
                ul [] [
                    li [] [ linkTo Home [] [ str "Home" ] ]
                    li [] [ linkTo (Route.Counter 0) [] [ str "Counter starting at 0" ] ]
                    li [] [ linkTo (Route.Counter 50) [] [ str "Counter starting at 50" ] ]
                    li [] [ linkTo Route.Time [] [ str "Time" ] ]
                ]
            ]
            hr []
        ]
        main [] <| pageView model dispatch
    ]

// startApp() is exported from the Main module
let startApp (flags: Flags) =
    Program.mkProgram (init flags) update (lazyView2 view)
    |> Program.withSubscription subscriptions
    |> Program.toNavigable parser urlUpdate
    |> Program.withReactBatched "root"
#if DEBUG
    |> Program.withDebugger
#endif
    |> Program.run

    printfn "App started."

#if DEBUG
// app started here to allow HMR
startApp { count = 42 }
#endif
