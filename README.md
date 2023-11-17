# F# counter web app

## Prerequisites

- [.NET version 6](https://dotnet.microsoft.com/en-us/download/dotnet/6.0) as [Fable](https://fable.io/) does not support .NET version 7 yet. For Ubuntu, install it with: `sudo apt install dotnet-sdk-6.0`
- [pnpm](https://pnpm.io/)
- [Node.js](https://nodejs.org/) installed via pnpm with `pnpm env use --global latest`
- run `pnpm i` to install npm dependencies as well as F# dependencies.

[Ionide](https://ionide.io/) plugin for your IDE is highly recommended.

## F# interactive mode

```bash
dotnet fsi
```

## TO DO

- set indentation to 2 (Ionide + Fantomas + VSCode)

- Routing with https://elmish.github.io/browser/
