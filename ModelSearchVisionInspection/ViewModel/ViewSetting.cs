using System.ComponentModel;

public class MainWindowViewModel : INotifyPropertyChanged
{
    private string _currentDateTime;
    public string CurrentDateTime
    {
        get => _currentDateTime;
        set
        {
            if (_currentDateTime != value)
            {
                _currentDateTime = value;
                OnPropertyChanged(nameof(CurrentDateTime));
            }
        }
    }
    public void UpdateCurrentDateTime()
    {
        // 형식: 2025-06-23 (월) 오전 09:32:15
        string time = DateTime.Now.ToString("yyyy-MM-dd (ddd) tt hh:mm:ss");
        CurrentDateTime = time;
    }


    private double _tabControlWidth;
    public double TabControlWidth
    {
        get => _tabControlWidth;
        set
        {
            if (_tabControlWidth != value)
            {
                _tabControlWidth = value;
                OnPropertyChanged(nameof(TabControlWidth));
            }
        }
    }

    private double _tabItemWidth;
    public double TabItemWidth
    {
        get => _tabItemWidth;
        set
        {
            if (_tabItemWidth != value)
            {
                _tabItemWidth = value;
                OnPropertyChanged(nameof(TabItemWidth));
            }
        }
    }
    private double _WindowWidth;
    public double WindowWidth
    {
        get => _WindowWidth;
        set
        {
            if (_WindowWidth != value)
            {
                _WindowWidth = value;
                OnPropertyChanged(nameof(WindowWidth));
            }
        }
    }

    private double _windowHeight;
    public double WindowHeight
    {
        get => _windowHeight;
        set
        {
            if (_windowHeight != value)
            {
                _windowHeight = value;
                OnPropertyChanged(nameof(WindowHeight));
            }
        }
    }


   public void UpdateSize(double windowWidth, double windowHeight)
{
    WindowWidth = windowWidth;
    WindowHeight = windowHeight;

    TabControlWidth = windowWidth + 5;
    TabItemWidth = windowWidth / 5;
}





    public event PropertyChangedEventHandler PropertyChanged;
    protected void OnPropertyChanged(string name)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
