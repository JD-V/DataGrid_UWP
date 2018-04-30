using DataGridWithDataBinding.ViewModels;
using MyToolkit.Collections;
using MyToolkit.Controls;
using MyToolkit.Utilities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using DataGridWithDataBinding.Models;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace DataGridWithDataBinding
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    /// 

    public sealed partial class MainPage : Page
    {
        private DataGridColumnBase _removedColumn;
        public MainPage()
        {
            this.InitializeComponent();
        }


        public DataGridPageModel ViewModel { get { return (DataGridPageModel)Resources["ViewModel"]; } }

        private void OnSelectLastPerson(object sender, RoutedEventArgs e)
        {
            ViewModel.SelectedPerson = ViewModel.People
                .Where(p => p != ViewModel.SelectedPerson).ToList()
                .TakeRandom(1).Single();
        }

        private void OnRemoveFirstColumn(object sender, RoutedEventArgs e)
        {
            if (DataGrid.Columns.Count > 0)
            {
                _removedColumn = DataGrid.Columns.First();
                DataGrid.Columns.Remove(_removedColumn);
            }
        }

        private void OnAddRemovedColumn(object sender, RoutedEventArgs e)
        {
            if (_removedColumn != null)
            {
                DataGrid.Columns.Add(_removedColumn);
                _removedColumn = null;
            }
        }

        private void OnFilterChanged(object sender, TextChangedEventArgs e)
        {
            var filter = ((TextBox)sender).Text;
            if (string.IsNullOrEmpty(filter))
                DataGrid.RemoveFilter();
            else
            {
                filter = filter.ToLower();
                DataGrid.SetFilter<Person>(p => p.Firstname.ToLower().Contains(filter) || p.Lastname.ToLower().Contains(filter));
            }
        }


        private void OnSelectedItemsChanged(object sender, SelectionChangedEventArgs e)
        {
            SelectedItems.Text = string.Join(", ", DataGrid.SelectedItems.OfType<Person>().Select(p => p.Firstname));
        }

        private void OnAdd10Elements(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < 10; i++)
            {
                ViewModel.People.Add(new Person
                {
                    Firstname = "Firstname" + i,
                    Lastname = "Lastname" + i,
                    Category = "Category" + i
                });
            }
        }
    }
}
