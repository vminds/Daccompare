using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.SqlServer.Dac;

namespace Daccompare
{
    class Program
    {
        static void Main(string[] args)
        {
            ILogger consoleLogger = new ConsoleLogger();
            if (args.Count() == 0 || args.Any(a => a.Contains("help")) || args.Any(a => a.Contains("/h")) || args.Any(a => a.Contains("-h")) || args.Any(a => a.Contains("?h")) || args.Any(a => a.Contains("-?")) || args.Any(a => a.Contains("/?")))
            {
                WriteHelpInfo(consoleLogger);
            }
            else
            {
                consoleLogger.WriteInfoLine("Application started");

                string _sourcePackagePath = GetSourceFileFromArgs(args);
                string _targetPackagePath = GetTargetFileFromArgs(args);
                string _scriptpath = GetScriptFileFromArgs(args);
                string _targetDatabaseName = "MyTargetDb";
                bool _nosqlcmd = GetNoSqlCmdFromArgs(args);

                var _deployOptions = new DacDeployOptions();
                _deployOptions.SqlCommandVariableValues.Add("RefDb1", "MyReferencedDb");
                _deployOptions.CommentOutSetVarDeclarations = _nosqlcmd;

                consoleLogger.WriteInfoLine($"Source path set to: {_sourcePackagePath}");
                consoleLogger.WriteInfoLine($"Target path set to: {_targetPackagePath}");
                consoleLogger.WriteInfoLine($"Script path set to: {_scriptpath}");
                consoleLogger.WriteInfoLine($"NoSQLcmd set to: {_nosqlcmd}");

                try
                {
                    using (var sourcePac = DacPackage.Load(_sourcePackagePath))
                    {
                        using (var targetPac = DacPackage.Load(_targetPackagePath))
                        {
                            consoleLogger.WriteInfoLine("Generating deploy script");
                            string script = DacServices.GenerateDeployScript(sourcePac, targetPac, _targetDatabaseName, _deployOptions);
                            File.WriteAllText(_scriptpath, script);
                        }
                    }

                }
                catch (Exception ex)
                {
                    consoleLogger.WriteErrorLine(ex);
                    consoleLogger.WriteErrorLine("Ending application");
                }
            }
        }
                
        private static void WriteHelpInfo(ILogger consoleLogger)
        {
            consoleLogger.WriteInfoLine("Possible params:");

            consoleLogger.WriteInfoLine("/sourcefile=<full path to source dacpac file>");
            consoleLogger.WriteInfoLine("/targetfile=<full path to target dacpac file>");
            consoleLogger.WriteInfoLine("/scriptfile=<full path to sql script file to generate>");
            consoleLogger.WriteInfoLine("/nosqlcmd <optional param comment out sqlcmd section>");

        }

        private static string GetSourceFileFromArgs(string[] args)
        {
            string file = "";
            foreach (var arg in args.Where(a => a.ToLower().Contains("/sourcefile=")))
            {
                string[] stringpath = arg.Split(new string[] { "/sourcefile=" }, StringSplitOptions.RemoveEmptyEntries);
                file = Convert.ToString(stringpath.Single());
            }
            return file;
        }

        private static string GetTargetFileFromArgs(string[] args)
        {
            string file = "";
            foreach (var arg in args.Where(a => a.ToLower().Contains("/targetfile=")))
            {
                string[] stringpath = arg.Split(new string[] { "/targetfile=" }, StringSplitOptions.RemoveEmptyEntries);
                file = Convert.ToString(stringpath.Single());
            }
            return file;
        }

        private static string GetScriptFileFromArgs(string[] args)
        {
            string file = "";
            foreach (var arg in args.Where(a => a.ToLower().Contains("/scriptfile=")))
            {
                string[] stringpath = arg.Split(new string[] { "/scriptfile=" }, StringSplitOptions.RemoveEmptyEntries);
                file = Convert.ToString(stringpath.Single());
            }
            return file;
        }

        private static bool GetNoSqlCmdFromArgs(string[] args)
        {
            return args.Any(a => a.ToLower().Contains("/nosqlcmd"));
        }

    }
}

