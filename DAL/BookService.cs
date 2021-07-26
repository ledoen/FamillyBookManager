using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Model;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
    /// <summary>
    /// 通用数据访问类
    /// </summary>
    public class BookService
    {
        #region 添加书籍
        /// <summary>
        /// 添加书籍方法
        /// </summary>
        /// <param name="book"></param>
        /// <returns></returns>
        public static int AddBook(Book book)
        {
            //【1】构造sql操作语句
            string sql = $"insert into Books(BookName, Author, BookType, Publisher, Grade, IsExist, Location, ReadingStatus, Discribe)" +
                $" values('{book.BookName}','{book.Author}','{book.BookType}','{book.Publisher}','{book.Grade}','{book.IsExist}'," +
                $"'{book.Location}','{book.ReadingStatus}','{book.Discribe}')";
            //【2】调用数据库访问方法
            return DBHelper.Update(sql);
        }
        /// <summary>
        /// 验证图书名是否已存在
        /// </summary>
        /// <param name="bookName"></param>
        /// <returns></returns>
        public static bool IsBookNameExist(string bookName)
        {
            //【1】构造sql操作语句
            string sql = $"select count(*) from Books where BookName = '{bookName}'";
            //【2】调用数据库访问方法
            return ((int)DBHelper.GetSingleResult(sql) == 1);
        }
        #endregion

        #region 查询图书
        /// <summary>
        /// 使用书号查询图书
        /// </summary>
        /// <param name="bookID"></param>
        /// <returns></returns>
        public static DataSet SearchByBookID(string bookID)
        {
            //【1】构造sql操作语句
            string sql = $"select BookID, BookName, Author, BookType, Publisher, Grade, IsExist, Location, ReadingStatus, Discribe from Books where BookID = {bookID}";
            //【2】调用数据库访问方法
            return DBHelper.GetMultiResult(sql);
        }
        /// <summary>
        /// 获取全部图书
        /// </summary>
        /// <returns></returns>
        public static DataSet GetAllBooks()
        {
            //【1】构造sql操作语句
            string sql = $"select * from Books";
            //【2】调用数据库访问方法
            return DBHelper.GetMultiResult(sql);
        }
        /// <summary>
        /// 使用书名进行模糊查询
        /// </summary>
        /// <param name="bookName"></param>
        /// <returns></returns>
        public static DataSet SearchBookByName(string bookName)
        {
            //【1】构造sql操作语句
            string sql = $"select * from Books where BookName like '%{bookName}%'";
            //【2】调用数据库访问方法
            return DBHelper.GetMultiResult(sql);
        }
        #endregion

        #region 修改书籍
        /// <summary>
        /// 根据ID修改书籍信息
        /// </summary>
        /// <param name="book"></param>
        /// <returns></returns>
        public static int ModityBookInfo(Book book)
        {
            //【1】构造sql操作语句
            string sql = $"update Books set BookName='{book.BookName}', Author='{book.Author}', BookType='{book.BookType}', " +
                $"Publisher='{book.Publisher}', Grade='{book.Grade}', IsExist='{book.IsExist}', Location='{book.Location}', " +
                $"ReadingStatus='{book.ReadingStatus}', Discribe='{book.Discribe}' where BookID={book.BookID}";
            //【2】调用数据库访问方法
            return DBHelper.Update(sql);
        }
        /// <summary>
        /// 查找除当前ID以外是否有相同书名
        /// </summary>
        /// <param name="bookName"></param>
        /// <param name="bookID"></param>
        /// <returns></returns>
        public static bool IsBookNameExist(string bookName, string bookID)
        {
            //【1】构造sql操作语句
            string sql = $"select count(*) from Books where BookName = '{bookName}' and BookID <>{bookID}";
            //【2】调用数据库访问方法
            return ((int)DBHelper.GetSingleResult(sql) == 1);
        }
        #endregion
    }
}
