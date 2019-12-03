using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.App.EF.Migrations
{
    public partial class InitialDbCreation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MultiLangStrings",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Value = table.Column<string>(maxLength: 10240, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MultiLangStrings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    RoleId = table.Column<int>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ManuFacturers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ManuFacturerNameId = table.Column<int>(nullable: false),
                    ManuFacturerAadressId = table.Column<int>(nullable: false),
                    ManuFacturerPhoneNumberId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ManuFacturers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ManuFacturers_MultiLangStrings_ManuFacturerAadressId",
                        column: x => x.ManuFacturerAadressId,
                        principalTable: "MultiLangStrings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ManuFacturers_MultiLangStrings_ManuFacturerNameId",
                        column: x => x.ManuFacturerNameId,
                        principalTable: "MultiLangStrings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ManuFacturers_MultiLangStrings_ManuFacturerPhoneNumberId",
                        column: x => x.ManuFacturerPhoneNumberId,
                        principalTable: "MultiLangStrings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Shippers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ShipperNameId = table.Column<int>(nullable: false),
                    ShipperAddressId = table.Column<int>(nullable: false),
                    ShipperPhoneNumberId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Shippers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Shippers_MultiLangStrings_ShipperAddressId",
                        column: x => x.ShipperAddressId,
                        principalTable: "MultiLangStrings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Shippers_MultiLangStrings_ShipperNameId",
                        column: x => x.ShipperNameId,
                        principalTable: "MultiLangStrings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Shippers_MultiLangStrings_ShipperPhoneNumberId",
                        column: x => x.ShipperPhoneNumberId,
                        principalTable: "MultiLangStrings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Shops",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ShopShopNameId = table.Column<int>(nullable: false),
                    ShopShopAddressId = table.Column<int>(nullable: false),
                    ShopShopContactId = table.Column<int>(nullable: false),
                    ShopShopContact2Id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Shops", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Shops_MultiLangStrings_ShopShopAddressId",
                        column: x => x.ShopShopAddressId,
                        principalTable: "MultiLangStrings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Shops_MultiLangStrings_ShopShopContact2Id",
                        column: x => x.ShopShopContact2Id,
                        principalTable: "MultiLangStrings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Shops_MultiLangStrings_ShopShopContactId",
                        column: x => x.ShopShopContactId,
                        principalTable: "MultiLangStrings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Shops_MultiLangStrings_ShopShopNameId",
                        column: x => x.ShopShopNameId,
                        principalTable: "MultiLangStrings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Translations",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Culture = table.Column<string>(maxLength: 5, nullable: true),
                    Value = table.Column<string>(maxLength: 10240, nullable: true),
                    MultiLangStringId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Translations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Translations_MultiLangStrings_MultiLangStringId",
                        column: x => x.MultiLangStringId,
                        principalTable: "MultiLangStrings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    UserName = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(maxLength: 256, nullable: true),
                    Email = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    PasswordHash = table.Column<string>(nullable: true),
                    SecurityStamp = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false),
                    FirstName = table.Column<string>(maxLength: 64, nullable: false),
                    LastName = table.Column<string>(maxLength: 64, nullable: false),
                    Aadress = table.Column<string>(nullable: true),
                    ShopId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUsers_Shops_ShopId",
                        column: x => x.ShopId,
                        principalTable: "Shops",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CategoryNameId = table.Column<int>(nullable: false),
                    ShopId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Categories_MultiLangStrings_CategoryNameId",
                        column: x => x.CategoryNameId,
                        principalTable: "MultiLangStrings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Categories_Shops_ShopId",
                        column: x => x.ShopId,
                        principalTable: "Shops",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Defects",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    DefectDescriptionId = table.Column<int>(nullable: false),
                    ShopId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Defects", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Defects_MultiLangStrings_DefectDescriptionId",
                        column: x => x.DefectDescriptionId,
                        principalTable: "MultiLangStrings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Defects_Shops_ShopId",
                        column: x => x.ShopId,
                        principalTable: "Shops",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Inventory",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    InventoryDescriptionId = table.Column<int>(nullable: false),
                    InventoryCreationTime = table.Column<DateTime>(nullable: false),
                    ShopId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Inventory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Inventory_MultiLangStrings_InventoryDescriptionId",
                        column: x => x.InventoryDescriptionId,
                        principalTable: "MultiLangStrings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Inventory_Shops_ShopId",
                        column: x => x.ShopId,
                        principalTable: "Shops",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    OrderDescriptionId = table.Column<int>(nullable: false),
                    OrderCreationTime = table.Column<DateTime>(nullable: false),
                    ShipperId = table.Column<int>(nullable: false),
                    ShopId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orders_MultiLangStrings_OrderDescriptionId",
                        column: x => x.OrderDescriptionId,
                        principalTable: "MultiLangStrings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Orders_Shippers_ShipperId",
                        column: x => x.ShipperId,
                        principalTable: "Shippers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Orders_Shops_ShopId",
                        column: x => x.ShopId,
                        principalTable: "Shops",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Returns",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ReturnDescriptionId = table.Column<int>(nullable: false),
                    ShopId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Returns", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Returns_MultiLangStrings_ReturnDescriptionId",
                        column: x => x.ReturnDescriptionId,
                        principalTable: "MultiLangStrings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Returns_Shops_ShopId",
                        column: x => x.ShopId,
                        principalTable: "Shops",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<int>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(nullable: false),
                    ProviderKey = table.Column<string>(nullable: false),
                    ProviderDisplayName = table.Column<string>(nullable: true),
                    UserId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<int>(nullable: false),
                    RoleId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<int>(nullable: false),
                    LoginProvider = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Sales",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    SaleDescriptionId = table.Column<int>(nullable: false),
                    SaleInitialCreationTime = table.Column<DateTime>(nullable: false),
                    AppUserId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sales", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Sales_AspNetUsers_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Sales_MultiLangStrings_SaleDescriptionId",
                        column: x => x.SaleDescriptionId,
                        principalTable: "MultiLangStrings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ProductManuFacturerItemCodeId = table.Column<int>(nullable: false),
                    ProductShopCodeId = table.Column<int>(nullable: false),
                    ProductProductNameId = table.Column<int>(nullable: false),
                    ProductWeightId = table.Column<int>(nullable: false),
                    ProductLengthId = table.Column<int>(nullable: false),
                    BuyPrice = table.Column<decimal>(type: "decimal(18, 6)", nullable: false),
                    PercentageAddedToBuyPrice = table.Column<int>(nullable: false),
                    SellPrice = table.Column<decimal>(type: "decimal(18, 6)", nullable: false),
                    Quantity = table.Column<int>(nullable: false),
                    ManuFacturerId = table.Column<int>(nullable: false),
                    InventoryId = table.Column<int>(nullable: true),
                    ShopId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Products_Inventory_InventoryId",
                        column: x => x.InventoryId,
                        principalTable: "Inventory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Products_ManuFacturers_ManuFacturerId",
                        column: x => x.ManuFacturerId,
                        principalTable: "ManuFacturers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Products_MultiLangStrings_ProductLengthId",
                        column: x => x.ProductLengthId,
                        principalTable: "MultiLangStrings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Products_MultiLangStrings_ProductManuFacturerItemCodeId",
                        column: x => x.ProductManuFacturerItemCodeId,
                        principalTable: "MultiLangStrings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Products_MultiLangStrings_ProductProductNameId",
                        column: x => x.ProductProductNameId,
                        principalTable: "MultiLangStrings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Products_MultiLangStrings_ProductShopCodeId",
                        column: x => x.ProductShopCodeId,
                        principalTable: "MultiLangStrings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Products_MultiLangStrings_ProductWeightId",
                        column: x => x.ProductWeightId,
                        principalTable: "MultiLangStrings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Products_Shops_ShopId",
                        column: x => x.ShopId,
                        principalTable: "Shops",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Comments",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CommentTitleId = table.Column<int>(nullable: false),
                    CommentBodyId = table.Column<int>(nullable: false),
                    ProductId = table.Column<int>(nullable: false),
                    ShopId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Comments_MultiLangStrings_CommentBodyId",
                        column: x => x.CommentBodyId,
                        principalTable: "MultiLangStrings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Comments_MultiLangStrings_CommentTitleId",
                        column: x => x.CommentTitleId,
                        principalTable: "MultiLangStrings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Comments_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Comments_Shops_ShopId",
                        column: x => x.ShopId,
                        principalTable: "Shops",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProductsInCategory",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ProductId = table.Column<int>(nullable: false),
                    CategoryId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductsInCategory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductsInCategory_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProductsInCategory_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProductsInOrder",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Quantity = table.Column<int>(nullable: false),
                    ProductInOrderPlacingTime = table.Column<DateTime>(nullable: false),
                    ProductId = table.Column<int>(nullable: false),
                    OrderId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductsInOrder", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductsInOrder_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProductsInOrder_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProductsReturned",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Quantity = table.Column<int>(nullable: false),
                    ProductReturnedTime = table.Column<DateTime>(nullable: false),
                    ProductId = table.Column<int>(nullable: false),
                    ReturnId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductsReturned", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductsReturned_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProductsReturned_Returns_ReturnId",
                        column: x => x.ReturnId,
                        principalTable: "Returns",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProductsSold",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Quantity = table.Column<int>(nullable: false),
                    ProductSoldTime = table.Column<DateTime>(nullable: false),
                    ProductId = table.Column<int>(nullable: false),
                    SaleId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductsSold", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductsSold_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProductsSold_Sales_SaleId",
                        column: x => x.SaleId,
                        principalTable: "Sales",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProductsWithDefect",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Quantity = table.Column<int>(nullable: false),
                    DefectRecordingTime = table.Column<DateTime>(nullable: false),
                    ProductId = table.Column<int>(nullable: false),
                    DefectId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductsWithDefect", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductsWithDefect_Defects_DefectId",
                        column: x => x.DefectId,
                        principalTable: "Defects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProductsWithDefect_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_ShopId",
                table: "AspNetUsers",
                column: "ShopId");

            migrationBuilder.CreateIndex(
                name: "IX_Categories_CategoryNameId",
                table: "Categories",
                column: "CategoryNameId");

            migrationBuilder.CreateIndex(
                name: "IX_Categories_ShopId",
                table: "Categories",
                column: "ShopId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_CommentBodyId",
                table: "Comments",
                column: "CommentBodyId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_CommentTitleId",
                table: "Comments",
                column: "CommentTitleId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_ProductId",
                table: "Comments",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_ShopId",
                table: "Comments",
                column: "ShopId");

            migrationBuilder.CreateIndex(
                name: "IX_Defects_DefectDescriptionId",
                table: "Defects",
                column: "DefectDescriptionId");

            migrationBuilder.CreateIndex(
                name: "IX_Defects_ShopId",
                table: "Defects",
                column: "ShopId");

            migrationBuilder.CreateIndex(
                name: "IX_Inventory_InventoryDescriptionId",
                table: "Inventory",
                column: "InventoryDescriptionId");

            migrationBuilder.CreateIndex(
                name: "IX_Inventory_ShopId",
                table: "Inventory",
                column: "ShopId");

            migrationBuilder.CreateIndex(
                name: "IX_ManuFacturers_ManuFacturerAadressId",
                table: "ManuFacturers",
                column: "ManuFacturerAadressId");

            migrationBuilder.CreateIndex(
                name: "IX_ManuFacturers_ManuFacturerNameId",
                table: "ManuFacturers",
                column: "ManuFacturerNameId");

            migrationBuilder.CreateIndex(
                name: "IX_ManuFacturers_ManuFacturerPhoneNumberId",
                table: "ManuFacturers",
                column: "ManuFacturerPhoneNumberId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_OrderDescriptionId",
                table: "Orders",
                column: "OrderDescriptionId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_ShipperId",
                table: "Orders",
                column: "ShipperId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_ShopId",
                table: "Orders",
                column: "ShopId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_InventoryId",
                table: "Products",
                column: "InventoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_ManuFacturerId",
                table: "Products",
                column: "ManuFacturerId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_ProductLengthId",
                table: "Products",
                column: "ProductLengthId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_ProductManuFacturerItemCodeId",
                table: "Products",
                column: "ProductManuFacturerItemCodeId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_ProductProductNameId",
                table: "Products",
                column: "ProductProductNameId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_ProductShopCodeId",
                table: "Products",
                column: "ProductShopCodeId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_ProductWeightId",
                table: "Products",
                column: "ProductWeightId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_ShopId",
                table: "Products",
                column: "ShopId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductsInCategory_CategoryId",
                table: "ProductsInCategory",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductsInCategory_ProductId",
                table: "ProductsInCategory",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductsInOrder_OrderId",
                table: "ProductsInOrder",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductsInOrder_ProductId",
                table: "ProductsInOrder",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductsReturned_ProductId",
                table: "ProductsReturned",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductsReturned_ReturnId",
                table: "ProductsReturned",
                column: "ReturnId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductsSold_ProductId",
                table: "ProductsSold",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductsSold_SaleId",
                table: "ProductsSold",
                column: "SaleId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductsWithDefect_DefectId",
                table: "ProductsWithDefect",
                column: "DefectId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductsWithDefect_ProductId",
                table: "ProductsWithDefect",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Returns_ReturnDescriptionId",
                table: "Returns",
                column: "ReturnDescriptionId");

            migrationBuilder.CreateIndex(
                name: "IX_Returns_ShopId",
                table: "Returns",
                column: "ShopId");

            migrationBuilder.CreateIndex(
                name: "IX_Sales_AppUserId",
                table: "Sales",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Sales_SaleDescriptionId",
                table: "Sales",
                column: "SaleDescriptionId");

            migrationBuilder.CreateIndex(
                name: "IX_Shippers_ShipperAddressId",
                table: "Shippers",
                column: "ShipperAddressId");

            migrationBuilder.CreateIndex(
                name: "IX_Shippers_ShipperNameId",
                table: "Shippers",
                column: "ShipperNameId");

            migrationBuilder.CreateIndex(
                name: "IX_Shippers_ShipperPhoneNumberId",
                table: "Shippers",
                column: "ShipperPhoneNumberId");

            migrationBuilder.CreateIndex(
                name: "IX_Shops_ShopShopAddressId",
                table: "Shops",
                column: "ShopShopAddressId");

            migrationBuilder.CreateIndex(
                name: "IX_Shops_ShopShopContact2Id",
                table: "Shops",
                column: "ShopShopContact2Id");

            migrationBuilder.CreateIndex(
                name: "IX_Shops_ShopShopContactId",
                table: "Shops",
                column: "ShopShopContactId");

            migrationBuilder.CreateIndex(
                name: "IX_Shops_ShopShopNameId",
                table: "Shops",
                column: "ShopShopNameId");

            migrationBuilder.CreateIndex(
                name: "IX_Translations_MultiLangStringId",
                table: "Translations",
                column: "MultiLangStringId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "Comments");

            migrationBuilder.DropTable(
                name: "ProductsInCategory");

            migrationBuilder.DropTable(
                name: "ProductsInOrder");

            migrationBuilder.DropTable(
                name: "ProductsReturned");

            migrationBuilder.DropTable(
                name: "ProductsSold");

            migrationBuilder.DropTable(
                name: "ProductsWithDefect");

            migrationBuilder.DropTable(
                name: "Translations");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Returns");

            migrationBuilder.DropTable(
                name: "Sales");

            migrationBuilder.DropTable(
                name: "Defects");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Shippers");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Inventory");

            migrationBuilder.DropTable(
                name: "ManuFacturers");

            migrationBuilder.DropTable(
                name: "Shops");

            migrationBuilder.DropTable(
                name: "MultiLangStrings");
        }
    }
}
