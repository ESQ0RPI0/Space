﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Space.Server.Database.Context;

#nullable disable

namespace Space.Server.Database.NewSpaceMigrations
{
    [DbContext(typeof(NewSpaceContext))]
    [Migration("20230426225149_NewSpaceInit")]
    partial class NewSpaceInit
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Space.Server.Datamodel.DatabaseModels.NewSpace.NewSpaceCompanyDbModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.HasKey("Id");

                    b.ToTable("NewSpaceCompanies");
                });

            modelBuilder.Entity("Space.Server.Datamodel.DatabaseModels.NewSpace.NewSpaceLaunchVehicleDbModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.HasKey("Id");

                    b.ToTable("NewSpaceLaunchVehicles");
                });
#pragma warning restore 612, 618
        }
    }
}