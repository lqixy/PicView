using Avalonia.Controls;
using Avalonia.Input;
using PicView.ViewModels;

namespace PicView.Views;

public partial class MainWindow : Window
{
    private MainWindowViewModel ViewModel => (MainWindowViewModel)DataContext!;
    
    public MainWindow()
    {
        InitializeComponent();
        
        // 添加键盘事件处理
        KeyDown += MainWindow_KeyDown;
        
        // 添加窗口关闭事件处理
        Closing += (s, e) => 
        {
            // 清理资源
            if (DataContext is MainWindowViewModel vm)
            {
                vm.CleanupResources();
            }
        };
    }
    
    private void MainWindow_KeyDown(object? sender, KeyEventArgs e)
    {
        switch (e.Key)
        {
            case Key.Left:
                ViewModel.PreviousImageCommand.Execute(null);
                e.Handled = true;
                break;
                
            case Key.Right:
                ViewModel.NextImageCommand.Execute(null);
                e.Handled = true;
                break;
        }
    }
}