using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Multilingual;

namespace MultilingualTool
{
   public  class FinBaseMultilingual
    {
       public string FinBasePath { get; private set; }

        public LangType Type { get; set; }

        public List<string> ChineseList { get; set; }
       public FinBaseMultilingual()
       {
       }

       public List<string> ScannChinese(string finBasePath)
       {
           FinBasePath = finBasePath;
            FlieHelper flieHelper = new FlieHelper();
           var paths = FlieHelper.GetAllFliePath(FinBasePath);
            var newlist = flieHelper.ScreenFliePath(paths, ".cs", ExistPosition.Suffix);
            newlist = flieHelper.ExcludeCharacter(newlist, @"\obj\");
            List < string > chineseList = new List<string>();
            foreach (var path in newlist)
            {
                var text = flieHelper.ReadFile(path);
                chineseList.AddRange(flieHelper.GetChineseString(text));
            }
           return chineseList;
       }
    }
    
}
