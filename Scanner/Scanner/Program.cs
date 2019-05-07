using System;
using System.Collections.Generic;
using System.IO;

namespace Scanner
{
    class Program
    {
        static string[] reservedWords = {"if","then","else","end","repeat","until","read","write"};
        static string[] specialsymbol = {"+","-","=","*","/","<",">","(",")",";",":="};
        static string[] numbers= { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9" };

        static void Main(string[] args)
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();
            List<Token> tokens = new List<Token>();
            StreamReader sr = new StreamReader("program.txt");
            string code;
            string[] line;
            char[] separators = {' ','\n'};
            code = sr.ReadToEnd();

            code = code.Replace(";"," ;");
            code = code.Replace("+", " + ");
            code = code.Replace("-", " - ");
            code = code.Replace("=", " = ");
            code = code.Replace("*", " * ");
            code = code.Replace("/", " / ");
            code = code.Replace("<", " < ");
            code = code.Replace(">", " > ");
            code = code.Replace("(", " ( ");
            code = code.Replace(")", " ) ");
            code = code.Replace(":=", " := ");

            while (code.Contains("{") || code.Contains("}"))
            {
                int i = code.IndexOf('{');
                int y = code.IndexOf('}');
                string comment = code.Substring(i, y - i + 1);
                Token t = new Token(comment, Token.tokentypes.comment);
                tokens.Add(t);
                Console.WriteLine(comment + "\t:Comment");
                code = code.Remove(i, y - i + 1);
            }

            //Console.WriteLine(code + "\n");

            line = code.Split(separators, StringSplitOptions.RemoveEmptyEntries);

            foreach (string s in line)
            {
                s.Trim();
                if (s.Equals(""))
                    continue;
                Token t = gettoken(s.Trim());
                if(t!=null)
                {
                    tokens.Add(t);
                }
            }
            sr.Close();
            watch.Stop();
            Console.WriteLine($"Execution Time: {watch.ElapsedMilliseconds} ms");
            Console.ReadLine();
        }

        static Token gettoken(string str)
        {
            Token t = new Token();
            if(str.Equals(""))
            {
                return null;
            }
            for (int i = 0; i < reservedWords.Length; i++)
            {
                if (str == reservedWords[i])
                {
                    t.setval(str);
                    t.settype(Token.tokentypes.reserved_word);
                    Console.WriteLine(str + "\t:reserved word");
                    return t;
                }
            }
            for(int i = 0; i < specialsymbol.Length; i++)
            {
                if (str == specialsymbol[i])
                {
                    t.setval(str);
                    t.settype(Token.tokentypes.special_symbol);
                    Console.WriteLine(str + "\t:specialsymbol");
                    return t;
                }
            }
            for(int i = 0; i < numbers.Length; i++)
            {
                if (isnumber(str))
                {
                    t.setval(str);
                    t.settype(Token.tokentypes.number);
                    Console.WriteLine(str + "\t:digit");
                    return t;
                }
            }
            Console.WriteLine(str + "\t:identifier");
            t.setval(str);
            t.settype(Token.tokentypes.identifier);
            return t;
        }

        static bool isnumber(string str)
        {
            for(int i =0;i<numbers.Length;i++)
            {
                if (str.StartsWith(numbers[i]))
                    return true;
            }
            return false;
        }

        static void printtokens(List<Token> tokens)
        {
            foreach(Token t in tokens)
            {
                Console.WriteLine(t.getval());
            }
        }

    }
}
