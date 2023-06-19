var conventionalCommitsParser = require('conventional-commits-parser');

const defaultChangelogOpts = require('conventional-changelog-angular');

console.log(conventionalCommitsParser(defaultChangelogOpts))