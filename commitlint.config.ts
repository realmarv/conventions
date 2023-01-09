
// to convert from 'any' type
function convertAnyToString(potentialString: any, paramName: string): string {
    if (potentialString === null || potentialString === undefined) {
        // otherwise, String(null) might give us the stupid string "null"
        throw new Error('Unexpected ' + paramName + '===null or ' + paramName + '===undefined happened');
    }
    return String(potentialString);
}

enum RuleStatus {
    Disabled = 0,
    Warning = 1,
    Error = 2,
}

let bodyMaxLineLength = 64;
let headerMaxLineLength = 50;

function isValidUrl(text: string) {
    if (text.indexOf(" ") >= 0) {
        return false;
    }

    // Borrowed from https://www.freecodecamp.org/news/check-if-a-javascript-string-is-a-url/
    try { 
        return Boolean(new URL(text));
    }
    catch(e){ 
        return false; 
    }
}

function assertUrl(url: string) {
    if (!isValidUrl(url)) {
        throw Error('This function expects a url as input')   
    }
}

function assertCharacter(letter: string) {
    if (letter.length !== 1) {
        throw Error('This function expects a character as input')
    }
}

function assertLine(line: string) {
    if (line.includes('\n')) {
        throw Error('This function expects a line as input')
    }
}

function assertWord(word: string) {
    if (word.includes('\n') || word.includes(' ')) {
        throw Error("This function expects a word as input.\n" +
                    "A word doesn't include line breaks and whitespaces.")
    }
}

function isBigBlock(line: string) {
    assertLine(line);
    let bigBlockDelimiter = "```";
    return (line.length == bigBlockDelimiter.length) && (line.indexOf("```") == 0);
}

function isUpperCase(letter: string) {
    assertCharacter(letter);
    let isUpperCase = letter.toUpperCase() == letter;
    let isLowerCase = letter.toLowerCase() == letter;

    return (isUpperCase && !isLowerCase);
}

function isLowerCase(letter: string) {
    assertCharacter(letter);
    let isUpperCase = letter.toUpperCase() == letter;
    let isLowerCase = letter.toLowerCase() == letter;

    return (isLowerCase && !isUpperCase);
}

function isFooterReference(line: string) {
    assertLine(line);
    return (line[0] === "[" && line.indexOf("] ") > 0);
}

function isFixesSentence(line: string) {
    assertLine(line);
    return (line.indexOf("Fixes ") == 0);
}

function isCoAuthoredByTag(line: string) {
    assertLine(line);
    return (line.indexOf("Co-authored-by: ") == 0);
}

function isFooterNote(line: string): boolean {
    assertLine(line);
    return isFooterReference(line) ||
        isCoAuthoredByTag(line) ||
        isFixesSentence(line);
}

function numUpperCaseLetters(word: string) {
    assertWord(word)
    return word.length - word.replace(/[A-Z]/g, '').length;
}

function numNonAlphabeticalCharacters(word: string) {
    assertWord(word)
    return word.length - word.replace(/[^a-zA-Z]/g, '').length;
}

function isProperNoun(word: string) {
    assertWord(word)
    let numUpperCase = numUpperCaseLetters(word)
    let numNonAlphabeticalChars = numNonAlphabeticalCharacters(word)

    return (numNonAlphabeticalChars > 0) ||
            (isUpperCase(word[0]) && (numUpperCase > 1)) ||
            (isLowerCase(word[0]) && (numUpperCase > 0))
}

function wordIsStartOfSentence(word: string) {
    assertWord(word);
    if (isUpperCase(word[0])) {
        let numUpperCase = numUpperCaseLetters(word)
        let numNonAlphabeticalChars = numNonAlphabeticalCharacters(word)
        return numUpperCase == 1 && numNonAlphabeticalChars == 0;
    }
    return false;
}

function includesHashtagRef(text: string) {
    return text.match(`#[0-9]+`) !== null;
}

function removeAllCodeBlocks(text: string) {
    return text.replace(/```[^]*```/g, '');
}

function findUrls(text: string) {
    var urlRegex = /(https?:\/\/[^\s]+)/g;
    return text.match(urlRegex);
}

function isCommitUrl(url: string) {
    assertUrl(url)
    return url.includes('/commit/');
}

module.exports = {
    parserPreset: 'conventional-changelog-conventionalcommits',
    rules: {
        'body-leading-blank': [RuleStatus.Warning, 'always'],
        'body-soft-max-line-length': [RuleStatus.Error, 'always'],
        'empty-wip': [RuleStatus.Error, 'always'],
        'footer-leading-blank': [RuleStatus.Warning, 'always'],
        'footer-max-line-length': [RuleStatus.Error, 'always', 150],
        'footer-notes-misplacement': [RuleStatus.Error, 'always'],
        'footer-references-existence': [RuleStatus.Error, 'always'],
        'header-max-length-with-suggestions': [RuleStatus.Error, 'always', headerMaxLineLength],
        'subject-full-stop': [RuleStatus.Error, 'never', '.'],
        'type-empty': [RuleStatus.Warning, 'never'],
        'type-space-after-colon': [RuleStatus.Error, 'always'],
        'subject-lowercase': [RuleStatus.Error, 'always'],
        'body-prose': [RuleStatus.Error, 'always'],
        'type-space-after-comma': [RuleStatus.Error, 'always'],
        'trailing-whitespace': [RuleStatus.Error, 'always'],
        'prefer-slash-over-backslash': [RuleStatus.Error, 'always'],
        'type-space-before-paren': [RuleStatus.Error, 'always'],
        'type-with-square-brackets': [RuleStatus.Error, 'always'],
        'proper-issue-refs': [RuleStatus.Error, 'always'],
        'too-many-spaces': [RuleStatus.Error, 'always'],
        'commit-hash-alone': [RuleStatus.Error, 'always'],
        'title-uppercase': [RuleStatus.Error, 'always'],
    },
    plugins: [
        // TODO (ideas for more rules):
        // * Detect if paragraphs in body have been cropped too shortly (less than 64 chars), and suggest same auto-wrap command that body-soft-max-line-length suggests, since it unwraps and wraps (both).
        // * Detect reverts which have not been elaborated.
        // * Reject some stupid obvious words: change, update, modify (if first word after colon, error; otherwise warning).
        // * Think of how to reject this shitty commit message: https://github.com/nblockchain/NOnion/pull/34/commits/9ffcb373a1147ed1c729e8aca4ffd30467255594
        // * Title should not have dot at the end.
        // * Second line of commit msg should always be blank.
        // * Workflow: detect if wip commit in a branch not named "wip/*" or whose name contains "squashed".
        // * Detect if commit hash mention in commit msg actually exists in repo.
        // * Detect area(sub-area) in the title that doesn't include area part (e.g., writing (bar) instead of foo(bar))
        // * Fix false positive raised by body-prose: "title\n\nParagraph begin. (Some text inside parens.)"

        {
            rules: {
                'empty-wip': ({header}: {header:any}) => {
                    let headerStr = convertAnyToString(header, "header");
                    let offence = headerStr.toLowerCase() === "wip";
                    return [
                        !offence,
                        `Please add a number or description after the WIP prefix`
                    ];
                },
            }
        }
    ]
};
