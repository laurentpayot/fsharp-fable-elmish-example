# F# Fable Elmish example

This example is written in the [F# language](https://fsharp.org/). It uses [Fable](https://fable.io/) to transpile F# to JavaScript and [Elmish](https://elmish.github.io/elmish/) to get [The Elm Architecture](https://guide.elm-lang.org/architecture/) (TEA), also known as the Model View Update (MVU) pattern.

Elmish normally uses [React](https://react.dev/) under the hood, but in this example React was seamlessly replaced by [Preact](https://preactjs.com/) to get similar performance to Elm, both in term of speed and bundle size.

## Featuring

- Startup flags
- Getting a random number
- Routing
- Subscription via a JavaScript custom event
- Foreign Function Interface (FFI) using synchronous and asynchronous functions imported from JavaScript
- JSON decoding

The only missing Elm-like feature is the [Elmish debugger](https://elmish.github.io/debugger/) that is not maintained any more and does not work with the latest Elmish versions. Fortunately you can trace the state and messages as updates happen in the browser developer console when using the development server.

## Prerequisites

To run this example app on your local machine, you will need:

- [.NET 8 SDK](https://dotnet.microsoft.com/en-us/download)
- [pnpm](https://pnpm.io/installation)
- [Node.js](https://nodejs.org/) (can be installed via pnpm with `pnpm env use --global latest`)

[Ionide](https://ionide.io/) plugin for your IDE is highly recommended.

## Usage

- `pnpm i` to install Node.js dependencies as well as F# dependencies.
- `pnpm start` to start the Vite development server with automatic refresh on http://localhost:5000.
- `pnpm build` to build the bundle, then `pnpm serve` to serve it as a single page application on http://localhost:5000.
