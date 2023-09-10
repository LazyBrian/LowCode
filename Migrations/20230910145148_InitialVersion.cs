using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LowCode.Migrations
{
    /// <inheritdoc />
    public partial class InitialVersion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "InternalAttributeType",
                columns: table => new
                {
                    AttributeTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: true),
                    SqlType = table.Column<int>(type: "int", nullable: false),
                    CustomType = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: true),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<int>(type: "int", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AttributeType", x => x.AttributeTypeId)
                        .Annotation("SqlServer:Clustered", false);
                });

            migrationBuilder.CreateTable(
                name: "InternalEntity",
                columns: table => new
                {
                    EntityId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LogicalName = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: true),
                    DisplayName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    IsCustomEntity = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "((0))"),
                    EntityMask = table.Column<int>(type: "int", nullable: false, defaultValueSql: "((0))"),
                    CreatedBy = table.Column<int>(type: "int", unicode: false, maxLength: 20, nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime", nullable: true),
                    ModifiedBy = table.Column<int>(type: "int", unicode: false, maxLength: 20, nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Entity", x => x.EntityId)
                        .Annotation("SqlServer:Clustered", false);
                });

            migrationBuilder.CreateTable(
                name: "InternalAttribute",
                columns: table => new
                {
                    AttributeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LogicalName = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: true),
                    DisplayName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    AttributeMask = table.Column<int>(type: "int", nullable: false, defaultValueSql: "((0))"),
                    DefaultValue = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: true),
                    IsCustomField = table.Column<bool>(type: "bit", nullable: true, defaultValueSql: "((0))"),
                    IsPKAttribute = table.Column<bool>(type: "bit", nullable: true, defaultValueSql: "((0))"),
                    MaxLength = table.Column<int>(type: "int", nullable: true),
                    MinValue = table.Column<decimal>(type: "decimal(18,0)", nullable: true),
                    MaxValue = table.Column<decimal>(type: "decimal(18,0)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "((1))"),
                    CreatedBy = table.Column<int>(type: "int", unicode: false, maxLength: 20, nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime", nullable: true),
                    ModifiedBy = table.Column<int>(type: "int", unicode: false, maxLength: 20, nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "datetime", nullable: true),
                    EntityId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    AttributeTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Attribute", x => x.AttributeId)
                        .Annotation("SqlServer:Clustered", false);
                    table.ForeignKey(
                        name: "FK_InternalAttribute_InternalAttributeType_AttributeTypeId",
                        column: x => x.AttributeTypeId,
                        principalTable: "InternalAttributeType",
                        principalColumn: "AttributeTypeId");
                    table.ForeignKey(
                        name: "FK_InternalAttribute_InternalEntity_EntityId",
                        column: x => x.EntityId,
                        principalTable: "InternalEntity",
                        principalColumn: "EntityId");
                });

            migrationBuilder.CreateTable(
                name: "InternalUIForm",
                columns: table => new
                {
                    FormId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FormName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    FormJson = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    IsDefault = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "((0))"),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime", nullable: true),
                    ModifiedBy = table.Column<int>(type: "int", unicode: false, maxLength: 20, nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "datetime", nullable: true),
                    EntityId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UIForm", x => x.FormId)
                        .Annotation("SqlServer:Clustered", false);
                    table.ForeignKey(
                        name: "FK_InternalUIForm_InternalEntity_EntityId",
                        column: x => x.EntityId,
                        principalTable: "InternalEntity",
                        principalColumn: "EntityId");
                });

            migrationBuilder.CreateTable(
                name: "InternalUiView",
                columns: table => new
                {
                    ViewId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ViewName = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: true),
                    SearchFormJson = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LayoutJson = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDefault = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    Description = table.Column<short>(type: "smallint", nullable: true),
                    QueryType = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<int>(type: "int", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EntityId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UIView", x => x.ViewId)
                        .Annotation("SqlServer:Clustered", false);
                    table.ForeignKey(
                        name: "FK_InternalUiView_InternalEntity_EntityId",
                        column: x => x.EntityId,
                        principalTable: "InternalEntity",
                        principalColumn: "EntityId");
                });

            migrationBuilder.CreateIndex(
                name: "idx_attribute_displayname",
                table: "InternalAttribute",
                column: "DisplayName");

            migrationBuilder.CreateIndex(
                name: "idx_attribute_logicalname",
                table: "InternalAttribute",
                column: "LogicalName");

            migrationBuilder.CreateIndex(
                name: "IX_InternalAttribute_AttributeTypeId",
                table: "InternalAttribute",
                column: "AttributeTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_InternalAttribute_EntityId",
                table: "InternalAttribute",
                column: "EntityId");

            migrationBuilder.CreateIndex(
                name: "idx_name",
                table: "InternalAttributeType",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "idx_entity_displayname",
                table: "InternalEntity",
                column: "DisplayName");

            migrationBuilder.CreateIndex(
                name: "idx_entity_logicalname",
                table: "InternalEntity",
                column: "LogicalName");

            migrationBuilder.CreateIndex(
                name: "idx_uiform_formname",
                table: "InternalUIForm",
                column: "FormName");

            migrationBuilder.CreateIndex(
                name: "IX_InternalUIForm_EntityId",
                table: "InternalUIForm",
                column: "EntityId");

            migrationBuilder.CreateIndex(
                name: "idx_uiview_viewname",
                table: "InternalUiView",
                column: "ViewName");

            migrationBuilder.CreateIndex(
                name: "IX_InternalUiView_EntityId",
                table: "InternalUiView",
                column: "EntityId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InternalAttribute");

            migrationBuilder.DropTable(
                name: "InternalUIForm");

            migrationBuilder.DropTable(
                name: "InternalUiView");

            migrationBuilder.DropTable(
                name: "InternalAttributeType");

            migrationBuilder.DropTable(
                name: "InternalEntity");
        }
    }
}
