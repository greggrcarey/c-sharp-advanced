using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarehouseManagementSystem.Domain
{
    public record Customer(string Firstname, string Lastname)
    {
        public string Fullname => $"{Firstname} {Lastname}";
        //public Address Address { get; set; }//getter and setter makes this property NOT imutable on the Customer record
        public Address Address { get; init; }//init in the setter makes the property immutable
    }

    public record Address(string street, string postalCode);

    public record PriorityCustomer(string Firstname, string Lastname)
        : Customer(Firstname, Lastname);
}
