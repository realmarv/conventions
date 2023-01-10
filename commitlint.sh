#!/usr/bin/env bash
set -euxo pipefail

# cd to directory of this script
cd "$(dirname "$0")"
node --version
npm --version
npx commitlint --version
npx commitlint $@
cd ..
