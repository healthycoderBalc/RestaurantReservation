using RestaurantReservation.Db.Models;
using RestaurantReservation.Db.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantReservation.FunctionalityDemonstration
{
    public class CustomerFunctionalityDemonstration : IFunctionalityDemonstration
    {
        public async Task DemonstrateAsync()
        {
            using (var customerDemonstration = new CustomerRepository())
            {
                Console.WriteLine("----------CUSTOMER DEMONSTRATION---------");
                string firstName = "Peter";
                string lastName = "Stone";
                string email = "peter.stone@example.com";
                string phoneNumber = "1234567890";

                int createdCustomerId = await customerDemonstration.CreateCustomerAsync(firstName, lastName, email, phoneNumber);

                await customerDemonstration.ReadCustomerAsync(createdCustomerId);

                string newEmail = "newpeter.stone@example.com";
                string newPhoneNumber = "9876543210";
                await customerDemonstration.UpdateCustomerAsync(createdCustomerId, newEmail, newPhoneNumber);
                await customerDemonstration.ReadCustomerAsync(createdCustomerId);

                await customerDemonstration.DeleteCustomerAsync(createdCustomerId);
                await customerDemonstration.ReadCustomerAsync(createdCustomerId);

                Console.WriteLine();
                int partySize = 2;
                Console.WriteLine($"Customers with Party Size Greater than: {partySize}");
                List<Customer> customersPartySize = await customerDemonstration.CustomerWithPartySizeGreaterThanAsync(partySize);
                if (customersPartySize.Count > 0)
                {
                    foreach (Customer cps in customersPartySize)
                    {
                        Console.WriteLine($"Customer: {cps.CustomerId}. {cps.FirstName} {cps.LastName}, Email: {cps.Email}, Phone Number: {cps.PhoneNumber}");
                    }
                }
                Console.WriteLine();
            }
        }

    }
}
