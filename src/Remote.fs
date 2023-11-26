module Remote

type Remote<'a> =
    | NotAsked
    | Loading
    | Loaded of 'a
    | Error of exn
