#!/usr/bin/env bash
set -euxo pipefail

# cd to directory of this script
cd "$(dirname "$0")"
npm install typescript -g
npm i --save-dev @types/node
npm install --save commitlint@17.4.0 @commitlint/cli@17.4.0 conventional-changelog-conventionalcommits
npm i --save-dev conventional-changelog-conventionalcommits
npm install @commitlint/config-conventional
npx commitlint --version
npx commitlint $@
cd ..
