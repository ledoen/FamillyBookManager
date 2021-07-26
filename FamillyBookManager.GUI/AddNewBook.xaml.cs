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
using DAL;

namespace FamillyBookManager.GUI
{
    /// <summary>
    /// AddNewBook.xaml 的交互逻辑
    /// </summary>
    public partial class AddNewBook : Window
    {
        //添加Book字段，用于数据绑定
        private Book book = new Book();

        //声明委托及事件，用于刷新主窗口控件
        public delegate void BookAddedEventHandler(object Sender, EventArgs e);
        public event BookAddedEventHandler BookAdded;
        private void OnBookAdded()
        {
            BookAdded?.Invoke(this, new EventArgs());
        }

        public AddNewBook()
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

        private void Window_Closed(object sender, EventArgs e)
        {
            //将增加书籍的窗体实例清零，为下次打开做准备
            MainUI.WindowAddNewBook = null;
        }

        private void buttonForClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        /// <summary>
        /// 增加一本书
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonForAdd_Click(object sender, RoutedEventArgs e)
        {
            #region 数据验证
            //【1】数据验证
            //非空验证
            if (this.textBoxForName.Text.Trim().Length < 1)
            {
                MessageBox.Show("书名不能为空！","提示信息");
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
            //验证书是否已经存在
            if (BookService.IsBookNameExist(this.textBoxForName.Text.Trim()))
            {
                MessageBox.Show("该书已存在", "提示信息");
                return;
            }
            #endregion
            //【添加】
            if (BookService.AddBook(book) == 1)
            {
                MessageBoxResult result = MessageBox.Show("添加成功！是否继续添加？", "提示信息",MessageBoxButton.OKCancel);
                if (result == MessageBoxResult.OK)
                {
                    EmptyBook();
                }
                else
                {
                    //关闭窗口
                    this.Close();
                }
                //更新列表信息
                OnBookAdded();
            }
            else
            {
                MessageBox.Show("添加失败！", "提示信息");
                return;
            }
        }
        private void EmptyBook()
        {
            book.Author = "";
            book.BookName = "";
            book.BookType = "";
            book.Discribe = "";
            book.Grade = "";
            book.IsExist = "";
            book.Location = "";
            book.Publisher = "";
            book.ReadingStatus = "";
        }
    }
}
