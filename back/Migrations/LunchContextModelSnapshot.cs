﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace back.Migrations
{
    [DbContext(typeof(LunchContext))]
    partial class LunchContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.10");

            modelBuilder.Entity("Restaurant", b =>
                {
                    b.Property<int>("RestaurantId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("RestaurantId");

                    b.ToTable("Restaurants");

                    b.HasData(
                        new
                        {
                            RestaurantId = 1,
                            Name = "McDonalds"
                        },
                        new
                        {
                            RestaurantId = 2,
                            Name = "BK"
                        },
                        new
                        {
                            RestaurantId = 3,
                            Name = "Sujinho Da Esquina"
                        },
                        new
                        {
                            RestaurantId = 4,
                            Name = "Limpinho Caro"
                        },
                        new
                        {
                            RestaurantId = 5,
                            Name = "Delivery"
                        });
                });

            modelBuilder.Entity("Vote", b =>
                {
                    b.Property<int>("VoteId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("Day")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Identifier")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("Month")
                        .HasColumnType("INTEGER");

                    b.Property<int>("RestaurantId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("WeekNumber")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Year")
                        .HasColumnType("INTEGER");

                    b.HasKey("VoteId");

                    b.HasIndex("RestaurantId");

                    b.HasIndex("Identifier", "Year", "Month", "Day")
                        .IsUnique();

                    b.ToTable("Votes");
                });

            modelBuilder.Entity("Vote", b =>
                {
                    b.HasOne("Restaurant", "Restaurant")
                        .WithMany("Votes")
                        .HasForeignKey("RestaurantId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Restaurant");
                });

            modelBuilder.Entity("Restaurant", b =>
                {
                    b.Navigation("Votes");
                });
#pragma warning restore 612, 618
        }
    }
}
