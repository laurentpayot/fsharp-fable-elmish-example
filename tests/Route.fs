module Tests

open Xunit

open Route


[<Fact>]
let ``Home test`` () = Assert.True(toString Home = "/")
