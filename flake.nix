{
  description = "A very basic flake";

  inputs = {
    utils.url = github:numtide/flake-utils;
    nixpkgs.url = github:nixos/nixpkgs/nixpkgs-unstable;
  };

  outputs = { self, nixpkgs, utils }:
    utils.lib.eachDefaultSystem (system:
      let
        pkgs = import nixpkgs { inherit system; config.allowUnfree = true; };
      in
     {

        devShell = pkgs.mkShell {
          buildInputs = with pkgs; [
            fsharp
            nodePackages.pnpm
            nodejs
	    dotnet-sdk_8
          ];

	  shellHook = let
	    npmrc = pkgs.writeTextFile {
	      name = ".npmrc";
	      text = ''
# to ensure bash is used instead of Ubuntu’s default dash
script-shell="${pkgs.bashInteractive}/bin/bash"
color="always"

# to fix Vite and Workbox imports
# shamefully-hoist=true
              '';
	    };
	  in
	    ''
mkdir -p .direnv
touch .direnv/paket.dependencies
cat ${npmrc} > .npmrc
	  '';
        };
      });
}
