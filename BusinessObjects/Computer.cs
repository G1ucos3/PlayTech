using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace BusinessObjects;

public partial class Computer : INotifyPropertyChanged, INotifyDataErrorInfo
{
    private Dictionary<string, List<string>> _errors = new Dictionary<string, List<string>>();

    private int _computerId;
    public int ComputerId { get; set; }

    private string _computerName;
    [Required(ErrorMessage = "Computer name is required")]
    public string ComputerName
    {
        get => _computerName;
        set
        {
            if (_computerName != value)
            {
                _computerName = value;
                OnPropertyChanged(nameof(ComputerName));
                ValidateProperty(nameof(ComputerName), value);
            }
        }
    }

    private string _computerSpec;
    [Required(ErrorMessage = "Computer spec is required")]
    public string ComputerSpec
    {
        get => _computerSpec;
        set
        {
            if (_computerSpec != value)
            {
                _computerSpec = value;
                OnPropertyChanged(nameof(ComputerSpec));
                ValidateProperty(nameof(ComputerSpec), value);
            }
        }
    }

    private bool _computerStatus;
    public bool ComputerStatus
    {
        get => _computerStatus;
        set
        {
            if (_computerStatus != value)
            {
                _computerStatus = value;
                OnPropertyChanged(nameof(ComputerStatus));
                ValidateProperty(nameof(ComputerStatus), value);
            }
        }
    }

    private ICollection<CurrentComputer> _currentComputers = new List<CurrentComputer>();
    public virtual ICollection<CurrentComputer> CurrentComputers
    {
        get => _currentComputers;
        set
        {
            if (_currentComputers != value)
            {
                _currentComputers = value;
                OnPropertyChanged(nameof(CurrentComputers));
                ValidateProperty(nameof(CurrentComputers), value);
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
