﻿// <auto-generated />
using System;
using E_proc.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace E_proc.Migrations
{
    [DbContext(typeof(AuthContext))]
    partial class AuthContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("E_proc.Models.Address", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("city")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("countryName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("postalCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("street1")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("street2")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Address");
                });

            modelBuilder.Entity("E_proc.Models.FileData", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("FileExtention")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FileName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FilePath")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FileSize")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MimeType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("OfferId")
                        .HasColumnType("int");

                    b.Property<int?>("TenderId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("OfferId");

                    b.HasIndex("TenderId");

                    b.ToTable("FileData");
                });

            modelBuilder.Entity("E_proc.Models.Licence", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("AcquisitionDate")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Category")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ExpirationDate")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("IssuingInstitutionName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RegistrationNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Licence");
                });

            modelBuilder.Entity("E_proc.Models.Offer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int?>("FinalTotalMontant")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("SupplierId")
                        .HasColumnType("int");

                    b.Property<int>("TenderId")
                        .HasColumnType("int");

                    b.Property<int>("TotalMontant")
                        .HasColumnType("int");

                    b.Property<bool?>("isAccepted")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.HasIndex("SupplierId");

                    b.HasIndex("TenderId");

                    b.ToTable("Offer");
                });

            modelBuilder.Entity("E_proc.Models.Representative", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Position")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SocialSecurityNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SocialSecurityNumberDate")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Representative");
                });

            modelBuilder.Entity("E_proc.Models.Tender", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Budget")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("BusinessKind")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DeadLine")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Departement")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("EvaluationMethod")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Financing")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("StartDate")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("addressReceiptId")
                        .HasColumnType("int");

                    b.Property<string>("createdAt")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("instituteId")
                        .IsRequired()
                        .HasColumnType("int");

                    b.Property<int>("responsibleId")
                        .HasColumnType("int");

                    b.Property<string>("specificationURL")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("updatedAt")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("addressReceiptId");

                    b.HasIndex("instituteId");

                    b.HasIndex("responsibleId");

                    b.ToTable("Tender");
                });

            modelBuilder.Entity("E_proc.Models.TenderClassification", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Amount")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("tenderId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("tenderId");

                    b.ToTable("TenderClassification");
                });

            modelBuilder.Entity("E_proc.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("createdAt")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("updatedAt")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users");

                    b.HasDiscriminator<string>("Discriminator").HasValue("User");
                });

            modelBuilder.Entity("E_proc.Models.UserLogin", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.HasKey("Id");

                    b.ToTable("UsersLogin");
                });

            modelBuilder.Entity("E_proc.Models.Citizen", b =>
                {
                    b.HasBaseType("E_proc.Models.User");

                    b.Property<string>("CIN")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasMaxLength(16)
                        .HasColumnType("nvarchar(16)")
                        .HasColumnName("Citizen_Phone");

                    b.HasDiscriminator().HasValue("Citizen");
                });

            modelBuilder.Entity("E_proc.Models.Institute", b =>
                {
                    b.HasBaseType("E_proc.Models.User");

                    b.Property<string>("AreaType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Fax")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Institute_Fax");

                    b.Property<string>("NameAr")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NameFr")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NotificationEmail")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Institute_Phone");

                    b.Property<string>("TypeOfInstitute")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("addressId")
                        .HasColumnType("int")
                        .HasColumnName("Institute_addressId");

                    b.Property<int>("interlocutorId")
                        .HasColumnType("int");

                    b.Property<string>("representativeName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasIndex("addressId");

                    b.HasIndex("interlocutorId");

                    b.HasDiscriminator().HasValue("Institute");
                });

            modelBuilder.Entity("E_proc.Models.Supplier", b =>
                {
                    b.HasBaseType("E_proc.Models.User");

                    b.Property<string>("BuisnessClassification")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("BuisnessType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CNSSId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Category")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CompanyName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Fax")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RegistrationDate")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RegistrationNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ReplyEmail")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SupplierName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TaxId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("addressId")
                        .HasColumnType("int");

                    b.Property<int>("licenceId")
                        .HasColumnType("int");

                    b.Property<int>("representativeId")
                        .HasColumnType("int");

                    b.HasIndex("addressId");

                    b.HasIndex("licenceId");

                    b.HasIndex("representativeId");

                    b.HasDiscriminator().HasValue("Supplier");
                });

            modelBuilder.Entity("E_proc.Models.FileData", b =>
                {
                    b.HasOne("E_proc.Models.Offer", "Offer")
                        .WithMany("Files")
                        .HasForeignKey("OfferId");

                    b.HasOne("E_proc.Models.Tender", "Tender")
                        .WithMany("Specifications")
                        .HasForeignKey("TenderId");

                    b.Navigation("Offer");

                    b.Navigation("Tender");
                });

            modelBuilder.Entity("E_proc.Models.Offer", b =>
                {
                    b.HasOne("E_proc.Models.Supplier", "Supplier")
                        .WithMany()
                        .HasForeignKey("SupplierId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("E_proc.Models.Tender", "Tender")
                        .WithMany("Offers")
                        .HasForeignKey("TenderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Supplier");

                    b.Navigation("Tender");
                });

            modelBuilder.Entity("E_proc.Models.Tender", b =>
                {
                    b.HasOne("E_proc.Models.Address", "AddressReceipt")
                        .WithMany()
                        .HasForeignKey("addressReceiptId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("E_proc.Models.Institute", "Institute")
                        .WithMany("Tender")
                        .HasForeignKey("instituteId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("E_proc.Models.Representative", "Responsible")
                        .WithMany()
                        .HasForeignKey("responsibleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AddressReceipt");

                    b.Navigation("Institute");

                    b.Navigation("Responsible");
                });

            modelBuilder.Entity("E_proc.Models.TenderClassification", b =>
                {
                    b.HasOne("E_proc.Models.Tender", "Tender")
                        .WithMany("TenderClassification")
                        .HasForeignKey("tenderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Tender");
                });

            modelBuilder.Entity("E_proc.Models.Institute", b =>
                {
                    b.HasOne("E_proc.Models.Address", "address")
                        .WithMany()
                        .HasForeignKey("addressId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("E_proc.Models.Representative", "Interlocutor")
                        .WithMany()
                        .HasForeignKey("interlocutorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Interlocutor");

                    b.Navigation("address");
                });

            modelBuilder.Entity("E_proc.Models.Supplier", b =>
                {
                    b.HasOne("E_proc.Models.Address", "address")
                        .WithMany()
                        .HasForeignKey("addressId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("E_proc.Models.Licence", "licence")
                        .WithMany()
                        .HasForeignKey("licenceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("E_proc.Models.Representative", "representative")
                        .WithMany()
                        .HasForeignKey("representativeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("address");

                    b.Navigation("licence");

                    b.Navigation("representative");
                });

            modelBuilder.Entity("E_proc.Models.Offer", b =>
                {
                    b.Navigation("Files");
                });

            modelBuilder.Entity("E_proc.Models.Tender", b =>
                {
                    b.Navigation("Offers");

                    b.Navigation("Specifications");

                    b.Navigation("TenderClassification");
                });

            modelBuilder.Entity("E_proc.Models.Institute", b =>
                {
                    b.Navigation("Tender");
                });
#pragma warning restore 612, 618
        }
    }
}
