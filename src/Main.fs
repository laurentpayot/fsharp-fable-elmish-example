open Fable.React
open Elmish
open Elmish.React
open Elmish.Navigation
open Elmish.Debug
open Elmish.HMR // must be after all the other Elmish imports

open Route


type PageModel =
    | Counter of Pages.Counter.Model
    | Time of Pages.Time.Model

type Msg =
    | CounterMsg of Pages.Counter.Msg
    | TimeMsg of Pages.Time.Msg

type Model = { pageModel: PageModel }

type Flags = { count: int }


let urlUpdate (routeOpt: Route option) (model: Model) : Model * Msg Cmd =
    match routeOpt with
    | Some(Home) ->
        let pageModel, cmd = Pages.Counter.init 42

        {
            model with
                pageModel = Counter pageModel
        },
        Cmd.map CounterMsg cmd


    | Some(Route.Counter count) ->
        let pageModel, cmd = Pages.Counter.init count

        {
            model with
                pageModel = Counter pageModel
        },
        Cmd.map CounterMsg cmd

    | Some(Route.Time) ->
        let pageModel, cmd = Pages.Time.init ()

        {
            model with
                pageModel = Time pageModel
        },
        Cmd.map TimeMsg cmd

    // no matching route
    | None -> model, Navigation.newUrl "/"


let init (flags: Flags) (routeOpt: Route option) : Model * Msg Cmd =
    let pageModel, _ = Pages.Counter.init flags.count

    urlUpdate routeOpt { pageModel = Counter pageModel }


let update (msg: Msg) (model: Model) : Model * Msg Cmd =
    match msg, model.pageModel with

    | CounterMsg pageMsg, Counter pageModel ->
        let newPageModel, newPageCmd = Pages.Counter.update pageMsg pageModel

        {
            model with
                pageModel = Counter newPageModel
        },
        Cmd.map CounterMsg newPageCmd


    | TimeMsg pageMsg, Time pageModel ->
        let newPageModel, newPageCmd = Pages.Time.update pageMsg pageModel

        {
            model with
                pageModel = Time newPageModel
        },
        Cmd.map TimeMsg newPageCmd

    | _ -> model, Cmd.none


let subscriptions (model: Model) : Msg Sub =
    Sub.batch [
        match model.pageModel with
        | Time pageModel -> Sub.map "time" TimeMsg <| Pages.Time.subscriptions pageModel
        | _ -> Sub.none
    ]

let view (model: Model) (dispatch: Msg -> unit) : ReactElement =
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
        main []
        <| match model.pageModel with
           | Counter pageModel -> Pages.Counter.view pageModel (CounterMsg >> dispatch)
           | Time pageModel -> Pages.Time.view pageModel (TimeMsg >> dispatch)
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
