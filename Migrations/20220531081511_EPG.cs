using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EPG_Api.Migrations
{
    public partial class EPG : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EPG_TVA",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    eid = table.Column<int>(type: "int", nullable: true),
                    start_time = table.Column<DateTime>(type: "datetime", nullable: true),
                    duration = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    sed_name_alb = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true),
                    sed_lang_alb = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    sed_name_eng = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true),
                    sed_lang_eng = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true),
                    eed_lang_alb = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    eed_text_alb = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true),
                    eed_lang_eng = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true),
                    eed_text_eng = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true),
                    cd_nibble1 = table.Column<short>(type: "smallint", nullable: true),
                    cd_nibble2 = table.Column<short>(type: "smallint", nullable: true),
                    prd_country_code = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    prd_value = table.Column<short>(type: "smallint", nullable: true),
                    date_prod = table.Column<DateTime>(type: "date", nullable: true),
                    genre = table.Column<byte[]>(type: "varbinary(250)", maxLength: 250, nullable: true),
                    channel = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true),
                    status = table.Column<short>(type: "smallint", nullable: true),
                    poster = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true),
                    trailer = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EPG_TVA", x => x.id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EPG_TVA");
        }
    }
}
