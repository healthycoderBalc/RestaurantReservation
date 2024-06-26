using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RestaurantReservation.Db.Migrations
{
    /// <inheritdoc />
    public partial class AddReservationsWithDetailsView : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                  CREATE VIEW ReservationsWithDetails AS
                  SELECT 
                        r.ReservationId,
                        r.ReservationDate,
                        r.PartySize,
                        c.CustomerId,
                        c.FirstName AS CustomerFirstName,
                        c.LastName AS CustomerLastName,
                        rst.RestaurantId,
                        rst.Name AS RestaurantName
                  FROM
                        Reservations r
                  JOIN      
                        Customers c ON r.CustomerId = c.CustomerId
                  JOIN  
                        Restaurants rst ON r.RestaurantId = rst.RestaurantId;
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP VIEW IF EXISTS ReservationsWithDetails;");
        }
    }
}
