module Tests

open Xunit

open Route


type ``toString tests``() =

    [<Fact>]
    let ``Home`` () = Assert.True(toString Home = "/")

    [<Fact>]
    let ``Counter 0`` () =
        Assert.True(toString (Counter 0) = "/counter/0")
