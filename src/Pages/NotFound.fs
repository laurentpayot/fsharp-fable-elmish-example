module Pages.NotFound

open Feliz
open type Html

open View
open Route

let view: ReactElement list = [
    h2 "Page Not Found"
    span [ P.style [ S.fontSize (length.rem 10) ]; __' "🕵️" ]
    p [ linkTo Home [ __' "Go Home" ] ]
]
