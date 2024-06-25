﻿using RestaurantReservation.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantReservation.FunctionalityDemonstration
{
    public class CustomerFunctionalityDemonstration : IFunctionalityDemonstration
    {
        public async Task Demonstrate()
        {
            using (var customerDemonstration = new CustomerRepository())
            {
                Console.WriteLine("----------CUSTOMER DEMONSTRATION---------");
                string firstName = "Peter";
                string lastName = "Stone";
                string email = "peter.stone@example.com";
                string phoneNumber = "1234567890";

                int createdCustomerId = await customerDemonstration.CreateCustomer(firstName, lastName, email, phoneNumber);

                await customerDemonstration.ReadCustomer(createdCustomerId);

                string newEmail = "newpeter.stone@example.com";
                string newPhoneNumber = "9876543210";
                await customerDemonstration.UpdateCustomer(createdCustomerId, newEmail, newPhoneNumber);
                await customerDemonstration.ReadCustomer(createdCustomerId);

                await customerDemonstration.DeleteCustomer(createdCustomerId);
                await customerDemonstration.ReadCustomer(createdCustomerId);
            }
        }

    }
}
