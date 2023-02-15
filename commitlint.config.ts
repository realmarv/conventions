import { Helpers } from "./commitlint/helpers";
import { Plugins } from "./commitlint/plugins";
import { RuleConfigSeverity } from "@commitlint/types";

let bodyMaxLineLength = 64;
let headerMaxLineLength = 50;
let footerMaxLineLength = 150;

function notNullStringErrorMessage(stringType: string): string {
    return `This is unexpected because ${stringType} should never be null`;
}

module.exports = {
    parserPreset: "conventional-changelog-conventionalcommits",
    rules: {
        "body-leading-blank": [RuleConfigSeverity.Error, "always"],
        "body-soft-max-line-length": [
            RuleConfigSeverity.Error,
            "always",
            bodyMaxLineLength,
        ],
        "empty-wip": [RuleConfigSeverity.Error, "always"],
        "footer-leading-blank": [RuleConfigSeverity.Warning, "always"],
        "footer-max-line-length": [
            RuleConfigSeverity.Error,
            "always",
            footerMaxLineLength,
        ],
        "footer-notes-misplacement": [RuleConfigSeverity.Error, "always"],
        "footer-references-existence": [RuleConfigSeverity.Error, "always"],
        "header-max-length-with-suggestions": [
            RuleConfigSeverity.Error,
            "always",
            headerMaxLineLength,
        ],
        "subject-full-stop": [RuleConfigSeverity.Error, "never", "."],
        "type-empty": [RuleConfigSeverity.Warning, "never"],
        "type-space-after-colon": [RuleConfigSeverity.Error, "always"],
        "subject-lowercase": [RuleConfigSeverity.Error, "always"],
        "body-prose": [RuleConfigSeverity.Error, "always"],
        "type-space-after-comma": [RuleConfigSeverity.Error, "always"],
        "trailing-whitespace": [RuleConfigSeverity.Error, "always"],
        "prefer-slash-over-backslash": [RuleConfigSeverity.Error, "always"],
        "type-space-before-paren": [RuleConfigSeverity.Error, "always"],
        "type-with-square-brackets": [RuleConfigSeverity.Error, "always"],
        "proper-issue-refs": [RuleConfigSeverity.Error, "always"],
        "too-many-spaces": [RuleConfigSeverity.Error, "always"],
        "commit-hash-alone": [RuleConfigSeverity.Error, "always"],
        "title-uppercase": [RuleConfigSeverity.Error, "always"],
        "default-revert-message": [RuleConfigSeverity.Error, "never"],
    },

    // Commitlint automatically ignores some kinds of commits like Revert commit messages.
    // We need to set this value to false to apply our rules on these messages.
    defaultIgnores: false,
    plugins: [
        // TODO (ideas for more rules):
        // * Detect if paragraphs in body have been cropped too shortly (less than 64 chars), and suggest same auto-wrap command that body-soft-max-line-length suggests, since it unwraps and wraps (both).
        // * Reject some stupid obvious words: change, update, modify (if first word after colon, error; otherwise warning).
        // * Think of how to reject this shitty commit message: https://github.com/nblockchain/NOnion/pull/34/commits/9ffcb373a1147ed1c729e8aca4ffd30467255594
        // * Title should not have dot at the end.
        // * Workflow: detect if wip commit in a branch not named "wip/*" or whose name contains "squashed".
        // * Detect if commit hash mention in commit msg actually exists in repo.
        // * Detect scope(sub-scope) in the title that doesn't include scope part (e.g., writing (bar) instead of foo(bar))

        {
            rules: {
                "body-prose": ({ raw }: { raw: any }) => {
                    let rawUncastedStr = Helpers.convertAnyToString(raw, "raw");
                    let rawStr = Helpers.assertNotNull(
                        rawUncastedStr,
                        notNullStringErrorMessage("raw")
                    ).trim();

                    return Plugins.bodyProse(rawStr);
                },

                "commit-hash-alone": ({ raw }: { raw: any }) => {
                    let rawUncastedStr = Helpers.convertAnyToString(raw, "raw");
                    let rawStr = Helpers.assertNotNull(
                        rawUncastedStr,
                        notNullStringErrorMessage("raw")
                    );

                    return Plugins.commitHashAlone(rawStr);
                },

                "empty-wip": ({ header }: { header: any }) => {
                    let headerUncastedStr = Helpers.convertAnyToString(
                        header,
                        "header"
                    );
                    let headerStr = Helpers.assertNotNull(
                        headerUncastedStr,
                        notNullStringErrorMessage("header")
                    );

                    return Plugins.emptyWip(headerStr);
                },

                "header-max-length-with-suggestions": (
                    { header }: { header: any },
                    _: any,
                    maxLineLength: number
                ) => {
                    let headerUncastedStr = Helpers.convertAnyToString(
                        header,
                        "header"
                    );
                    let headerStr = Helpers.assertNotNull(
                        headerUncastedStr,
                        notNullStringErrorMessage("header")
                    );

                    return Plugins.headerMaxLengthWithSuggestions(
                        headerStr,
                        maxLineLength
                    );
                },

                "footer-notes-misplacement": ({ body }: { body: any }) => {
                    let bodyStr = Helpers.convertAnyToString(body, "body");
                    return Plugins.footerNotesMisplacement(bodyStr);
                },

                "footer-references-existence": ({ body }: { body: any }) => {
                    let bodyStr = Helpers.convertAnyToString(body, "body");

                    return Plugins.footerReferencesExistence(bodyStr);
                },

                "prefer-slash-over-backslash": ({
                    header,
                }: {
                    header: any;
                }) => {
                    let headerUncastedStr = Helpers.convertAnyToString(
                        header,
                        "header"
                    );
                    let headerStr = Helpers.assertNotNull(
                        headerUncastedStr,
                        notNullStringErrorMessage("header")
                    );

                    return Plugins.preferSlashOverBackslash(headerStr);
                },

                "proper-issue-refs": ({ raw }: { raw: any }) => {
                    let rawUncastedStr = Helpers.convertAnyToString(raw, "raw");
                    let rawStr = Helpers.assertNotNull(
                        rawUncastedStr,
                        notNullStringErrorMessage("raw")
                    ).trim();

                    return Plugins.properIssueRefs(rawStr);
                },

                "default-revert-message": (
                    {
                        header,
                        body,
                    }: {
                        header: any;
                        body: any;
                    },
                    when: any
                ) => {
                    let bodyStr = Helpers.convertAnyToString(body, "body");
                    let headerUncastedStr = Helpers.convertAnyToString(
                        header,
                        "header"
                    );
                    let headerStr = Helpers.assertNotNull(
                        headerUncastedStr,
                        notNullStringErrorMessage("header")
                    );
                    console.log("when:" + when);
                    return Plugins.defaultRevertMessage(headerStr, bodyStr);
                },

                "title-uppercase": ({ header }: { header: any }) => {
                    let headerUncastedStr = Helpers.convertAnyToString(
                        header,
                        "header"
                    );
                    let headerStr = Helpers.assertNotNull(
                        headerUncastedStr,
                        notNullStringErrorMessage("header")
                    );

                    return Plugins.titleUppercase(headerStr);
                },

                "too-many-spaces": ({ raw }: { raw: any }) => {
                    let rawUncastedStr = Helpers.convertAnyToString(raw, "raw");
                    let rawStr = Helpers.assertNotNull(
                        rawUncastedStr,
                        notNullStringErrorMessage("raw")
                    );

                    return Plugins.tooManySpaces(rawStr);
                },

                "type-space-after-colon": ({ header }: { header: any }) => {
                    let headerUncastedStr = Helpers.convertAnyToString(
                        header,
                        "header"
                    );
                    let headerStr = Helpers.assertNotNull(
                        headerUncastedStr,
                        notNullStringErrorMessage("header")
                    );

                    return Plugins.typeSpaceAfterColon(headerStr);
                },

                "type-with-square-brackets": ({ header }: { header: any }) => {
                    let headerUncastedStr = Helpers.convertAnyToString(
                        header,
                        "header"
                    );
                    let headerStr = Helpers.assertNotNull(
                        headerUncastedStr,
                        notNullStringErrorMessage("header")
                    );

                    return Plugins.typeWithSquareBrackets(headerStr);
                },

                // NOTE: we use 'header' instead of 'subject' as a workaround to this bug: https://github.com/conventional-changelog/commitlint/issues/3404
                "subject-lowercase": ({ header }: { header: any }) => {
                    let headerUncastedStr = Helpers.convertAnyToString(
                        header,
                        "header"
                    );
                    let headerStr = Helpers.assertNotNull(
                        headerUncastedStr,
                        notNullStringErrorMessage("header")
                    );
                    return Plugins.subjectLowercase(headerStr);
                },

                "type-space-after-comma": ({ header }: { header: any }) => {
                    let headerUncastedStr = Helpers.convertAnyToString(
                        header,
                        "header"
                    );
                    let headerStr = Helpers.assertNotNull(
                        headerUncastedStr,
                        notNullStringErrorMessage("header")
                    );

                    return Plugins.typeSpaceAfterComma(headerStr);
                },

                "body-soft-max-line-length": (
                    { body }: { body: any },
                    _: any,
                    maxLineLength: number
                ) => {
                    let bodyStr = Helpers.convertAnyToString(body, "body");
                    return Plugins.bodySoftMaxLineLength(
                        bodyStr,
                        maxLineLength
                    );
                },

                "trailing-whitespace": ({ raw }: { raw: any }) => {
                    let rawUncastedStr = Helpers.convertAnyToString(raw, "raw");
                    let rawStr = Helpers.assertNotNull(
                        rawUncastedStr,
                        notNullStringErrorMessage("raw")
                    );

                    return Plugins.trailingWhitespace(rawStr);
                },

                "type-space-before-paren": ({ header }: { header: any }) => {
                    let headerUncastedStr = Helpers.convertAnyToString(
                        header,
                        "header"
                    );
                    let headerStr = Helpers.assertNotNull(
                        headerUncastedStr,
                        notNullStringErrorMessage("header")
                    );

                    return Plugins.typeSpaceBeforeParen(headerStr);
                },
            },
        },
    ],
};
