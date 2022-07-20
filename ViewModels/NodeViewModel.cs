using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoC.ViewModels
{
    public class NodeViewModel : ViewModelBase
    {
        public NodeViewModel()
        {
            Children = new ObservableCollection<NodeViewModel>();
        }

        static NodeViewModel()
        {
        }
        public string Id { get; set; }
        public string Name { get; set; }
        public ObservableCollection<NodeViewModel> Children { get; set; }

        private bool isSelected;
        public bool IsSelected
        {
            get
            {
                return isSelected;
            }
            set
            {
                isSelected = value;
                OnPropertyChanged(nameof(IsSelected));
            }
        }
    }
}
