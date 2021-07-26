using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

using Model;
using System.Data;
using DAL;

namespace FamillyBookManager.GUI
{
    /// <summary>
    /// ModifyBook.xaml 的交互逻辑
    /// </summary>
    public partial class ModifyBook : Window
    {
        //添加book对象用于数据绑定
        private Book book = new Book();

        public ModifyBook()
        {
            DataContext = book;
            InitializeComponent();
            //设置下拉框的数据源
            this.comboBoxForType.ItemsSource = BookProperties.BookTypes;
            this.comboBoxForGrade.ItemsSource = BookProperties.Grades;
            this.comboBoxForIsExist.ItemsSource = BookProperties.IsExists;
            this.comboBoxForLocation.ItemsSource = BookProperties.Locations;
            this.comboBoxForReadingStatus.ItemsSource = BookProperties.ReadingStatuses;
        }

        /// <summary>
        /// 重构带参数的构造函数，使用传递进来的参数对book进行封装
        /// </summary>
        /// <param name="dataRow"></param>
        public ModifyBook( DataRow dataRow)
            :this()
        {
            book.BookID = Convert.ToInt32(dataRow["BookID"]);
            book.Author = dataRow["Author"].ToString();
            book.ReadingStatus = dataRow["ReadingStatus"].ToString();
            book.BookName = dataRow["BookName"].ToString();
            book.BookType = dataRow["BookType"].ToString();
            book.Discribe = dataRow["Discribe"].ToString();
            book.Grade = dataRow["Grade"].ToString();
            book.IsExist = dataRow["IsExist"].ToString();
            book.Location = dataRow["Location"].ToString();
            book.Publisher = dataRow["Publisher"].ToString();

            this.Title += $"   书号:{book.BookID}";
        }

        private void buttonForAdd_Click(object sender, RoutedEventArgs e)
        {
            #region 数据验证
            //【1】数据验证
            //非空验证
            if (this.textBoxForName.Text.Trim().Length < 1)
            {
                MessageBox.Show("书名不能为空！", "提示信息");
                this.textBoxForName.Focus();
                return;
            }
            if (this.textBoxForAuthor.Text.Trim().Length < 1)
            {
                MessageBox.Show("作者不能为空！", "提示信息");
                this.textBoxForAuthor.Focus();
                return;
            }
            if (this.textBoxForPublisher.Text.Trim().Length < 1)
            {
                MessageBox.Show("出版社不能为空！", "提示信息");
                this.textBoxForPublisher.Focus();
                return;
            }
            if (this.comboBoxForType.Text.Trim().Length < 1)
            {
                MessageBox.Show("请选择类型！", "提示信息");
                return;
            }
            //验证修改过的书名是否已经存在
            if (BookService.IsBookNameExist(this.textBoxForName.Text.Trim(), book.BookID.ToString()))
            {
                MessageBox.Show("该书已存在", "提示信息");
                return;
            }
            #endregion
            //【添加】
            if (BookService.ModityBookInfo(book) == 1)
            {
                MessageBox.Show("修改成功！", "提示信息");
                this.DialogResult = true;
                this.Close();
            }
            else
            {
                MessageBox.Show("添加失败！", "提示信息");
                return;
            }
        }

        private void buttonForClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
