using System;
using Irony.Parsing;

namespace JsIronyParser
{
    // Used grammar:
    // https://people.mozilla.org/~jorendorff/es6-draft.html#sec-grammar-summary

    [Language("JavaScript", "6", "ECMA-262 6th Edition")]
    public class JsGrammar : Grammar
    {
        public JsGrammar()
        {

#region A.1 Lexical Grammar

            #region 1. Terminals
            var SourceCharacter = new RegexBasedTerminal("SourceCharacter", @"[\s\S]");
            var MultiLineNotAsteriskChar = new RegexBasedTerminal("MultiLineNotAsteriskChar", "[^*]");
            var MultiLineNotForwardSlashOrAsteriskChar = new RegexBasedTerminal("MultiLineNotForwardSlashOrAsteriskChar", "[^/*]");
            var SingleLineCommentChar = new RegexBasedTerminal("SingleLineCommentChar", "[^\u000A\u000D\u2028\u2029]");
            var UnicodeIDStart = new RegexBasedTerminal("UnicodeIDStart", "[\u0041-\u2FA1D]");
            var UnicodeIDContinue = new RegexBasedTerminal("UnicodeIDContinue", "[\u0030-\uE01DA]");
            var DecimalDigit = new RegexBasedTerminal("DecimalDigit", "[0-9]");
            var NonZeroDigit = new RegexBasedTerminal("NonZeroDigit", "[1-9]");
            var ExponentIndicator = new RegexBasedTerminal("ExponentIndicator", "[eE]");
            var BinaryDigit = new RegexBasedTerminal("BinaryDigit", "[01]");
            var OctalDigit = new RegexBasedTerminal("OctalDigit", "[0-7]");
            var HexDigit = new RegexBasedTerminal("HexDigit", "[0-9A-F]");
            var SingleEscapeCharacter = new RegexBasedTerminal("SingleEscapeCharacter", "['\"\\bfnrtv]");
            var NonEscapeCharacter = new RegexBasedTerminal("NonEscapeCharacter", "[^'\"\\bfnrtv0-9xu\u000A\u000D\u2028\u2029]");
            var RegularExpressionNonTerminator = new RegexBasedTerminal("RegularExpressionNonTerminator", "[^\u000A\u000D\u2028\u2029]");

            var TAB = ToTerm("\u0009", "TAB");
            var VT = ToTerm("\u000B", "VT");
            var FF = ToTerm("\u000C", "FF");
            var SP = ToTerm("\u0020", "SP");
            var NBSP = ToTerm("\u00A0", "NBSP");
            var zwnbsp = ToTerm("\uFEFF", "zwnbsp");
            var USP = ToTerm("\u1680") | "\u2000" |	"\u2001" | "\u2002" | "\u2003" | "\u2004" | "\u2005" | "\u2006" | "\u2007" | "\u2008" | "\u2009" | "\u200A" | "\u202F" | "\u205F" | "\u3000";

            var LF = ToTerm("\u000A", "LF");
            var CR = ToTerm("\u000D", "CR");
            var LS = ToTerm("\u2028", "LS");
            var PS = ToTerm("\u2029", "PS");

            var ZWNJ = ToTerm("\u200C", "ZWNJ");
            var ZWJ = ToTerm("\u200D", "ZWJ");

            var NullLiteral = ToTerm("null", "NullLiteral");
            var BooleanLiteral = ToTerm("true") | "false";
            #endregion

            #region 2. Non-terminals
            var InputElementDiv = new NonTerminal("InputElementDiv");
            var InputElementRegExp = new NonTerminal("InputElementRegExp");
            var InputElementRegExpOrTemplateTail = new NonTerminal("InputElementRegExpOrTemplateTail");
            var InputElementTemplateTail = new NonTerminal("InputElementTemplateTail");
            var Comment = new NonTerminal("Comment");
            var MultiLineComment = new NonTerminal("MultiLineComment");
            var MultiLineCommentChars = new NonTerminal("MultiLineCommentChars");
            var PostAsteriskCommentChars = new NonTerminal("PostAsteriskCommentChars");
            var SingleLineComment = new NonTerminal("SingleLineComment");
            var SingleLineCommentChars = new NonTerminal("SingleLineCommentChars");
            var CommonToken = new NonTerminal("CommonToken");
            var IdentifierName = new NonTerminal("IdentifierName");
            var IdentifierStart = new NonTerminal("IdentifierStart");
            var IdentifierPart = new NonTerminal("IdentifierPart");
            var ReservedWord = new NonTerminal("ReservedWord");
            var Keyword = new NonTerminal("Keyword");
            var FutureReservedWord = new NonTerminal("FutureReservedWord");
            var Punctuator = new NonTerminal("Punctuator");
            var DivPunctuator = new NonTerminal("DivPunctuator");
            var RightBracePunctuator = new NonTerminal("RightBracePunctuator");
            var NumericLiteral = new NonTerminal("NumericLiteral");
            var DecimalLiteral = new NonTerminal("DecimalLiteral");
            var DecimalIntegerLiteral = new NonTerminal("DecimalIntegerLiteral");
            var DecimalDigits = new NonTerminal("DecimalDigits");
            var ExponentPart = new NonTerminal("ExponentPart");
            var SignedInteger = new NonTerminal("SignedInteger");
            var BinaryIntegerLiteral = new NonTerminal("BinaryIntegerLiteral");
            var BinaryDigits = new NonTerminal("BinaryDigits");
            var OctalIntegerLiteral = new NonTerminal("OctalIntegerLiteral");
            var OctalDigits = new NonTerminal("OctalDigits");
            var HexIntegerLiteral = new NonTerminal("HexIntegerLiteral");
            var HexDigits = new NonTerminal("HexDigits");
            var StringLiteral = new NonTerminal("StringLiteral");
            var DoubleStringCharacters = new NonTerminal("DoubleStringCharacters");
            var SingleStringCharacters = new NonTerminal("SingleStringCharacters");
            var DoubleStringCharacter = new NonTerminal("DoubleStringCharacter");
            var SingleStringCharacter = new NonTerminal("SingleStringCharacter");
            var LineContinuation = new NonTerminal("LineContinuation");
            var EscapeSequence = new NonTerminal("EscapeSequence");
            var CharacterEscapeSequence = new NonTerminal("CharacterEscapeSequence");
            var EscapeCharacter = new NonTerminal("EscapeCharacter");
            var HexEscapeSequence = new NonTerminal("HexEscapeSequence");
            var UnicodeEscapeSequence = new NonTerminal("UnicodeEscapeSequence");
            var Hex4Digits = new NonTerminal("Hex4Digits");
            var RegularExpressionLiteral = new NonTerminal("RegularExpressionLiteral");
            var RegularExpressionBody = new NonTerminal("RegularExpressionBody");
            var RegularExpressionChars = new NonTerminal("RegularExpressionChars");
            var RegularExpressionFirstChar = new NonTerminal("RegularExpressionFirstChar");
            var RegularExpressionChar = new NonTerminal("RegularExpressionChar");
            var RegularExpressionBackslashSequence = new NonTerminal("RegularExpressionBackslashSequence");
            var RegularExpressionClass = new NonTerminal("RegularExpressionClass");
            var RegularExpressionClassChars = new NonTerminal("RegularExpressionClassChars");
            var RegularExpressionClassChar = new NonTerminal("RegularExpressionClassChar");
            var RegularExpressionFlags = new NonTerminal("RegularExpressionFlags");
            var Template = new NonTerminal("Template");
            var NoSubstitutionTemplate = new NonTerminal("NoSubstitutionTemplate");
            var TemplateHead = new NonTerminal("TemplateHead");
            var TemplateSubstitutionTail = new NonTerminal("TemplateSubstitutionTail");
            var TemplateMiddle = new NonTerminal("TemplateMiddle");
            var TemplateTail = new NonTerminal("TemplateTail");
            var TemplateCharacters = new NonTerminal("TemplateCharacters");
            var TemplateCharacter = new NonTerminal("TemplateCharacter");


            var WhiteSpace = new NonTerminal("WhiteSpace", TAB | VT | FF | SP | NBSP | zwnbsp | USP);
            var LineTerminator = new NonTerminal("LineTerminator", LF | CR | LS | PS);
            var LineTerminatorSequence = new NonTerminal("LineTerminatorSequence", LF | CR | LS | PS | CR + LF);
            #endregion

            #region 3. BNF rules
            InputElementDiv.Rule = WhiteSpace 
                                    | LineTerminator
                                    | Comment
                                    | CommonToken
                                    | DivPunctuator
                                    | RightBracePunctuator;

            InputElementRegExp.Rule = WhiteSpace
                                        | LineTerminator
                                        | Comment
                                        | CommonToken
                                        | RightBracePunctuator
                                        | RegularExpressionLiteral;

            InputElementRegExpOrTemplateTail.Rule = WhiteSpace
                                                    | LineTerminator
                                                    | Comment
                                                    | CommonToken
                                                    | RegularExpressionLiteral
                                                    | TemplateSubstitutionTail;

            InputElementTemplateTail.Rule = WhiteSpace
                                            | LineTerminator
                                            | Comment
                                            | CommonToken
                                            | DivPunctuator
                                            | TemplateSubstitutionTail;

            Comment.Rule = MultiLineComment
                            | SingleLineComment;

            MultiLineComment.Rule = ToTerm("/*") + MultiLineCommentChars.Q() + "*/";

            MultiLineCommentChars.Rule = MultiLineNotAsteriskChar + MultiLineCommentChars.Q()
                                        | ToTerm("*") + PostAsteriskCommentChars.Q();

            PostAsteriskCommentChars.Rule = MultiLineNotForwardSlashOrAsteriskChar + MultiLineCommentChars.Q()
                                            | ToTerm("*") + PostAsteriskCommentChars.Q();

            SingleLineComment.Rule = ToTerm("//") + SingleLineCommentChars.Q();

            SingleLineCommentChars.Rule = SingleLineCommentChar + SingleLineCommentChars.Q();

            CommonToken.Rule = IdentifierName
                                | Punctuator
                                | NumericLiteral
                                | StringLiteral
                                | Template;

            IdentifierName.Rule = IdentifierStart
                                    | IdentifierName + IdentifierPart;

            IdentifierStart.Rule = UnicodeIDStart
                                    | ToTerm("$")
                                    | ToTerm("_")
                                    | ToTerm("\\") + UnicodeEscapeSequence;

            IdentifierPart.Rule = UnicodeIDContinue
                                    | ToTerm("$")
                                    | ToTerm("_")
                                    | ToTerm("\\") + UnicodeEscapeSequence
                                    | ZWNJ
                                    | ZWJ;

            ReservedWord.Rule = Keyword
                                | FutureReservedWord
                                | NullLiteral
                                | BooleanLiteral;

            Keyword.Rule = ToTerm("break") | "do" | "in" | "typeof" | "case"
                          | "else" | "instanceof" | "var" | "catch"
                          | "export" | "new" | "void" | "class" | "extends"
                          | "return" | "while" | "const" | "finally" | "super"
                          | "with" | "continue" | "for" | "switch" | "yield"
                          | "debugger" | "function" | "this" | "default" | "if"
                          | "throw" | "delete" | "import" | "try";

            FutureReservedWord.Rule = ToTerm("enum") | "await" | "implements"
                                                     | "package" | "protected" | "interface"
                                                     | "private" | "public";

            Punctuator.Rule = ToTerm("{") | "}" | "(" | ")" | "[" | "]"
                                | "." | ";" | "," | "<" | ">" | "<="
                                | ">=" | "==" | "!=" | "===" | "!==	"
                                | "+" | "-" | "*" | "%" | "++" | "--"
                                | "<<" | ">>" | ">>>" | "&" | "|" | "^"
                                | "!" | "~" | "&&" | "||" | "?" | ":"
                                | "=" | "+=" | "-=" | "*=" | "%=" | "<<="
                                | ">>=" | ">>>=" | "&=" | "|=" | "^=" | "=>";

            DivPunctuator.Rule = ToTerm("/") | "/=";

            RightBracePunctuator.Rule = ToTerm("}");

            NumericLiteral.Rule = DecimalLiteral
                                  |  BinaryIntegerLiteral
                                  |  OctalIntegerLiteral
                                  |  HexIntegerLiteral;

            DecimalLiteral.Rule = DecimalIntegerLiteral + "." + DecimalDigits.Q() + ExponentPart.Q()
                                    | ToTerm(".") + DecimalDigits + ExponentPart.Q()
                                    | DecimalIntegerLiteral + ExponentPart.Q();

            DecimalIntegerLiteral.Rule = ToTerm("0")
                                         | NonZeroDigit + DecimalDigits.Q();

            DecimalDigits.Rule = DecimalDigit
                                 | DecimalDigits + DecimalDigit;

            ExponentPart.Rule = ExponentIndicator + SignedInteger;

            SignedInteger.Rule = DecimalDigits
                                 | ToTerm("+") + DecimalDigits
                                 | ToTerm("-") + DecimalDigits;

            BinaryIntegerLiteral.Rule = ToTerm("0b") + BinaryDigits
                                        | ToTerm("0B") + BinaryDigits;

            BinaryDigits.Rule = BinaryDigit
                                | BinaryDigits + BinaryDigit;

            OctalIntegerLiteral.Rule = ToTerm("0o") + OctalDigits
                                       | ToTerm("0O") + OctalDigits;

            OctalDigits.Rule = OctalDigit
                               | OctalDigits + OctalDigit;

            HexIntegerLiteral.Rule = ToTerm("0x") + HexDigits
                                     | ToTerm("0X") + HexDigits;

            HexDigits.Rule = HexDigit
                             | HexDigits + HexDigit;

            StringLiteral.Rule = ToTerm("\"") + DoubleStringCharacters.Q() + "\""
                                 | ToTerm("'") + SingleStringCharacters.Q() + "'";

            DoubleStringCharacters.Rule = DoubleStringCharacter + DoubleStringCharacters.Q();

            SingleStringCharacters.Rule = SingleStringCharacter + SingleStringCharacters.Q();

            DoubleStringCharacter.Rule = new RegexBasedTerminal("[^\"\\\u000A\u000D\u2028\u2029]")
                                            | ToTerm("\\") + EscapeSequence
                                            | LineContinuation;

            SingleStringCharacter.Rule = new RegexBasedTerminal("[^'\\\u000A\u000D\u2028\u2029]")
                                            | ToTerm("\\") + EscapeSequence
                                            | LineContinuation;

            LineContinuation.Rule = ToTerm("\\") + LineTerminatorSequence;

            EscapeSequence.Rule = CharacterEscapeSequence
                                  | ToTerm("0")
                                  | HexEscapeSequence
                                  | UnicodeEscapeSequence;

            CharacterEscapeSequence.Rule = SingleEscapeCharacter
                                           | NonEscapeCharacter;

            EscapeCharacter.Rule = SingleEscapeCharacter
                                   | DecimalDigit
                                   | ToTerm("x")
                                   | ToTerm("u");

            HexEscapeSequence.Rule = ToTerm("x") + HexDigit + HexDigit;

            UnicodeEscapeSequence.Rule = ToTerm("u") + Hex4Digits
                                         | ToTerm("u") + "{" + HexDigits + "}";

            Hex4Digits.Rule = HexDigit + HexDigit + HexDigit + HexDigit;

            RegularExpressionLiteral.Rule = ToTerm("/") + RegularExpressionBody + "/" + RegularExpressionFlags;

            RegularExpressionBody.Rule = RegularExpressionFirstChar + RegularExpressionChars;

            RegularExpressionChars.Rule = ToTerm("(?:)")
                                          | RegularExpressionChars + RegularExpressionChar;

            RegularExpressionFirstChar.Rule = new RegexBasedTerminal("[^*\\/[\u000A\u000D\u2028\u2029]")
                                                | RegularExpressionBackslashSequence
                                                | RegularExpressionClass;

            RegularExpressionChar.Rule = new RegexBasedTerminal("[^\\/[\u000A\u000D\u2028\u2029]")
                                            | RegularExpressionBackslashSequence
                                            | RegularExpressionClass;

            RegularExpressionBackslashSequence.Rule = ToTerm("\\") + RegularExpressionNonTerminator;

            RegularExpressionClass.Rule = ToTerm("[") + RegularExpressionClassChars + "]";

            RegularExpressionClassChars.Rule = ToTerm("(?:)")
                                               | RegularExpressionClassChars + RegularExpressionClassChar;

            RegularExpressionClassChar.Rule = new RegexBasedTerminal("[^]\\\u000A\u000D\u2028\u2029]")
                                         | RegularExpressionBackslashSequence;

            RegularExpressionFlags.Rule = ToTerm("(?:)")
                                     | RegularExpressionFlags + IdentifierPart;

            Template.Rule = NoSubstitutionTemplate
                            | TemplateHead;

            NoSubstitutionTemplate.Rule = ToTerm("`") + TemplateCharacters.Q() + "`";

            TemplateHead.Rule = ToTerm("`") + TemplateCharacters.Q() + "${";

            TemplateSubstitutionTail.Rule = TemplateMiddle
                                            | TemplateTail;

            TemplateMiddle.Rule = ToTerm("}") + TemplateCharacters.Q() + "${";

            TemplateTail.Rule = ToTerm("}") + TemplateCharacters.Q() + "`";

            TemplateCharacters.Rule = TemplateCharacter + TemplateCharacters.Q();

            TemplateCharacter.Rule = ToTerm("$")
                                     | "\\" + EscapeSequence
                                     | LineContinuation
                                     | LineTerminatorSequence
                                     | new RegexBasedTerminal("[^`\\$\u000A\u000D\u2028\u2029]");
            #endregion

#endregion


        }
    }
}
