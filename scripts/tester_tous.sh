#!/usr/bin/env bash
set -uo pipefail

racine="$(cd "$(dirname "${BASH_SOURCE[0]}")/.." && pwd)"
reussis=0
echoues=0

while IFS= read -r projet
do
    if rg -q '<OutputType>Exe</OutputType>' "$projet"
    then
        commande=(dotnet run --project "$projet" --no-restore --disable-build-servers -p:UseSharedCompilation=false)
    else
        commande=(dotnet test "$projet" --nologo --disable-build-servers -p:UseSharedCompilation=false)
    fi

    if DOTNET_CLI_DO_NOT_USE_MSBUILD_SERVER=1 DOTNET_CLI_HOME="${DOTNET_CLI_HOME:-/tmp/dotnet-home}" "${commande[@]}"
    then
        printf 'RÉUSSI %s\n' "${projet#"$racine"/}"
        reussis=$((reussis + 1))
    else
        printf 'ÉCHEC %s\n' "${projet#"$racine"/}" >&2
        echoues=$((echoues + 1))
    fi
done < <(find "$racine" -type f -name '*.Tests.csproj' -not -path '*/bin/*' -not -path '*/obj/*' | LC_ALL=C sort)

printf '\nBilan : %d projet(s) de tests réussi(s), %d échec(s).\n' "$reussis" "$echoues"
[[ "$echoues" -eq 0 ]]
