[<RequireQualifiedAccess>]
module MyJsLib

open Fable.Core.JS
open Fable.Core.JsInterop


let multiply (a: int) (b: int) : int = importMember "./my-js-lib.js"

let catBase64 (text: string) (fontSize: int) : string Promise = importMember "./my-js-lib.js"
