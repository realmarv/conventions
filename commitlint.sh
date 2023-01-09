#!/usr/bin/env bash
set -euxo pipefail

# cd to directory of this script
cd "$(dirname "$0")"
npm install git+https://github.com/realmarv/commitlint.git
npx commitlint --version
npx commitlint $@
cd ..
