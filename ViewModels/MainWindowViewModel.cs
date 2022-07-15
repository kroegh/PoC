using PoC.Commands;
using PoC.DAL;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace PoC.ViewModels
{
    public class MainWindowViewModel: ViewModelBase
    {
        public RelayCommand ClearSqlQueryCommand { get; private set; }
        public RelayCommand ClearFilterCommand { get; private set; }
        public RelayCommand ClearFilterByIdCommand { get; private set; }

        private IEnumerable<object> dataSet = new ObservableCollection<object>();
        private string sqlQueryString;
        private string filterString;
        private string filterByIdString;

        public IEnumerable<object> DataSet
        {
            get
            {
                return dataSet;
            }
            set
            {
                dataSet = value;
                OnPropertyChanged(nameof(DataSet));
            }
        }

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
            DataAccess db = new DataAccess();
            string query = "select * from employees";
            DataSet = db.GetEmployees(query);
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
