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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WSL2Onenne
{
    /// <summary>
    /// IntTextView.xaml の相互作用ロジック
    /// </summary>
    public partial class IntTextView : UserControl
    {
        public IntTextView()
        {
            InitializeComponent();
        }

        private int _value;

        public int Value
        {
            get
            {
                return _value;
            }
            set
            {
                _value = value;
                text.Text = value.ToString();
            }
        }

        public Brush Fore
        {
            get { return text.Foreground; }
            set { text.Foreground = value; }
        }
    }
}
