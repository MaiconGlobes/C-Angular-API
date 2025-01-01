﻿// <auto-generated />
using System;
using BaseCodeAPI.Src;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace BasicEssencialsAPI.Migrations
{
    [DbContext(typeof(Context))]
    partial class ContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("BaseCodeAPI.Src.Models.Entity.AddressModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    b.Property<string>("Address")
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)")
                        .HasColumnName("address");

                    b.Property<string>("City")
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)")
                        .HasColumnName("city");

                    b.Property<string>("District")
                        .HasMaxLength(35)
                        .HasColumnType("varchar(35)")
                        .HasColumnName("district");

                    b.Property<string>("Number")
                        .HasMaxLength(10)
                        .HasColumnType("varchar(10)")
                        .HasColumnName("number");

                    b.Property<int>("PersonId")
                        .HasColumnType("int")
                        .HasColumnName("person_id");

                    b.Property<string>("Uf")
                        .HasMaxLength(2)
                        .HasColumnType("varchar(2)")
                        .HasColumnName("uf");

                    b.Property<string>("ZipCode")
                        .HasMaxLength(8)
                        .HasColumnType("varchar(8)")
                        .HasColumnName("zip_code");

                    b.HasKey("Id");

                    b.HasIndex("PersonId", "Number", "ZipCode")
                        .IsUnique()
                        .HasDatabaseName("UK_address_for_person_number_zipcode");

                    b.ToTable("address");
                });

            modelBuilder.Entity("BaseCodeAPI.Src.Models.Entity.ClientModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    b.Property<decimal>("CreditLimit")
                        .HasColumnType("numeric(18,2)")
                        .HasColumnName("credit_limit");

                    b.Property<DateTime>("LastPurchase")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("last_purchase");

                    b.Property<int>("PersonId")
                        .HasColumnType("int")
                        .HasColumnName("person_id");

                    b.HasKey("Id");

                    b.HasIndex("PersonId")
                        .IsUnique();

                    b.ToTable("client");
                });

            modelBuilder.Entity("BaseCodeAPI.Src.Models.Entity.PersonModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    b.Property<string>("Document")
                        .IsRequired()
                        .HasMaxLength(14)
                        .HasColumnType("varchar(14)")
                        .HasColumnName("document");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("name");

                    b.Property<string>("Phone")
                        .HasMaxLength(11)
                        .HasColumnType("varchar(11)")
                        .HasColumnName("phone");

                    b.HasKey("Id");

                    b.HasIndex("Document")
                        .IsUnique();

                    b.ToTable("person");
                });

            modelBuilder.Entity("BaseCodeAPI.Src.Models.Entity.SupplierModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    b.Property<int>("PersonId")
                        .HasColumnType("int")
                        .HasColumnName("person_id");

                    b.HasKey("Id");

                    b.HasIndex("PersonId")
                        .IsUnique();

                    b.ToTable("supplier");
                });

            modelBuilder.Entity("BaseCodeAPI.Src.Models.Entity.TransporterModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    b.Property<int>("PersonId")
                        .HasColumnType("int")
                        .HasColumnName("person_id");

                    b.HasKey("Id");

                    b.HasIndex("PersonId")
                        .IsUnique();

                    b.ToTable("transporter");
                });

            modelBuilder.Entity("BaseCodeAPI.Src.Models.Entity.UserModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)")
                        .HasColumnName("email");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)")
                        .HasColumnName("password");

                    b.Property<int>("PersonId")
                        .HasColumnType("int")
                        .HasColumnName("person_id");

                    b.Property<string>("RefreshToken")
                        .HasColumnType("text")
                        .HasColumnName("refresh_token");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.HasIndex("PersonId")
                        .IsUnique();

                    b.HasIndex("PersonId", "Email")
                        .IsUnique()
                        .HasDatabaseName("UK_user_for_person_email");

                    b.ToTable("user");
                });

            modelBuilder.Entity("BaseCodeAPI.Src.Models.Entity.AddressModel", b =>
                {
                    b.HasOne("BaseCodeAPI.Src.Models.Entity.PersonModel", "Person")
                        .WithMany("Addresses")
                        .HasForeignKey("PersonId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Person");
                });

            modelBuilder.Entity("BaseCodeAPI.Src.Models.Entity.ClientModel", b =>
                {
                    b.HasOne("BaseCodeAPI.Src.Models.Entity.PersonModel", "Person")
                        .WithOne("Client")
                        .HasForeignKey("BaseCodeAPI.Src.Models.Entity.ClientModel", "PersonId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Person");
                });

            modelBuilder.Entity("BaseCodeAPI.Src.Models.Entity.SupplierModel", b =>
                {
                    b.HasOne("BaseCodeAPI.Src.Models.Entity.PersonModel", "Person")
                        .WithOne("Supplier")
                        .HasForeignKey("BaseCodeAPI.Src.Models.Entity.SupplierModel", "PersonId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Person");
                });

            modelBuilder.Entity("BaseCodeAPI.Src.Models.Entity.TransporterModel", b =>
                {
                    b.HasOne("BaseCodeAPI.Src.Models.Entity.PersonModel", "Person")
                        .WithOne("Transporter")
                        .HasForeignKey("BaseCodeAPI.Src.Models.Entity.TransporterModel", "PersonId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Person");
                });

            modelBuilder.Entity("BaseCodeAPI.Src.Models.Entity.UserModel", b =>
                {
                    b.HasOne("BaseCodeAPI.Src.Models.Entity.PersonModel", "Person")
                        .WithOne("User")
                        .HasForeignKey("BaseCodeAPI.Src.Models.Entity.UserModel", "PersonId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Person");
                });

            modelBuilder.Entity("BaseCodeAPI.Src.Models.Entity.PersonModel", b =>
                {
                    b.Navigation("Addresses");

                    b.Navigation("Client");

                    b.Navigation("Supplier");

                    b.Navigation("Transporter");

                    b.Navigation("User");
                });
#pragma warning restore 612, 618
        }
    }
}