module View

open Elmish.Navigation
open Browser.Types
open Feliz
open type Html

open Route


type P = prop
type S = style

let linkTo (route: Route) (linkProps: IReactProperty list) : ReactElement =
    let url = Route.toString route

    let onLinkClick =
        P.onClick (fun (e: MouseEvent) ->
            e.preventDefault ()
            Navigation.newUrl url |> List.map (fun f -> f ignore) |> ignore)

    a <| [ P.href url; onLinkClick ] @ linkProps
