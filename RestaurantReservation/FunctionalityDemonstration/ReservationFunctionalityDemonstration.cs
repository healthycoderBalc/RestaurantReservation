using RestaurantReservation.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantReservation.FunctionalityDemonstration
{
    public class ReservationFunctionalityDemonstration : IFunctionalityDemonstration
    {
        public void Demonstrate()
        {
            using (var reservationDemonstration = new ReservationRepository())
            {
                int customerId = 1;
                int restaurantId = 1;
                int tableId = 4;
                DateTime reservationDate = DateTime.Now.AddDays(6);
                int partySize = 3;
                int createdReservationId = reservationDemonstration.CreateReservation(customerId, restaurantId, tableId, reservationDate, partySize);
                reservationDemonstration.ReadReservation(createdReservationId);

                int newCustomerId = 1;
                int newRestaurantId = 1;
                int newTableId = 2;
                DateTime newReservationDate = DateTime.Now.AddDays(8);
                int newPartySize = 8;
                reservationDemonstration.UpdateReservation(createdReservationId, newCustomerId, newRestaurantId, newTableId, newReservationDate, newPartySize);
                reservationDemonstration.ReadReservation(createdReservationId);

                reservationDemonstration.DeleteReservation(createdReservationId);
                reservationDemonstration.ReadReservation(createdReservationId);
            }
        }
    }
}
