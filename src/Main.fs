open Fable.React
open Fable.React.Props
open Elmish
open Elmish.React
open Elmish.Navigation


type Page =
    | Counter of Pages.Counter.Model
    | Time of Pages.Time.Model

type Msg =
    | CounterMsg of Pages.Counter.Msg
    | TimeMsg of Pages.Time.Msg

type Model = { page: Page }


let urlUpdate routeOpt model =
    match routeOpt with

    | Some(Router.Route.Counter count) ->
        let page, cmd = Pages.Counter.init count

        { model with page = Counter page }, Cmd.map CounterMsg cmd

    | Some(Router.Route.Time) ->
        let page, cmd = Pages.Time.init ()

        { model with page = Time page }, Cmd.map TimeMsg cmd

    // no matching route: go home
    | None -> model, Navigation.modifyUrl "/"


let init routeOpt =
    let model, _ = Pages.Counter.init 0

    urlUpdate routeOpt { page = (Counter model) }


let update msg model =
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


let view model dispatch =
    div [] [
        h1 [] [ str "F# example" ]
        menu [] [
            li [] [ a [ Href "/counter/0" ] [ str "Counter" ] ]
            li [] [ a [ Href "/time" ] [ str "Time" ] ]
        ]
        hr []
        match model.page with
        | Counter page -> Pages.Counter.view page (CounterMsg >> dispatch)
        | Time page -> Pages.Time.view page (TimeMsg >> dispatch)
    ]

// App
Program.mkProgram init update view
|> Program.toNavigable (Router.parser) urlUpdate
|> Program.withReactBatched "root"
// |> Program.withConsoleTrace
|> Program.run
