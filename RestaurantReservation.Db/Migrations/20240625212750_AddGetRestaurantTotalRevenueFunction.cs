﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RestaurantReservation.Db.Migrations
{
    /// <inheritdoc />
    public partial class AddGetRestaurantTotalRevenueFunction : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                CREATE FUNCTION dbo.GetRestaurantTotalRevenue
                (
                    @RestaurantId INT
                )
                RETURNS DECIMAL(10, 2)
                AS
                BEGIN
                    DECLARE @TotalRevenue DECIMAL(10, 2);

                    SELECT @TotalRevenue = SUM(o.TotalAmount)
                    FROM Orders o
                    INNER JOIN Reservations r ON o.ReservationId = r.ReservationId
                    WHERE r.RestaurantId = @RestaurantId;

                    RETURN ISNULL(@TotalRevenue, 0);
                END;
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP FUNCTION IF EXISTS dbo.GetRestaurantTotalRevenue;");
        }
    }
}
