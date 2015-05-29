using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JsIronyParser;
using Irony.Parsing;

namespace Test
{
    public class Program
    {
        public static ParseTreeNode getRoot(string sourceCode, Grammar grammar)
        {

            LanguageData language = new LanguageData(grammar);

            Parser parser = new Parser(language);

            ParseTree parseTree = parser.Parse(sourceCode);

            ParseTreeNode root = parseTree.Root;

            return root;

        }

        public static void dispTree(ParseTreeNode node, int level)
        {
            for (int i = 0; i < level; i++)
                Console.Write("  ");
            Console.WriteLine(node);

            foreach (ParseTreeNode child in node.ChildNodes)
                dispTree(child, level + 1);

        }

        static void Main(string[] args)
        {
            dispTree(getRoot("", new JsGrammar()), 1);
        }        
    }
}
