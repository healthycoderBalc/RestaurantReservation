using RestaurantReservation.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantReservation.FunctionalityDemonstration
{
    public class CustomerFunctionalityDemonstration : IFunctionalityDemonstration
    {
        public void Demonstrate()
        {
            using (var customerDemonstration = new CustomerRepository())
            {
                string firstName = "Peter";
                string lastName = "Stone";
                string email = "peter.stone@example.com";
                string phoneNumber = "1234567890";

                int createdCustomerId = customerDemonstration.CreateCustomer(firstName, lastName, email, phoneNumber);
                customerDemonstration.ReadCustomer(createdCustomerId);

                string newEmail = "newpeter.stone@example.com";
                string newPhoneNumber = "9876543210";
                customerDemonstration.UpdateCustomer(createdCustomerId, newEmail, newPhoneNumber);
                customerDemonstration.ReadCustomer(createdCustomerId);

                customerDemonstration.DeleteCustomer(createdCustomerId);
                customerDemonstration.ReadCustomer(createdCustomerId);
            }
        }
    }
}
