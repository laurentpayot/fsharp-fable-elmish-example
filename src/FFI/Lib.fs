[<RequireQualifiedAccess>]
module Lib

open Fable.Core.JS
open Fable.Core.JsInterop


let multiply (a: int) (b: int) : int = importMember "../lib.js"

let catBase64 (text: string) (fontSize: int) : string Promise = importMember "../lib.js"
