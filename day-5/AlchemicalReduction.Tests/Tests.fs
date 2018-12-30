module Tests

open System
open Xunit
open AlchemicalReduction.API.Reduction

[<Fact>]
let ``Removes Polymers Correctly``() =
    Assert.Equal("", "")
    Assert.Equal("dabCBAcaDA", reducePolymers "dabAcCaCBAcCcaDA")
