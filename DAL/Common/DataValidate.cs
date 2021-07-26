using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
namespace DAL
{
    public class DataValidate
    {
        /// <summary>
        /// 验证是否为整数
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsInteger(string str)
        {
            Regex objregex = new Regex(@"^[0-9]*[1-9][0-9]*$");
            return objregex.IsMatch(str);
        }

        public static bool IsCitizenID(string str)
        {
            Regex objregex = new Regex(@"^(\d{6})(\d{4})(\d{2})(\d{2})(\d{3})([0-9]|X|x)$");
            return objregex.IsMatch(str);
        }
    }
}
