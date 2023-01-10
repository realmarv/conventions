#!/usr/bin/env bash
set -euxo pipefail

# cd to directory of this script
cd "$(dirname "$0")"
npm install npm@8.15.0
npm install --save commitlint@17.4.0 @commitlint/cli@17.4.0 conventional-changelog-conventionalcommits
npm i --save-dev conventional-changelog-conventionalcommits
npm install @commitlint/config-conventional@17.1.0
node --version
npm --version
npx commitlint --version
npx commitlint $@
cd ..
