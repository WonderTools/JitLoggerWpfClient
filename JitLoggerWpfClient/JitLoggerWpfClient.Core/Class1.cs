using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Prism.Commands;

namespace JitLoggerWpfClient.Core
{
    public class Class1
    {
    }

    public class JitLoggerViewModel
    {
        public string Name { get; set; }
        public string Url { get; set; }
        public int RefreshPeriodInSeconds { get; set; }
        public DelegateCommand  RemoveCommand { get; set; }
    }


    public class JitLoggerAddingViewModel
    {
        public string Name { get; set; }
        public string Url { get; set; }
        public int RefreshPeriodInSeconds { get; set; }
        public DelegateCommand AddCommand { get; set; }
    }

    public class JitLoggerSourceViewModel
    {
        public JitLoggerSourceViewModel(Action closeAction)
        {
            CloseCommand = new DelegateCommand(closeAction);
        }

        public DelegateCommand CloseCommand { get; set; }
        public ObservableCollection<JitLoggerAddingViewModel> JitLoggers { get; set; }
        public JitLoggerAddingViewModel Adder { get; set; }
    }

    public class MainViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public MainViewModel()
        {
            IsContentMenuEnabled = true;
            IsSourcesVisible = false;
            ShowSourceCommand = new DelegateCommand(ShowSource);
            SourceViewModel = new JitLoggerSourceViewModel(HideSource);
        }

        private void ShowSource()
        {
            IsSourcesVisible = true;
            IsContentMenuEnabled = false;
        }

        private void HideSource()
        {
            IsSourcesVisible = false;
            IsContentMenuEnabled = true;
        }

        private bool _isSourcesVisible;
        private bool _isContentMenuEnabled;

        public bool IsSourcesVisible
        {
            get => _isSourcesVisible;
            set
            {
                if (_isSourcesVisible != value)
                {
                    _isSourcesVisible = value;
                    FirePropertyChanged();
                }
            }
        }

        public bool IsContentMenuEnabled
        {
            get => _isContentMenuEnabled;
            set
            {
                if (_isContentMenuEnabled != value)
                {
                    _isContentMenuEnabled = value;
                    FirePropertyChanged();
                }
            }
        }

        public DelegateCommand ShowSourceCommand { get; set; }

        public JitLoggerSourceViewModel SourceViewModel { get; set; }

        protected virtual void FirePropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

}
