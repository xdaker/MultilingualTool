using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultilingualTool
{
    class Program
    {
        public static TransApi TransApi = new TransApi("2015063000000001", "1435660288");
        static void Main(string[] args)
        {
           
            string result = TransApi.GetTransResult("你好", "auto", "auto");
            Console.ReadKey();
        }
    }
}
