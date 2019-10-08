using Blog.Services.Models;
using Microsoft.EntityFrameworkCore;

namespace Blog.Services.Context
{
    public partial class BlogDbContext : DbContext
    {
        public BlogDbContext()
        {
        }

        public BlogDbContext(DbContextOptions<BlogDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<About> About { get; set; }
        public virtual DbSet<Article> Article { get; set; }
        public virtual DbSet<ArticleDetail> ArticleDetail { get; set; }
        public virtual DbSet<Author> Author { get; set; }
        public virtual DbSet<BlogVizitor> BlogVizitor { get; set; }
        public virtual DbSet<Category> Category { get; set; }
        public virtual DbSet<Comment> Comment { get; set; }
        public virtual DbSet<Download> Download { get; set; }
        public virtual DbSet<FeedBack> FeedBack { get; set; }
        public virtual DbSet<OuterDownladLink> OuterDownladLink { get; set; }
        public virtual DbSet<SubCategory> SubCategory { get; set; }
        public virtual DbSet<Subscriber> Subscriber { get; set; }
        public virtual DbSet<Video> Video { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.6-servicing-10079");

            modelBuilder.Entity<About>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");
            });

            modelBuilder.Entity<Article>(entity =>
            {
                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.Property(e => e.SubCategoryId).HasColumnName("SubCategoryID");

                entity.HasOne(d => d.SubCategory)
                    .WithMany(p => p.Article)
                    .HasForeignKey(d => d.SubCategoryId)
                    .HasConstraintName("FK_Book");
            });

            modelBuilder.Entity<ArticleDetail>(entity =>
            {
                entity.HasIndex(e => e.ArticleId)
                    .HasName("KEY_Article_bookID")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.ArticleId).HasColumnName("ArticleID");

                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.HasOne(d => d.Article)
                    .WithOne(p => p.ArticleDetail)
                    .HasForeignKey<ArticleDetail>(d => d.ArticleId)
                    .HasConstraintName("FK_Article_Book_Id");
            });

            modelBuilder.Entity<Author>(entity =>
            {
                entity.Property(e => e.ArticleId).HasColumnName("ArticleID");

                entity.HasOne(d => d.Article)
                    .WithMany(p => p.Author)
                    .HasForeignKey(d => d.ArticleId)
                    .HasConstraintName("FK_Author_Article_Id");
            });

            modelBuilder.Entity<BlogVizitor>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.Property(e => e.Ip).HasColumnName("IP");
            });

            modelBuilder.Entity<Comment>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.ArticleDeatilId).HasColumnName("ArticleDeatilID");

                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.Property(e => e.UserId)
                    .IsRequired()
                    .HasColumnName("UserID");

                entity.HasOne(d => d.ArticleDeatil)
                    .WithMany(p => p.Comment)
                    .HasForeignKey(d => d.ArticleDeatilId)
                    .HasConstraintName("FK_Comment_ArticleDetail_id");
            });

            modelBuilder.Entity<Download>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.ArticleDetailId).HasColumnName("ArticleDetailID");

                entity.HasOne(d => d.ArticleDetail)
                    .WithMany(p => p.Download)
                    .HasForeignKey(d => d.ArticleDetailId)
                    .HasConstraintName("FK_Download_ArticleDetail_id");
            });

            modelBuilder.Entity<FeedBack>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");
            });

            modelBuilder.Entity<OuterDownladLink>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.ArticleDetailId).HasColumnName("ArticleDetailID");

                entity.HasOne(d => d.ArticleDetail)
                    .WithMany(p => p.OuterDownladLink)
                    .HasForeignKey(d => d.ArticleDetailId)
                    .HasConstraintName("FK_OuterDownladLink_ArticleDetail_id");
            });

            modelBuilder.Entity<SubCategory>(entity =>
            {
                entity.Property(e => e.CategoryId).HasColumnName("CategoryID");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.SubCategory)
                    .HasForeignKey(d => d.CategoryId)
                    .HasConstraintName("FK_SubCategory_Category_Id");
            });

            modelBuilder.Entity<Subscriber>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Date).HasColumnType("datetime");
            });

            modelBuilder.Entity<Video>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.ArticleDetailId).HasColumnName("ArticleDetailID");

                entity.HasOne(d => d.ArticleDetail)
                    .WithMany(p => p.Video)
                    .HasForeignKey(d => d.ArticleDetailId)
                    .HasConstraintName("FK_Video_ArticleDetail_id");
            });
        }
    }
}
