using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using CinnaBerry.ViewModels;

namespace CinnaBerry.Views;

public partial class CreateUserView : UserControl
{
    public CreateUserView()
    {
        DataContext = new CreateUserViewModel();
        InitializeComponent();
    }
}