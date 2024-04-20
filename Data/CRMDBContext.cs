using iTech.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace iTech.Data
{
    public class CRMDBContext : DbContext
    {
        public CRMDBContext()
        {
        }

        public CRMDBContext(DbContextOptions<CRMDBContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PaymentType>().HasData(new PaymentType { PaymentTypeId = 1, TitleAr = "كاش", TitleEn = "Cash" });
            modelBuilder.Entity<PaymentType>().HasData(new PaymentType { PaymentTypeId = 2, TitleAr = "اونلاين", TitleEn = "Online" });
            modelBuilder.Entity<RequestStatus>().HasData(new RequestStatus { RequestStatusId = 1, StatusTitleAr = "تم انشاءالطلب", StatusTitleEn = "Initiated" });
            modelBuilder.Entity<RequestStatus>().HasData(new RequestStatus { RequestStatusId = 2, StatusTitleAr = "تم رفض الطلب", StatusTitleEn = "Rejected" });
            modelBuilder.Entity<RequestStatus>().HasData(new RequestStatus { RequestStatusId = 3, StatusTitleAr = "عملية الدفع تمت بنجاح", StatusTitleEn = "Paid" });
            modelBuilder.Entity<PageContent>().HasData(new PageContent { PageContentId = 1, PageTitleAr = "سياسة الخصوصية", PageTitleEn = "Privacy", ContentAr = "Privacy", ContentEn = "Privacy" });
            modelBuilder.Entity<PageContent>().HasData(new PageContent { PageContentId = 2, PageTitleAr = "من نحن", PageTitleEn = "AboutUs", ContentAr = "AboutUs", ContentEn = "AboutUs" });
            modelBuilder.Entity<PageContent>().HasData(new PageContent { PageContentId = 3, PageTitleAr = "الشروط والأحكام", PageTitleEn = "Terms", ContentAr = "Terms", ContentEn = "Terms" });
            modelBuilder.Entity<BrandsBrief>().HasData(new BrandsBrief { BrandsBriefId = 1, BriefAR = "يستخدم 45000 عميل في 100 دولة نموذج ITECH. قابل عملائنا.", BriefEN = "45,000 CUSTOMERS IN 100 COUNTRIES USE ITECH TEMPLATE. MEET OUR CUSTOMERS." });
            modelBuilder.Entity<PublicVideo>().HasData(new PublicVideo { PublicVideoId = 1, TitleAr = "إنشاء موقعك في دقائق", TitleEn = "Create your site in minutes",BriefAr="عن طريق الاي تيك في اقل من خمس خطوات",BriefEn= "Through iTech in less than five steps",Background= "Public/img/generic-3.jpg", VideoUrl= "https://vimeo.com/45830194" });

            modelBuilder.Entity<Count>().HasData(new Count { CountId = 1, count = 300, CountInfoId = 1, TitleAR = "مستشارون محترفون", TitleEN = "Pro Consultants" });
            modelBuilder.Entity<Count>().HasData(new Count { CountId = 2, count = 200, CountInfoId = 1, TitleAR = "عملاء راضون", TitleEN = "Satisfied Clients" });
            modelBuilder.Entity<Count>().HasData(new Count { CountId = 3, count = 100, CountInfoId = 1, TitleAR = "حالات ناجحة", TitleEN = "Sucessfull Cases" });
            modelBuilder.Entity<Count>().HasData(new Count { CountId = 4, count = 20, CountInfoId = 1, TitleAR = "سنوات في الأعمال", TitleEN = "Years in Business" });


            modelBuilder.Entity<CountInfo>().HasData(new CountInfo { CountInfoId = 1, TitleEN = "Company in Numbers", TitleAR = "  أرقام الشركة", DescEN = "Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Phasellus blandit massa enim. Nullam id varius nunc.",DescAR= "Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Phasellus blandit massa enim. Nullam id varius nunc." });
            modelBuilder.Entity<Plan>().HasData(new Plan { PlanId = 1, TitleAr = "ألاساسة", TitleEn = "BASIC", PagesCount = 3,Price=120});
            modelBuilder.Entity<Plan>().HasData(new Plan { PlanId = 2, TitleAr = "القياسية", TitleEn = "Standard", PagesCount = 5,Price=150});
            modelBuilder.Entity<Plan>().HasData(new Plan { PlanId = 3, TitleAr = "غير محدودة", TitleEn = "Enterprise", PagesCount = 100,Price=190});
            modelBuilder.Entity<AffilateFeedback>().HasData(new AffilateFeedback { AffilateFeedbackId = 1, TitleAr = "الاي تيك هي شركة النجاح", TitleEn = "Itech Is Successfull Company", UserOneFeedbackAr = "لقد ساعدتني الاي تيك كثرا في بناء حياتي ....اسلام احمد", UserOneFeedbackEn = "Itech Help Me To Build My Live ....Eslam Ahmed", UserTwoFeedbackAr = "لقد ساعدتني الاي تيك كثرا في بناء حياتي ....اسلام احمد", UserTwoFeedbackEn = "Itech Help Me To Build My Live ....Eslam Ahmed", UserThreeFeedbackAr = "لقد ساعدتني الاي تيك كثرا في بناء حياتي ....اسلام احمد", UserThreeFeedbackEn= "Itech Help Me To Build My Live ....Eslam Ahmed" });

            modelBuilder.Entity<CountDownSection>().HasData(new CountDownSection { CountDownSectionId = 1, TextAR = "قسم العد التنازلي", TextEN = "countdown section" });

        }
        public DbSet<Site> Sites { get; set; }
        public DbSet<ContactUs> contactUs { get; set; }
        public DbSet<WebEditorSubscription> WebEditorSubscriptions { get; set; }
        public DbSet<WebEditorPlan> WebEditorPlans { get; set; }
        public DbSet<PlanSpecs> PlansSpecs { get; set; }
        public DbSet<SiteModificationRequest> SiteModificationRequests { get; set; }
        public DbSet<Brands> Brands { get; set; }
        public DbSet<BrandsBrief> BrandsBrief { get; set; }
        public virtual DbSet<SiteCategory> SiteCategories { get; set; }
        public virtual DbSet<Template> Templates { get; set; }
        public virtual DbSet<Count> Count { get; set; }
        public virtual DbSet<CountDownSection> CountDownSections { get; set; }
        public virtual DbSet<Feature> Features { get; set; }
        public virtual DbSet<PaymentMethod> PaymentMethods { get; set; }
		
		public virtual DbSet<Services> Services { get; set; }
        public virtual DbSet<TemplatePage> TemplatePages { get; set; }
        public virtual DbSet<SitePage> SitePages { get; set; }
        public virtual DbSet<Country> Countries { get; set; }
        public virtual DbSet<CountInfo> CountInfos { get; set; }
        public virtual DbSet<AffiliatePrice> AffiliatePrices { get; set; }
        public virtual DbSet<PointConfiguration> PointConfigurations { get; set; }
        public virtual DbSet<PaymentRequest> PaymentRequests { get; set; }
        public virtual DbSet<PaymentType> PaymentTypes { get; set; }
        public virtual DbSet<RequestStatus> RequestStatuses { get; set; }
        public virtual DbSet<Newsletter> Newsletters { get; set; }
        public virtual DbSet<SocialMediaLink> SocialMediaLinks { get; set; }
        public virtual DbSet<PageContent> PageContents { get; set; }
        public virtual DbSet<PublicSlider> PublicSliders { get; set; }
        public virtual DbSet<PublicHomeContent> PublicHomeContents { get; set; }
        public virtual DbSet<AffiliateVideo> AffiliateVideos { get; set; }
        public virtual DbSet<PublicVideo> PublicVideos { get; set; }
        public virtual DbSet<Plan> Plans { get; set; }
        public virtual DbSet<AffilateFeedback> AffilateFeedbacks { get; set; }
        public virtual DbSet<SiteSubscription> SiteSubscriptions { get; set; }

       
        }
    }
