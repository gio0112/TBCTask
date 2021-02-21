using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Domain.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Attachments",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PersonID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Attachments", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Cities",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cities", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Genders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genders", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PersonTypes",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonTypes", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "PhoneTypes",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhoneTypes", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "People",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PersonalNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BirthDay = table.Column<DateTime>(type: "datetime2", nullable: false),
                    GenderID = table.Column<int>(type: "int", nullable: false),
                    AddressID = table.Column<int>(type: "int", nullable: false),
                    AttachmentID = table.Column<int>(type: "int", nullable: true),
                    PersonTypeID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_People", x => x.ID);
                    table.ForeignKey(
                        name: "FK_People_Attachments_AttachmentID",
                        column: x => x.AttachmentID,
                        principalTable: "Attachments",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_People_Cities_AddressID",
                        column: x => x.AddressID,
                        principalTable: "Cities",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_People_Genders_GenderID",
                        column: x => x.GenderID,
                        principalTable: "Genders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_People_PersonTypes_PersonTypeID",
                        column: x => x.PersonTypeID,
                        principalTable: "PersonTypes",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PersonRelations",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PersonTypeID = table.Column<int>(type: "int", nullable: false),
                    PersonID = table.Column<int>(type: "int", nullable: false),
                    ContactID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonRelations", x => x.ID);
                    table.ForeignKey(
                        name: "FK_PersonRelations_People_ContactID",
                        column: x => x.ContactID,
                        principalTable: "People",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PersonRelations_People_PersonID",
                        column: x => x.PersonID,
                        principalTable: "People",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PersonRelations_PersonTypes_PersonTypeID",
                        column: x => x.PersonTypeID,
                        principalTable: "PersonTypes",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Phones",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PhoneTypeID = table.Column<int>(type: "int", nullable: false),
                    Number = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PersonID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Phones", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Phones_People_PersonID",
                        column: x => x.PersonID,
                        principalTable: "People",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Phones_PhoneTypes_PhoneTypeID",
                        column: x => x.PhoneTypeID,
                        principalTable: "PhoneTypes",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_People_AddressID",
                table: "People",
                column: "AddressID");

            migrationBuilder.CreateIndex(
                name: "IX_People_AttachmentID",
                table: "People",
                column: "AttachmentID");

            migrationBuilder.CreateIndex(
                name: "IX_People_GenderID",
                table: "People",
                column: "GenderID");

            migrationBuilder.CreateIndex(
                name: "IX_People_PersonTypeID",
                table: "People",
                column: "PersonTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_PersonRelations_ContactID",
                table: "PersonRelations",
                column: "ContactID");

            migrationBuilder.CreateIndex(
                name: "IX_PersonRelations_PersonID",
                table: "PersonRelations",
                column: "PersonID");

            migrationBuilder.CreateIndex(
                name: "IX_PersonRelations_PersonTypeID",
                table: "PersonRelations",
                column: "PersonTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_Phones_PersonID",
                table: "Phones",
                column: "PersonID");

            migrationBuilder.CreateIndex(
                name: "IX_Phones_PhoneTypeID",
                table: "Phones",
                column: "PhoneTypeID");


            

             //insert data

            migrationBuilder.InsertData(
            table: "Genders",
            columns: new[] { "Value" },
            values: new object[] { "Male"});

            migrationBuilder.InsertData(
           table: "Genders",
           columns: new[] { "Value" },
           values: new object[] { "Female" });

            migrationBuilder.InsertData(
          table: "Cities",
          columns: new[] { "Value" },
          values: new object[] { "Tbilisi" });

            migrationBuilder.InsertData(
          table: "Cities",
          columns: new[] { "Value" },
          values: new object[] { "Batumi" });

            migrationBuilder.InsertData(
         table: "PhoneTypes",
         columns: new[] { "Value" },
         values: new object[] { "Mobile" });

            migrationBuilder.InsertData(
        table: "PhoneTypes",
        columns: new[] { "Value" },
        values: new object[] { "Office" });

            migrationBuilder.InsertData(
        table: "PhoneTypes",
        columns: new[] { "Value" },
        values: new object[] { "Home" });

            migrationBuilder.InsertData(
        table: "PersonTypes",
        columns: new[] { "Value" },
        values: new object[] { "Colleague " });
            migrationBuilder.InsertData(
        table: "PersonTypes",
        columns: new[] { "Value" },
        values: new object[] { "Familiar" });
            migrationBuilder.InsertData(
        table: "PersonTypes",
        columns: new[] { "Value" },
        values: new object[] { "Relative" });
            migrationBuilder.InsertData(
        table: "PersonTypes",
        columns: new[] { "Value" },
        values: new object[] { "Other" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PersonRelations");

            migrationBuilder.DropTable(
                name: "Phones");

            migrationBuilder.DropTable(
                name: "People");

            migrationBuilder.DropTable(
                name: "PhoneTypes");

            migrationBuilder.DropTable(
                name: "Attachments");

            migrationBuilder.DropTable(
                name: "Cities");

            migrationBuilder.DropTable(
                name: "Genders");

            migrationBuilder.DropTable(
                name: "PersonTypes");
        }
    }
}
