open Fable.React
open Fable.React.Props
open Fable.Core.JsInterop
open Browser.Types
open Elmish
open Elmish.React
open Elmish.Navigation
open Elmish.HMR // must be after all the other Elmish imports

open Router


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

    // Home or no matching route
    | Some(Home)
    | None -> model, Navigation.modifyUrl "/"


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


let goToUrl (e: MouseEvent) =
    e.preventDefault ()
    let href: string = !!e.currentTarget?attributes?href?value
    Navigation.modifyUrl href |> List.map (fun f -> f ignore) |> ignore

let view (model: Model) (dispatch: Msg -> unit) : ReactElement =
    div [] [
        header [] [
            h1 [] [ str "F# Fable Elmish example" ]
            nav [] [
                ul [] [
                    li [] [ a [ Href "/" ] [ str "Home" ] ]
                    li [] [
                        a [ Href "/counter/0"; OnClick goToUrl ] [ str "Counter starting at 0" ]
                    ]
                    li [] [ a [ Href "/counter/50" ] [ str "Counter starting at 50" ] ]
                    li [] [ a [ Href "/time" ] [ str "Time" ] ]
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
    Program.mkProgram (init flags) update view
    |> Program.withSubscription subscriptions
    |> Program.toNavigable parser urlUpdate
    |> Program.withReactBatched "root"
#if DEBUG
    |> Program.withConsoleTrace
#endif
    |> Program.run

printfn "Starting app..."
// app started here to allow HMR
startApp { count = 42 }
