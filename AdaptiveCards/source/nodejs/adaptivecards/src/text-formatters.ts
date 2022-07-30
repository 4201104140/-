abstract class AbstractTextFormatter {
  private _regularExpression: RegExp;

  protected abstract internalFormat(lang: string | undefined, matches: RegExpExecArray): string;

  constructor(regularExpression: RegExp) {
    this._regularExpression = regularExpression;
  }

  format(lang: string | undefined, input: string | undefined): string | undefined {
    let matches;

    if (input) {
      let result = input;

      while ((matches = this._regularExpression.exec(input)) != null) {
        result = result.replace(matches[0], this.internalFormat(lang, matches));
      }

      return result;
    } else {
      return input;
    }
  }
}

