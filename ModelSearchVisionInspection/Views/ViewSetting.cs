using System.ComponentModel;
using System.Windows;

public class MainWindowViewModel : INotifyPropertyChanged
{
    //날짜
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

    //탭 컨트롤 사이즈 계산
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
    //탭 아이템 크기 계산
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

    // 카메라 영역 크기 
    private double _CamWidth;
    public double CamWidth
    {
        get => _CamWidth;
        set
        {
            if (_CamWidth != value)
            {
                _CamWidth = value;
                OnPropertyChanged(nameof(CamWidth));
            }
        }
    }

    // 창 전체 크기
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

    //창 전체 높이 
    private double _windowHeight;
    public double TabItemHight
    {
        get => _windowHeight;
        set
        {
            if (_windowHeight != value)
            {
                _windowHeight = value;
                OnPropertyChanged(nameof(TabItemHight));
            }
        }
    }

    public void UpdateSize(double windowWidth, double windowHeight)
    {
        WindowWidth = windowWidth;
        TabControlWidth = windowWidth + 5;
        TabItemWidth = windowWidth / 5;
        CamWidth = windowWidth / 3;
        TabItemHight = windowHeight - 65;
    }

    public event PropertyChangedEventHandler PropertyChanged;
    protected void OnPropertyChanged(string name)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

    }


}
