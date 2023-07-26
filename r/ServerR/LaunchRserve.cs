using RDotNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerR
{
    public class LaunchRserve
    {
        static void Main(string[] args)
        {
            REngine.SetEnvironmentVariables();
            REngine engine = REngine.GetInstance();
            engine.Initialize();

            engine.Evaluate("library(Rserve)");
            engine.Evaluate("Rserve()");

            // Déconnexion du serveur Rserve
            engine.Dispose();
        }
    }
}
