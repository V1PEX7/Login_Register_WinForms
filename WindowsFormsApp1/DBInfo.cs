using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    static class DBInfo
    {
        public static string server { get; }   = "localhost";
        public static string port { get; }     = "3306";
        public static string username { get; } = "root";
        public static string password { get; } = "root";
        public static string database { get; } = "vipex";
    }
}
