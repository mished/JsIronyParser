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

            // 1. Terminals
            var SourceCharacter = new RegexBasedTerminal("SourceCharacter", @"[\s\S]");
            var MultiLineNotAsteriskChar = new RegexBasedTerminal("MultiLineNotAsteriskChar", "[^*]");
            var MultiLineNotForwardSlashOrAsteriskChar = new RegexBasedTerminal("MultiLineNotForwardSlashOrAsteriskChar", "[^/*]");
            var SingleLineCommentChar = new RegexBasedTerminal("SingleLineCommentChar", "[^\u000A\u000D\u2028\u2029]");
            var UnicodeIDStart = new RegexBasedTerminal("UnicodeIDStart", "[\u0041-\u2FA1D]");
            var UnicodeIDContinue = new RegexBasedTerminal("UnicodeIDContinue", "[\u0030-\uE01DA]");

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

            // 2. Non-terminals
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


            var WhiteSpace = new NonTerminal("WhiteSpace", TAB | VT | FF | SP | NBSP | zwnbsp | USP); // USP ? https://people.mozilla.org/~jorendorff/es6-draft.html#table-32
            var LineTerminator = new NonTerminal("LineTerminator", LF | CR | LS | PS);
            var LineTerminatorSequence = new NonTerminal("LineTerminatorSequence", LF | CR | LS | PS | CR + LF);

            // 3. BNF rules
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

            FutureReservedWord.Rule = ToTerm("enum") | "await";

#endregion
        }
    }
}
