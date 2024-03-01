using Microsoft.EntityFrameworkCore.Migrations;

namespace AndresVillarreal_Project.Data
{
    public static class ExtraMigration
    {
        public static void Steps(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
                @"
                CREATE TRIGGER SetAthleteTimestampOnUpdate
                AFTER UPDATE ON Athletes
                BEGIN
                    UPDATE Athletes
                    SET RowVersion = randomblob(8)
                    WHERE rowid = NEW.rowid;
                END
            ");
            migrationBuilder.Sql(
                @"
                CREATE TRIGGER SetAthleteTimestampOnInsert
                AFTER INSERT ON Athletes
                BEGIN
                    UPDATE Athletes
                    SET RowVersion = randomblob(8)
                    WHERE rowid = NEW.rowid;
                END
            ");
            migrationBuilder.Sql(
                @"
                CREATE TRIGGER SetSportTimestampOnUpdate
                AFTER UPDATE ON Sports
                BEGIN
                    UPDATE Sports
                    SET RowVersion = randomblob(8)
                    WHERE rowid = NEW.rowid;
                END
            ");
            migrationBuilder.Sql(
                @"
                CREATE TRIGGER SetSportTimestampOnInsert
                AFTER INSERT ON Sports
                BEGIN
                    UPDATE Sports
                    SET RowVersion = randomblob(8)
                    WHERE rowid = NEW.rowid;
                END
            ");
        }
    }
}
