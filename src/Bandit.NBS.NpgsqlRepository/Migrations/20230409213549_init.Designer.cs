﻿// <auto-generated />
using System;
using Bandit.NBS.NpgsqlRepository;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Bandit.NBS.NpgsqlRepository.Migrations
{
    [DbContext(typeof(NpgsqlDbContext))]
    [Migration("20230409213549_init")]
    partial class init
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Bandit.NBS.NpgsqlRepository.Models.Transaction", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("AuthenticationMethod")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("ClientAge")
                        .HasColumnType("integer");

                    b.Property<DateTime>("ClientBirthDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("ClientGender")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("ClientId")
                        .HasColumnType("uuid");

                    b.Property<string>("ClientMaritalStatus")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<double>("ClientMonthlySalary")
                        .HasColumnType("double precision");

                    b.Property<string>("CreditBank")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("DebitBank")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("MerchantActivity")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("MerchantId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("MerchantName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("TransactionDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<double>("TransferredAmount")
                        .HasColumnType("double precision");

                    b.HasKey("Id");

                    b.ToTable("Transactions");
                });
#pragma warning restore 612, 618
        }
    }
}
