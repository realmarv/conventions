import { Helpers } from "./commitlint/helpers";
import { Plugins } from "./commitlint/plugins";
import { RuleConfigSeverity } from "@commitlint/types";

let bodyMaxLineLength = 64;
let headerMaxLineLength = 50;
let footerMaxLineLength = 150;

module.exports = {
    parserPreset: "conventional-changelog-conventionalcommits",
    rules: {
        "body-leading-blank": [RuleConfigSeverity.Warning, "always"],
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

        {
            rules: {
                "body-prose": ({ raw }: { raw: any }) => {
                    let rawUncastedStr = Helpers.convertAnyToString(raw, "raw");
                    Helpers.assertRaw(rawUncastedStr);
                    let rawStr = (rawUncastedStr as string).trim();

                    return Plugins.bodyProse(rawStr);
                },

                "commit-hash-alone": ({ raw }: { raw: any }) => {
                    let rawUncastedStr = Helpers.convertAnyToString(raw, "raw");
                    Helpers.assertRaw(rawUncastedStr);
                    let rawStr = (rawUncastedStr as string).trim();

                    return Plugins.commitHashAlone(rawStr);
                },

                "empty-wip": ({ header }: { header: any }) => {
                    let headerUncastedStr = Helpers.convertAnyToString(
                        header,
                        "header"
                    );
                    Helpers.assertHeader(headerUncastedStr);
                    let headerStr = headerUncastedStr as string;
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
                    Helpers.assertHeader(headerUncastedStr);
                    let headerStr = headerUncastedStr as string;
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
                    Helpers.assertHeader(headerUncastedStr);
                    let headerStr = headerUncastedStr as string;
                    return Plugins.preferSlashOverBackslash(headerStr);
                },

                "proper-issue-refs": ({ raw }: { raw: any }) => {
                    let rawUncastedStr = Helpers.convertAnyToString(raw, "raw");
                    Helpers.assertRaw(rawUncastedStr);
                    let rawStr = (rawUncastedStr as string).trim();

                    return Plugins.properIssueRefs(rawStr);
                },

                "title-uppercase": ({ header }: { header: any }) => {
                    let headerUncastedStr = Helpers.convertAnyToString(
                        header,
                        "header"
                    );
                    Helpers.assertHeader(headerUncastedStr);
                    let headerStr = headerUncastedStr as string;

                    return Plugins.titleUppercase(headerStr);
                },

                "too-many-spaces": ({ raw }: { raw: any }) => {
                    let rawUncastedStr = Helpers.convertAnyToString(raw, "raw");
                    Helpers.assertRaw(rawUncastedStr);
                    let rawStr = (rawUncastedStr as string).trim();

                    return Plugins.tooManySpaces(rawStr);
                },

                "type-space-after-colon": ({ header }: { header: any }) => {
                    let headerUncastedStr = Helpers.convertAnyToString(
                        header,
                        "header"
                    );
                    Helpers.assertHeader(headerUncastedStr);
                    let headerStr = headerUncastedStr as string;
                    return Plugins.typeSpaceAfterColon(headerStr);
                },

                "type-with-square-brackets": ({ header }: { header: any }) => {
                    let headerUncastedStr = Helpers.convertAnyToString(
                        header,
                        "header"
                    );
                    Helpers.assertHeader(headerUncastedStr);
                    let headerStr = headerUncastedStr as string;
                    return Plugins.typeWithSquareBrackets(headerStr);
                },

                // NOTE: we use 'header' instead of 'subject' as a workaround to this bug: https://github.com/conventional-changelog/commitlint/issues/3404
                "subject-lowercase": ({ header }: { header: any }) => {
                    let headerUncastedStr = Helpers.convertAnyToString(
                        header,
                        "header"
                    );
                    Helpers.assertHeader(headerUncastedStr);
                    let headerStr = headerUncastedStr as string;
                    return Plugins.subjectLowercase(headerStr);
                },

                "type-space-after-comma": ({ header }: { header: any }) => {
                    let headerUncastedStr = Helpers.convertAnyToString(
                        header,
                        "header"
                    );
                    Helpers.assertHeader(headerUncastedStr);
                    let headerStr = headerUncastedStr as string;
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
                    Helpers.assertRaw(rawUncastedStr);
                    let rawStr = (rawUncastedStr as string)

                    return Plugins.trailingWhitespace(rawStr);
                },

                "type-space-before-paren": ({ header }: { header: any }) => {
                    let headerUncastedStr = Helpers.convertAnyToString(
                        header,
                        "header"
                    );
                    Helpers.assertHeader(headerUncastedStr);
                    let headerStr = headerUncastedStr as string;
                    return Plugins.typeSpaceBeforeParen(headerStr);
                },
            },
        },
    ],
};
