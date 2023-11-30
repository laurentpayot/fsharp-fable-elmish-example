# F# Fable Elmish example

This example is written in the [F# language](https://fsharp.org/). It uses [Fable](https://fable.io/) to transpile F# to JavaScript and [Elmish](https://elmish.github.io/elmish/) to get [The Elm Architecture](https://guide.elm-lang.org/architecture/) (TEA), also known as the Model View Update (MVU).

## Prerequisites

- [.NET 8](https://dotnet.microsoft.com/en-us/download/dotnet): for Ubuntu, install it with `sudo apt install dotnet-sdk-8.0`
- [pnpm](https://pnpm.io/)
- [Node.js](https://nodejs.org/) can be installed via pnpm with `pnpm env use --global latest`
- run `pnpm i` to install npm dependencies as well as F# dependencies.

[Ionide](https://ionide.io/) plugin for your IDE is highly recommended.

## Usage

- `pnpm start` to start the Vite development server with automatic refresh on http://localhost:5000.
- `pnpm build` to build the bundle.

## TO DO

- Put a list of all the Elmish features used by this example in the README file.
