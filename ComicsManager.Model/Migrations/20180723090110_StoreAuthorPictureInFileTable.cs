using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace ComicsManager.Model.Migrations
{
    public partial class StoreAuthorPictureInFileTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Photo",
                table: "Authors");

            migrationBuilder.AddColumn<Guid>(
                name: "PhotoId",
                table: "Authors",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Authors_PhotoId",
                table: "Authors",
                column: "PhotoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Authors_Files_PhotoId",
                table: "Authors",
                column: "PhotoId",
                principalTable: "Files",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Authors_Files_PhotoId",
                table: "Authors");

            migrationBuilder.DropIndex(
                name: "IX_Authors_PhotoId",
                table: "Authors");

            migrationBuilder.DropColumn(
                name: "PhotoId",
                table: "Authors");

            migrationBuilder.AddColumn<string>(
                name: "Photo",
                table: "Authors",
                nullable: true);
        }
    }
}
