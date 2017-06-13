using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordNator.Models
{
    public class LessonQuestionsVM : INotifyPropertyChanged
    {
        private string englishWord;
        public string EnglishWord
        {
            get { return this.englishWord; }
            set
            {
                if(this.englishWord!=value)
                {
                    this.englishWord = value;
                    this.NotifyPropertyChanged("EnglishWord");
                }
            }
        }

        private string polishWord;
        public string PolishWord
        {
            get { return this.polishWord; }
            set
            {
                if(this.polishWord!=value)
                {
                    this.polishWord = value;
                    this.NotifyPropertyChanged("PolishWord");
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged(string propName)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }
    }
}
