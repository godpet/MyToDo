using System.Collections.ObjectModel;
using MyToDo.Common.Models;
using MyToDo.Extensions;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;

namespace MyToDo.ViewModels;

public class MainViewModel : BindableBase
{
    private readonly IRegionManager regionManager;
    private IRegionNavigationJournal journal;
    
    public MainViewModel(IRegionManager regionManager)
    {
        this.regionManager = regionManager;
        MenuBars = new ObservableCollection<MenuBar>();
        CreateMenuBar();
        NavigateCommand = new DelegateCommand<MenuBar>(Navigate);

        //后退
        GoBackCommand = new DelegateCommand(() =>
        {
            if(journal != null && journal.CanGoBack)
                journal.GoBack();
        });

        //前进
        GoForwardCommand = new DelegateCommand(() =>
        {
            if(journal != null && journal.CanGoForward)
                journal.GoForward();
        });
    }
    
    private ObservableCollection<MenuBar> menBars;
    public ObservableCollection<MenuBar> MenuBars
    {
        get { return menBars; }
        set { menBars = value; RaisePropertyChanged(); }
    }
    
    public DelegateCommand<MenuBar> NavigateCommand { get; private set; }
    
    public DelegateCommand GoBackCommand { get; private set; }
    
    public DelegateCommand GoForwardCommand { get; private set; }

    private void CreateMenuBar()
    {
        MenuBars.Add(new MenuBar(){Icon = "Home", Title = "首页", NameSpace = "IndexView"});
        MenuBars.Add(new MenuBar(){Icon = "NotebookOutline", Title = "待办事项", NameSpace = "ToDoView"});
        MenuBars.Add(new MenuBar(){Icon = "NotebookPlus", Title = "备忘录", NameSpace = "MemoView"});
        MenuBars.Add(new MenuBar(){Icon = "Cog", Title = "设置", NameSpace = "SettingsView"});
    }
    
    private void Navigate(MenuBar menuBar)
    {
        if(menuBar == null || string.IsNullOrWhiteSpace(menuBar.NameSpace))
            return;
        regionManager.Regions[PrismManager.MainViewRegionName].RequestNavigate(menuBar.NameSpace, callback =>
        {
            journal = callback.Context.NavigationService.Journal;
        });
    }

}