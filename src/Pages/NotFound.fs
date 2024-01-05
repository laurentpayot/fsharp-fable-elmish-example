module Pages.NotFound

open Fable.React
open Fable.React.Props

open Route

let view: ReactElement list = [
    h2 [] [ str "Page Not Found" ]
    span [ Style [ FontSize "10rem" ] ] [ str "🕵️" ]
    p [] [ linkTo Home [] [ str "Go Home" ] ]
]
