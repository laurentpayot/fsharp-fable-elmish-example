open Fable.React
open Fable.React.Props
open Elmish
open Elmish.React


type Model = { count: int }

type Msg =
    | Increment
    | Decrement

let init () : Model = { count = 0 }

let update (msg: Msg) (model: Model) =
    match msg with
    | Increment -> { model with count = model.count + 1 }
    | Decrement -> { model with count = model.count - 1 }


let view model dispatch =
    div
        []
        [ button [ OnClick(fun _ -> dispatch Decrement) ] [ str "-" ]
          div [] [ str (sprintf "%A" model) ]
          button [ OnClick(fun _ -> dispatch Increment) ] [ str "+" ] ]


// App
Program.mkSimple init update view
|> Program.withReactBatched "root"
// |> Program.withConsoleTrace
|> Program.run
