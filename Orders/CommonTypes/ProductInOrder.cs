using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orders.CommonTypes
{
    public class ProductInOrder : BindableBase
    {
        private int id;
        public int ID
        {
            get =>id;
            set =>SetProperty(ref id,value);
        }

        private string name;
        public string Name
        {
            get =>name;
            set =>SetProperty(ref name,value);
        }

        private decimal unitPrice;
        public decimal UnitPrice
        {
            get =>unitPrice;
            set =>SetProperty(ref unitPrice,value);
        }

        private short quantity;
        public short Quantity
        {
            get =>quantity;
            set =>SetProperty(ref quantity,value);
        }

        private float discount;
        public float Discount
        {
            get =>discount;
            set =>SetProperty(ref discount,value);
        }

    }

}
