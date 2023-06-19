const { spawnSync } = require("child_process");

function runCommitLintOnMsg(inputMsg: string) {
    return spawnSync("npx", ["commitlint", "--verbose"], { input: inputMsg });
}

test("proper-issue-refs4", () => {
    let commitMsgWithFullUrl =
        "foo: blah blah" + "\n\n" + "It turns out that robocopy might fail when copying timestamp\nattributes because exFat[1]'s spec says times need to be later\nthan 1980 [2].\n\n[1] https://superuser.com/a/1447347/600757\n[2] https://learn.microsoft.com/en-us/windows/win32/fileio/exfat-specification#7486-year-field";
    let properIssueRefs4 = runCommitLintOnMsg(commitMsgWithFullUrl);
    console.log("=output=>:" + properIssueRefs4.stdout);
    expect(properIssueRefs4.status).toBe(0);
});
