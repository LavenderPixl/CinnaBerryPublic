using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using CinnaBerry.ViewModels;

namespace CinnaBerry;

public partial class SetContentArea : UserControl
{
    static SetContentArea? Instance;

    public SetContentArea()
    {
        InitializeComponent();
        if (Instance == null) Instance = this;
    }
    public static void Navigate(ViewModelBase v)
    {
        if (Instance == null) return;
        {
            Instance.Content = null;
            Instance.Content = v;
        }
    }
}