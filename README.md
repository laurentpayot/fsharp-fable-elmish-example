# F# counter web app

## Prerequisites

- [.NET](https://dotnet.microsoft.com/en-us/download/dotnet) version 8 (the latest) for Fable and version 7 for the update tool. For Ubuntu, install them with: `sudo apt install dotnet-sdk-7.0 dotnet-sdk-8.0`
- [pnpm](https://pnpm.io/)
- [Node.js](https://nodejs.org/) installed via pnpm with `pnpm env use --global latest`
- run `pnpm i` to install npm dependencies as well as F# dependencies.

[Ionide](https://ionide.io/) plugin for your IDE is highly recommended.

## F# interactive mode

```bash
dotnet fsi
```

## TO DO

- Subscriptions using custom events https://github.com/fable-compiler/fable-browser/blob/master/src/Event/Browser.Event.fs
