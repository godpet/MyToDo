using System.Windows;
using System.Windows.Input;

namespace MyToDo.Views;

public partial class MainView : Window
{
    public MainView()
    {
        InitializeComponent();
        
        minBtn.Click += (sender, args) =>
        {
            this.WindowState = WindowState.Minimized;
        };

        maxBtn.Click += (sender, args) =>
        {
            if (this.WindowState == WindowState.Maximized)
            {
                this.WindowState = WindowState.Normal;
            }
            else
            {
                this.WindowState = WindowState.Maximized;
            }
        };

        closeBtn.Click += (sender, args) =>
        {
            this.Close();
        };

        //colorZone鼠标拖动事件
        colorZone.MouseMove += (sender, args) =>
        {
            //判断鼠标状态
            if (args.LeftButton == MouseButtonState.Pressed)
            {
                this.DragMove();
            }
        };
            
        //colorZone鼠标双击事件
        colorZone.MouseDoubleClick += (sender, args) =>
        {
            if (this.WindowState == WindowState.Normal)
            {
                this.WindowState = WindowState.Maximized;
            }
            else
            {
                this.WindowState = WindowState.Normal;
            }
        };

        //切换导航条自动收缩
        menuBar.SelectionChanged += (sender, args) =>
        {
            drawerHost.IsLeftDrawerOpen = false;
        };
    }
}