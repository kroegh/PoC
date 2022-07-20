using PoC.Commands;
using PoC.DAL;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace PoC.ViewModels
{
    public class MainWindowViewModel: ViewModelBase
    {
        public RelayCommand ClearSqlQueryCommand { get; private set; }
        public RelayCommand ExecuteQueryCommand { get; private set; }
        public RelayCommand ClearFilterCommand { get; private set; }
        public RelayCommand ClearFilterByIdCommand { get; private set; }
        public RelayCommand FilterCommand { get; private set; }
        public RelayCommand FilterByIdCommand { get; private set; }
        public RelayCommand SelectDbCommand { get; private set; }

        private DataView dataSet;
        private string sqlQueryString;
        private string filterString;
        private string filterByIdString;
        private DataTable unfilteredDT;

        public MainWindowViewModel Tree
        {
            get { return this; }
        }

        public DataView DataSet
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
            BuildTree();
            ClearSqlQueryCommand = new RelayCommand(ClearSqlQuery, CanClearSqlQuery);
            ExecuteQueryCommand = new RelayCommand(ExecuteQuery, CanExecuteQuery);

            ClearFilterCommand = new RelayCommand(ClearFilter, CanClearFilter);
            FilterCommand = new RelayCommand(Filter, CanFilter);

            ClearFilterByIdCommand = new RelayCommand(ClearFilterById, CanClearFilterById);
            FilterByIdCommand = new RelayCommand(FilterById, CanFilterById);

            //DataAccess db = new DataAccess();
            //string query = "select * from employees";
            //dataSet = db.GetEmployees(query).DefaultView;
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
            FilterString = string.Empty;
        }

        public bool CanClearFilter(object parameter)
        {
            return true;
        }

        public void ClearFilterById(object parameter)
        {
            FilterByIdString = string.Empty;
        }

        public bool CanClearFilterById(object parameter)
        {
            return true;
        }

        public void ExecuteQuery(object parameter)
        {
            string dbName = "";
            foreach (var collection in Items)
            {
                foreach (var item in collection.Children)
                {
                    if (item.IsSelected)
                    {
                        dbName = item.Name;
                    }
                }
            }
            DataAccess da = new DataAccess();
            DataTable dt = da.GetEmployees(sqlQueryString, dbName);
            DataSet = dt.DefaultView;
            unfilteredDT = dt;

        }

        public bool CanExecuteQuery(object parameter)
        {
            return true;
        }

        public void Filter(object parameter)
        {
            DataTable dt;
            var rows = from a in unfilteredDT.AsEnumerable()
                       where string.Join(",", a.ItemArray).ToLower().Contains(FilterString.ToLower())
                       select a;
            dt = rows.CopyToDataTable();
            DataSet = dt.DefaultView;
        }

        public bool CanFilter(object parameter)
        {
            return true;
        }

        public void FilterById(object parameter)
        {
            DataTable dt;
            var rows = from a in unfilteredDT.AsEnumerable()
                       where a.ItemArray[0].ToString().Equals(FilterByIdString)
                       select a;
            dt = rows.CopyToDataTable();
            DataSet = dt.DefaultView;
        }

        public bool CanFilterById(object parameter)
        {
            return true;
        }

        private void BuildTree()
        {
            using (var connection = new SqlConnection(Helper.CnnVal("TestDB")))
            {
                // Connect to the database then retrieve the schema information.
                connection.Open();

                // Get the schema information of Databases in your instance
                DataTable databasesSchemaTable = connection.GetSchema("Databases");


                Items = new ObservableCollection<NodeViewModel>();
                var rootNode = new NodeViewModel
                {
                    Name = "Databases",
                    Children = new ObservableCollection<NodeViewModel>()
                };
                Items.Add(rootNode);

                IEnumerable<string> databases = GetNameList(databasesSchemaTable.Rows, 0);

                //foreach (string dbName in databases)
                //{
                //    var dbNode = new NodeViewModel { Name = dbName };
                //    rootNode.Children.Add(dbNode);
                //    if (dbName.Equals("TestDB"))
                //    {
                //        DataTable table = connection.GetSchema("Tables");
                //        IEnumerable<string> tables = GetNameList(table.Rows, 2);

                //        var tableNode = new NodeViewModel { Name = "Tables" };
                //        dbNode.Children.Add(tableNode);
                //        foreach (string tableName in tables)
                //        {
                //            tableNode.Children.Add(new NodeViewModel { Name = tableName });
                //        }
                //    }
                //}
            }
        }

        private IEnumerable<string> GetNameList(DataRowCollection drc, int index)
        {
            return drc.Cast<DataRow>().Select(r => r.ItemArray[index].ToString()).OrderBy(r => r).ToList();
        }
        public ObservableCollection<NodeViewModel> Items { get; set; }
    }
}
