using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using ZealandDimselab.WPF.DtoModels;
using ZealandDimselab.WPF.Extensions;
using ZealandDimselab.WPF.Services;

namespace ZealandDimselab.WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly HttpCategoryService _categoryService;

        public ObservableCollection<CategoryDto> Categories { get; set; } = new ObservableCollection<CategoryDto>();
        public MainWindow(HttpCategoryService categoryService)
        {
            
            
            _categoryService = categoryService;
            LoadCategoriesAsync();
            InitializeComponent();
            catagoryList.ItemsSource = Categories;
        }

        private async void LoadCategoriesAsync()
        {
            var tempCategories = (await _categoryService.GetAllCategoriesAsync()).ToObserableCollection();


            foreach (var category in tempCategories)
            {
                Categories.Add(category);
            }

        }
    }
}
