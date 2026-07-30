#!/usr/bin/env bash
set -uo pipefail

racine="$(cd "$(dirname "${BASH_SOURCE[0]}")/.." && pwd)"
reussis=0
echoues=0
ignores=0
options_build=(--nologo --disable-build-servers -p:UseSharedCompilation=false)
if [[ "${EXEMPLES_SANS_RESTAURATION:-0}" == "1" ]]
then
    options_build+=(--no-restore)
fi

while IFS= read -r projet
do
    if rg -q '<UseWindowsForms>true</UseWindowsForms>' "$projet" && [[ "$(uname -s)" != "MINGW"* && "$(uname -s)" != "CYGWIN"* ]]
    then
        printf 'IGNORÉ (Windows) %s\n' "${projet#"$racine"/}"
        ignores=$((ignores + 1))
    elif DOTNET_CLI_DO_NOT_USE_MSBUILD_SERVER=1 DOTNET_CLI_HOME="${DOTNET_CLI_HOME:-/tmp/dotnet-home}" dotnet build "$projet" "${options_build[@]}"
    then
        printf 'RÉUSSI %s\n' "${projet#"$racine"/}"
        reussis=$((reussis + 1))
    else
        printf 'ÉCHEC %s\n' "${projet#"$racine"/}" >&2
        echoues=$((echoues + 1))
    fi
done < <(find "$racine" -type f -name '*.csproj' -not -path '*/bin/*' -not -path '*/obj/*' | LC_ALL=C sort)

printf '\nBilan : %d réussi(s), %d échec(s), %d ignoré(s).\n' "$reussis" "$echoues" "$ignores"
[[ "$echoues" -eq 0 ]]
