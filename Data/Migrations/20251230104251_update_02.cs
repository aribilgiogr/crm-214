using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class update_02 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Activities_Leads_ReleatedLeadId",
                table: "Activities");

            migrationBuilder.DropIndex(
                name: "IX_Activities_ReleatedLeadId",
                table: "Activities");

            migrationBuilder.DropColumn(
                name: "ReleatedLeadId",
                table: "Activities");

            migrationBuilder.CreateIndex(
                name: "IX_Activities_RelatedLeadId",
                table: "Activities",
                column: "RelatedLeadId");

            migrationBuilder.AddForeignKey(
                name: "FK_Activities_Leads_RelatedLeadId",
                table: "Activities",
                column: "RelatedLeadId",
                principalTable: "Leads",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Activities_Leads_RelatedLeadId",
                table: "Activities");

            migrationBuilder.DropIndex(
                name: "IX_Activities_RelatedLeadId",
                table: "Activities");

            migrationBuilder.AddColumn<int>(
                name: "ReleatedLeadId",
                table: "Activities",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Activities_ReleatedLeadId",
                table: "Activities",
                column: "ReleatedLeadId");

            migrationBuilder.AddForeignKey(
                name: "FK_Activities_Leads_ReleatedLeadId",
                table: "Activities",
                column: "ReleatedLeadId",
                principalTable: "Leads",
                principalColumn: "Id");
        }
    }
}
