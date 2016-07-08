using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.International.Converters.PinYinConverter;
namespace CaterCommon
{
    public class PinyinHelper
    {
        /// <summary>
        /// 获取词组的拼音首字母
        /// </summary>
        /// <param name="str">词组</param>
        /// <returns>拼音首字母</returns>
        public static string GetPinyin(string str)
        {
            string pinyinStr = "";
            foreach (char c in str)
            {
                ChineseChar cc = new ChineseChar(c);
                //获取拼音集合的首字母
                pinyinStr += cc.Pinyins[0][0];
            }
            return pinyinStr;
        }
    }
}
