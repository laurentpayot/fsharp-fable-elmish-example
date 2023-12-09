namespace App.Tests.Route

open Xunit
open FsUnit.Xunit

open Route


module ``toString tests`` =

    module ``Home`` =

        [<Fact>]
        let ``Home`` () = toString Home |> should equal "/"


    module ``Counter`` =
        [<Fact>]
        let ``Counter 0`` () =
            toString (Counter 0) |> should equal "/counter/0"

        [<Fact>]
        let ``Counter 1`` () =
            toString (Counter 1) |> should equal "/counter/1"
