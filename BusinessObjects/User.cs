using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace BusinessObjects;

public partial class User : INotifyPropertyChanged, INotifyDataErrorInfo
{
    private Dictionary<string, List<string>> _errors = new Dictionary<string, List<string>>();

    private int _userId;
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

    private string? _userAvatar;
    public string? UserAvatar
    {
        get => _userAvatar;
        set
        {
            if (_userAvatar != value)
            {
                _userAvatar = value;
                OnPropertyChanged(nameof(UserAvatar));
                ValidateProperty(nameof(UserAvatar), value);
            }
        }
    }

    private string _userName;
    [Required(ErrorMessage = "Username is required")]
    public string UserName
    {
        get => _userName;

        set
        {
            if (_userName != value)
            {
                _userName = value;
                OnPropertyChanged(nameof(UserName));
                ValidateProperty(nameof(UserName), value);
            }
        }

    }

    private string _userEmail;
    [Required(ErrorMessage = "User Email is required")]
    public string UserEmail
    {
        get => _userEmail;
        set
        {
            if (_userEmail != value)
            {
                _userEmail = value;
                OnPropertyChanged(nameof(UserEmail));
                ValidateProperty(nameof(UserEmail), value);
            }
        }
    }

    private int _userBalance;
    public int UserBalance
    {
        get => _userBalance;
        set
        {
            if (_userBalance != value)
            {
                _userBalance = value;
                OnPropertyChanged(nameof(UserBalance));
                ValidateProperty(nameof(UserBalance), value);
            }
        }
    }

    private string _userPassword;
    [Required(ErrorMessage = "Password is required")]
    public string UserPassword
    {
        get => _userPassword;
        set
        {
            if (_userPassword != value)
            {
                _userPassword = value;
                OnPropertyChanged(nameof(UserPassword));
                ValidateProperty(nameof(UserPassword), value);
            }
        }
    }

    private int _userRoles;
    [Required(ErrorMessage = "Role is required")]
    public int UserRoles
    {
        get => _userRoles;
        set
        {
            if (value != _userRoles)
            {
                _userRoles = value;
                OnPropertyChanged(nameof(UserRoles));
                ValidateProperty(nameof(UserRoles), value);
            }
        }
    }

    private ICollection<CurrentComputer> _currentComputers = new List<CurrentComputer>();
    public virtual ICollection<CurrentComputer> CurrentComputers
    {
        get => _currentComputers;
        set
        {
            if (value != _currentComputers)
            {
                _currentComputers = value;
                OnPropertyChanged(nameof(CurrentComputers));
                ValidateProperty(nameof(CurrentComputers), value);
            }
        }
    }

    private ICollection<Order> _orders = new List<Order>();
    public virtual ICollection<Order> Orders
    {
        get => _orders;
        set
        {
            if (value != _orders)
            {
                _orders = value;
                OnPropertyChanged(nameof(Orders));
                ValidateProperty(nameof(Orders), value);
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
            if (propertyName == nameof(UserPassword))
            {
                var password = value as string;
                if (password != null && password.Length < 8)
                {
                    _errors[propertyName] = new List<string> { "Password must be at least 8 characters long" };
                    OnErrorsChanged(propertyName);
                    return;
                }
            }
            
            _errors.Remove(propertyName);
            OnErrorsChanged(propertyName);
        }
    }

    protected void OnErrorsChanged(string propertyName)
    {
        ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
    }
}
