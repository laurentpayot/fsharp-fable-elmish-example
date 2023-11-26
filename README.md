# F# counter web app

## Prerequisites

- [.NET 8](https://dotnet.microsoft.com/en-us/download/dotnet): for Ubuntu, install it with `sudo apt install dotnet-sdk-8.0`
- [pnpm](https://pnpm.io/)
- [Node.js](https://nodejs.org/) installed via pnpm with `pnpm env use --global latest`
- run `pnpm i` to install npm dependencies as well as F# dependencies.

[Ionide](https://ionide.io/) plugin for your IDE is highly recommended.

## F# interactive mode

```bash
dotnet fsi
```

## TO DO

- async JS interop
  - better types
  - try something different from `Cmd.OfPromise.either` (two messages needed)
