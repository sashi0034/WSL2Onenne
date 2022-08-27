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
    enum ECommandViewState
    {
        WaitForStart,
        WaitForStop,
    }

    /// <summary>
    /// CommandView.xaml の相互作用ロジック
    /// </summary>
    public partial class CommandView : UserControl
    {
        public CommandView()
        {
            InitializeComponent();
            init();
        }

        private void init()
        {
            setState(ECommandViewState.WaitForStart);
            AddEvent(
                () => { setState(ECommandViewState.WaitForStop); },
                () => { setState(ECommandViewState.WaitForStart); });
        }

        public void AddEvent(Action onStart, Action onStop)
        {
            buttonStart.Click += (_, _) => { onStart(); };
            buttonStop.Click += (_, _) => { onStop(); };
        }

        private void setState(ECommandViewState state)
        {
            gridStart.Visibility = state == ECommandViewState.WaitForStart ? Visibility.Visible : Visibility.Collapsed;
            gridStop.Visibility = state == ECommandViewState.WaitForStop ? Visibility.Visible : Visibility.Collapsed;
        }

        public string? TextStart
        {
            get { return buttonStart.Content as string; }
            set { buttonStart.Content = value; }
        }
        public string? TextStop
        {
            get { return buttonStop.Content as string; }
            set { buttonStop.Content = value; }
        }
    }
}
