open Fable.React
open Fable.React.Props
open Elmish
open Elmish.React


type Page =
    | Counter of Pages.Counter.Model
    | Time of Pages.Time.Model


type Msg =
    | CounterMsg of Pages.Counter.Msg
    | TimeMsg of Pages.Time.Msg


type Model = { title: string; page: Page }

let init () =
    let page, cmd = Pages.Counter.init ()

    {
        title = "F# example"
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
        h1 [] [ str model.title ]
        hr []
        match model.page with
        | Counter page -> Pages.Counter.view page (CounterMsg >> dispatch)
        | Time page -> Pages.Time.view page (TimeMsg >> dispatch)
    ]

// App
Program.mkProgram init update view
|> Program.withReactBatched "root"
// |> Program.withConsoleTrace
|> Program.run
