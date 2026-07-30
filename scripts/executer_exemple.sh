#!/usr/bin/env bash
set -euo pipefail

if [[ "$#" -ne 1 ]]
then
    printf 'Usage : %s IDENTIFIANT\n' "$0" >&2
    exit 2
fi

racine="$(cd "$(dirname "${BASH_SOURCE[0]}")/.." && pwd)"
manifestes=()
while IFS= read -r manifeste_trouve
do
    manifestes+=("$manifeste_trouve")
done < <(rg -l --glob 'exemple.json' "\"id\"[[:space:]]*:[[:space:]]*\"$1\"" "$racine" || true)

if [[ "${#manifestes[@]}" -ne 1 ]]
then
    printf "L'identifiant %s correspond à %d manifeste(s).\n" "$1" "${#manifestes[@]}" >&2
    exit 1
fi

manifeste="${manifestes[0]}"
projet="$(python3 -c 'import json,sys; print(json.load(open(sys.argv[1])).get("projetExecutable", ""))' "$manifeste")"
if [[ -z "$projet" ]]
then
    printf "L'exemple %s n'est pas exécutable.\n" "$1" >&2
    exit 1
fi

DOTNET_CLI_HOME="${DOTNET_CLI_HOME:-/tmp/dotnet-home}" dotnet run --project "$(dirname "$manifeste")/$projet"
