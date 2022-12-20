using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFormsApp1
{
    static class LocalStorage
    {
        public static string Credentials =
            $"Server = localhost;" +
            $"Integrated security = SSPI;" +
            $"database = VKR;";
    }
}
