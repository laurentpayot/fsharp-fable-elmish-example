open Fable.React
open Fable.React.Props
open Elmish
open Elmish.React
open Elmish.Navigation
open Elmish.UrlParser

type Route =
    | Counter' of int
    | Time'

let router = oneOf [ map Counter' (s "counter" </> i32); map Time' (s "time") ]


type Page =
    | Counter of Pages.Counter.Model
    | Time of Pages.Time.Model


type Msg =
    | CounterMsg of Pages.Counter.Msg
    | TimeMsg of Pages.Time.Msg


type Model = { route: Route; page: Page }


let urlUpdate (result: Option<Route>) model =
    match result with

    | Some(Counter' count) ->
        let page, cmd = Pages.Counter.init count

        {
            model with
                route = result.Value
                page = Counter page
        },
        cmd

    | Some Time' ->
        let page, cmd = Pages.Time.init ()

        {
            model with
                route = result.Value
                page = Time page
        },
        cmd

    // no matching route - go home
    | None -> model, Navigation.modifyUrl "#"




let init () =
    let initialCount = 0
    let page, cmd = Pages.Counter.init initialCount

    {
        route = Counter' initialCount
        page = Counter page
    },
    Cmd.map CounterMsg cmd

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
|> Program.toNavigable (parseHash router) urlUpdate
|> Program.withReactBatched "root"
// |> Program.withConsoleTrace
|> Program.run
