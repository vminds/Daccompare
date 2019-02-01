using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Daccompare
{
    public interface ILogger
    {
        void ClearLine();
        void WriteEmptyLine();
        void WriteSeparator();
        void WriteError(string message);
        void WriteError(Exception ex);
        void WriteErrorLine(Exception ex);
        void WriteErrorLine(string message);
        void WriteInfo(string message);
        void WriteInfoLine(string message);
        void WriteSuccess(string message);
        void WriteSuccessLine(string message);
        void WriteWarning(string message);
        void WriteWarningLine(string message);
    }
}
