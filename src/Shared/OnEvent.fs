module OnEvent

open System
open Fable.Core.JsInterop
open Browser.Dom
open Elmish


let onEvent (eventName: string) (toMsg: string -> 'msg) : ('msg Dispatch -> IDisposable) =
    let start dispatch =
        document.addEventListener (
            eventName,
            (fun event -> dispatch (toMsg <| event?detail)),
            false
        )

        { new IDisposable with
            member _.Dispose() =
                document.removeEventListener (eventName, (fun _ -> ()))
        }

    start
