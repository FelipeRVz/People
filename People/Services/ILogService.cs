using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace People.Services
{
    public interface ILogService
    {
        public void LogWrite(string message);
    }
}
