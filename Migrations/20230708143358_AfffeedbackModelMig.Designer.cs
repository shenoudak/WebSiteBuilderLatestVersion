﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using iTech.Data;

#nullable disable

namespace iTech.Migrations
{
    [DbContext(typeof(CRMDBContext))]
    [Migration("20230708143358_AfffeedbackModelMig")]
    partial class AfffeedbackModelMig
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("iTech.Models.AffilateFeedback", b =>
                {
                    b.Property<int>("AffilateFeedbackId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("AffilateFeedbackId"));

                    b.Property<string>("TitleAr")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TitleEn")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserOneFeedbackAr")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserOneFeedbackEn")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserThreeFeedbackAr")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserThreeFeedbackEn")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserTwoFeedbackAr")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserTwoFeedbackEn")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("AffilateFeedbackId");

                    b.ToTable("AffilateFeedbacks");
                });

            modelBuilder.Entity("iTech.Models.AffiliatePrice", b =>
                {
                    b.Property<int>("AffiliatePriceId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("AffiliatePriceId"));

                    b.Property<string>("AffiliatePriceTitle")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("From")
                        .HasColumnType("float");

                    b.Property<double>("Point")
                        .HasColumnType("float");

                    b.Property<double>("To")
                        .HasColumnType("float");

                    b.HasKey("AffiliatePriceId");

                    b.ToTable("AffiliatePrices");
                });

            modelBuilder.Entity("iTech.Models.AffiliateVideo", b =>
                {
                    b.Property<int>("AffiliateVideoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("AffiliateVideoId"));

                    b.Property<bool>("IsUrl")
                        .HasColumnType("bit");

                    b.Property<string>("VideoMedia")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("VideoMiniDescAr")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("VideoMiniDescEn")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("VideoTitleAr")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("VideoTitleEn")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("VideoUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("AffiliateVideoId");

                    b.ToTable("AffiliateVideos");
                });

            modelBuilder.Entity("iTech.Models.Brands", b =>
                {
                    b.Property<int>("BrandsId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("BrandsId"));

                    b.Property<string>("Images")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("BrandsId");

                    b.ToTable("Brands");
                });

            modelBuilder.Entity("iTech.Models.BrandsBrief", b =>
                {
                    b.Property<int>("BrandsBriefId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("BrandsBriefId"));

                    b.Property<string>("BriefAR")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("BriefEN")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("BrandsBriefId");

                    b.ToTable("BrandsBrief");

                    b.HasData(
                        new
                        {
                            BrandsBriefId = 1,
                            BriefAR = "يستخدم 45000 عميل في 100 دولة نموذج ITECH. قابل عملائنا.",
                            BriefEN = "45,000 CUSTOMERS IN 100 COUNTRIES USE ITECH TEMPLATE. MEET OUR CUSTOMERS."
                        });
                });

            modelBuilder.Entity("iTech.Models.ContactUs", b =>
                {
                    b.Property<int>("ContactUsID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ContactUsID"));

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Message")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("SendingDate")
                        .HasColumnType("datetime2");

                    b.HasKey("ContactUsID");

                    b.ToTable("contactUs");
                });

            modelBuilder.Entity("iTech.Models.Count", b =>
                {
                    b.Property<int>("CountId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CountId"));

                    b.Property<int>("CountInfoId")
                        .HasColumnType("int");

                    b.Property<string>("TitleAR")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TitleEN")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("count")
                        .HasColumnType("int");

                    b.HasKey("CountId");

                    b.HasIndex("CountInfoId");

                    b.ToTable("Count");

                    b.HasData(
                        new
                        {
                            CountId = 1,
                            CountInfoId = 1,
                            TitleAR = "مستشارون محترفون",
                            TitleEN = "Pro Consultants",
                            count = 300
                        },
                        new
                        {
                            CountId = 2,
                            CountInfoId = 1,
                            TitleAR = "عملاء راضون",
                            TitleEN = "Satisfied Clients",
                            count = 200
                        },
                        new
                        {
                            CountId = 3,
                            CountInfoId = 1,
                            TitleAR = "حالات ناجحة",
                            TitleEN = "Sucessfull Cases",
                            count = 100
                        },
                        new
                        {
                            CountId = 4,
                            CountInfoId = 1,
                            TitleAR = "سنوات في الأعمال",
                            TitleEN = "Years in Business",
                            count = 20
                        });
                });

            modelBuilder.Entity("iTech.Models.CountInfo", b =>
                {
                    b.Property<int>("CountInfoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CountInfoId"));

                    b.Property<string>("DescAR")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DescEN")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TitleAR")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TitleEN")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CountInfoId");

                    b.ToTable("CountInfos");

                    b.HasData(
                        new
                        {
                            CountInfoId = 1,
                            DescAR = "Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Phasellus blandit massa enim. Nullam id varius nunc.",
                            DescEN = "Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Phasellus blandit massa enim. Nullam id varius nunc.",
                            TitleAR = "  أرقام الشركة",
                            TitleEN = "Company in Numbers"
                        });
                });

            modelBuilder.Entity("iTech.Models.Country", b =>
                {
                    b.Property<int>("CountryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CountryId"));

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<int>("SortOrder")
                        .HasColumnType("int");

                    b.Property<string>("TitleAr")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TitleEn")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CountryId");

                    b.ToTable("Countries");
                });

            modelBuilder.Entity("iTech.Models.Newsletter", b =>
                {
                    b.Property<int>("NewsletterId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("NewsletterId"));

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("NewsletterId");

                    b.ToTable("Newsletters");
                });

            modelBuilder.Entity("iTech.Models.PageContent", b =>
                {
                    b.Property<int>("PageContentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PageContentId"));

                    b.Property<string>("ContentAr")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ContentEn")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PageTitleAr")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PageTitleEn")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("PageContentId");

                    b.ToTable("PageContents");

                    b.HasData(
                        new
                        {
                            PageContentId = 1,
                            ContentAr = "Privacy",
                            ContentEn = "Privacy",
                            PageTitleAr = "سياسة الخصوصية",
                            PageTitleEn = "Privacy"
                        },
                        new
                        {
                            PageContentId = 2,
                            ContentAr = "AboutUs",
                            ContentEn = "AboutUs",
                            PageTitleAr = "من نحن",
                            PageTitleEn = "AboutUs"
                        },
                        new
                        {
                            PageContentId = 3,
                            ContentAr = "Terms",
                            ContentEn = "Terms",
                            PageTitleAr = "الشروط والأحكام",
                            PageTitleEn = "Terms"
                        });
                });

            modelBuilder.Entity("iTech.Models.PaymentRequest", b =>
                {
                    b.Property<int>("PaymentRequestId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PaymentRequestId"));

                    b.Property<string>("AffiliateId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("ConfirmAmount")
                        .HasColumnType("float");

                    b.Property<double>("ConfirmPoints")
                        .HasColumnType("float");

                    b.Property<double>("CurrentBalance")
                        .HasColumnType("float");

                    b.Property<bool>("IsPaid")
                        .HasColumnType("bit");

                    b.Property<int>("PaymentTypeId")
                        .HasColumnType("int");

                    b.Property<DateTime>("RequestData")
                        .HasColumnType("datetime2");

                    b.Property<double>("RequestPoints")
                        .HasColumnType("float");

                    b.Property<int>("RequestStatusId")
                        .HasColumnType("int");

                    b.HasKey("PaymentRequestId");

                    b.HasIndex("PaymentTypeId");

                    b.HasIndex("RequestStatusId");

                    b.ToTable("PaymentRequests");
                });

            modelBuilder.Entity("iTech.Models.PaymentType", b =>
                {
                    b.Property<int>("PaymentTypeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PaymentTypeId"));

                    b.Property<string>("TitleAr")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TitleEn")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("PaymentTypeId");

                    b.ToTable("PaymentTypes");

                    b.HasData(
                        new
                        {
                            PaymentTypeId = 1,
                            TitleAr = "كاش",
                            TitleEn = "Cash"
                        },
                        new
                        {
                            PaymentTypeId = 2,
                            TitleAr = "اونلاين",
                            TitleEn = "Online"
                        });
                });

            modelBuilder.Entity("iTech.Models.Plan", b =>
                {
                    b.Property<int>("PlanId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PlanId"));

                    b.Property<int>("PagesCount")
                        .HasColumnType("int");

                    b.Property<double>("Price")
                        .HasColumnType("float");

                    b.Property<string>("TitleAr")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TitleEn")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("PlanId");

                    b.ToTable("Plans");

                    b.HasData(
                        new
                        {
                            PlanId = 1,
                            PagesCount = 3,
                            Price = 120.0,
                            TitleAr = "ألاساسة",
                            TitleEn = "BASIC"
                        },
                        new
                        {
                            PlanId = 2,
                            PagesCount = 5,
                            Price = 150.0,
                            TitleAr = "القياسية",
                            TitleEn = "Standard"
                        },
                        new
                        {
                            PlanId = 3,
                            PagesCount = 100,
                            Price = 190.0,
                            TitleAr = "غير محدودة",
                            TitleEn = "Enterprise"
                        });
                });

            modelBuilder.Entity("iTech.Models.PointConfiguration", b =>
                {
                    b.Property<int>("PointConfigurationId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PointConfigurationId"));

                    b.Property<double>("Value")
                        .HasColumnType("float");

                    b.HasKey("PointConfigurationId");

                    b.ToTable("PointConfigurations");
                });

            modelBuilder.Entity("iTech.Models.PublicHomeContent", b =>
                {
                    b.Property<int>("PublicHomeContentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PublicHomeContentId"));

                    b.Property<string>("AboutSectionDescAr")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("AboutSectionDescEn")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("AboutSectionTitleAr")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("AboutSectionTitleEn")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Pic")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("PublicHomeContentId");

                    b.ToTable("PublicHomeContents");
                });

            modelBuilder.Entity("iTech.Models.PublicSlider", b =>
                {
                    b.Property<int>("PublicSliderId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PublicSliderId"));

                    b.Property<string>("Background")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DescriptionAr")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DescriptionEn")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsImage")
                        .HasColumnType("bit");

                    b.Property<string>("Title1Ar")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title1En")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title2Ar")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title2En")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("PublicSliderId");

                    b.ToTable("PublicSliders");
                });

            modelBuilder.Entity("iTech.Models.PublicVideo", b =>
                {
                    b.Property<int>("PublicVideoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PublicVideoId"));

                    b.Property<string>("Background")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("BriefAr")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("BriefEn")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TitleAr")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TitleEn")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("VideoUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("PublicVideoId");

                    b.ToTable("PublicVideos");

                    b.HasData(
                        new
                        {
                            PublicVideoId = 1,
                            Background = "Public/img/generic-3.jpg",
                            BriefAr = "عن طريق الاي تيك في اقل من خمس خطوات",
                            BriefEn = "Through iTech in less than five steps",
                            TitleAr = "إنشاء موقعك في دقائق",
                            TitleEn = "Create your site in minutes",
                            VideoUrl = "https://vimeo.com/45830194"
                        });
                });

            modelBuilder.Entity("iTech.Models.RequestStatus", b =>
                {
                    b.Property<int>("RequestStatusId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("RequestStatusId"));

                    b.Property<string>("StatusTitleAr")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("StatusTitleEn")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("RequestStatusId");

                    b.ToTable("RequestStatuses");

                    b.HasData(
                        new
                        {
                            RequestStatusId = 1,
                            StatusTitleAr = "تم انشاءالطلب",
                            StatusTitleEn = "Initiated"
                        },
                        new
                        {
                            RequestStatusId = 2,
                            StatusTitleAr = "تم رفض الطلب",
                            StatusTitleEn = "Rejected"
                        },
                        new
                        {
                            RequestStatusId = 3,
                            StatusTitleAr = "عملية الدفع تمت بنجاح",
                            StatusTitleEn = "Paid"
                        });
                });

            modelBuilder.Entity("iTech.Models.Services", b =>
                {
                    b.Property<int>("ServicesId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ServicesId"));

                    b.Property<string>("ContentAr")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ContentEn")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DescAR")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DescEN")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DescTITLEAR")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DescTITLEEN")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Pic")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TitleAr")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TitleEn")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ServicesId");

                    b.ToTable("Services");
                });

            modelBuilder.Entity("iTech.Models.Site", b =>
                {
                    b.Property<int>("SiteId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("SiteId"));

                    b.Property<string>("AffiliateUser")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<bool>("IsPaid")
                        .HasColumnType("bit");

                    b.Property<string>("PhysicalPath")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SiteTitle")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("TemplateId")
                        .HasColumnType("int");

                    b.Property<string>("UserID")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("UserTemplatePrice")
                        .HasColumnType("float");

                    b.HasKey("SiteId");

                    b.HasIndex("TemplateId");

                    b.ToTable("Sites");
                });

            modelBuilder.Entity("iTech.Models.SiteCategory", b =>
                {
                    b.Property<int>("SiteCategoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("SiteCategoryId"));

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("Pic")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("SortOrder")
                        .HasColumnType("int");

                    b.Property<string>("TitleAr")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TitleEn")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("SiteCategoryId");

                    b.ToTable("SiteCategories");
                });

            modelBuilder.Entity("iTech.Models.SitePage", b =>
                {
                    b.Property<int>("SitePageId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("SitePageId"));

                    b.Property<string>("CssContent")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Document")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("HtmlContent")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MetaContent")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PageTitle")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("SiteId")
                        .HasColumnType("int");

                    b.HasKey("SitePageId");

                    b.HasIndex("SiteId");

                    b.ToTable("SitePages");
                });

            modelBuilder.Entity("iTech.Models.SocialMediaLink", b =>
                {
                    b.Property<int>("SocialMediaLinkId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("SocialMediaLinkId"));

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ContactMail")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ContactPhone1")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ContactPhone2")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Facebook")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Instgram")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LinkedIn")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Twitter")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("WhatSapp")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Youtube")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("SocialMediaLinkId");

                    b.ToTable("SocialMediaLinks");
                });

            modelBuilder.Entity("iTech.Models.Template", b =>
                {
                    b.Property<int>("TemplateId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TemplateId"));

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<int?>("SiteCategoryId")
                        .HasColumnType("int");

                    b.Property<int>("SortOrder")
                        .HasColumnType("int");

                    b.Property<string>("TemplateName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TemplatePath")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TemplatePic")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("TemplatePrice")
                        .HasColumnType("float");

                    b.Property<string>("TitleAr")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TitleEn")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("TemplateId");

                    b.HasIndex("SiteCategoryId");

                    b.ToTable("Templates");
                });

            modelBuilder.Entity("iTech.Models.TemplatePage", b =>
                {
                    b.Property<int>("TemplatePageId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TemplatePageId"));

                    b.Property<string>("CssContent")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Document")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("HtmlContent")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MetaContent")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PageTitle")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("TemplateId")
                        .HasColumnType("int");

                    b.HasKey("TemplatePageId");

                    b.HasIndex("TemplateId");

                    b.ToTable("TemplatePages");
                });

            modelBuilder.Entity("iTech.Models.Count", b =>
                {
                    b.HasOne("iTech.Models.CountInfo", "countInfo")
                        .WithMany("counts")
                        .HasForeignKey("CountInfoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("countInfo");
                });

            modelBuilder.Entity("iTech.Models.PaymentRequest", b =>
                {
                    b.HasOne("iTech.Models.PaymentType", "PaymentType")
                        .WithMany()
                        .HasForeignKey("PaymentTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("iTech.Models.RequestStatus", "RequestStatus")
                        .WithMany()
                        .HasForeignKey("RequestStatusId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("PaymentType");

                    b.Navigation("RequestStatus");
                });

            modelBuilder.Entity("iTech.Models.Site", b =>
                {
                    b.HasOne("iTech.Models.Template", "Templates")
                        .WithMany("Sites")
                        .HasForeignKey("TemplateId");

                    b.Navigation("Templates");
                });

            modelBuilder.Entity("iTech.Models.SitePage", b =>
                {
                    b.HasOne("iTech.Models.Site", "Sites")
                        .WithMany("SitePages")
                        .HasForeignKey("SiteId");

                    b.Navigation("Sites");
                });

            modelBuilder.Entity("iTech.Models.Template", b =>
                {
                    b.HasOne("iTech.Models.SiteCategory", "SiteCategories")
                        .WithMany("Templates")
                        .HasForeignKey("SiteCategoryId");

                    b.Navigation("SiteCategories");
                });

            modelBuilder.Entity("iTech.Models.TemplatePage", b =>
                {
                    b.HasOne("iTech.Models.Template", "Templates")
                        .WithMany("TemplatePages")
                        .HasForeignKey("TemplateId");

                    b.Navigation("Templates");
                });

            modelBuilder.Entity("iTech.Models.CountInfo", b =>
                {
                    b.Navigation("counts");
                });

            modelBuilder.Entity("iTech.Models.Site", b =>
                {
                    b.Navigation("SitePages");
                });

            modelBuilder.Entity("iTech.Models.SiteCategory", b =>
                {
                    b.Navigation("Templates");
                });

            modelBuilder.Entity("iTech.Models.Template", b =>
                {
                    b.Navigation("Sites");

                    b.Navigation("TemplatePages");
                });
#pragma warning restore 612, 618
        }
    }
}
