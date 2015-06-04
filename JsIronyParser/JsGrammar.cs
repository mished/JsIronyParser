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
            // UnicodeIDStart: https://people.mozilla.org/~jorendorff/es6-draft.html#sec-names-and-keywords
            var UnicodeIDStart = new RegexBasedTerminal("UnicodeIDStart", "[\u0041-\u005A\u0061-\u007A\u00AA\u00B5\u00BA\u00C0-\u00D6\u00D8-\u00F6\u00F8-\u021F\u0222-\u0233\u0250-\u02AD\u02B0-\u02B8\u02BB-\u02C1\u02D0-\u02D1\u02E0-\u02E4\u02EE\u037A\u0386\u0388-\u038A\u038C\u038E-\u03A1\u03A3-\u03CE\u03D0-\u03D7\u03DA-\u03F3\u0400-\u0481\u048C-\u04C4\u04C7-\u04C8\u04CB-\u04CC\u04D0-\u04F5\u04F8-\u04F9\u0531-\u0556\u0559\u0561-\u0587\u05D0-\u05EA\u05F0-\u05F2\u0621-\u063A\u0640-\u064A\u0671-\u06D3\u06D5\u06E5-\u06E6\u06FA-\u06FC\u0710\u0712-\u072C\u0780-\u07A5\u0905-\u0939\u093D\u0950\u0958-\u0961\u0985-\u098C\u098F-\u0990\u0993-\u09A8\u09AA-\u09B0\u09B2\u09B6-\u09B9\u09DC-\u09DD\u09DF-\u09E1\u09F0-\u09F1\u0A05-\u0A0A\u0A0F-\u0A10\u0A13-\u0A28\u0A2A-\u0A30\u0A32-\u0A33\u0A35-\u0A36\u0A38-\u0A39\u0A59-\u0A5C\u0A5E\u0A72-\u0A74\u0A85-\u0A8B\u0A8D\u0A8F-\u0A91\u0A93-\u0AA8\u0AAA-\u0AB0\u0AB2-\u0AB3\u0AB5-\u0AB9\u0ABD\u0AD0\u0AE0\u0B05-\u0B0C\u0B0F-\u0B10\u0B13-\u0B28\u0B2A-\u0B30\u0B32-\u0B33\u0B36-\u0B39\u0B3D\u0B5C-\u0B5D\u0B5F-\u0B61\u0B85-\u0B8A\u0B8E-\u0B90\u0B92-\u0B95\u0B99-\u0B9A\u0B9C\u0B9E-\u0B9F\u0BA3-\u0BA4\u0BA8-\u0BAA\u0BAE-\u0BB5\u0BB7-\u0BB9\u0C05-\u0C0C\u0C0E-\u0C10\u0C12-\u0C28\u0C2A-\u0C33\u0C35-\u0C39\u0C60-\u0C61\u0C85-\u0C8C\u0C8E-\u0C90\u0C92-\u0CA8\u0CAA-\u0CB3\u0CB5-\u0CB9\u0CDE\u0CE0-\u0CE1\u0D05-\u0D0C\u0D0E-\u0D10\u0D12-\u0D28\u0D2A-\u0D39\u0D60-\u0D61\u0D85-\u0D96\u0D9A-\u0DB1\u0DB3-\u0DBB\u0DBD\u0DC0-\u0DC6\u0E01-\u0E30\u0E32-\u0E33\u0E40-\u0E46\u0E81-\u0E82\u0E84\u0E87-\u0E88\u0E8A\u0E8D\u0E94-\u0E97\u0E99-\u0E9F\u0EA1-\u0EA3\u0EA5\u0EA7\u0EAA-\u0EAB\u0EAD-\u0EB0\u0EB2-\u0EB3\u0EBD-\u0EC4\u0EC6\u0EDC-\u0EDD\u0F00\u0F40-\u0F6A\u0F88-\u0F8B\u1000-\u1021\u1023-\u1027\u1029-\u102A\u1050-\u1055\u10A0-\u10C5\u10D0-\u10F6\u1100-\u1159\u115F-\u11A2\u11A8-\u11F9\u1200-\u1206\u1208-\u1246\u1248\u124A-\u124D\u1250-\u1256\u1258\u125A-\u125D\u1260-\u1286\u1288\u128A-\u128D\u1290-\u12AE\u12B0\u12B2-\u12B5\u12B8-\u12BE\u12C0\u12C2-\u12C5\u12C8-\u12CE\u12D0-\u12D6\u12D8-\u12EE\u12F0-\u130E\u1310\u1312-\u1315\u1318-\u131E\u1320-\u1346\u1348-\u135A\u13A0-\u13B0\u13B1-\u13F4\u1401-\u1676\u1681-\u169A\u16A0-\u16EA\u1780-\u17B3\u1820-\u1877\u1880-\u18A8\u1E00-\u1E9B\u1EA0-\u1EE0\u1EE1-\u1EF9\u1F00-\u1F15\u1F18-\u1F1D\u1F20-\u1F39\u1F3A-\u1F45\u1F48-\u1F4D\u1F50-\u1F57\u1F59\u1F5B\u1F5D\u1F5F-\u1F7D\u1F80-\u1FB4\u1FB6-\u1FBC\u1FBE\u1FC2-\u1FC4\u1FC6-\u1FCC\u1FD0-\u1FD3\u1FD6-\u1FDB\u1FE0-\u1FEC\u1FF2-\u1FF4\u1FF6-\u1FFC\u207F\u2102\u2107\u210A-\u2113\u2115\u2119-\u211D\u2124\u2126\u2128\u212A-\u212D\u212F-\u2131\u2133-\u2139\u2160-\u2183\u3005-\u3007\u3021-\u3029\u3031-\u3035\u3038-\u303A\u3041-\u3094\u309D-\u309E\u30A1-\u30FA\u30FC-\u30FE\u3105-\u312C\u3131-\u318E\u31A0-\u31B7\u3400\u4DB5\u4E00\u9FA5\uA000-\uA48C\uAC00\uD7A3\uF900-\uFA2D\uFB00-\uFB06\uFB13-\uFB17\uFB1D\uFB1F-\uFB28\uFB2A-\uFB36\uFB38-\uFB3C\uFB3E\uFB40-\uFB41\uFB43-\uFB44\uFB46-\uFBB1\uFBD3-\uFD3D\uFD50-\uFD8F\uFD92-\uFDC7\uFDF0-\uFDFB\uFE70-\uFE72\uFE74\uFE76-\uFEFC\uFF21-\uFF3A\uFF41-\uFF5A\uFF66-\uFFBE\uFFC2-\uFFC7\uFFCA-\uFFCF\uFFD2-\uFFD7\uFFDA-\uFFDC]");
            var UnicodeIDContinue = new RegexBasedTerminal("UnicodeIDContinue", "[\u0300-\u034E\u0360-\u0362\u0483-\u0486\u0591-\u05A1\u05A3-\u05B9\u05BB-\u05BD\u05BF\u05C1-\u05C2\u05C4\u064B-\u0655\u0670\u06D6-\u06DC\u06DF-\u06E4\u06E7-\u06E8\u06EA-\u06ED\u0711\u0730-\u074A\u07A6-\u07B0\u0901-\u0903\u093C\u093E-\u094D\u0951-\u0954\u0962-\u0963\u0981-\u0983\u09BC-\u09C4\u09C7-\u09C8\u09CB-\u09CD\u09D7\u09E2-\u09E3\u0A02\u0A3C\u0A3E-\u0A42\u0A47-\u0A48\u0A4B-\u0A4D\u0A70-\u0A71\u0A81-\u0A83\u0ABC\u0ABE-\u0AC5\u0AC7-\u0AC9\u0ACB-\u0ACD\u0B01-\u0B03\u0B3C\u0B3E-\u0B43\u0B47-\u0B48\u0B4B-\u0B4D\u0B56-\u0B57\u0B82-\u0B83\u0BBE-\u0BC2\u0BC6-\u0BC8\u0BCA-\u0BCD\u0BD7\u0C01-\u0C03\u0C3E-\u0C44\u0C46-\u0C48\u0C4A-\u0C4D\u0C55-\u0C56\u0C82-\u0C83\u0CBE-\u0CC4\u0CC6-\u0CC8\u0CCA-\u0CCD\u0CD5-\u0CD6\u0D02-\u0D03\u0D3E-\u0D43\u0D46-\u0D48\u0D4A-\u0D4D\u0D57\u0D82-\u0D83\u0DCA\u0DCF-\u0DD4\u0DD6\u0DD8-\u0DDF\u0DF2-\u0DF3\u0E31\u0E34-\u0E3A\u0E47-\u0E4E\u0EB1\u0EB4-\u0EB9\u0EBB-\u0EBC\u0EC8-\u0ECD\u0F18-\u0F19\u0F35\u0F37\u0F39\u0F3E-\u0F3F\u0F71-\u0F84\u0F86-\u0F87\u0F90-\u0F97\u0F99-\u0FBC\u0FC6\u102C-\u1032\u1036-\u1039\u1056-\u1059\u17B4-\u17D3\u18A9\u20D0-\u20DC\u20E1\u302A-\u302F\u3099-\u309A\uFB1E\uFE20-\uFE23\u0903\u093E\u093F\u0940\u0949\u094A\u094B\u094C\u0982\u0983\u09BE\u09BF\u09C0\u09C7\u09C8\u09CB\u09CC\u09D7\u0A03\u0A3E\u0A3F\u0A40\u0A83\u0ABE\u0ABF\u0AC0\u0AC9\u0ACB\u0ACC\u0B02\u0B03\u0B3E\u0B40\u0B47\u0B48\u0B4B\u0B4C\u0B57\u0BBE\u0BBF\u0BC1\u0BC2\u0BC6\u0BC7\u0BC8\u0BCA\u0BCB\u0BCC\u0BD7\u0C01\u0C02\u0C03\u0C41\u0C42\u0C43\u0C44\u0C82\u0C83\u0CBE\u0CC0\u0CC1\u0CC2\u0CC3\u0CC4\u0CC7\u0CC8\u0CCA\u0CCB\u0CD5\u0CD6\u0D02\u0D03\u0D3E\u0D3F\u0D40\u0D46\u0D47\u0D48\u0D4A\u0D4B\u0D4C\u0D57\u0D82\u0D83\u0DCF\u0DD0\u0DD1\u0DD8\u0DD9\u0DDA\u0DDB\u0DDC\u0DDD\u0DDE\u0DDF\u0DF2\u0DF3\u0F3E\u0F3F\u0F7F\u102C\u1031\u1038\u1056\u1057\u17B6\u17BE\u17BF\u17C0\u17C1\u17C2\u17C3\u17C4\u17C5\u17C7\u17C8\u1923\u1924\u1925\u1926\u1929\u192A\u192B\u1930\u1931\u1933\u1934\u1935\u1936\u1937\u1938\u19B0\u19B1\u19B2\u19B3\u19B4\u19B5\u19B6\u19B7\u19B8\u19B9\u19BA\u19BB\u19BC\u19BD\u19BE\u19BF\u19C0\u19C8\u19C9\u1A19\u1A1A\u1A1B\uA802\uA823\uA824\uA827\u1D16\u1D16\u1D16\u1D16\u1D16\u1D17\u1D17\u1D17\u0030-\u0039\u0660-\u0669\u06F0-\u06F9\u0966-\u096F\u09E6-\u09EF\u0A66-\u0A6F\u0AE6-\u0AEF\u0B66-\u0B6F\u0BE7-\u0BEF\u0C66-\u0C6F\u0CE6-\u0CEF\u0D66-\u0D6F\u0E50-\u0E59\u0ED0-\u0ED9\u0F20-\u0F29\u1040-\u1049\u1369-\u1371\u17E0-\u17E9\u1810-\u1819\uFF10-\uFF19\u005F\u203F-\u2040\u30FB\uFE33-\uFE34\uFE4D-\uFE4F\uFF3F\uFF65]") | UnicodeIDStart;
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

            
            #region A.2 Expressions

            #region 1. Terminals

            #endregion

            #region 2. Non-terminals
            var IdentifierReference = new NonTerminal("IdentifierReference");
            var BindingIdentifier = new NonTerminal("BindingIdentifier");
            var LabelIdentifier = new NonTerminal("LabelIdentifier");
            var Identifier = new NonTerminal("Identifier");
            var PrimaryExpression = new NonTerminal("PrimaryExpression");
            var CoverParenthesizedExpressionAndArrowParameterList = new NonTerminal("CoverParenthesizedExpressionAndArrowParameterList");
            var ParenthesizedExpression = new NonTerminal("ParenthesizedExpression");
            var Literal = new NonTerminal("Literal");
            var ArrayLiteral = new NonTerminal("ArrayLiteral");
            var ElementList = new NonTerminal("ElementList");
            var Elision = new NonTerminal("Elision");
            var SpreadElement = new NonTerminal("SpreadElement");
            var ObjectLiteral = new NonTerminal("ObjectLiteral");
            var PropertyDefinitionList = new NonTerminal("PropertyDefinitionList");
            var PropertyDefinition = new NonTerminal("PropertyDefinition");
            var PropertyName = new NonTerminal("PropertyName");
            var LiteralPropertyName = new NonTerminal("LiteralPropertyName");
            var ComputedPropertyName = new NonTerminal("ComputedPropertyName");
            var CoverInitializedName = new NonTerminal("CoverInitializedName");
            var Initializer = new NonTerminal("Initializer");
            var TemplateLiteral = new NonTerminal("TemplateLiteral");
            var TemplateSpans = new NonTerminal("TemplateSpans");
            var TemplateMiddleList = new NonTerminal("TemplateMiddleList");
            var MemberExpression = new NonTerminal("MemberExpression");
            var SuperProperty = new NonTerminal("SuperProperty");
            var MetaProperty = new NonTerminal("MetaProperty");
            var NewTarget = new NonTerminal("NewTarget");
            var NewExpression = new NonTerminal("NewExpression");
            var CallExpression = new NonTerminal("CallExpression");
            var SuperCall = new NonTerminal("SuperCall");
            var Arguments = new NonTerminal("Arguments");
            var ArgumentList = new NonTerminal("ArgumentList");
            var LeftHandSideExpression = new NonTerminal("LeftHandSideExpression");
            var PostfixExpression = new NonTerminal("PostfixExpression");
            var UnaryExpression = new NonTerminal("UnaryExpression");
            var MultiplicativeExpression = new NonTerminal("MultiplicativeExpression");
            var MultiplicativeOperator = new NonTerminal("MultiplicativeOperator");
            var AdditiveExpression = new NonTerminal("AdditiveExpression");
            var ShiftExpression = new NonTerminal("ShiftExpression");
            var RelationalExpression = new NonTerminal("RelationalExpression");
            var EqualityExpression = new NonTerminal("EqualityExpression");
            var BitwiseANDExpression = new NonTerminal("BitwiseANDExpression");
            var BitwiseXORExpression = new NonTerminal("BitwiseXORExpression");
            var BitwiseORExpression = new NonTerminal("BitwiseORExpression");
            var LogicalANDExpression = new NonTerminal("LogicalANDExpression");
            var LogicalORExpression = new NonTerminal("LogicalORExpression");
            var ConditionalExpression = new NonTerminal("ConditionalExpression");
            var AssignmentExpression = new NonTerminal("AssignmentExpression");
            var AssignmentOperator = new NonTerminal("AssignmentOperator");
            var Expression = new NonTerminal("Expression");

            #endregion

            #region 3. BNF rules
            IdentifierReference.Rule = IdentifierName
                                        | "yield";

            BindingIdentifier.Rule = IdentifierName // TODO: http://people.mozilla.org/~jorendorff/es6-draft.html#sec-identifiers
                                    | "yield";               

            LabelIdentifier.Rule = IdentifierName  //
                                    | "yield";
                                    
            Identifier.Rule = IdentifierName; // 

            PrimaryExpression.Rule = "this"
                                    | IdentifierReference
                                    | Literal
                                    | ArrayLiteral
                                    | ObjectLiteral
                                    | FunctionExpression
                                    | ClassExpression
                                    | GeneratorExpression
                                    | RegularExpressionLiteral
                                    | TemplateLiteral
                                    | CoverParenthesizedExpressionAndArrowParameterList;

            CoverParenthesizedExpressionAndArrowParameterList.Rule = "(" + Expression + ")"
                                                                    | "(" + ")"
                                                                    | "(" + SpreadElement + BindingIdentifier + ")"
                                                                    | "(" + Expression + "," + SpreadElement + BindingIdentifier + ")";
            
            ParenthesizedExpression.Rule = "(" + Expression + ")";

            Literal.Rule = NullLiteral
                           | BooleanLiteral
                           | NumericLiteral
                           | StringLiteral;

            ArrayLiteral.Rule = "[" + Elision.Q() + "]"
                                | "[" + ElementList + "]"
                                | "[" + ElementList + "," + Elision.Q() + "]";

            ElementList.Rule = Elision.Q() + AssignmentExpression
                               | Elision.Q() + SpreadElement
                               | ElementList + "," + Elision.Q() + AssignmentExpression
                               | ElementList + "," + Elision.Q() + SpreadElement;

            Elision.Rule = ","
                           | Elision + ",";

            SpreadElement.Rule = AssignmentExpression; // TODO: "..." http://people.mozilla.org/~jorendorff/es6-draft.html#sec-array-initializer

            ObjectLiteral.Rule = "{" + "}"
                                 | "{" + PropertyDefinitionList + "}"
                                 | "{" + PropertyDefinitionList + "," + "}";

            PropertyDefinitionList.Rule = PropertyDefinition
                                          | PropertyDefinitionList + "," + PropertyDefinition;

            PropertyDefinition.Rule = IdentifierReference
                                      | CoverInitializedName
                                      | PropertyName + ":" + AssignmentExpression
                                      | MethodDefinition;

            PropertyName.Rule = LiteralPropertyName  // [+GeneratorParameter] http://people.mozilla.org/~jorendorff/es6-draft.html#sec-object-initializer
                                | ComputedPropertyName
                                | ComputedPropertyName;

            LiteralPropertyName.Rule = IdentifierName
                                       | StringLiteral
                                       | NumericLiteral;

            ComputedPropertyName.Rule = "[" + AssignmentExpression + "]";

            CoverInitializedName.Rule = IdentifierReference + Initializer;

            Initializer.Rule = "=" + AssignmentExpression;

            TemplateLiteral.Rule = NoSubstitutionTemplate
                                   | TemplateHead + Expression + TemplateSpans;

            TemplateSpans.Rule = TemplateTail
                                 | TemplateMiddleList + TemplateTail;

            TemplateMiddleList.Rule = TemplateMiddle + Expression
                                      | TemplateMiddleList + TemplateMiddle + Expression;

            MemberExpression.Rule = PrimaryExpression
                                    | MemberExpression + "[" + Expression + "]"
                                    | MemberExpression + "." + IdentifierName
                                    | MemberExpression + TemplateLiteral
                                    | SuperProperty
                                    | MetaProperty
                                    | "new" + MemberExpression + Arguments;

            SuperProperty.Rule = "super" + "[" + Expression + "]"
                                 | "super" + "." + IdentifierName;

            MetaProperty.Rule = NewTarget;

            NewTarget.Rule = "new" + "." + "target";

            NewExpression.Rule = MemberExpression
                                 | "new" + NewExpression;

            CallExpression.Rule = MemberExpression + Arguments
                                  | SuperCall
                                  | CallExpression + Arguments
                                  | CallExpression + "[" + Expression + "]"
                                  | CallExpression + "." + IdentifierName
                                  | CallExpression + TemplateLiteral;

            SuperCall.Rule = "super" + Arguments;

            Arguments.Rule = "(" + ")"
                             | "(" + ArgumentList + ")";

            ArgumentList.Rule = AssignmentExpression
                                | AssignmentExpression // ... + AssignmentExpression
                                | ArgumentList + "," + AssignmentExpression
                                | ArgumentList + "," + AssignmentExpression; // ... + AssignmentExpression

            LeftHandSideExpression.Rule = NewExpression
                                          | CallExpression;

            PostfixExpression.Rule = LeftHandSideExpression
                                     | LeftHandSideExpression + "++"  // [no LineTerminator here] ++
                                     | LeftHandSideExpression + "--";

            UnaryExpression.Rule = PostfixExpression
                                   | "delete" + UnaryExpression
                                   | "void" + UnaryExpression
                                   | "typeof" + UnaryExpression
                                   | "++" + UnaryExpression
                                   | "--" + UnaryExpression
                                   | "+" + UnaryExpression
                                   | "-" + UnaryExpression
                                   | "~" + UnaryExpression
                                   | "!" + UnaryExpression;

            MultiplicativeExpression.Rule = UnaryExpression
                                            | MultiplicativeExpression + MultiplicativeOperator + UnaryExpression;

            MultiplicativeOperator.Rule = ""; // TODO: one of * / %

            AdditiveExpression.Rule = MultiplicativeExpression
                                      | AdditiveExpression + "+" + MultiplicativeExpression
                                      | AdditiveExpression + "-" + MultiplicativeExpression;

            ShiftExpression.Rule = AdditiveExpression
                                   | ShiftExpression + "<<" + AdditiveExpression
                                   | ShiftExpression + ">>" + AdditiveExpression
                                   | ShiftExpression + ">>>" + AdditiveExpression;

            RelationalExpression.Rule = ShiftExpression
                                        | RelationalExpression + "<" + ShiftExpression
                                        | RelationalExpression + ">" + ShiftExpression
                                        | RelationalExpression + "<=" + ShiftExpression
                                        | RelationalExpression + ">=" + ShiftExpression
                                        | RelationalExpression + "instanceof" + ShiftExpression
                                        | RelationalExpression + "in" + ShiftExpression;  // [+In] RelationalExpression in ShiftExpression http://people.mozilla.org/~jorendorff/es6-draft.html#sec-relational-operators

            EqualityExpression.Rule = RelationalExpression
                                      | EqualityExpression + "==" + RelationalExpression
                                      | EqualityExpression + "!=" + RelationalExpression
                                      | EqualityExpression + "===" + RelationalExpression
                                      | EqualityExpression + "!==" + RelationalExpression;

            BitwiseANDExpression.Rule = EqualityExpression
                                        | BitwiseANDExpression + "&" + EqualityExpression;

            BitwiseXORExpression.Rule = BitwiseANDExpression
                                        | BitwiseXORExpression + "^" + BitwiseANDExpression;

            BitwiseORExpression.Rule = BitwiseXORExpression
                                       | BitwiseORExpression + "|" + BitwiseXORExpression;

            LogicalANDExpression.Rule = BitwiseORExpression
                                        | LogicalANDExpression + "&&" + BitwiseORExpression;

            LogicalORExpression.Rule = LogicalANDExpression
                                       | LogicalORExpression + "||" + LogicalANDExpression;

            ConditionalExpression.Rule = LogicalORExpression
                                         | LogicalORExpression + "?" + AssignmentExpression + ":" + AssignmentExpression; // Check

            AssignmentExpression.Rule = ConditionalExpression
                                        | YieldExpression
                                        | ArrowFunction
                                        | LeftHandSideExpression + "=" + AssignmentExpression
                                        | LeftHandSideExpression + AssignmentOperator + AssignmentExpression;

            AssignmentOperator.Rule = ""; // one of *=	/=	%=	+=	-=	<<=	>>=	>>>=	&=	^=	|=

            Expression.Rule = AssignmentExpression
                              | Expression + "," + AssignmentExpression;

            #endregion

            #endregion


            #region A.3 Statements

            #region 1. Terminals

            var LetOrConst = ToTerm("let") | "const";
            var EmptyStatement = ToTerm(";");

            #endregion
            
            #region 2. Non-terminals

            var Statement = new NonTerminal("Statement");
            var Declaration = new NonTerminal("Declaration");
            var HoistableDeclaration = new NonTerminal("HoistableDeclaration");
            var BreakableStatement = new NonTerminal("BreakableStatement");
            var BlockStatement = new NonTerminal("BlockStatement");
            var Block = new NonTerminal("Block");
            var StatementList = new NonTerminal("StatementList");
            var StatementListItem = new NonTerminal("StatementListItem");
            var LexicalDeclaration = new NonTerminal("LexicalDeclaration");            
            var BindingList = new NonTerminal("BindingList");
            var LexicalBinding = new NonTerminal("LexicalBinding");
            var VariableStatement = new NonTerminal("VariableStatement");
            var VariableDeclarationList = new NonTerminal("VariableDeclarationList");
            var VariableDeclaration = new NonTerminal("VariableDeclaration");
            var BindingPattern = new NonTerminal("BindingPattern");
            var ObjectBindingPattern = new NonTerminal("ObjectBindingPattern");
            var ArrayBindingPattern = new NonTerminal("ArrayBindingPattern");
            var BindingPropertyList = new NonTerminal("BindingPropertyList");
            var BindingElementList = new NonTerminal("BindingElementList");
            var BindingElisionElement = new NonTerminal("BindingElisionElement");
            var BindingProperty = new NonTerminal("BindingProperty");
            var BindingElement = new NonTerminal("BindingElement");
            var SingleNameBinding = new NonTerminal("SingleNameBinding");
            var BindingRestElement = new NonTerminal("BindingRestElement");
            var ExpressionStatement = new NonTerminal("ExpressionStatement");
            var IfStatement = new NonTerminal("IfStatement");
            var IterationStatement = new NonTerminal("IterationStatement");
            var ForDeclaration = new NonTerminal("ForDeclaration");
            var ForBinding = new NonTerminal("ForBinding");
            var ContinueStatement = new NonTerminal("ContinueStatement");
            var BreakStatement = new NonTerminal("BreakStatement");
            var ReturnStatement = new NonTerminal("ReturnStatement");
            var WithStatement = new NonTerminal("WithStatement");
            var SwitchStatement = new NonTerminal("SwitchStatement");
            var CaseBlock = new NonTerminal("CaseBlock");
            var CaseClauses = new NonTerminal("CaseClauses");
            var CaseClause = new NonTerminal("CaseClause");
            var DefaultClause = new NonTerminal("DefaultClause");
            var LabelledStatement = new NonTerminal("LabelledStatement");
            var LabelledItem = new NonTerminal("LabelledItem");
            var ThrowStatement = new NonTerminal("ThrowStatement");
            var TryStatement = new NonTerminal("TryStatement");
            var Catch = new NonTerminal("Catch");
            var Finally = new NonTerminal("Finally");
            var CatchParameter = new NonTerminal("CatchParameter");
            var DebuggerStatement = new NonTerminal("DebuggerStatement");

            #endregion

            #region 3. BNF rules

            Statement.Rule = BlockStatement
                                | VariableStatement
                                | EmptyStatement
                                | ExpressionStatement
                                | IfStatement
                                | BreakableStatement
                                | ContinueStatement
                                | BreakStatement
                                | ReturnStatement
                                | WithStatement
                                | LabelledStatement
                                | ThrowStatement
                                | TryStatement
                                | DebuggerStatement;

            Declaration.Rule = HoistableDeclaration
                                | ClassDeclaration
                                | LexicalDeclaration;

            HoistableDeclaration.Rule = FunctionDeclaration
                                        | GeneratorDeclaration;

            BreakableStatement.Rule = IterationStatement
                                        | SwitchStatement;

            BlockStatement.Rule = Block;

            Block.Rule = ToTerm("{") + StatementList.Q() + "}";

            StatementList.Rule = StatementListItem
                                    | StatementList + StatementListItem;

            StatementListItem.Rule = Statement
                                        | Declaration;

            LexicalDeclaration.Rule = LetOrConst + BindingList;

            BindingList.Rule = LexicalBinding
                                | BindingList + ToTerm(",") + LexicalBinding;

            LexicalBinding.Rule = BindingIdentifier + Initializer.Q()
                                    | BindingPattern + Initializer;

            VariableStatement.Rule = ToTerm("var") + VariableDeclarationList;

            VariableDeclarationList.Rule = VariableDeclaration
                                            | VariableDeclarationList + "," + VariableDeclaration;

            VariableDeclaration.Rule = BindingIdentifier + Initializer.Q()
                                        | BindingPattern + Initializer;

            BindingPattern.Rule = ObjectBindingPattern
                                    | ArrayBindingPattern;

            ObjectBindingPattern.Rule = ToTerm("{") + "}"
                                        | "{" + BindingPropertyList + "}"
                                        | "{" + BindingPropertyList + "," + "}";

            ArrayBindingPattern.Rule = ToTerm("[") + Elision.Q() + BindingRestElement.Q() + "]"
                                        | "[" + BindingElementList + "]"
                                        | "[" + BindingElementList + "," + Elision.Q() + BindingRestElement.Q() + "]";

            BindingPropertyList.Rule = BindingProperty
                                        | BindingPropertyList + "," + BindingProperty;

            BindingElementList.Rule = BindingElisionElement
                                        | BindingElementList + "," + BindingElisionElement;

            BindingElisionElement.Rule = Elision.Q() + BindingElement;

            BindingProperty.Rule = SingleNameBinding
                                    | PropertyName + ":" + BindingElement;

            BindingElement.Rule = SingleNameBinding
                                    | BindingPattern + Initializer.Q()
                                    | BindingPattern + BindingPattern.Q();  // https://people.mozilla.org/~jorendorff/es6-draft.html#sec-destructuring-binding-patterns

            SingleNameBinding.Rule = BindingIdentifier + Initializer.Q()
                                        | BindingIdentifier + Initializer.Q();

            BindingRestElement.Rule = ToTerm("...") + BindingIdentifier
                                        | ToTerm("...") + BindingIdentifier;

            ExpressionStatement.Rule = Expression + ";"; // [lookahead ∉ {{, function, class, let [}] https://people.mozilla.org/~jorendorff/es6-draft.html#sec-expression-statement

            IfStatement.Rule = ToTerm("if") + "(" + Expression + ")" + Statement + "else" + Statement
                                | "if" + "(" + Expression + ")" + Statement;

            // See 13.6 https://people.mozilla.org/~jorendorff/es6-draft.html#sec-iteration-statements
            IterationStatement.Rule = ToTerm("do") + Statement + "while" + "(" + Expression + ")" + ";"
                                        | "while" + "(" + Expression + ")" + Statement
                                        | "for" + "(" + Expression.Q() + ";" + Expression.Q() + ";" + Expression.Q() + ")" + Statement
                                        | "for" + "(" + "var" + VariableDeclarationList + ";" + Expression.Q() + ";" + Expression.Q() + ")" + Statement
                                        | "for" + "(" + LexicalDeclaration + Expression.Q() + ";" + Expression.Q() + ")" + Statement
                                        | "for" + "(" + LeftHandSideExpression + "in" + Expression + ")" + Statement
                                        | "for" + "(" + "var" + ForBinding + "in" + Expression + ")" + Statement
                                        | "for" + "(" + ForDeclaration + "in" + Expression + ")" + Statement
                                        | "for" + "(" + LeftHandSideExpression + "of" + AssignmentExpression + ")" + Statement
                                        | "for" + "(" + "var" + ForBinding + "of" + AssignmentExpression + ")" + Statement
                                        | "for" + "(" + ForDeclaration + "of" + AssignmentExpression + ")" + Statement;

            ForDeclaration.Rule = LetOrConst + ForBinding;

            ForBinding.Rule = BindingIdentifier
                                | BindingPattern;

            ContinueStatement.Rule = ToTerm("continue") + ";"
                                        | "continue" + LabelIdentifier + ";";

            BreakStatement.Rule = ToTerm("break") + ";"
                                    | "break" + LabelIdentifier + ";";

            ReturnStatement.Rule = ToTerm("return") + ";"
                                    | "return" + Expression + ";";

            WithStatement.Rule = ToTerm("with") + "(" + Expression + ")" + Statement;

            SwitchStatement.Rule = ToTerm("switch") + "(" + Expression + ")" + CaseBlock;

            CaseBlock.Rule = ToTerm("{") + CaseClauses.Q() + "}"
                                | "{" + CaseClauses.Q() + DefaultClause + CaseClauses.Q() + "}";

            CaseClauses.Rule = CaseClause
                                | CaseClauses + CaseClause;

            CaseClause.Rule = ToTerm("case") + Expression + ":" + StatementList.Q();

            DefaultClause.Rule = ToTerm("default") + ":" + StatementList.Q();

            LabelledStatement.Rule = LabelIdentifier + ":" + LabelledItem;

            LabelledItem.Rule = Statement
                                | FunctionDeclaration;

            ThrowStatement.Rule = ToTerm("throw") + Expression + ";";

            TryStatement.Rule = ToTerm("try") + Block + Catch
                                | "try" + Block + Finally
                                | "try" + Catch + Finally;

            Catch.Rule = ToTerm("catch") + "(" + CatchParameter + ")" + Block;

            Finally.Rule = ToTerm("finally") + Block;

            CatchParameter.Rule = BindingIdentifier
                                    | BindingPattern;

            DebuggerStatement.Rule = "debugger" + ";";   

            #endregion

            #endregion


            #region A.4 Functions and Classes

            #region 1. Terminals

            #endregion

            #region 2. Non-terminals
            var FunctionDeclaration = new NonTerminal("FunctionDeclaration");
            var FunctionExpression = new NonTerminal("FunctionExpression");
            var StrictFormalParameters = new NonTerminal("StrictFormalParameters");
            var FormalParameters = new NonTerminal("FormalParameters");
            var FormalParameterList = new NonTerminal("FormalParameterList");
            var FormalsList = new NonTerminal("FormalsList");
            var FunctionRestParameter = new NonTerminal("FunctionRestParameter");
            var FormalParameter = new NonTerminal("FormalParameter");
            var FunctionBody = new NonTerminal("FunctionBody");
            var FunctionStatementList = new NonTerminal("FunctionStatementList");
            var ArrowFunction = new NonTerminal("ArrowFunction");
            var ArrowParameters = new NonTerminal("ArrowParameters");
            var ConciseBody = new NonTerminal("ConciseBody");
            var ArrowFormalParameters = new NonTerminal("ArrowFormalParameters");
            var MethodDefinition = new NonTerminal("MethodDefinition");
            var PropertySetParameterList = new NonTerminal("PropertySetParameterList");
            var GeneratorMethod = new NonTerminal("GeneratorMethod");
            var GeneratorDeclaration = new NonTerminal("GeneratorDeclaration");
            var GeneratorExpression = new NonTerminal("GeneratorExpression");
            var GeneratorBody = new NonTerminal("GeneratorBody");
            var YieldExpression = new NonTerminal("YieldExpression");
            var ClassDeclaration = new NonTerminal("ClassDeclaration");
            var ClassExpression = new NonTerminal("ClassExpression");
            var ClassTail = new NonTerminal("ClassTail");
            var ClassHeritage = new NonTerminal("ClassHeritage");
            var ClassBody = new NonTerminal("ClassBody");
            var ClassElementList = new NonTerminal("ClassElementList");
            var ClassElement = new NonTerminal("ClassElement");


            #endregion

            #region 3. BNF rules
            FunctionDeclaration.Rule = "function" + BindingIdentifier + "(" + FormalParameters + ")" + "{" + FunctionBody + "}"
                                       | "function" + "(" + FormalParameters + ")" + "{" + FunctionBody + "}";

            FunctionExpression.Rule = "function" + BindingIdentifier.Q() + "(" + FormalParameters + ")" + "{" + FunctionBody + "}";

            StrictFormalParameters.Rule = FormalParameters;

            FormalParameters.Rule = ToTerm("(?:)") // empty string?
                                    | FormalParameterList;

            FormalParameterList.Rule = FunctionRestParameter
                                       | FormalsList
                                       | FormalsList + "," + FunctionRestParameter;

            FormalsList.Rule = FormalParameter
                               | FormalsList + "," + FormalParameter;

            FunctionRestParameter.Rule = BindingRestElement;

            FormalParameter.Rule = BindingElement;

            FunctionBody.Rule = FunctionStatementList;

            FunctionStatementList.Rule = StatementList;

            ArrowFunction.Rule = ArrowParameters + "=>" + ConciseBody; //ArrowParameters [no LineTerminator here] "=>"  this rule is correct?

            ArrowParameters.Rule = BindingIdentifier
                                   | CoverParenthesizedExpressionAndArrowParameterList; // ????? http://people.mozilla.org/~jorendorff/es6-draft.html#sec-arrow-function-definitions

            ConciseBody.Rule = AssignmentExpression // [lookahead ≠ { ]?
                               | "{" + FunctionBody + "}";

            ArrowFormalParameters.Rule = "(" + StrictFormalParameters + ")";

            MethodDefinition.Rule = PropertyName + "(" + StrictFormalParameters + ")" + "{" + FunctionBody + "}"
                                    | GeneratorMethod
                                    | "get" + PropertyName + "(" + ")" + "{" + FunctionBody + "}"
                                    | "set" + PropertyName + "(" + PropertySetParameterList + ")" + "{" + FunctionBody + "}";

            PropertySetParameterList.Rule = FormalParameter;

            GeneratorMethod.Rule = "*" + PropertyName + "(" + StrictFormalParameters + ")" + "{" + GeneratorBody + "}";

            GeneratorDeclaration.Rule = "function" + "*" + BindingIdentifier + "(" + FormalParameters + ")" + "{" + GeneratorBody + "}"
                                        | "function" + "*" + "(" + FormalParameters + ")" + "{" + GeneratorBody + "}";

            GeneratorExpression.Rule = "function" + "*" + BindingIdentifier.Q() + "(" + FormalParameters + ")" + "{" + GeneratorBody + "}";

            GeneratorBody.Rule = FunctionBody;

            YieldExpression.Rule = "yield"
                                   | "yield" + AssignmentExpression  // yield [no LineTerminator here] AssignmentExpression
                                   | "yield" + "*" + AssignmentExpression; // yield [no LineTerminator here] * AssignmentExpression

            ClassDeclaration.Rule = "class" + BindingIdentifier + ClassTail
                                    | "class" + ClassTail;

            ClassExpression.Rule = "class" + BindingIdentifier.Q() + ClassTail;

            ClassTail.Rule = ClassHeritage.Q() + "{" + ClassBody.Q() + "}";

            ClassHeritage.Rule = "extends" + LeftHandSideExpression;

            ClassBody.Rule = ClassElementList;

            ClassElementList.Rule = ClassElement
                                    | ClassElementList + ClassElement;

            ClassElement.Rule = MethodDefinition
                                | "static" + MethodDefinition // || ; or && ;?
                                | ";";  

            #endregion 

            #endregion


            #region A.5 Scripts and Modules

            #region 1. Terminals

            #endregion

            #region 2. Non-terminals

            var Script = new NonTerminal("Script");
            var ScriptBody = new NonTerminal("ScriptBody");
            var Module = new NonTerminal("Module");
            var ModuleBody = new NonTerminal("ModuleBody");
            var ModuleItemList = new NonTerminal("ModuleItemList");
            var ModuleItem = new NonTerminal("ModuleItem");
            var ImportDeclaration = new NonTerminal("ImportDeclaration");
            var ImportClause = new NonTerminal("ImportClause");
            var ImportedDefaultBinding = new NonTerminal("ImportedDefaultBinding");
            var NameSpaceImport = new NonTerminal("NameSpaceImport");
            var NamedImports = new NonTerminal("NamedImports");
            var FromClause = new NonTerminal("FromClause");
            var ImportsList = new NonTerminal("ImportsList");
            var ImportSpecifier = new NonTerminal("ImportSpecifier");
            var ModuleSpecifier = new NonTerminal("ModuleSpecifier");
            var ImportedBinding = new NonTerminal("ImportedBinding");
            var ExportDeclaration = new NonTerminal("ExportDeclaration");
            var ExportClause = new NonTerminal("ExportClause");
            var ExportsList = new NonTerminal("ExportsList");
            var ExportSpecifier = new NonTerminal("ExportSpecifier");

            #endregion

            #region 3. BNF rules

            Script.Rule = ScriptBody.Q();

            ScriptBody.Rule = StatementList;

            Module.Rule = ModuleBody.Q();

            ModuleBody.Rule = ModuleItemList;

            ModuleItemList.Rule = ModuleItem
                                    | ModuleItemList + ModuleItem;

            ModuleItem.Rule = ImportDeclaration
                                | ExportDeclaration
                                | StatementListItem;

            ImportDeclaration.Rule = ToTerm("import") + ImportClause + FromClause + ";"
                                        | "import" + ModuleSpecifier + ";";

            ImportClause.Rule = ImportedDefaultBinding
                                    | NameSpaceImport
                                    | NamedImports
                                    | ImportedDefaultBinding + "," + NameSpaceImport
                                    | ImportedDefaultBinding + "," + NamedImports;

            ImportedDefaultBinding.Rule = ImportedBinding;

            NameSpaceImport.Rule = ToTerm("*") + "as" + ImportedBinding;

            NamedImports.Rule = ToTerm("{") + "}"
                                | "{" + ImportsList + "}"
                                | "{" + ImportsList + "," + "}";

            FromClause.Rule = ToTerm("from") + ModuleSpecifier;

            ImportsList.Rule = ImportSpecifier
                                | ImportsList + "," + ImportSpecifier;

            ImportSpecifier.Rule = ImportedBinding
                                    | IdentifierName + "as" + ImportedBinding;

            ModuleSpecifier.Rule = StringLiteral;

            ImportedBinding.Rule = BindingIdentifier;

            ExportDeclaration.Rule = ToTerm("export") + "*" + FromClause + ";"
                                    | "export" + ExportClause + FromClause + ";"
                                    | "export" + ExportClause + ";"
                                    | "export" + VariableStatement
                                    | "export" + Declaration
                                    | "export" + "default" + HoistableDeclaration
                                    | "export" + "default" + ClassDeclaration
                                    | "export" + "default" + AssignmentExpression; // [lookahead ∉ {function, class}] https://people.mozilla.org/~jorendorff/es6-draft.html#sec-exports

            ExportClause.Rule = ToTerm("{") + "}"
                                | "{" + ExportsList + "}"
                                | "{" + ExportsList + "," + "}";

            ExportsList.Rule = ExportSpecifier
                                | ExportsList + "," + ExportSpecifier;

            ExportSpecifier.Rule = IdentifierName
                                    | IdentifierName + "as" + IdentifierName;

            #endregion

            #endregion


            #region A.8 Regular Expressions

            #region 1. Terminals

            var SyntaxCharacter = new RegexBasedTerminal("SyntaxCharacter", @"[$\\.*+?()[]{}|^]"); // TODO: Check regex
            var ControlEscape = new RegexBasedTerminal("ControlEscape", @"[fnrtv]");
            var ControlLetter = new RegexBasedTerminal("ControlLetter", @"[abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ]");
            var CharacterClassEscape = new RegexBasedTerminal("CharacterClassEscape", @"[dDsSwW]");
            var PatternCharacter = new RegexBasedTerminal("PatternCharacter", @"[^$\\.*+?()[]{}|^]"); // TODO: Check regex

            #endregion

            #region 2. Non-terminals

            var Pattern = new NonTerminal("Pattern");
            var Disjunction = new NonTerminal("Disjunction");
            var Alternative = new NonTerminal("Alternative");
            var Term = new NonTerminal("Term");
            var Assertion = new NonTerminal("Assertion");
            var Quantifier = new NonTerminal("Quantifier");
            var QuantifierPrefix = new NonTerminal("QuantifierPrefix");
            var Atom = new NonTerminal("Atom");
            var PatternCharacter = new NonTerminal("PatternCharacter");
            var AtomEscape = new NonTerminal("AtomEscape");
            var CharacterEscape = new NonTerminal("CharacterEscape");
            var RegExpUnicodeEscapeSequence = new NonTerminal("RegExpUnicodeEscapeSequence");
            var LeadSurrogate = new NonTerminal("LeadSurrogate");
            var TrailSurrogate = new NonTerminal("TrailSurrogate");
            var NonSurrogate = new NonTerminal("NonSurrogate");
            var IdentityEscape = new NonTerminal("IdentityEscape");
            var DecimalEscape = new NonTerminal("DecimalEscape");
            var CharacterClass = new NonTerminal("CharacterClass");
            var ClassRanges = new NonTerminal("ClassRanges");
            var NonemptyClassRanges = new NonTerminal("NonemptyClassRanges");
            var NonemptyClassRangesNoDash = new NonTerminal("NonemptyClassRangesNoDash");
            var ClassAtom = new NonTerminal("ClassAtom");
            var ClassAtomNoDash = new NonTerminal("ClassAtomNoDash");
            var ClassEscape = new NonTerminal("ClassEscape");

            #endregion

            #region 3. BNF rules

            Pattern.Rule = Disjunction;

            Disjunction.Rule = Alternative
                                | Alternative + "|" + Disjunction;

            Alternative.Rule = Alternative + Term;  // https://people.mozilla.org/~jorendorff/es6-draft.html#sec-patterns
                                // | [empty]

            Term.Rule = Assertion
                        | Atom
                        | Atom + Quantifier;

            Assertion.Rule = ToTerm("^")
                                | "$"
                                | "\\" + "b"
                                | "\\" + "B"
                                | "(" + "?" + "=" + Disjunction + ")"
                                | "(" + "?" + "!" + Disjunction + ")";

            QuantifierPrefix.Rule = ToTerm("*")
                                    | "+"
                                    | "?"
                                    | "{" + DecimalDigits + "}"
                                    | "{" + DecimalDigits + "," + "}"
                                    | "{" + DecimalDigits + "," + DecimalDigits + "}";

            Atom.Rule = PatternCharacter
                        | ToTerm(".")
                        | "\\" + AtomEscape
                        | CharacterClass
                        | "(" + Disjunction + ")"
                        | "(" + "?" + ":" + Disjunction + ")";

            AtomEscape.Rule = DecimalEscape
                                | CharacterEscape
                                | CharacterClassEscape;

            CharacterEscape.Rule = ControlEscape
                                    | ToTerm("c") + ControlLetter
                                    | HexEscapeSequence
                                    | RegExpUnicodeEscapeSequence
                                    | IdentityEscape;

            RegExpUnicodeEscapeSequence.Rule = ToTerm("u") + LeadSurrogate + "\\u" + TrailSurrogate
                                                | "u" + LeadSurrogate
                                                | "u" + TailSurrogate
                                                | "u" + NonSurrogate
                                                | "u" + Hex4Digits
                                                | "u{" + HexDigits + "}";

            //LeadSurrogate.Rule ...


            #endregion

            #endregion
        }
    }
}