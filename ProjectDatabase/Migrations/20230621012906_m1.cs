using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjectDatabase.Migrations
{
    /// <inheritdoc />
    public partial class m1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Membership",
                columns: table => new
                {
                    id = table.Column<string>(type: "character varying(8)", maxLength: 8, nullable: false),
                    name = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    description = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true),
                    discountvalue = table.Column<float>(name: "discount_value", type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Membership", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Order_type",
                columns: table => new
                {
                    id = table.Column<string>(type: "character varying(8)", maxLength: 8, nullable: false),
                    name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    description = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Order_type", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Product_Type",
                columns: table => new
                {
                    id = table.Column<string>(type: "character varying(8)", maxLength: 8, nullable: false),
                    name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    description = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product_Type", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Province",
                columns: table => new
                {
                    id = table.Column<string>(type: "character varying(8)", maxLength: 8, nullable: false),
                    name = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Province", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Role",
                columns: table => new
                {
                    id = table.Column<string>(type: "character varying(8)", maxLength: 8, nullable: false),
                    name = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    description = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Customer",
                columns: table => new
                {
                    id = table.Column<string>(type: "character varying(8)", maxLength: 8, nullable: false),
                    name = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    membershipid = table.Column<string>(name: "membership_id", type: "character varying(11)", maxLength: 11, nullable: false),
                    phone = table.Column<string>(type: "text", nullable: false),
                    dob = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customer", x => x.id);
                    table.ForeignKey(
                        name: "FK_Customer_Membership_membership_id",
                        column: x => x.membershipid,
                        principalTable: "Membership",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Product",
                columns: table => new
                {
                    id = table.Column<string>(type: "character varying(8)", maxLength: 8, nullable: false),
                    name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Price = table.Column<decimal>(type: "numeric", nullable: false),
                    Description = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true),
                    discount = table.Column<float>(type: "real", nullable: false),
                    type = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    deleteAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product", x => x.id);
                    table.ForeignKey(
                        name: "FK_Product_Product_Type_type",
                        column: x => x.type,
                        principalTable: "Product_Type",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "District",
                columns: table => new
                {
                    id = table.Column<string>(type: "character varying(8)", maxLength: 8, nullable: false),
                    name = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    provinceid = table.Column<string>(name: "province_id", type: "character varying(8)", maxLength: 8, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_District", x => x.id);
                    table.ForeignKey(
                        name: "FK_District_Province_province_id",
                        column: x => x.provinceid,
                        principalTable: "Province",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Store",
                columns: table => new
                {
                    id = table.Column<string>(type: "character varying(8)", maxLength: 8, nullable: false),
                    name = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    description = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    provinceid = table.Column<string>(name: "province_id", type: "character varying(8)", maxLength: 8, nullable: false),
                    districtid = table.Column<string>(name: "district_id", type: "character varying(8)", maxLength: 8, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Store", x => x.id);
                    table.ForeignKey(
                        name: "FK_Store_District_district_id",
                        column: x => x.districtid,
                        principalTable: "District",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Store_Province_province_id",
                        column: x => x.provinceid,
                        principalTable: "Province",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Store_product",
                columns: table => new
                {
                    id = table.Column<string>(type: "character varying(8)", maxLength: 8, nullable: false),
                    storeid = table.Column<string>(name: "store_id", type: "character varying(8)", maxLength: 8, nullable: false),
                    productid = table.Column<string>(name: "product_id", type: "character varying(8)", maxLength: 8, nullable: false),
                    quantity = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Store_product", x => x.id);
                    table.ForeignKey(
                        name: "FK_Store_product_Product_product_id",
                        column: x => x.productid,
                        principalTable: "Product",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Store_product_Store_store_id",
                        column: x => x.storeid,
                        principalTable: "Store",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    id = table.Column<string>(type: "character varying(8)", maxLength: 8, nullable: false),
                    username = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    password = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    name = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    phone = table.Column<string>(type: "character varying(11)", maxLength: 11, nullable: false),
                    roleid = table.Column<string>(name: "role_id", type: "character varying(8)", maxLength: 8, nullable: false),
                    storeid = table.Column<string>(name: "store_id", type: "character varying(8)", maxLength: 8, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.id);
                    table.ForeignKey(
                        name: "FK_User_Role_role_id",
                        column: x => x.roleid,
                        principalTable: "Role",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_User_Store_store_id",
                        column: x => x.storeid,
                        principalTable: "Store",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "Order",
                columns: table => new
                {
                    id = table.Column<string>(type: "character varying(8)", maxLength: 8, nullable: false),
                    totalprice = table.Column<decimal>(name: "total_price", type: "numeric", nullable: false),
                    note = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: false),
                    createby = table.Column<string>(name: "create_by", type: "character varying(8)", maxLength: 8, nullable: true),
                    storeid = table.Column<string>(name: "store_id", type: "character varying(8)", maxLength: 8, nullable: false),
                    createat = table.Column<DateTime>(name: "create_at", type: "timestamp with time zone", nullable: false),
                    updateat = table.Column<DateTime>(name: "update_at", type: "timestamp with time zone", nullable: false),
                    type = table.Column<string>(type: "character varying(8)", maxLength: 8, nullable: false),
                    customerid = table.Column<string>(name: "customer_id", type: "character varying(8)", maxLength: 8, nullable: true),
                    discountmembership = table.Column<decimal>(name: "discount_membership", type: "numeric", nullable: false),
                    finalprice = table.Column<decimal>(name: "final_price", type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Order", x => x.id);
                    table.ForeignKey(
                        name: "FK_Order_Customer_customer_id",
                        column: x => x.customerid,
                        principalTable: "Customer",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_Order_Order_type_type",
                        column: x => x.type,
                        principalTable: "Order_type",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Order_Store_store_id",
                        column: x => x.storeid,
                        principalTable: "Store",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Order_User_create_by",
                        column: x => x.createby,
                        principalTable: "User",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "Orderline",
                columns: table => new
                {
                    id = table.Column<string>(type: "character varying(8)", maxLength: 8, nullable: false),
                    productid = table.Column<string>(name: "product_id", type: "character varying(8)", maxLength: 8, nullable: false),
                    quantity = table.Column<int>(type: "integer", nullable: false),
                    orderid = table.Column<string>(name: "order_id", type: "character varying(8)", maxLength: 8, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orderline", x => x.id);
                    table.ForeignKey(
                        name: "FK_Orderline_Order_order_id",
                        column: x => x.orderid,
                        principalTable: "Order",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Orderline_Product_product_id",
                        column: x => x.productid,
                        principalTable: "Product",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Customer_membership_id",
                table: "Customer",
                column: "membership_id");

            migrationBuilder.CreateIndex(
                name: "IX_District_province_id",
                table: "District",
                column: "province_id");

            migrationBuilder.CreateIndex(
                name: "IX_Order_create_by",
                table: "Order",
                column: "create_by");

            migrationBuilder.CreateIndex(
                name: "IX_Order_customer_id",
                table: "Order",
                column: "customer_id");

            migrationBuilder.CreateIndex(
                name: "IX_Order_store_id",
                table: "Order",
                column: "store_id");

            migrationBuilder.CreateIndex(
                name: "IX_Order_type",
                table: "Order",
                column: "type");

            migrationBuilder.CreateIndex(
                name: "IX_Orderline_order_id",
                table: "Orderline",
                column: "order_id");

            migrationBuilder.CreateIndex(
                name: "IX_Orderline_product_id",
                table: "Orderline",
                column: "product_id");

            migrationBuilder.CreateIndex(
                name: "IX_Product_type",
                table: "Product",
                column: "type");

            migrationBuilder.CreateIndex(
                name: "IX_Store_district_id",
                table: "Store",
                column: "district_id");

            migrationBuilder.CreateIndex(
                name: "IX_Store_province_id",
                table: "Store",
                column: "province_id");

            migrationBuilder.CreateIndex(
                name: "IX_Store_product_product_id",
                table: "Store_product",
                column: "product_id");

            migrationBuilder.CreateIndex(
                name: "IX_Store_product_store_id",
                table: "Store_product",
                column: "store_id");

            migrationBuilder.CreateIndex(
                name: "IX_User_role_id",
                table: "User",
                column: "role_id");

            migrationBuilder.CreateIndex(
                name: "IX_User_store_id",
                table: "User",
                column: "store_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Orderline");

            migrationBuilder.DropTable(
                name: "Store_product");

            migrationBuilder.DropTable(
                name: "Order");

            migrationBuilder.DropTable(
                name: "Product");

            migrationBuilder.DropTable(
                name: "Customer");

            migrationBuilder.DropTable(
                name: "Order_type");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "Product_Type");

            migrationBuilder.DropTable(
                name: "Membership");

            migrationBuilder.DropTable(
                name: "Role");

            migrationBuilder.DropTable(
                name: "Store");

            migrationBuilder.DropTable(
                name: "District");

            migrationBuilder.DropTable(
                name: "Province");
        }
    }
}
