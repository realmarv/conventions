#!/usr/bin/env bash
set -euxo pipefail

# cd to directory of this script
cd "$(dirname "$0")"
npm install conventional-changelog-conventionalcommits
npm install commitlint@17.4.2
npx commitlint --version
npx commitlint $@
cd ..
