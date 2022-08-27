using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using ZealandDimselab.WPF.DtoModels;

namespace ZealandDimselab.WPF
{
    /// <summary>
    /// Interaction logic for CartEntry.xaml
    /// </summary>
    public partial class CartEntry : Window
    {
        public CartEntry(List<ItemDto> items)
        {
            InitializeComponent();
            cartItemsList.ItemsSource = items;
        }
    }
}
