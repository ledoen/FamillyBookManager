using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace Model
{
    public class Book : INotifyPropertyChanged
    {
        private int bookID;
        private string bookName;
        private string author;
        private string publisher;
        private string bookType;
        private string location;
        private string grade;
        private string discribe;
        private string readingStatus;
        private string isExist;

        public int BookID 
        {
            get { return bookID; } 
            set
            {
                bookID = value;
                OnPropertyChanged(nameof(BookID));
            }
        }
        public string BookName
        {
            get { return bookName; }
            set
            {
                bookName = value;
                OnPropertyChanged(nameof(BookName));
            }
        }
        public string Author
        {
            get { return author; }
            set
            {
                author = value;
                OnPropertyChanged(nameof(Author));
            }
        }
        public string Publisher
        {
            get { return publisher; }
            set
            {
                publisher = value;
                OnPropertyChanged(nameof(Publisher));
            }
        }
        public string BookType
        {
            get { return bookType; }
            set
            {
                bookType = value;
                OnPropertyChanged(nameof(BookType));
            }
        }
        public string Location
        {
            get { return location; }
            set
            {
                location = value;
                OnPropertyChanged(nameof(Location));
            }
        }
        public string Grade
        {
            get { return grade; }
            set
            {
                grade = value;
                OnPropertyChanged(nameof(Grade));
            }
        }
        public string Discribe
        {
            get { return discribe; }
            set
            {
                discribe = value;
                OnPropertyChanged(nameof(Discribe));
            }
        }
        public string ReadingStatus
        {
            get { return readingStatus; }
            set
            {
                readingStatus = value;
                OnPropertyChanged(nameof(ReadingStatus));
            }
        }
        public string IsExist
        {
            get { return isExist; }
            set
            {
                isExist = value;
                OnPropertyChanged(nameof(IsExist));
            }
        }

        #region 事件相关
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
