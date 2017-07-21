using System;
using System.Collections.Generic;
using Multilingual;

namespace MultilingualTool
{
    class Program
    {
        
        static void Main(string[] args)
        {
            FinBaseMultilingual finBaseMultilingual = new FinBaseMultilingual();
            //初始化中文字典
            finBaseMultilingual.InitializationInformationFormScannPath(@"D:\work\FinBase\FinBase");
            finBaseMultilingual.GenerateKey();
            finBaseMultilingual.WriteJson();
            int i = 1;
            foreach (var value in finBaseMultilingual.KeyDictionary.Values)
            {
                //foreach (var str in value)
                //{
                    Console.Write(i++);
                    Console.WriteLine(value);
                //}
            }
            Console.Read();
            Console.ReadKey();
        }
    }
   
}
