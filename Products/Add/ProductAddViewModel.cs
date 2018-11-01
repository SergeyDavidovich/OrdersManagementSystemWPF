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
        enum States { Edit, Add };
        States state;

        public ProductAddViewModel(LocalDbContext context, IEventAggregator eventAggregator)
        {
            //IsGroupBoxEnabled = false;
            
            _eventAggregator = eventAggregator;
            _context = context;

            _eventAggregator.GetEvent<OnProductSelectedEvent>().Subscribe(SelectProductEventHandler);
            _eventAggregator.GetEvent<OnProductAddEvent>().Subscribe(AddProductEventHandler);
            _eventAggregator.GetEvent<OnProductEditEvent>().Subscribe(EditProductEventHandler);

            validator = new ProductValidator();

            CancelCommand = new DelegateCommand(Cancel);
            SaveCommand = new DelegateCommand(Save, CanSave);

            Suppliers = _context.Suppliers.ToList<Supplier>();
            Categories = _context.Categories.ToList<Category>();
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

        string unitPrice;
        public string UnitPrice
        {
            get => unitPrice;
            set
            {
                SetProperty(ref unitPrice, value);
                SaveCommand.RaiseCanExecuteChanged();
            }
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
        bool discontinued;
        public bool Discontinued
        {
            get => discontinued;
            set => SetProperty(ref discontinued, value);
        }

        #endregion

        #region Commands

        public DelegateCommand CancelCommand { get; set; }
        private void Cancel()
        {
            currentProduct = null;
            PopulateBindings(false);
            IsGroupBoxEnabled = false;
            _eventAggregator.GetEvent<OnProductCompleted>().Publish();

            Title = "";
        }

        public DelegateCommand SaveCommand { get; set; }
        private void Save()
        {
            EditCurrentProduct();
            if (state == States.Add)
                _context.Products.Add(currentProduct);

           int result = _context.SaveChanges();

            IsGroupBoxEnabled = false;
            _eventAggregator.GetEvent<OnProductCompleted>().Publish();
            Title = "";
        }
        private bool CanSave() { return validator.Validate(this).IsValid; ; }

        #endregion

        #region PubSubEvents
        private void AddProductEventHandler()
        {
            state = States.Add;
            Title = "ADD NEW";
            currentProduct = new Product();
            PopulateBindings(false);
            IsGroupBoxEnabled = true;
        }

        private void EditProductEventHandler(int id)
        {
            state = States.Edit;
            currentProduct = _context.Products.Find(id);
            Title = "EDIT: ";
            Title += $"{currentProduct.ProductName}";
            IsGroupBoxEnabled = true;
        }

        private void SelectProductEventHandler(int id)
        {
            currentProduct = _context.Products.Find(id);
            PopulateBindings(true);
            Title = "VIEW: ";
            Title += $"{currentProduct.ProductName}";
            IsGroupBoxEnabled = false;
        }

        public bool IsGroupBoxEnabled
        {
            get => _isGroupBoxEnabled;
            set => SetProperty(ref _isGroupBoxEnabled, value);
        }
        #endregion

        #region Utilites

        private void PopulateBindings(bool exist)
        {
            if (exist)
            {
                this.Name = currentProduct.ProductName;
                this.SelectedCategory = _context.Categories.Find(currentProduct.Categories.CategoryID);
                this.SelectedSupplier = _context.Suppliers.Find(currentProduct.Suppliers.SupplierID);
                this.UnitPrice = currentProduct.UnitPrice.ToString();
                this.Quantity = currentProduct.QuantityPerUnit;
                this.Discontinued = currentProduct.Discontinued;
            }
            else
            {
                this.Name = null;
                this.SelectedCategory = null;
                this.SelectedSupplier = null;
                this.UnitPrice = null;
                this.Quantity = null;
                this.Discontinued = false;
            }
        }
        private void EditCurrentProduct()
        {
            currentProduct.ProductName = this.Name;
            currentProduct.Categories = this.SelectedCategory;
            currentProduct.Suppliers = this.SelectedSupplier;
            currentProduct.UnitPrice = Decimal.Parse(this.UnitPrice);
            currentProduct.QuantityPerUnit = this.Quantity;
            currentProduct.Discontinued = this.Discontinued;
        }
        #endregion
    }
}
