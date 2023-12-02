module Remote

type 'a Remote =
    | NotAsked
    | Loading
    | Loaded of 'a
    | Error of exn
