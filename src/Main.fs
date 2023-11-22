open Fable.React
open Fable.React.Props
open Elmish
open Elmish.React
open Elmish.Navigation
open Elmish.HMR // must be the last Elmish package

open Router

type PageModel =
    | Counter of Pages.Counter.Model
    | Time of Pages.Time.Model

type Msg =
    | CounterMsg of Pages.Counter.Msg
    | TimeMsg of Pages.Time.Msg

type Model = { pageModel: PageModel }


let urlUpdate routeOpt model =
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

    // no matching route: go home
    | None -> model, Navigation.modifyUrl "/"


let init routeOpt =
    let pageModel, _ = Pages.Counter.init 0

    urlUpdate routeOpt { pageModel = Counter pageModel }


let update msg model =
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

let subscriptions model =
    Sub.batch [
        match model.pageModel with
        | Time pageModel -> Sub.map "time" TimeMsg <| Pages.Time.subscriptions pageModel
        | _ -> Sub.none
    ]

let view model dispatch =
    div [] [
        h1 [] [ str "F# example" ]
        menu [] [
            li [] [ a [ Href "/counter/0" ] [ str "Counter starting at 0" ] ]
            li [] [ a [ Href "/counter/50" ] [ str "Counter starting at 50" ] ]
            li [] [ a [ Href "/time" ] [ str "Time" ] ]
        ]
        hr []
        match model.pageModel with
        | Counter pageModel -> Pages.Counter.view pageModel (CounterMsg >> dispatch)
        | Time pageModel -> Pages.Time.view pageModel (TimeMsg >> dispatch)
    ]

// App
Program.mkProgram init update view
|> Program.withSubscription subscriptions
|> Program.toNavigable parser urlUpdate
|> Program.withReactBatched "root"
// |> Program.withConsoleTrace
|> Program.run
