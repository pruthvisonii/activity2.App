using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using activity2.Models;

public class HolidaysViewModel : INotifyPropertyChanged
{
    private readonly IHolidayService _holidayService;
    private int _selectedYear;
    private bool _isOptional;
    private ObservableCollection<Holiday> _holidays;

    public HolidaysViewModel(IHolidayService holidayService)
    {
        _holidayService = holidayService;
        Holidays = new ObservableCollection<Holiday>();
        SelectedYear = DateTime.Now.Year;
        IsOptional = false;
    }

    public int SelectedYear
    {
        get => _selectedYear;
        set
        {
            if (_selectedYear != value)
            {
                _selectedYear = value;
                OnPropertyChanged();
                LoadHolidays();
            }
        }
    }

    public bool IsOptional
    {
        get => _isOptional;
        set
        {
            if (_isOptional != value)
            {
                _isOptional = value;
                OnPropertyChanged();
                LoadHolidays();
            }
        }
    }

    public ObservableCollection<Holiday> Holidays
    {
        get => _holidays;
        set
        {
            _holidays = value;
            OnPropertyChanged();
        }
    }

    public async Task LoadHolidays()
    {
        var holidays = await _holidayService.GetHolidaysAsync(SelectedYear, IsOptional);
        Holidays.Clear();
        foreach (var holiday in holidays)
        {
            Holidays.Add(holiday);
        }
    }

    #region INotifyPropertyChanged

    public event PropertyChangedEventHandler PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    #endregion
}
