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
        //public static ParseTreeNode getRoot(string sourceCode, Grammar grammar)
        //{

        //    Parser parser = new Parser(grammar);

        //    ParseTree parseTree = parser.Parse(sourceCode);

        //    ParseTreeNode root = parseTree.Root;

        //    return root;

        //}

        //public static void dispTree(ParseTreeNode node, int level)
        //{
        //    for (int i = 0; i < level; i++)
        //        Console.Write("  ");
        //    Console.WriteLine(node);

        //    foreach (ParseTreeNode child in node.ChildNodes)
        //        dispTree(child, level + 1);

        //}

        static void Main(string[] args)
        {
            Grammar grammar = new JsGrammar();
            Parser parser = new Parser(grammar);
            ParseTree parseTree = parser.Parse("var");
            var root = parseTree.Root;
            Console.WriteLine(root.ToString());
            foreach (var child in root.ChildNodes)
                Console.WriteLine(child.ToString());
        }        
    }
}
