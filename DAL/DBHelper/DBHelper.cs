using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Model;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace DAL
{
    public class DBHelper
    {
        //数据库访问字符串
        private static string connectInfo = ConfigurationManager.ConnectionStrings["DBInfo"].ToString();

        /// <summary>
        /// 无查询结果的数据库操作，实现增删改
        /// </summary>
        /// <param name="sql"></param>
        /// <returns>返回受影响行数</returns>
        public static int Update(string sql)
        {
            //【1】新建连接
            SqlConnection sqlConnection = new SqlConnection(connectInfo);
            //【2】新建命令
            SqlCommand sqlCommand = new SqlCommand(sql, sqlConnection);
            //【3】打开连接执行命令
            try
            {
                sqlConnection.Open();
                return sqlCommand.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception("数据访问出现问题，具体原因为：" + ex.Message);
            }
            finally
            {
                //【4】关闭连接
                sqlConnection.Close();
            }
        }
        /// <summary>
        /// 返回单一数据的数据库操作
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public static object GetSingleResult(string sql)
        {
            //【1】新建连接
            SqlConnection sqlConnection = new SqlConnection(connectInfo);
            //【2】新建命令
            SqlCommand sqlCommand = new SqlCommand(sql, sqlConnection);
            //【3】打开连接执行命令
            try
            {
                sqlConnection.Open();
                return sqlCommand.ExecuteScalar();
            }
            catch (Exception ex)
            {
                throw new Exception("数据访问出现问题，具体原因为：" + ex.Message);
            }
            finally
            {
                //【4】关闭连接
                sqlConnection.Close();
            }
        }
        /// <summary>
        /// 执行查询返回DataSet
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public static DataSet GetMultiResult(string sql)
        { 
            //【1】新建连接
            SqlConnection sqlConnection = new SqlConnection(connectInfo);
            //【2】新建命令
            SqlCommand sqlCommand = new SqlCommand(sql, sqlConnection);
            //【3】新建Adapter对象
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
            //【4】新建一个内存数据集
            DataSet dataSet = new DataSet();
            //【5】使用数据适配器
            try
            {
                sqlConnection.Open();
                sqlDataAdapter.Fill(dataSet);
                return dataSet;
            }
            catch (Exception ex)
            {
                throw new Exception("数据访问出现问题，具体原因为：" + ex.Message);
            }
            finally
            {
                //【6】关闭连接
                sqlConnection.Close();
            }
        }
    }
}
