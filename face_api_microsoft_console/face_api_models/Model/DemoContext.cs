using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace face_api_commons.Model
{
    public partial class DemoContext : DbContext
    {
        public virtual DbSet<BusinessClient> BusinessClient { get; set; }
        public virtual DbSet<FacePhotoPath> FacePhotoPath { get; set; }
        public virtual DbSet<FaceRepository> FaceRepository { get; set; }
        public virtual DbSet<FaceRepositoryBusinessClient> FaceRepositoryBusinessClient { get; set; }
        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<UserBusinessClient> UserBusinessClient { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            #warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
            optionsBuilder.UseSqlite(@"data source=E:\temp\face_api_microsoft\face_api_wpf\demo.db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BusinessClient>(entity =>
            {
                entity.ToTable("business_client");

                entity.Property(e => e.ClientName)
                    .IsRequired()
                    .HasColumnName("client_name");
            });

            modelBuilder.Entity<FacePhotoPath>(entity =>
            {
                entity.ToTable("face_photo_path");

                entity.Property(e => e.FacePhotoPath1)
                    .IsRequired()
                    .HasColumnName("face_photo_path");
            });

            modelBuilder.Entity<FaceRepository>(entity =>
            {
                entity.ToTable("face_repository");

                entity.Property(e => e.Availiable)
                    .HasColumnName("availiable")
                    .HasColumnType("integer");

                entity.Property(e => e.FaceRepositoryId)
                    .IsRequired()
                    .HasColumnName("face_repository_id")
                    .HasColumnType("text");

                entity.Property(e => e.FaceRepositoryName)
                    .HasColumnName("face_repository_name")
                    .HasColumnType("text");

                entity.Property(e => e.FaceRepositoryComments)
                    .HasColumnName("face_repository_comments")
                    .HasColumnType("text");
            });

            modelBuilder.Entity<FaceRepositoryBusinessClient>(entity =>
            {
                entity.ToTable("face_repository_business_client");

                entity.Property(e => e.BusinessClientId).HasColumnName("business_client_id");

                entity.Property(e => e.FaceReposityId).HasColumnName("face_reposity_id");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("user");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasColumnName("password");

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasColumnName("user_name");
            });

            modelBuilder.Entity<UserBusinessClient>(entity =>
            {
                entity.ToTable("user_business_client");

                entity.Property(e => e.BusinessClientId).HasColumnName("business_client_id");

                entity.Property(e => e.UserId).HasColumnName("user_id");
            });
        }
    }
}