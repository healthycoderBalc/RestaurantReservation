using RestaurantReservation.Db.Models;
using RestaurantReservation.Db.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantReservation.FunctionalityDemonstration
{
    public class ReservationFunctionalityDemonstration : IFunctionalityDemonstration
    {
        public async Task DemonstrateAsync()
        {
            using (var reservationDemonstration = new ReservationRepository())
            {
                Console.WriteLine("----------RESERVATION DEMONSTRATION---------");

                int customerId = 1;
                int restaurantId = 1;
                int tableId = 4;
                DateTime reservationDate = DateTime.Now.AddDays(6);
                int partySize = 3;
                int createdReservationId = await reservationDemonstration.CreateReservationAsync(customerId, restaurantId, tableId, reservationDate, partySize);
                await reservationDemonstration.ReadReservationAsync(createdReservationId);

                int newCustomerId = 1;
                int newRestaurantId = 1;
                int newTableId = 2;
                DateTime newReservationDate = DateTime.Now.AddDays(8);
                int newPartySize = 8;
                await reservationDemonstration.UpdateReservationAsync(createdReservationId, newCustomerId, newRestaurantId, newTableId, newReservationDate, newPartySize);
                await reservationDemonstration.ReadReservationAsync(createdReservationId);

                await reservationDemonstration.DeleteReservationAsync(createdReservationId);
                await reservationDemonstration.ReadReservationAsync(createdReservationId);

                Console.WriteLine();
                Console.WriteLine("Get reservation by customer");
                List<Reservation> reservations = await reservationDemonstration.GetReservationsByCustomerAsync(customerId);
                if (reservations.Count > 0)
                {
                    foreach (Reservation reservation in reservations)
                    {
                        Console.WriteLine($"ID: {reservation.ReservationId} - Restaurant: {reservation.Restaurant.Name}, Table: {reservation.Table.TableId}, Date: {reservation.ReservationDate}, Size: {reservation.PartySize}");
                    }
                }
                Console.WriteLine();

                Console.WriteLine("Get reservations with customer and restaurant information (from view)");
                List<ReservationWithDetails> reservationsFromView = await reservationDemonstration.GetReservationsWithCustomerAndRestaurantInformationFromViewAsync();
                if (reservationsFromView.Count > 0)
                {
                    foreach (ReservationWithDetails reservation in reservationsFromView)
                    {
                        Console.WriteLine($"ID: {reservation.ReservationId} - ReservationDate: {reservation.ReservationDate},  Size: {reservation.PartySize} | Customer: {reservation.CustomerId}. {reservation.CustomerFirstName} {reservation.CustomerLastName}. Restaurant: {reservation.RestaurantId}. {reservation.RestaurantName}");
                    }
                }
                Console.WriteLine();


            }
        }
    }
}
