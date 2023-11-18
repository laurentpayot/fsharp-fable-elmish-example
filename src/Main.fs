open Fable.React
open Fable.React.Props
open Elmish
open Elmish.React


type Model = { count: int }

type Msg =
    | Increment
    | Decrement
    | Random

let init () : Model = { count = 0 }

let update msg model =
    match msg with
    | Increment -> { model with count = model.count + 1 }
    | Decrement -> { model with count = model.count - 1 }
    | Random -> {
        model with
            count = System.Random().Next(100)
      }


let view model dispatch =
    div [] [
        p [] [
            button [ OnClick(fun _ -> dispatch Decrement) ] [ str "-" ]
            str (sprintf "%A" model.count)
            button [ OnClick(fun _ -> dispatch Increment) ] [ str "+" ]
        ]
        p [] [ button [ OnClick(fun _ -> dispatch Random) ] [ str "Random" ] ]
    ]


// App
Program.mkSimple init update view
|> Program.withReactBatched "root"
// |> Program.withConsoleTrace
|> Program.run
