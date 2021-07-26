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

using System.Data;
using DAL;
using Model;

namespace FamillyBookManager.GUI
{
    /// <summary>
    /// MainUI.xaml 的交互逻辑
    /// </summary>
    public partial class MainUI : Window
    {
        //添加窗体成员
        public static AddNewBook WindowAddNewBook = null;
        //添加datatable成员，与datagrid配合使用
        private DataTable dataTable = new DataTable();

        /// <summary>
        /// 构造函数，初始化下拉控件即图书总数
        /// </summary>
        public MainUI()
        {
            InitializeComponent();
            //更新并绑定dataTable到dataGrid的数据源
            UpdateBookTable();
            this.dataGridForDisplay.DataContext = dataTable;

            this.comboBoxForType.ItemsSource = BookProperties.BookTypes;
            this.comboBoxForGrade.ItemsSource = BookProperties.Grades;
            this.comboBoxForLocation.ItemsSource = BookProperties.Locations;
            this.comboBoxForCountType.ItemsSource = new string[] {"分类","评分","位置","阅读进度","状态" };
            this.comboBoxForCountType.SelectedIndex = 0;
        }

        #region 增加图书
        private void WindowAddNewBook_BookAdded(object Sender, EventArgs e)
        {
            //刷新dataTable及统计信息
            UpdateBookTable();
        }

        /// <summary>
        /// 添加新书按钮响应
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonForAddBook_Click(object sender, RoutedEventArgs e)
        {
            //产生添加书籍窗口
            if (WindowAddNewBook == null)
            {
                WindowAddNewBook = new AddNewBook();
                //注册AddedBook事件处理函数
                WindowAddNewBook.BookAdded += WindowAddNewBook_BookAdded;
                WindowAddNewBook.Show();
            }
            else
            {
                WindowAddNewBook.Activate();
                WindowAddNewBook.WindowState = WindowState.Normal;
            }
        }
        #endregion

        #region 查询功能
        /// <summary>
        /// 书号查询按钮响应
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonForSearchByID_Click(object sender, RoutedEventArgs e)
        {
            //判空
            if (this.textBoxForBookID.Text.Trim().Length < 1)
            {
                MessageBox.Show("请填写书号！","提示信息");
                return;
            }
            //判断是否为整数
            if (DataValidate.IsInteger(this.textBoxForBookID.Text.Trim()) == false)
            {
                MessageBox.Show("请输入整数！", "提示信息");
                return;
            }
            //调用查询
            dataTable.DefaultView.RowFilter = $"BookID = {this.textBoxForBookID.Text.Trim()}";
            if (dataTable.DefaultView.Count < 1)
            {
                MessageBox.Show("未找到！", "提示信息");
            }
        }

        /// <summary>
        /// 书名模糊查询按钮响应
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonForSearchByName_Click(object sender, RoutedEventArgs e)
        {
            //判空
            if (this.textBoxForBookName.Text.Trim().Length < 1)
            {
                MessageBox.Show("请填写书号！", "提示信息");
                return;
            }

            //调用查询
            dataTable.DefaultView.RowFilter = $"BookName like '%{this.textBoxForBookName.Text.Trim()}%'";
            if (dataTable.DefaultView.Count < 1)
            {
                MessageBox.Show("未找到！", "提示信息");
            }
        }
        #endregion

        #region 获取全部图书信息
        /// <summary>
        /// 显示全部图书响应
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonForAllBooks_Click(object sender, RoutedEventArgs e)
        {
            //从数据库更新dataTable
            UpdateBookTable();
            if (dataTable.Rows.Count < 1)
            {
                MessageBox.Show("没有图书！", "提示信息");
            }
        }
        /// <summary>
        /// 更新dataTable
        /// </summary>
        private void UpdateBookTable()
        {
            try
            {
                DataSet dataSet = BookService.GetAllBooks();
                dataTable = dataSet.Tables[0];
                //每次更新dataTable，必须重新绑定datagrid，因为dataTable为引用类型，之前绑定的对象已失效
                this.dataGridForDisplay.DataContext = dataTable;
                UpdateBookCount();
                comboBoxForCountType_SelectionChanged(null,null);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
        }
        #endregion

        #region 分别使用三个下拉框选择分类查看
        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //判断是否为空
            if (this.comboBoxForType.SelectedIndex < 0)
            {
                return;
            }
            //将其它两个combox清空
            this.comboBoxForGrade.SelectedIndex = -1;
            this.comboBoxForLocation.SelectedIndex = -1;
            //筛选
            dataTable.DefaultView.RowFilter = $"BookType='{this.comboBoxForType.SelectedItem.ToString()}'";
        }

        private void comboBoxForGrade_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //判断是否为空
            if (this.comboBoxForGrade.SelectedIndex < 0)
            {
                return;
            }
            //将其它两个combox清空
            this.comboBoxForType.SelectedIndex = -1;
            this.comboBoxForLocation.SelectedIndex = -1;
            //筛选
            //dataTable.DefaultView.RowFilter = "";
            dataTable.DefaultView.RowFilter = $"Grade='{this.comboBoxForGrade.SelectedItem.ToString()}'";
        }

