import { Helpers } from "./helpers";

export abstract class Plugins {
    public static commitHashAlone(rawStr: string) {
        let offence = false;

        let urls = Helpers.findUrls(rawStr)

        let gitRepo = process.env['GITHUB_REPOSITORY'];
        if (gitRepo !== undefined && urls !== null) {
            for (let url of urls.entries()) {
                let urlStr = url[1].toString()
                if (Helpers.isCommitUrl(urlStr) && urlStr.includes(gitRepo)) {
                    offence = true;
                    break;
                }
            }
        }

        return [
            !offence,
            `Please use the commit hash instead of the commit full URL.`
                + Helpers.errMessageSuffix
        ];
    }

    public static bodyProse(rawStr: string) {
        let offence = false;
        
        let lineBreakIndex = rawStr.indexOf('\n');

        if (lineBreakIndex >= 0){
            // Extracting bodyStr from rawStr rather than using body directly is a
            // workaround for https://github.com/conventional-changelog/commitlint/issues/3412
            let bodyStr = rawStr.substring(lineBreakIndex);

            bodyStr = Helpers.removeAllCodeBlocks(bodyStr).trim();
            
            if (bodyStr !== ''){

                function paragraphHasValidEnding(paragraph: string): boolean {

                    let paragraphWords = paragraph.split(' ');
                    let lastWordInParagraph = paragraphWords[paragraphWords.length - 1];
                    let isParagraphEndingWithUrl = Helpers.isValidUrl(lastWordInParagraph);
                    if (isParagraphEndingWithUrl){
                        return true
                    }

                    let endingChar = paragraph[paragraph.length - 1];
                    if (endingChar === '.' ||
                        endingChar === ':' ||
                        endingChar === '!' ||
                        endingChar === '?') {
                        return true;
                    }
                    if (endingChar === ')' && paragraph.length > 1 &&
                        paragraphHasValidEnding(paragraph[paragraph.length - 2])) {
                        return true;
                    }
                    return false;
                }

                for (let paragraph of bodyStr.split('\n\n')){
                    
                    paragraph = paragraph.trim()

                    if (paragraph === ''){
                        continue
                    }

                    let startWithLowerCase = Helpers.isLowerCase(paragraph[0]);

                    let validParagraphEnd = paragraphHasValidEnding(paragraph);

                    let lines = paragraph.split(/\r?\n/);

                    if (startWithLowerCase) {
                        if (!(lines.length == 1 && Helpers.isValidUrl(lines[0]))) {
                            offence = true;
                        }
                    }

                    if (!validParagraphEnd &&
                        !Helpers.isValidUrl(lines[lines.length - 1]) &&
                        !Helpers.isFooterNote(lines[lines.length - 1])) {

                        offence = true;
                    }
                }
                                
            }
        }

        return [
            !offence,
            `Please begin a paragraph with uppercase letter and end it with a dot.`
                + Helpers.errMessageSuffix
        ];
    }
}
