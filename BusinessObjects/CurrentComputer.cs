using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace BusinessObjects;

public partial class CurrentComputer : INotifyPropertyChanged, INotifyDataErrorInfo
{
    private Dictionary<string, List<string>> _errors = new Dictionary<string, List<string>>();

    private int _userId;
    [Range(1, int.MaxValue, ErrorMessage = "UserID must be a positive number.")]
    public int UserId 
    { 
        get => _userId;
        set
        {
            if (_userId != value)
            {
                _userId = value;
                OnPropertyChanged(nameof(UserId));
                ValidateProperty(nameof(UserId), value);
            }
        }
    }

    private int _computerId;
    [Range(1, int.MaxValue, ErrorMessage = "ComputerID must be a positive number.")]
    public int ComputerId 
    {
        get => _computerId;
        set
        {
            if (value != _computerId)
            {
                _computerId = value;
                OnPropertyChanged(nameof(ComputerId));
                ValidateProperty(nameof(ComputerId), value);
            }
        }
    }

    private DateTime? _startTime;
    public DateTime? StartTime 
    {
        get => _startTime;
        set
        {
            if (!_startTime.HasValue)
            {
                _startTime = value;
                OnPropertyChanged(nameof(StartTime));
                ValidateProperty(nameof(StartTime), value);
            }
        }
    }

    private Computer _computer;
    public virtual Computer Computer {
        get => _computer;
        set
        {
            if(value != _computer)
            {
                _computer = value;
                OnPropertyChanged(nameof(Computer));
                ValidateProperty(nameof(Computer), value);
            }
        }
    }

    private User _user;
    public virtual User User 
    { 
        get => _user;
        set
        {
            if (_user != value)
            {
                _user = value;
                OnPropertyChanged(nameof(User));
                ValidateProperty(nameof(User), value);
            }
        }
    }



    public event PropertyChangedEventHandler? PropertyChanged;
    protected void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    public bool HasErrors => _errors.Count > 0;

    public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

    public IEnumerable GetErrors(string propertyName)
    {
        if (string.IsNullOrEmpty(propertyName) || !_errors.ContainsKey(propertyName))
            return null;

        return _errors[propertyName];
    }

    protected void ValidateProperty(string propertyName, object value)
    {
        var results = new List<ValidationResult>();
        var context = new ValidationContext(this) { MemberName = propertyName };

        if (!Validator.TryValidateProperty(value, context, results))
        {
            _errors[propertyName] = results.Select(r => r.ErrorMessage).ToList();
            OnErrorsChanged(propertyName);
        }
        else
        {
            _errors.Remove(propertyName);
            OnErrorsChanged(propertyName);
        }
    }

    protected void OnErrorsChanged(string propertyName)
    {
        ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
    }
}
