# F# Fable Elmish example

This example app is written in the [F# language](https://fsharp.org/). It uses [Fable](https://fable.io/) to transpile F# to JavaScript and [Elmish](https://elmish.github.io/elmish/) to get [The Elm Architecture](https://guide.elm-lang.org/architecture/) (TEA), also known as the Model View Update (MVU) pattern.

Elmish normally uses [React](https://react.dev/) under the hood, but in this example React was seamlessly replaced by [Preact](https://preactjs.com/) to get performances [similar to Elm](https://krausest.github.io/js-framework-benchmark/2022/table_chrome_102.0.5005.61.html), both in term of speed and bundle size.

## Featuring

- Startup flags
- Getting a random number
- Routing
- Subscription via a JavaScript custom event
- Foreign Function Interface (FFI) using synchronous and asynchronous functions imported from JavaScript
- JSON decoding
- Hot Module Replacement (HMR)
- Debugger (via Redux DevTools)

## Prerequisites

To run this example app on your local machine, you will need:

- [.NET 8 SDK](https://dotnet.microsoft.com/en-us/download)
- [pnpm](https://pnpm.io/installation)
- [Node.js](https://nodejs.org/) (can be installed via pnpm with `pnpm env use --global latest`)
- [Redux DevTools](https://github.com/reduxjs/redux-devtools) browser extension for the debugger, and maybe [Preact DevTools](https://preactjs.github.io/preact-devtools/) if you wish to inspect the rendering itself.

The [Ionide](https://ionide.io/) plugin for your IDE is highly recommended.

## Usage

- `pnpm i` to install Node.js dependencies as well as F# dependencies.
- `pnpm start` to start the Vite development server with automatic refresh on http://localhost:5000.
- `pnpm build` to build the bundle, then `pnpm serve` to serve it as a single page application on http://localhost:5000.

## TO DO

- tests
