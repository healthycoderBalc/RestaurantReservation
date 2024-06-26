using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RestaurantReservation.Db.Migrations
{
    /// <inheritdoc />
    public partial class AddEmployeesWithRestaurantDetailsView : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                CREATE VIEW EmployeesWithRestaurantDetails AS
                SELECT 
                    e.EmployeeId,
                    e.FirstName,
                    e.LastName,
                    e.Position,
                    rst.RestaurantId,
                    rst.Name AS RestaurantName,
	                rst.Address,
	                rst.PhoneNumber,
	                rst.OpeningHours
                FROM 
                    Employees e
                JOIN 
                    Restaurants rst ON e.RestaurantId = rst.RestaurantId;
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP VIEW IF EXISTS EmployeesWithRestaurantDetails;");
        }
    }
}
