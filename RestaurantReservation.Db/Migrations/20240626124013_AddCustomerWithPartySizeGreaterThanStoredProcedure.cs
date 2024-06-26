using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RestaurantReservation.Db.Migrations
{
    /// <inheritdoc />
    public partial class AddCustomerWithPartySizeGreaterThanStoredProcedure : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                CREATE PROCEDURE sp_CustomersWithPartySizeGreaterThan
	                @partysize int
                AS
                BEGIN
	                SELECT DISTINCT
		                c.CustomerId, c.FirstName, c.LastName, c.Email, c.PhoneNumber, r.ReservationId, r.PartySize
	                FROM Customers c
	                INNER JOIN Reservations r ON c.CustomerId = r.CustomerId
	                WHERE r.PartySize > @partysize
                END
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS sp_CustomersWithPartySizeGreaterThan");
        }
    }
}
