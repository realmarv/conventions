
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

module.exports = {
    parserPreset: 'conventional-changelog-conventionalcommits',
    rules: {
        'empty-wip': [RuleStatus.Error, 'always'],
    },
    plugins: [
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
