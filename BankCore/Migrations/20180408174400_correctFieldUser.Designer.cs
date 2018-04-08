﻿// <auto-generated />
using BankCore.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;

namespace BankCore.Migrations
{
    [DbContext(typeof(BankCoreContext))]
    [Migration("20180408174400_correctFieldUser")]
    partial class correctFieldUser
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.1-rtm-125")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("BankCore.Models.Compte", b =>
                {
                    b.Property<Guid>("_id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("Deleted");

                    b.Property<DateTime?>("DeletedAt");

                    b.Property<DateTime?>("UpdatedAt");

                    b.Property<DateTime>("creation_date");

                    b.Property<string>("iban")
                        .IsRequired();

                    b.Property<Guid?>("owner_id")
                        .IsRequired();

                    b.Property<double>("solde");

                    b.HasKey("_id");

                    b.HasIndex("owner_id");

                    b.ToTable("Comptes");
                });

            modelBuilder.Entity("BankCore.Models.Mouvement", b =>
                {
                    b.Property<Guid>("_id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("Deleted");

                    b.Property<DateTime?>("DeletedAt");

                    b.Property<DateTime?>("UpdatedAt");

                    b.Property<Guid?>("compte_id")
                        .IsRequired();

                    b.Property<DateTime>("date");

                    b.Property<string>("libelle")
                        .IsRequired();

                    b.Property<double>("montant");

                    b.HasKey("_id");

                    b.HasIndex("compte_id");

                    b.ToTable("Mouvements");
                });

            modelBuilder.Entity("BankCore.Models.User", b =>
                {
                    b.Property<Guid>("_id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("Deleted");

                    b.Property<DateTime?>("DeletedAt");

                    b.Property<DateTime?>("UpdatedAt");

                    b.Property<string>("email")
                        .IsRequired();

                    b.Property<string>("firstname")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("lastname")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("password")
                        .IsRequired();

                    b.HasKey("_id");

                    b.HasIndex("email")
                        .IsUnique();

                    b.ToTable("Users");
                });

            modelBuilder.Entity("BankCore.Models.Virement", b =>
                {
                    b.Property<Guid>("_id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("Deleted");

                    b.Property<DateTime?>("DeletedAt");

                    b.Property<DateTime?>("UpdatedAt");

                    b.Property<DateTime>("date");

                    b.Property<bool>("done");

                    b.Property<double>("montant");

                    b.Property<string>("recipient_iban")
                        .IsRequired();

                    b.Property<Guid?>("sender_id")
                        .IsRequired();

                    b.HasKey("_id");

                    b.HasIndex("sender_id");

                    b.ToTable("Virements");
                });

            modelBuilder.Entity("BankCore.Models.Compte", b =>
                {
                    b.HasOne("BankCore.Models.User", "owner")
                        .WithMany()
                        .HasForeignKey("owner_id")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("BankCore.Models.Mouvement", b =>
                {
                    b.HasOne("BankCore.Models.Compte", "compte")
                        .WithMany()
                        .HasForeignKey("compte_id")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("BankCore.Models.Virement", b =>
                {
                    b.HasOne("BankCore.Models.User", "sender")
                        .WithMany()
                        .HasForeignKey("sender_id")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
