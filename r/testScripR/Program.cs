using System;
using RDotNet;
using RDotNet.Devices;
using RserveCLI2;

namespace testScripR
{
    public class Program
    {
        static void Main(string[] args)
        {          
           
            using (var connection = new RConnection())
            {

                //création d'un chemin relatif
                connection.Eval("s <- getwd()");
                String s = connection.Eval("s").ToString();
                //changement du working directory
                String S = "\"setwd('" + s.Replace("/","//") + "')\"";
                connection.Eval(S);
                Console.WriteLine(s);
                Console.WriteLine(S);

                //utilisation comme source du script R
                //connection.VoidEval("source(\"Scripts//GenTransaction.R\")");
                connection.VoidEval("source(\"Scripts//GenChallenge.R\")");
                connection.VoidEval("n <- 4");
                
                // lancement du Script R
                connection.VoidEval("GenChallenge(n)");
                
            }
        }
    }
}