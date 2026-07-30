#!/usr/bin/env bash
set -euo pipefail
script="$(cd "$(dirname "${BASH_SOURCE[0]}")" && pwd)"
python3 "$script/catalogue.py" verifier
