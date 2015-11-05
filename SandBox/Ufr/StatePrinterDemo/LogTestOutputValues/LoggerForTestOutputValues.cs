using System;
using System.Collections.Generic;
using System.Linq;

namespace LogTestOutputValues
{
    using System.IO;

    using StatePrinter;
    using StatePrinter.Introspection;
    using StatePrinter.OutputFormatters;

    public class LoggerForTestOutputValues
    {
        private readonly Stateprinter printer;

        private readonly CvsFormatter cvsFormatter;

        public LoggerForTestOutputValues()
        {
            cvsFormatter = new CvsFormatter();
            printer = new Stateprinter();
            printer.Configuration.OutputFormatter = cvsFormatter;
        }

        public void LogTestOutputValues(object obj)
        {
            var cvsLine = printer.PrintObject(obj);

            File.AppendAllText(LogFilename, cvsLine + Environment.NewLine);
        }

        public string LogFilename { get; set; }

        public string FieldSeperator
        {
            get
            {
                return cvsFormatter.FieldSeperator;
            }
            set
            {
                cvsFormatter.FieldSeperator = value;
            }
        }

        private class CvsFormatter : IOutputFormatter
        {
            public string FieldSeperator { get; set; }

            public string Print(List<Token> tokens)
            {
                var retVal = tokens
                    .Where(token => token.Field != null && token.Field.Name != null)
                    .Aggregate(string.Empty, (current, token) => current + token.Value + FieldSeperator);

                return retVal;
            }
        }
    }
}
