module OnEvent

open System
open Fable.Core.JsInterop
open Browser.Dom
open Elmish
open Browser.Types


let onEvent (eventName: string) (toMsg: string -> 'msg) : ('msg Dispatch -> IDisposable) =
    let start dispatch =
        let handler (event: Event) : unit = dispatch (toMsg <| event?detail)

        document.addEventListener (eventName, handler, false)

        { new IDisposable with
            member _.Dispose() =
                document.removeEventListener (eventName, handler, false)
        }

    start
