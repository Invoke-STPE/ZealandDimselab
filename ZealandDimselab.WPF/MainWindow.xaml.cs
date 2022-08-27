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
using ZealandDimselab.WPF.Cart;

namespace ZealandDimselab.WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly HttpCategoryService _categoryService;
        private readonly HttpItemService _itemService;

        public ObservableCollection<CategoryDto> Categories { get; set; } = new ObservableCollection<CategoryDto>();
        public ObservableCollection<ItemDto> Items { get; set; } = new ObservableCollection<ItemDto>();
        public int CartCount { get; set; } = 0;
        public MainWindow(HttpCategoryService categoryService, HttpItemService itemService)
        {
            _categoryService = categoryService;
            _itemService = itemService;
            LoadCategoriesAsync();
            LoadItemsAsync();
            InitializeComponent();
            catagoryList.ItemsSource = Categories;
            itemList.ItemsSource = Items;
            cartText.Text = CartCount.ToString();
        }

        private async void LoadCategoriesAsync()
        {
            CategoryDto allCategory = new CategoryDto { CategoryId = 0, CategoryName = "All", ImageName = null };
            var tempCategories = (await _categoryService.GetAllCategoriesAsync()).ToList();
            tempCategories.Insert(0, allCategory);
            foreach (var category in tempCategories)
            {
                Categories.Add(category);
            }
        }

        private async void LoadItemsAsync()
        {
            var tempItems = await _itemService.GetAllItemsAsync();

            // Pick up here
            foreach (var item in tempItems)
            {
                string imagePath = $"C:\\Users\\Steven\\source\\repos\\ZealandDimselab\\ZealandDimselab.WPF\\Images\\{item.ImageName}";
                item.ImageName = imagePath;
                Items.Add(item);
            }
        }

        private async void TextBlock_MouseUp(object sender, MouseButtonEventArgs e)
        {
            TextBlock textBlock = (TextBlock)sender;
            CategoryDto category = (CategoryDto)textBlock.DataContext;
            List<ItemDto> tempItems = new List<ItemDto>();

            tempItems = category.CategoryName == "All"
                ? await _itemService.GetAllItemsAsync()
                : await _itemService.GetItemsWithCategoryIdAsync(category.CategoryId);
            Items.Clear();

            foreach (ItemDto item in tempItems)
            {
                
                Items.Add(item);
            }
        }
        private void CartButton_Click(object sender, RoutedEventArgs e)
        {
            CartEntry cartEntry = new CartEntry(Cart.Cart.GetCartItems());
            cartEntry.Show();
        }

        private void AddItemToCart_Click(object sender, RoutedEventArgs e)
        {
            MenuItem menuItem = (MenuItem)sender;

            ItemDto itemDto = (ItemDto)menuItem.DataContext;

            Cart.Cart.AddToCart(itemDto);
            cartText.Text = Cart.Cart.GetCartCount().ToString();
        }
    }
}
