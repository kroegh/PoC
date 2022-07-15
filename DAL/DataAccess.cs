﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using System.Text.RegularExpressions;
using System.Windows;
using System.Collections.ObjectModel;
using System.Data.SqlClient;

namespace PoC.DAL
{
    public class DataAccess
    {
        public string dbName = "TestDB";
        public IEnumerable<object> GetEmployees(string country)
        {
            //DataTable dt = new DataTable();
            //foreach (Window window in Application.Current.Windows)
            //{
            //    if (window.GetType() == typeof(MainWindow))
            //    {
            //        dbName = (window as MainWindow).DatabaseComboBox.SelectedValue.ToString();
            //    }
            //}
            using (var connection = new SqlConnection(Helper.CnnVal(dbName)))
            {
                string pattern = @"(?<= from\s).*(?=\swhere)";

                string TableName = Regex.Match(country, pattern).Value;

                var q =
                from a in connection.Query<object>(country)
                select a;

                return q;
                //using (SqlDataAdapter da = new SqlDataAdapter(country, connection = new SqlConnection(Helper.CnnVal(dbName))))
                //{
                //da.Fill(dt);
                //return dt;
                //}
                //}

            }
        }

        public DataTable GetDbSchema()
        {
            //foreach (Window window in Application.Current.Windows)
            //{
            //    if (window.GetType() == typeof(MainWindow))
            //    {
            //        dbName = (window as MainWindow).DatabaseComboBox.SelectedValue.ToString();
            //    }
            //}
            using (var connection = new SqlConnection(Helper.CnnVal(dbName)))
            {
                connection.Open();
                DataTable dt = connection.GetSchema("Tables");
                foreach (DataRow row in dt.Rows)
                {
                    Console.WriteLine(row.ItemArray[1]);
                    Console.WriteLine(row.ItemArray[2]);
                }
                connection.Close();
                return dt;
            }
        }
    }
}
