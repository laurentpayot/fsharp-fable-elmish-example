module Remote

type 'a Remote =
    | NotAsked
    | Loading
    | Loaded of 'a
    | LoadError of exn
