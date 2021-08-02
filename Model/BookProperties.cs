using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class BookProperties
    {
        //添加书籍属性取值列表
        public static string[] BookTypes = new string[]{
            "历史",
            "金融经济",
            "软件",
            "心理学",
            "小说",
            "文学",
            "管理",
            "科普",
            "儿童读物"
        };
        public static string[] Grades = new string[]
        {
            "1",
            "2",
            "3",
            "5"
        };
        public static string[] Locations = new string[]
        {
            "餐厅书柜里",
            "餐厅书柜外",
            "餐厅书柜上",
            "电视柜书柜",
            "次卧书柜",
            "阳台书柜"
        };
        public static string[] ReadingStatuses = new string[]
        {
            "未读",
            "阅读中",
            "已读"
        };
        public static string[] IsExists = new string[]
        {
            "在架",
            "丢弃"
        };
    }
}
