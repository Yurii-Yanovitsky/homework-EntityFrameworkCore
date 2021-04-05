using Microsoft.EntityFrameworkCore.Migrations;

namespace ParkingLotAppDB.Migrations
{
    public partial class CreateTrigger : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
CREATE TRIGGER Visits_INSERT_UPDATE
ON Visits
AFTER INSERT, UPDATE
AS
	IF EXISTS
	(
		SELECT * FROM inserted AS i
		JOIN  ParkingSpots AS ps
		ON i.ParkingSpotId = ps.Id
		WHERE ps.IsBusy = 1
	)
		BEGIN
			PRINT('Место занято')
			ROLLBACK TRAN
		END
	ELSE
		BEGIN
			DECLARE @PSId int
			DECLARE @Left datetime2

			SET @PSId = (SELECT MIN(i.ParkingSpotId) FROM inserted AS i)

			WHILE @PSId IS NOT NULL
				BEGIN
					SET @Left = (SELECT i.[Left] FROM inserted AS i
					WHERE @PSId = i.ParkingSpotId)

					IF (@Left IS NULL)
						BEGIN
							UPDATE ParkingSpots
							SET IsBusy = 1
							WHERE Id = @PSId
						END
					ELSE
						BEGIN
							UPDATE ParkingSpots
							SET IsBusy = 0
							WHERE Id = @PSId
						END

					SET @PSId = (SELECT MIN(i.ParkingSpotId) FROM inserted AS i
					WHERE i.ParkingSpotId > @PSId)
				END
		END
GO");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
			migrationBuilder.Sql("DROP TRIGGER Visits_INSERT_UPDATE");
        }
    }
}
