namespace Tests.Route

open Xunit

open Route


module ``toString tests`` =

    module ``Home`` =

        [<Fact>]
        let ``Home`` () = Assert.True(toString Home = "/")


    module ``Counter`` =
        [<Fact>]
        let ``Counter 0`` () =
            Assert.True(toString (Counter 0) = "/counter/0")

        [<Fact>]
        let ``Counter 1`` () =
            Assert.True(toString (Counter 1) = "/counter/1")
