export abstract class Helpers {

    public static errMessageSuffix = "\nFor reference, these are the guidelines that include our commit message conventions: https://github.com/nblockchain/conventions/blob/master/WorkflowGuidelines.md";

    public static isValidUrl(text: string) {
        if (text.indexOf(" ") >= 0) {
            return false;
        }

        // Borrowed from https://www.freecodecamp.org/news/check-if-a-javascript-string-is-a-url/
        try {
            return Boolean(new URL(text));
        }
        catch (e) {
            return false;
        }
    }

    // to convert from 'any' type
    public static convertAnyToString(potentialString: any, paramName: string): string {
        if (potentialString === null || potentialString === undefined) {
            // otherwise, String(null) might give us the stupid string "null"
            throw new Error('Unexpected ' + paramName + '===null or ' + paramName + '===undefined happened');
        }
        return String(potentialString);
    }

    public static assertCharacter(letter: string) {
        if (letter.length !== 1) {
            throw Error('This function expects a character as input')
        }
    }
    
    public static assertLine(line: string) {
        if (line.includes('\n')) {
            throw Error('This function expects a line as input')
        }
    }
    
    public static assertWord(word: string) {
        if (word.includes('\n') || word.includes(' ')) {
            throw Error("This function expects a word as input.\n" +
                        "A word doesn't include line breaks and whitespaces.")
        }
    }

    public static assertUrl(url: string) {
        if (!Helpers.isValidUrl(url)) {
            throw Error('This function expects a url as input')
        }
    }

    public static findUrls(text: string) {
        var urlRegex = /(https?:\/\/[^\s]+)/g;
        return text.match(urlRegex);
    }

    public static isCommitUrl(url: string) {
        Helpers.assertUrl(url);
        return url.includes('/commit/');
    }

    public static isBigBlock(line: string) {
        Helpers.assertLine(line);
        let bigBlockDelimiter = "```";
        return (line.length == bigBlockDelimiter.length) && (line.indexOf("```") == 0);
    }

}


