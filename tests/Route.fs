module Tests

open Xunit

open Route


[<Fact>]
let ``MyJsLib test`` () = Assert.True(toString Home = "/")
