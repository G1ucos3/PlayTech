using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BusinessObjects;

public partial class Product : INotifyPropertyChanged, INotifyDataErrorInfo
{
    private Dictionary<string, List<string>> _errors = new Dictionary<string, List<string>>();

    private int _productId;
    public int ProductId
    {
        get => _productId;
        set
        {
            if (_productId != value)
            {
                _productId = value;
                OnPropertyChanged(nameof(ProductId));
                ValidateProperty(nameof(ProductId), value);
            }
        }
    }

    private string? _productImg;
    public string? ProductImg
    {
        get => _productImg;
        set
        {
            if (_productImg != value)
            {
                _productImg = value;
                OnPropertyChanged(nameof(ProductImg));
                ValidateProperty(nameof(ProductImg), value);
            }
        }
    }

    private string _productName;
    [Required(ErrorMessage = "Product Name is required")]
    public string ProductName
    {
        get => _productName;
        set
        {
            if (value != _productName)
            {
                _productName = value;
                OnPropertyChanged(nameof(ProductName));
                ValidateProperty(nameof(ProductName), value);
            }
        }
    }

    private int _productPrice;
    [Range(1, int.MaxValue, ErrorMessage = "Price must be a positive number.")]
    public int ProductPrice
    {
        get => _productPrice;
        set
        {
            if (_productPrice != value)
            {
                _productPrice = value;
                OnPropertyChanged(nameof(ProductPrice));
                ValidateProperty(nameof(ProductPrice), value);
            }
        }
    }

    private int _productQuantity;
    [Range(1, int.MaxValue, ErrorMessage = "Quantity must be a positive number.")]
    public int ProductQuantity
    {
        get => _productQuantity;
        set
        {
            if (_productQuantity != value)
            {
                _productQuantity = value;
                OnPropertyChanged(nameof(ProductQuantity));
                ValidateProperty(nameof(ProductQuantity), value);
            }
        }
    }

    private string _productType;
    public string ProductType
    {
        get => _productType;
        set
        {
            if (_productType != value)
            {
                _productType = value;
                OnPropertyChanged(nameof(ProductType));
                ValidateProperty(nameof(ProductType), value);
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
            _errors.Remove(propertyName);
            OnErrorsChanged(propertyName);
        }
    }

    protected void OnErrorsChanged(string propertyName)
    {
        ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
    }
}