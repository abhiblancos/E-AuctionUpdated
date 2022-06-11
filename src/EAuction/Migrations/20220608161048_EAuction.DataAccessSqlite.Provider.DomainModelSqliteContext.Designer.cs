﻿// <auto-generated />
using System;
using EAuction.DataAccessSqlite.Provider;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace EAuction.API.Write.Migrations
{
    [DbContext(typeof(DomainModelSqliteContext))]
    [Migration("20220608161048_EAuction.DataAccessSqlite.Provider.DomainModelSqliteContext")]
    partial class EAuctionDataAccessSqliteProviderDomainModelSqliteContext
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.17")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("EAuction.Domain.Buyer.BuyerInfo", b =>
                {
                    b.Property<int>("BuyerId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("BidAmount")
                        .HasColumnType("float");

                    b.Property<string>("City")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PinCode")
                        .HasColumnType("int");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<string>("State")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("BuyerId");

                    b.ToTable("BuyerInfo");
                });

            modelBuilder.Entity("EAuction.Domain.Model.DataEventRecord", b =>
                {
                    b.Property<long>("DataEventRecordId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("SourceInfoId")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("Timestamp")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("UpdatedTimestamp")
                        .HasColumnType("datetime2");

                    b.HasKey("DataEventRecordId");

                    b.HasIndex("SourceInfoId");

                    b.ToTable("DataEventRecords");
                });

            modelBuilder.Entity("EAuction.Domain.Model.SourceInfo", b =>
                {
                    b.Property<long>("SourceInfoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Timestamp")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("UpdatedTimestamp")
                        .HasColumnType("datetime2");

                    b.HasKey("SourceInfoId");

                    b.ToTable("SourceInfos");
                });

            modelBuilder.Entity("EAuction.Domain.Product.Product", b =>
                {
                    b.Property<int>("ProductId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("BidEndDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Category")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("DetailedDescription")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("ProductName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("SellerId")
                        .HasColumnType("int");

                    b.Property<string>("ShortDescription")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("StartingPrice")
                        .HasColumnType("float");

                    b.HasKey("ProductId");

                    b.ToTable("ProductInfo");
                });

            modelBuilder.Entity("EAuction.Domain.Seller.Seller", b =>
                {
                    b.Property<int>("SellerId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("City")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PinCode")
                        .HasColumnType("int");

                    b.Property<string>("State")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("SellerId");

                    b.ToTable("SellerInfo");
                });

            modelBuilder.Entity("EAuction.Domain.Model.DataEventRecord", b =>
                {
                    b.HasOne("EAuction.Domain.Model.SourceInfo", "SourceInfo")
                        .WithMany("DataEventRecords")
                        .HasForeignKey("SourceInfoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("SourceInfo");
                });

            modelBuilder.Entity("EAuction.Domain.Model.SourceInfo", b =>
                {
                    b.Navigation("DataEventRecords");
                });
#pragma warning restore 612, 618
        }
    }
}
