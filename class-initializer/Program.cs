using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace class_initializer
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var someClass = new SomeClass()
                {
                    Guid = Guid.NewGuid().ToString(),
                    InduceFail = 10.55,
                    WillReachThisLine = false
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine();
                Console.WriteLine("Exception Info: " + ex.StackTrace);
            }

            // Pause
            Console.ReadKey();
        }
    }
    class SomeClass : INotifyPropertyChanging , INotifyPropertyChanged
    {
        // D E S I G N E D    T O    F A I L
        public object InduceFail
        {
            get => _induceFail.ToString();
            set
            {
                OnPropertyChanging(new PropertyChangingEventArgs(nameof(InduceFail)));
                _induceFail = (int)value;
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(InduceFail)));
            }
        }
        private int _induceFail = 0;

        // D E S I G N E D    T O     S U C C E E D
        public string Guid 
        {
            get => _guid;
            set
            {
                if(value != _guid)
                {
                    OnPropertyChanging(new PropertyChangingEventArgs(nameof(Guid)));
                    _guid = value;
                    OnPropertyChanged(new PropertyChangedEventArgs(nameof(Guid)));
                }
            }
        }

        private string _guid;

        public bool WillReachThisLine 
        {
            get => _willReachThisLine;

            set
            {
                if (value != _willReachThisLine)
                {
                    OnPropertyChanging(new PropertyChangingEventArgs(nameof(Guid)));
                    _willReachThisLine = value;
                    OnPropertyChanged(new PropertyChangedEventArgs(nameof(Guid)));
                }
            }
        }
        private bool _willReachThisLine;


        public event PropertyChangingEventHandler PropertyChanging;
        protected virtual void OnPropertyChanging(PropertyChangingEventArgs e)
        {
            PropertyChanging?.Invoke(this, e);
            Console.WriteLine("The " + e.PropertyName + " property is changing.");
        }
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            PropertyChanged?.Invoke(this, e);
            Console.WriteLine("The " + e.PropertyName + " property has changed." + Environment.NewLine);
        }
    }
}
