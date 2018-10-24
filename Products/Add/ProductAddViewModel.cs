using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Base;
using Prism.Events;
using DAL_LocalDb;
using Prism.Regions;
using Products.Events;
using Prism.Commands;
using System.ComponentModel;
using FluentValidation;
using Products.Validators;
using System.Data.Entity;
using System.Collections.ObjectModel;

namespace Products.Add
{
    public class ProductAddViewModel : NavigationAwareViewModelBase, IDataErrorInfo
    {
        IEventAggregator _eventAggregator;
        LocalDbContext _context;
        bool _isGroupBoxEnabled = false;
        Product currentProduct;
        ProductValidator validator;

        public ProductAddViewModel(LocalDbContext context, IEventAggregator eventAggregator)
        {
            //IsGroupBoxEnabled = false;
            _eventAggregator = eventAggregator;
            _context = context;
            _eventAggregator.GetEvent<OnProductSelectedEvent>().Subscribe(SetSelectedID);
            _eventAggregator.GetEvent<OnProductAddEvent>().Subscribe(AddProduct);
            _eventAggregator.GetEvent<OnProductEditEvent>().Subscribe(EditProduct);

            validator = new ProductValidator();

            CancelCommand = new DelegateCommand(Cancel);
            SaveCommand = new DelegateCommand(Save, CanSave);
        }

        public override void OnNavigatedTo(NavigationContext navigationContext)
        {
            base.OnNavigatedTo(navigationContext);

            _context.Categories.Load();
            Categories = _context.Categories.Local.ToList<Category>();

            _context.Suppliers.Load();
            Suppliers = _context.Suppliers.Local.ToList<Supplier>();

        }


        #region IDataErrorInfo implemntation
        public string this[string columnName]
        {
            get
            {
                var firstError = validator.Validate(this).Errors.FirstOrDefault(e => e.PropertyName == columnName);
                if (firstError != null)
                    return validator != null ? firstError.ErrorMessage : "";
                return "";
            }
        }

        public string Error
        {
            get
            {
                if (validator != null)
                {
                    var results = validator.Validate(this);
                    if (results != null && results.Errors.Any())
                    {
                        var errors = string.Join(Environment.NewLine, results.Errors.Select(x => x.ErrorMessage).ToArray());
                        return errors;
                    }
                }
                return string.Empty;
            }
        }
        #endregion


        #region Bindable properties

        string name;
        public string Name
        {
            get => name;
            set
            {
                SetProperty(ref name, value);
                SaveCommand.RaiseCanExecuteChanged();
            }
        }

        string quantity;
        public string Quantity
        {
            get => quantity;
            set
            {
                SetProperty(ref quantity, value);
                SaveCommand.RaiseCanExecuteChanged();
            }
        }

        decimal unitPrice;
        public decimal UnitPrice
        {
            get => unitPrice;
            set => SetProperty(ref unitPrice, value);
        }

        public List<Category> Categories { get; set; }

        Category selectedCategory;
        public Category SelectedCategory
        {
            get => selectedCategory;
            set => SetProperty(ref selectedCategory, value);
        }
        public List<Supplier> Suppliers { get; set; }

        Supplier selectedSupplier;
        public Supplier SelectedSupplier
        {
            get => selectedSupplier;
            set => SetProperty(ref selectedSupplier, value);
        }

        #endregion

        #region Commands

        public DelegateCommand CancelCommand { get; set; }
        private void Cancel()
        {
            currentProduct = null;
            IsGroupBoxEnabled = false;
            _eventAggregator.GetEvent<OnProductCompleted>().Publish();

            Title = "";
        }

        public DelegateCommand SaveCommand { get; set; }
        private void Save()
        {
            IsGroupBoxEnabled = false;
            _eventAggregator.GetEvent<OnProductCompleted>().Publish();
            Title = "";
        }
        private bool CanSave() { return validator.Validate(this).IsValid; ; }

        #endregion

        #region PubSubEvents
        private void AddProduct()
        {
            Title = "Add new product";
            currentProduct = new Product();
            IsGroupBoxEnabled = true;
        }

        private void EditProduct(int id)
        {
            currentProduct = _context.Products.Find(id);
            Title = "Edit product: ";
            Title += $"{currentProduct.ProductName}";
            IsGroupBoxEnabled = true;
        }

        private void SetSelectedID(int id)
        {
            currentProduct = _context.Products.Find(id);

            Title = "View product: ";
            Title += $"{currentProduct.ProductName}"; IsGroupBoxEnabled = false;
        }

        public bool IsGroupBoxEnabled
        {
            get => _isGroupBoxEnabled;
            set => SetProperty(ref _isGroupBoxEnabled, value);
        }
        #endregion

        #region Utilites

        private void PopulateCurrentProduct()
        {
            currentProduct.ProductName = this.Name;
            currentProduct.Categories.CategoryID = this.selectedCategory.CategoryID;
            currentProduct.Suppliers.SupplierID = this.SelectedSupplier.SupplierID;
            currentProduct.UnitPrice = this.UnitPrice;
            currentProduct.QuantityPerUnit = this.Quantity;
        }

        #endregion

    }
}
