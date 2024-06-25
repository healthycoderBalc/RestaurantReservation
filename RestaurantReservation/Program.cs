using RestaurantReservation.FunctionalityDemonstration;

namespace RestaurantReservation
{
    internal class Program
    {
        static void Main(string[] args)
        {
            TestCustomerFunctionality();
            TestRestaurantFunctionality();
            TestTableFunctionality();
            TestEmployeeFunctionality();
            TestMenuItemFunctionality();
            TestReservationFunctionality();
            TestOrderFunctionality();
            TestOrderItemFunctionality();
        }

        static void TestCustomerFunctionality()
        {
            using (var customerDemonstration = new CustomerFunctionalityDemonstration())
            {
                string firstName = "Peter";
                string lastName = "Stone";
                string email = "peter.stone@example.com";
                string newEmail = "newpeter.stone@example.com";
                string phoneNumber = "1234567890";
                string newPhoneNumber = "9876543210";
                int createdCustomerId = customerDemonstration.CreateCustomer(firstName, lastName, email, phoneNumber);
                Console.WriteLine(createdCustomerId);
                customerDemonstration.ReadCustomer(createdCustomerId);
                customerDemonstration.UpdateCustomer(createdCustomerId, newEmail, newPhoneNumber);
                customerDemonstration.DeleteCustomer(createdCustomerId);
            }
        }

        static void TestRestaurantFunctionality()
        {
            using (var restaurantDemonstration = new RestaurantFunctionalityDemonstration())
            {
                string name = "Peter";
                string address = "123 Main St";
                string phoneNumber = "555-1234";
                string openingHours = "9AM - 9PM";
                string newAddress = "1234567890";
                string newPhoneNumber = "555-5678";
                string newOpeningHours = "9AM - 7PM";
                int createdRestaurantId = restaurantDemonstration.CreateRestaurant(name, address, phoneNumber, openingHours);
                restaurantDemonstration.ReadRestaurant(createdRestaurantId);
                restaurantDemonstration.UpdateRestaurant(createdRestaurantId, newAddress, newPhoneNumber, newOpeningHours);
                restaurantDemonstration.DeleteRestaurant(createdRestaurantId);
            }
        }

        static void TestTableFunctionality()
        {
            using (var tableDemonstration = new TableFunctionalityDemonstration())
            {
                int restaurantId = 1;
                int capacity = 10;

                int newRestaurantId = 2;
                int newCapacity = 6;

                int createdTableId = tableDemonstration.CreateTable(restaurantId, capacity);
                tableDemonstration.ReadTable(createdTableId);
                tableDemonstration.UpdateTable(createdTableId, newRestaurantId, newCapacity);
                tableDemonstration.DeleteTable(createdTableId);
            }
        }

        static void TestEmployeeFunctionality()
        {
            using (var employeeDemonstration = new EmployeeFunctionalityDemonstration())
            {

                int restaurantId = 1;
                string firstName = "Mary";
                string lastName = "Jane";
                string position = "Manager";
                int newRestaurantId = 1;
                string newFirstName = "Michelle";
                string newLastName = "Jones";
                string newPosition = "Supervisor";
                var newEmployeeId = employeeDemonstration.CreateEmployee(restaurantId, firstName, lastName, position);
                employeeDemonstration.ReadEmployee(newEmployeeId);
                employeeDemonstration.UpdateEmployee(newEmployeeId, newRestaurantId, newFirstName, newLastName, newPosition);
                employeeDemonstration.DeleteEmployee(newEmployeeId);
            }
        }

        static void TestMenuItemFunctionality()
        {
            using (var menuItemDemonstration = new MenuItemFunctionalityDemonstration())
            {
                int restaurantId = 1;
                string name = "BBQ";
                string description = "Asado Argentino";
                decimal price = 59.65M;

                int newRestaurantId = 1;
                string newName = "BBQ";
                string newDescription = "Asado Argentino";
                decimal newPrice = 59.65M;

                int createdMenuItemId = menuItemDemonstration.CreateMenuItem(restaurantId, name, description, price);
                menuItemDemonstration.ReadMenuItem(createdMenuItemId);
                menuItemDemonstration.UpdateMenuItem(createdMenuItemId, newRestaurantId, newName, newDescription, newPrice);
                menuItemDemonstration.DeleteMenuItem(createdMenuItemId);
            }
        }

        static void TestReservationFunctionality()
        {
            using (var reservationDemonstration = new ReservationFunctionalityDemonstration())
            {
                int customerId = 1;
                int restaurantId = 1;
                int tableId = 1;
                DateTime reservationDate = DateTime.Now.AddDays(6);
                int partySize = 8;

                int newCustomerId = 1;
                int newRestaurantId = 1;
                int newTableId = 1;
                DateTime newReservationDate = DateTime.Now.AddDays(8);
                int newPartySize = 8;

                int createdReservationId = reservationDemonstration.CreateReservation(customerId, restaurantId, tableId, reservationDate, partySize);
                reservationDemonstration.ReadReservation(createdReservationId);
                reservationDemonstration.UpdateReservation(createdReservationId, newCustomerId, newRestaurantId, newTableId, newReservationDate, newPartySize);
                reservationDemonstration.DeleteReservation(createdReservationId);
            }
        }

        static void TestOrderFunctionality()
        {
            using (var orderDemonstration = new OrderFunctionalityDemonstration())
            {
                int reservationId = 1;
                int employeeId = 1;
                DateTime orderDate = DateTime.Now.AddDays(6);
                decimal totalAmount = 48.3M;

                int newReservationId = 1;
                int newEmployeeId = 1;
                DateTime newOrderDate = DateTime.Now.AddDays(8);
                decimal newTotalAmount = 49.3M;

                int createdOrderId = orderDemonstration.CreateOrder(reservationId, employeeId, orderDate, totalAmount);
                orderDemonstration.ReadOrder(createdOrderId);
                orderDemonstration.UpdateOrder(createdOrderId, newReservationId, newEmployeeId, newOrderDate, newTotalAmount);
                orderDemonstration.DeleteOrder(createdOrderId);
            }
        }

        static void TestOrderItemFunctionality()
        {
            using (var orderItemDemonstration = new OrderItemFunctionalityDemonstration())
            {
                int orderId = 1;
                int itemId = 1;
                int quantity = 2;

                int newOrderId = 1;
                int newItemId = 1;
                int newQuantity = 3;

                int createdOrderItemId = orderItemDemonstration.CreateOrderItem(orderId, itemId, quantity);
                orderItemDemonstration.ReadOrderItem(createdOrderItemId);
                orderItemDemonstration.UpdateOrderItem(createdOrderItemId, newOrderId, newItemId, newQuantity);
                orderItemDemonstration.DeleteOrderItem(createdOrderItemId);
            }
        }

    }
}
