﻿using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace BlogCoreAPI.Models
{
    public partial class UdemyAngularBlogDBContext : DbContext
    {
        public UdemyAngularBlogDBContext()
        {
        }

        public UdemyAngularBlogDBContext(DbContextOptions<UdemyAngularBlogDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Article> Articles { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Comment> Comments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Article>(entity =>
            {
                entity.ToTable("Article");

                entity.Property(e => e.CategoryId).HasColumnName("category_id");

                entity.Property(e => e.ContentMain)
                    .IsRequired()
                    .HasColumnName("content_main");

                entity.Property(e => e.ContentSummary)
                    .IsRequired()
                    .HasMaxLength(500)
                    .HasColumnName("content_summary");

                entity.Property(e => e.Picture)
                    .HasMaxLength(300)
                    .HasColumnName("picture");

                entity.Property(e => e.PublishDate)
                    .HasColumnType("datetime")
                    .HasColumnName("publish_date");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(500)
                    .HasColumnName("title");

                entity.Property(e => e.ViewCount).HasColumnName("viewCount");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Articles)
                    .HasForeignKey(d => d.CategoryId)
                    .HasConstraintName("FK_Article_Category_Cascade");
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.ToTable("Category");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<Comment>(entity =>
            {
                entity.ToTable("Comment");

                entity.Property(e => e.ArticleId).HasColumnName("article_id");

                entity.Property(e => e.ContentMain)
                    .IsRequired()
                    .HasColumnName("content_main");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("name");

                entity.Property(e => e.PublishDate)
                    .HasColumnType("datetime")
                    .HasColumnName("publish_date");

                entity.HasOne(d => d.Article)
                    .WithMany(p => p.Comments)
                    .HasForeignKey(d => d.ArticleId)
                    .HasConstraintName("FK_Comment_Article_Cascade");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
