using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using WpfMVVM.Commands;

namespace PoC.ViewModels
{
    class MainWindowViewModel
    {
        public RelayCommand ClearSqlQueryCommand { get; private set; }
        public RelayCommand ClearFilterCommand { get; private set; }
        public RelayCommand ClearFilterByIdCommand { get; private set; }

        public event PropertyChangedEventHandler PropertyChanged;
        private string sqlQueryString = string.Empty;
        private string filterString = string.Empty;
        private string filterByIdString = string.Empty;
        public string SqlQueryString
        {
            get
            {
                return sqlQueryString;
            }
            set
            {
                sqlQueryString = value;
                OnPropertyChanged(nameof(SqlQueryString));
            }
        }

        public string FilterString
        {
            get
            {
                return filterString;
            }
            set
            {
                filterString = value;
                OnPropertyChanged(nameof(FilterString));
            }
        }

        public string FilterByIdString
        {
            get
            {
                return filterByIdString;
            }
            set
            {
                filterByIdString = value;
                OnPropertyChanged(nameof(FilterByIdString));
            }
        }

        public MainWindowViewModel()
        {
            ClearSqlQueryCommand = new RelayCommand(ClearSqlQuery, CanClearSqlQuery);
            ClearFilterCommand = new RelayCommand(ClearFilter, CanClearFilter);
            ClearFilterByIdCommand = new RelayCommand(ClearFilterById, CanClearFilterById);
        }

        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public void ClearSqlQuery(object parameter)
        {
            SqlQueryString = string.Empty;
        }

        public bool CanClearSqlQuery(object parameter)
        {
            return true;
        }

        public void ClearFilter(object parameter)
        {

        }

        public bool CanClearFilter(object parameter)
        {
            return true;
        }

        public void ClearFilterById(object parameter)
        {

        }

        public bool CanClearFilterById(object parameter)
        {
            return true;
        }
    }
}
