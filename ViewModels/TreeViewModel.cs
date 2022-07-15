using PoC.DAL;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;

namespace PoC.ViewModels
{
    public class TreeViewModel : ViewModelBase
    {
        private ObservableCollection<string> _listOfDatabases = new ObservableCollection<string> { "TestDB", "TestDB1" };
        public ObservableCollection<string> ListOfDatabases
        {
            get
            {
                return _listOfDatabases;
            }
        }

        private int _dropdownValue;
        public int DropdownValue
        {
            get
            {
                return _dropdownValue;
            }
            set
            {
                _dropdownValue = value;
                OnPropertyChanged(nameof(DropdownValue));
                Console.WriteLine(_dropdownValue);
            }
        }

        private MainWindowViewModel _mainWindowViewModel;
        public MainWindowViewModel MainWindowViewModel
        {
            get { return _mainWindowViewModel; }
        }

        //public string dbName;
        public TreeViewModel()
        {
            BuildTree();
        }

        public TreeViewModel Tree
        {
            get { return this; }
        }

        private void BuildTree()
        {
            //foreach (Window window in Application.Current.Windows)
            //{
            //    if (window.GetType() == typeof(MainWindow))
            //    {
            //        dbName = (window as MainWindow).DatabaseComboBox.SelectedValue.ToString();
            //    }
            //}
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

                foreach (string dbName in databases)
                {
                    var dbNode = new NodeViewModel { Name = dbName };
                    rootNode.Children.Add(dbNode);
                    if (dbName.Equals("TestDB1"))
                    {
                        DataTable table = connection.GetSchema("Tables");
                        IEnumerable<string> tables = GetNameList(table.Rows, 2);

                        var tableNode = new NodeViewModel { Name = "Tables" };
                        dbNode.Children.Add(tableNode);
                        foreach (string tableName in tables)
                        {
                            tableNode.Children.Add(new NodeViewModel { Name = tableName });
                        }
                    }
                }
            }
        }

        private IEnumerable<string> GetNameList(DataRowCollection drc, int index)
        {
            return drc.Cast<DataRow>().Select(r => r.ItemArray[index].ToString()).OrderBy(r => r).ToList();
        }

        public ObservableCollection<NodeViewModel> Items { get; set; }
    }
}
