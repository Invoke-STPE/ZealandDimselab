using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using ZealandDimselab.WPF.DtoModels;

namespace ZealandDimselab.WPF.Extensions
{
    // Watch https://www.youtube.com/watch?v=l6s7AvZx5j8 to make this type conversion generic
    public static class Extensions
    {
        public static ObservableCollection<T> ToObserableCollection<T>(this IEnumerable<T> collection)
        {
            return new ObservableCollection<T>(collection);
        }
    }
}
