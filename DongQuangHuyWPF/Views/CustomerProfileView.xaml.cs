using BusinessLayer;
using DataLayer;
using DongQuangHuyWPF.ViewModels;
using System.Windows.Controls;

namespace DongQuangHuyWPF.Views
{
    /// <summary>
    /// Interaction logic for CustomerProfileView.xaml
    /// </summary>
    public partial class CustomerProfileView : UserControl
    {
        public CustomerProfileView(Customer customer)
        {
            InitializeComponent();
            DataContext = customer; // The DataContext is the Customer object itself
        }
    }
} 