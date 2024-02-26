# F# Fable Elmish example

The goal of this example is to show how to get typical [Elm](https://elm-lang.org/) features (see below) in [F#](https://fsharp.org/). It uses [Fable](https://fable.io/) to transpile F# to JavaScript and [Elmish](https://elmish.github.io/elmish/) to get [The Elm Architecture](https://guide.elm-lang.org/architecture/) (TEA), also known as the Model View Update (MVU) pattern.

Elmish normally uses [React](https://react.dev/) under the hood, but in this example React was seamlessly replaced by [Preact](https://preactjs.com/) to get performances [similar to Elm](https://krausest.github.io/js-framework-benchmark/2022/table_chrome_102.0.5005.61.html), both in term of speed and bundle size.

## Featuring

- [x] Startup flags
- [x] Getting a random number
- [x] Routing
- [x] Subscription via a JavaScript custom event
- [x] Foreign Function Interface (FFI) using synchronous and asynchronous functions imported from JavaScript
- [x] JSON decoding
- [x] Keyed list
- [x] Unit tests
- [x] Hot Module Replacement (HMR)
- [x] Debugger (via Redux DevTools)

## Prerequisites

To run this example app on your local machine, you will need:

- [.NET 8 SDK](https://dotnet.microsoft.com/en-us/download)
- [pnpm](https://pnpm.io/installation)
- [Node.js](https://nodejs.org/) (can be installed via pnpm with `pnpm env use --global latest`)
- [Redux DevTools](https://github.com/reduxjs/redux-devtools) browser extension for the debugger, and maybe [Preact DevTools](https://preactjs.github.io/preact-devtools/) if you wish to inspect the rendering itself.
- [Ionide](https://ionide.io/) plugin for your IDE is highly recommended.

## Usage

- `pnpm i` to install Node.js dependencies as well as F# dependencies.
- `pnpm start` to start the Vite development server with automatic refresh on http://localhost:5000.
- `pnpm build` to build the bundle, then `pnpm serve` to serve it as a single page application on http://localhost:5000.
- `pnpm test` to run unit tests.
- `pnpm test:watch` to run unit tests in watch mode.

If you get an error like `ENOSPC: System limit for number of file watchers reached`, run `pnpm watchers-fix` to increase the number of watchers on your system. If you want this increase to be permanent, see [this answer on StackOverflow](https://stackoverflow.com/a/55543310/2675387).

## License

[MIT](https://github.com/laurentpayot/fsharp-fable-elmish-example/blob/main/LICENSE)