        private void comboBoxForLocation_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //判断是否为空
            if (this.comboBoxForLocation.SelectedIndex < 0)
            {
                return;
            }
            //将其它两个combox清空
            this.comboBoxForGrade.SelectedIndex = -1;
            this.comboBoxForType.SelectedIndex = -1;
            //筛选
            //dataTable.DefaultView.RowFilter = "";
            dataTable.DefaultView.RowFilter = $"Location='{this.comboBoxForLocation.SelectedItem.ToString()}'";
        }
        #endregion

        #region 编辑图书
        /// <summary>
        /// 编辑书籍响应
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonForModify_Click(object sender, RoutedEventArgs e)
        {
            //判断datagrid控件是否有行被选中
            if (this.dataGridForDisplay.SelectedItem == null)
            {
                MessageBox.Show("未选中书籍信息","提示信息");
                return;
            }
            //获得DataRow对象
            DataRow dataRow = ((DataRowView)this.dataGridForDisplay.SelectedItem).Row;
            //object obj = this.dataGridForDisplay.SelectedItem;
            //obj = ((DataRowView)obj).Row["BookID"];

            //弹出窗口
            ModifyBook modifyBook = new ModifyBook(dataRow);
            modifyBook.ShowDialog();
            if (modifyBook.DialogResult.Value)
            {
                //修改成功，调用一次按ID查询
                UpdateBookTable();
                string tempID = dataRow["BookID"].ToString();
                this.textBoxForBookID.Text = tempID;
                buttonForSearchByID_Click(null,null);
                this.textBoxForBookID.Text = "";
            }
        }
        /// <summary>
        /// 右键菜单响应
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void contextMenuModify_Click(object sender, RoutedEventArgs e)
        {
            //判断是否有数据
            if (this.dataGridForDisplay.Items.Count < 1)
            {
                MessageBox.Show("没有数据","提示信息");
                return;
            }
            //判断是否有选中行
            if (this.dataGridForDisplay.SelectedItem == null)
            {
                MessageBox.Show("请选中一行", "提示信息");
                return;
            }
            //调用修改
            buttonForModify_Click(null,null);
        }
        #endregion

        #region 统计信息展示
        /// <summary>
        /// 更新状态栏统计信息，每次更新列表后自动运行
        /// </summary>
        private void UpdateBookCount()
        {
            int totalCount = dataTable.Select("IsExist = '在架'").Count();
            this.labelForBookCount.Content = $"{totalCount}本";
        }
        /// <summary>
        /// 分类显示统计信息响应
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboBoxForCountType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dataTable.Rows.Count < 1)
            {
                return;
            }
            if (this.comboBoxForCountType.SelectedIndex == -1)
            {
                return;
            }
            switch (this.comboBoxForCountType.SelectedItem.ToString())
            {
                case "分类":
                    //按分类获取个数，并组装string语句
                    string labelContent = "";
                    foreach (string item in BookProperties.BookTypes)
                    {
                        int bookCount = dataTable.Select($"BookType = '{item}' and IsExist = '在架'").Count();
                        labelContent += $"{item}类:{bookCount}本 \n";
                    }
                    //将string语句展示到label控件
                    this.LabelForTypeBookCount.Content = labelContent;
                    break;
                case "评分":
                    labelContent = "";
                    foreach (string item in BookProperties.Grades)
                    {
                        int bookCount = dataTable.Select($"Grade = '{item}' and IsExist = '在架'").Count();
                        labelContent += $"{item}分:{bookCount}本 \n";
                    }
                    this.LabelForTypeBookCount.Content = labelContent;
                    break;
                case "阅读进度":
                    //按阅读进度获取个数，并组装string语句
                    labelContent = "";
                    foreach (string item in BookProperties.ReadingStatuses)
                    {
                        int bookCount = dataTable.Select($"ReadingStatus = '{item}' and IsExist = '在架'").Count();
                        labelContent += $"{item}:{bookCount}本 \n";
                    }
                    //将string语句展示到label控件
                    this.LabelForTypeBookCount.Content = labelContent;
                    break;
                case "状态":
                    //按状态获取个数，并组装string语句
                    labelContent = "";
                    foreach (string item in BookProperties.IsExists)
                    {
                        int bookCount = dataTable.Select($"IsExist = '{item}'").Count();
                        labelContent += $"{item}:{bookCount}本 \n";
                    }
                    //将string语句展示到label控件
                    this.LabelForTypeBookCount.Content = labelContent;
                    break;
                case "位置":
                    //按位置获取个数，并组装string语句
                    labelContent = "";
                    foreach (string item in BookProperties.Locations)
                    {
                        int bookCount = dataTable.Select($"Location = '{item}' and IsExist = '在架'").Count();
                        labelContent += $"{item}:{bookCount}本 \n";
                    }
                    //将string语句展示到label控件
                    this.LabelForTypeBookCount.Content = labelContent;
                    break;
                default:
                    break;
            }
        }
    }
    #endregion
}
