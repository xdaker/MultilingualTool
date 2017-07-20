using System;
using System.Collections.Generic;
using Multilingual;

namespace MultilingualTool
{
    class Program
    {
        public static TransApi TransApi = new TransApi("2015063000000001", "1435660288");
        static void Main(string[] args)
        {
            //string result = TransApi.GetTransResult("你好", "auto", "en");
            var list = FlieHelper.GetAllFliePath(@"D:\work\FinBase\FinBaseCopy\FinBase");
            FlieHelper flieHelper = new FlieHelper();
            var newlist = flieHelper.ScreenFliePath(list, ".cs", ExistPosition.Suffix);
            newlist = flieHelper.ExcludeCharacter(newlist, @"\obj\");

            Console.Read();
            Console.ReadKey();
        }
    }
   
}
