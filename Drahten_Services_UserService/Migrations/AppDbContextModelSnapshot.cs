﻿// <auto-generated />
using System;
using Drahten_Services_UserService.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Drahten_Services_UserService.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Drahten_Services_UserService.Models.Article", b =>
                {
                    b.Property<string>("ArticleId")
                        .HasColumnType("text");

                    b.Property<string>("Author")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Data")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Date")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Link")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("PrevTitle")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("TopicId")
                        .HasColumnType("integer");

                    b.HasKey("ArticleId");

                    b.HasIndex("TopicId")
                        .IsUnique();

                    b.ToTable("Article", (string)null);
                });

            modelBuilder.Entity("Drahten_Services_UserService.Models.ArticleComment", b =>
                {
                    b.Property<int>("ArticleCommentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("ArticleCommentId"));

                    b.Property<string>("ArticleId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Comment")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("DateTime")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int?>("ParentArticleCommentId")
                        .HasColumnType("integer");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("ArticleCommentId");

                    b.HasIndex("ArticleId");

                    b.HasIndex("ParentArticleCommentId");

                    b.HasIndex("UserId");

                    b.ToTable("ArticleComment", (string)null);
                });

            modelBuilder.Entity("Drahten_Services_UserService.Models.ArticleCommentThumbsDown", b =>
                {
                    b.Property<int>("ArticleCommentId")
                        .HasColumnType("integer");

                    b.Property<string>("UserId")
                        .HasColumnType("text");

                    b.HasKey("ArticleCommentId", "UserId");

                    b.HasIndex("UserId");

                    b.ToTable("ArticleCommentThumbsDown", (string)null);
                });

            modelBuilder.Entity("Drahten_Services_UserService.Models.ArticleCommentThumbsUp", b =>
                {
                    b.Property<int>("ArticleCommentId")
                        .HasColumnType("integer");

                    b.Property<string>("UserId")
                        .HasColumnType("text");

                    b.HasKey("ArticleCommentId", "UserId");

                    b.HasIndex("UserId");

                    b.ToTable("ArticleCommentThumbsUp", (string)null);
                });

            modelBuilder.Entity("Drahten_Services_UserService.Models.ArticleLike", b =>
                {
                    b.Property<int>("ArticleLikeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("ArticleLikeId"));

                    b.Property<string>("ArticleId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("DateTime")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("ArticleLikeId");

                    b.HasIndex("ArticleId");

                    b.HasIndex("UserId");

                    b.ToTable("ArticleLike", (string)null);
                });

            modelBuilder.Entity("Drahten_Services_UserService.Models.ContactRequest", b =>
                {
                    b.Property<int>("ContactRequestId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("ContactRequestId"));

                    b.Property<DateTime>("DateTime")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Message")
                        .HasColumnType("text");

                    b.Property<string>("ReceiverId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("SenderId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("UserId")
                        .HasColumnType("text");

                    b.HasKey("ContactRequestId");

                    b.HasIndex("ReceiverId");

                    b.HasIndex("SenderId");

                    b.HasIndex("UserId");

                    b.ToTable("ContactRequest", (string)null);
                });

            modelBuilder.Entity("Drahten_Services_UserService.Models.PrivateHistory", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("text");

                    b.Property<DateTime>("HistoryLiveTime")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("UserId");

                    b.ToTable("PrivateHistory", (string)null);
                });

            modelBuilder.Entity("Drahten_Services_UserService.Models.PublicHistory", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("text");

                    b.HasKey("UserId");

                    b.ToTable("PublicHistory", (string)null);
                });

            modelBuilder.Entity("Drahten_Services_UserService.Models.SearchedTopicDataPrivateHist", b =>
                {
                    b.Property<int>("SearchedTopicDataPrivateHistId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("SearchedTopicDataPrivateHistId"));

                    b.Property<string>("PrivateHistoryId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("SearchTime")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("SearchedData")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("TopicId")
                        .HasColumnType("integer");

                    b.HasKey("SearchedTopicDataPrivateHistId");

                    b.HasIndex("PrivateHistoryId");

                    b.HasIndex("TopicId");

                    b.ToTable("SearchedTopicDataPrivateHist", (string)null);
                });

            modelBuilder.Entity("Drahten_Services_UserService.Models.SearchedTopicDataPublicHist", b =>
                {
                    b.Property<int>("SearchedTopicDataPublicHistId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("SearchedTopicDataPublicHistId"));

                    b.Property<string>("PublicHistoryId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("SearchTime")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("SearchedData")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("TopicId")
                        .HasColumnType("integer");

                    b.HasKey("SearchedTopicDataPublicHistId");

                    b.HasIndex("PublicHistoryId");

                    b.HasIndex("TopicId");

                    b.ToTable("SearchedTopicDataPublicHist", (string)null);
                });

            modelBuilder.Entity("Drahten_Services_UserService.Models.Topic", b =>
                {
                    b.Property<int>("TopicId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("TopicId"));

                    b.Property<int?>("ParentTopicId")
                        .HasColumnType("integer");

                    b.Property<string>("TopicName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("TopicId");

                    b.HasIndex("ParentTopicId");

                    b.ToTable("Topic", (string)null);

                    b.HasData(
                        new
                        {
                            TopicId = 1,
                            TopicName = "Cybersecurity"
                        },
                        new
                        {
                            TopicId = 2,
                            TopicName = "Programming"
                        },
                        new
                        {
                            TopicId = 3,
                            ParentTopicId = 1,
                            TopicName = "News"
                        },
                        new
                        {
                            TopicId = 4,
                            ParentTopicId = 1,
                            TopicName = "Projects"
                        },
                        new
                        {
                            TopicId = 5,
                            ParentTopicId = 1,
                            TopicName = "Laws"
                        },
                        new
                        {
                            TopicId = 6,
                            ParentTopicId = 1,
                            TopicName = "Law regulations"
                        },
                        new
                        {
                            TopicId = 7,
                            ParentTopicId = 2,
                            TopicName = "News"
                        },
                        new
                        {
                            TopicId = 8,
                            ParentTopicId = 2,
                            TopicName = "Projects"
                        },
                        new
                        {
                            TopicId = 9,
                            ParentTopicId = 3,
                            TopicName = "America"
                        },
                        new
                        {
                            TopicId = 10,
                            ParentTopicId = 3,
                            TopicName = "Asia"
                        },
                        new
                        {
                            TopicId = 11,
                            ParentTopicId = 3,
                            TopicName = "Europe"
                        });
                });

            modelBuilder.Entity("Drahten_Services_UserService.Models.User", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("text");

                    b.Property<string>("EmailAddress")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<string>("NickName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.HasKey("UserId");

                    b.ToTable("User", (string)null);
                });

            modelBuilder.Entity("Drahten_Services_UserService.Models.UserArticle", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("text");

                    b.Property<string>("ArticleId")
                        .HasColumnType("text");

                    b.HasKey("UserId", "ArticleId");

                    b.HasIndex("ArticleId");

                    b.ToTable("UserArticle", (string)null);
                });

            modelBuilder.Entity("Drahten_Services_UserService.Models.UserTopic", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("text");

                    b.Property<int>("TopicId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("SubscriptionTime")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("UserId", "TopicId");

                    b.HasIndex("TopicId");

                    b.ToTable("UserTopic", (string)null);
                });

            modelBuilder.Entity("Drahten_Services_UserService.Models.UserTracking", b =>
                {
                    b.Property<int>("UserTrackingId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("UserTrackingId"));

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("UserTrackingId");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("UserTracking", (string)null);
                });

            modelBuilder.Entity("Drahten_Services_UserService.Models.ViewedArticlePrivateHist", b =>
                {
                    b.Property<string>("ArticleId")
                        .HasColumnType("text");

                    b.Property<string>("PrivateHistoryId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("ViewTime")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("ArticleId");

                    b.HasIndex("PrivateHistoryId");

                    b.ToTable("ViewedArticlePrivateHist", (string)null);
                });

            modelBuilder.Entity("Drahten_Services_UserService.Models.ViewedArticlePublicHist", b =>
                {
                    b.Property<string>("ArticleId")
                        .HasColumnType("text");

                    b.Property<string>("PublicHistoryId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("ViewTime")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("ArticleId");

                    b.HasIndex("PublicHistoryId");

                    b.ToTable("ViewedArticlePublicHist", (string)null);
                });

            modelBuilder.Entity("Drahten_Services_UserService.Models.ViewedUserPrivateHist", b =>
                {
                    b.Property<string>("ViewedUserId")
                        .HasColumnType("text");

                    b.Property<DateTime>("DateTime")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("PrivateHistoryId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("ViewedUserId");

                    b.HasIndex("PrivateHistoryId");

                    b.ToTable("ViewedUserPrivateHist", (string)null);
                });

            modelBuilder.Entity("Drahten_Services_UserService.Models.ViewedUserPublicHist", b =>
                {
                    b.Property<string>("ViewedUserId")
                        .HasColumnType("text");

                    b.Property<DateTime>("DateTime")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("PublicHistoryId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("ViewedUserId");

                    b.HasIndex("PublicHistoryId");

                    b.ToTable("ViewedUserPublicHist", (string)null);
                });

            modelBuilder.Entity("Drahten_Services_UserService.Models.Article", b =>
                {
                    b.HasOne("Drahten_Services_UserService.Models.Topic", "Topic")
                        .WithOne()
                        .HasForeignKey("Drahten_Services_UserService.Models.Article", "TopicId")
                        .OnDelete(DeleteBehavior.SetNull)
                        .IsRequired()
                        .HasConstraintName("FK_Topic_Article");

                    b.Navigation("Topic");
                });

            modelBuilder.Entity("Drahten_Services_UserService.Models.ArticleComment", b =>
                {
                    b.HasOne("Drahten_Services_UserService.Models.Article", "Article")
                        .WithMany("ArticleComments")
                        .HasForeignKey("ArticleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_Article_ArticleComment");

                    b.HasOne("Drahten_Services_UserService.Models.ArticleComment", "Parent")
                        .WithMany("Children")
                        .HasForeignKey("ParentArticleCommentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .HasConstraintName("FK_ParentArticleComment_ChildArticleComments");

                    b.HasOne("Drahten_Services_UserService.Models.User", "User")
                        .WithMany("ArticleComments")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_User_ArticleComment");

                    b.Navigation("Article");

                    b.Navigation("Parent");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Drahten_Services_UserService.Models.ArticleCommentThumbsDown", b =>
                {
                    b.HasOne("Drahten_Services_UserService.Models.ArticleComment", "ArticleComment")
                        .WithMany("ArticleCommentThumbsDown")
                        .HasForeignKey("ArticleCommentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_ArticleComment_ArticleCommentThumbsDown");

                    b.HasOne("Drahten_Services_UserService.Models.User", "User")
                        .WithMany("ArticleCommentThumbsDown")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.SetNull)
                        .IsRequired()
                        .HasConstraintName("FK_User_ArticleCommentThumbsDown");

                    b.Navigation("ArticleComment");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Drahten_Services_UserService.Models.ArticleCommentThumbsUp", b =>
                {
                    b.HasOne("Drahten_Services_UserService.Models.ArticleComment", "ArticleComment")
                        .WithMany("ArticleCommentThumbsUp")
                        .HasForeignKey("ArticleCommentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_ArticleComment_ArticleCommentThumbsUp");

                    b.HasOne("Drahten_Services_UserService.Models.User", "User")
                        .WithMany("ArticleCommentThumbsUp")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.SetNull)
                        .IsRequired()
                        .HasConstraintName("FK_User_ArticleCommentThumbsUp");

                    b.Navigation("ArticleComment");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Drahten_Services_UserService.Models.ArticleLike", b =>
                {
                    b.HasOne("Drahten_Services_UserService.Models.Article", "Article")
                        .WithMany("ArticleLikes")
                        .HasForeignKey("ArticleId")
                        .OnDelete(DeleteBehavior.SetNull)
                        .IsRequired()
                        .HasConstraintName("FK_Article_ArticleLike");

                    b.HasOne("Drahten_Services_UserService.Models.User", "User")
                        .WithMany("ArticleLikes")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_User_ArticleLike");

                    b.Navigation("Article");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Drahten_Services_UserService.Models.ContactRequest", b =>
                {
                    b.HasOne("Drahten_Services_UserService.Models.User", "Receiver")
                        .WithMany()
                        .HasForeignKey("ReceiverId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_Receiver_ContactRequest");

                    b.HasOne("Drahten_Services_UserService.Models.User", "Sender")
                        .WithMany()
                        .HasForeignKey("SenderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_Sender_ContactRequest");

                    b.HasOne("Drahten_Services_UserService.Models.User", null)
                        .WithMany("ContactRequests")
                        .HasForeignKey("UserId");

                    b.Navigation("Receiver");

                    b.Navigation("Sender");
                });

            modelBuilder.Entity("Drahten_Services_UserService.Models.PrivateHistory", b =>
                {
                    b.HasOne("Drahten_Services_UserService.Models.User", "User")
                        .WithOne()
                        .HasForeignKey("Drahten_Services_UserService.Models.PrivateHistory", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_User_PrivateHistory");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Drahten_Services_UserService.Models.PublicHistory", b =>
                {
                    b.HasOne("Drahten_Services_UserService.Models.User", "User")
                        .WithOne()
                        .HasForeignKey("Drahten_Services_UserService.Models.PublicHistory", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_User_PublicHistory");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Drahten_Services_UserService.Models.SearchedTopicDataPrivateHist", b =>
                {
                    b.HasOne("Drahten_Services_UserService.Models.PrivateHistory", "PrivateHistory")
                        .WithMany("SearchedTopicData")
                        .HasForeignKey("PrivateHistoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_PrivateHistory_SearchedTopicDataPrivateHist");

                    b.HasOne("Drahten_Services_UserService.Models.Topic", "Topic")
                        .WithMany("SearchedTopicDataPrivateHist")
                        .HasForeignKey("TopicId")
                        .OnDelete(DeleteBehavior.SetNull)
                        .IsRequired()
                        .HasConstraintName("FK_Topic_SearchedTopicDataPrivateHist");

                    b.Navigation("PrivateHistory");

                    b.Navigation("Topic");
                });

            modelBuilder.Entity("Drahten_Services_UserService.Models.SearchedTopicDataPublicHist", b =>
                {
                    b.HasOne("Drahten_Services_UserService.Models.PublicHistory", "PublicHistory")
                        .WithMany("SearchedTopicData")
                        .HasForeignKey("PublicHistoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_PublicHistory_SearchedTopicDataPublicHist");

                    b.HasOne("Drahten_Services_UserService.Models.Topic", "Topic")
                        .WithMany("SearchedTopicDataPublicHist")
                        .HasForeignKey("TopicId")
                        .OnDelete(DeleteBehavior.SetNull)
                        .IsRequired()
                        .HasConstraintName("FK_Topic_SearchedTopicDataPublicHist");

                    b.Navigation("PublicHistory");

                    b.Navigation("Topic");
                });

            modelBuilder.Entity("Drahten_Services_UserService.Models.Topic", b =>
                {
                    b.HasOne("Drahten_Services_UserService.Models.Topic", "Parent")
                        .WithMany("Children")
                        .HasForeignKey("ParentTopicId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .HasConstraintName("FK_ParentTopic_ChildTopics");

                    b.Navigation("Parent");
                });

            modelBuilder.Entity("Drahten_Services_UserService.Models.UserArticle", b =>
                {
                    b.HasOne("Drahten_Services_UserService.Models.Article", "Article")
                        .WithMany("UserArticles")
                        .HasForeignKey("ArticleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_Article_UserArticle");

                    b.HasOne("Drahten_Services_UserService.Models.User", "User")
                        .WithMany("UserArticles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_User_UserArticle");

                    b.Navigation("Article");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Drahten_Services_UserService.Models.UserTopic", b =>
                {
                    b.HasOne("Drahten_Services_UserService.Models.Topic", "Topic")
                        .WithMany("Users")
                        .HasForeignKey("TopicId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_Topic_Users");

                    b.HasOne("Drahten_Services_UserService.Models.User", "User")
                        .WithMany("Topics")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_User_Topics");

                    b.Navigation("Topic");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Drahten_Services_UserService.Models.UserTracking", b =>
                {
                    b.HasOne("Drahten_Services_UserService.Models.User", "User")
                        .WithOne()
                        .HasForeignKey("Drahten_Services_UserService.Models.UserTracking", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_User_UserTracking");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Drahten_Services_UserService.Models.ViewedArticlePrivateHist", b =>
                {
                    b.HasOne("Drahten_Services_UserService.Models.Article", "Article")
                        .WithOne()
                        .HasForeignKey("Drahten_Services_UserService.Models.ViewedArticlePrivateHist", "ArticleId")
                        .OnDelete(DeleteBehavior.SetNull)
                        .IsRequired()
                        .HasConstraintName("FK_Article_ViewedArticlePrivateHist");

                    b.HasOne("Drahten_Services_UserService.Models.PrivateHistory", "PrivateHistory")
                        .WithMany("ViewedArticles")
                        .HasForeignKey("PrivateHistoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_PrivateHistory_ViewedArticlePrivateHist");

                    b.Navigation("Article");

                    b.Navigation("PrivateHistory");
                });

            modelBuilder.Entity("Drahten_Services_UserService.Models.ViewedArticlePublicHist", b =>
                {
                    b.HasOne("Drahten_Services_UserService.Models.Article", "Article")
                        .WithOne()
                        .HasForeignKey("Drahten_Services_UserService.Models.ViewedArticlePublicHist", "ArticleId")
                        .OnDelete(DeleteBehavior.SetNull)
                        .IsRequired()
                        .HasConstraintName("FK_Article_ViewedArticlePublicHist");

                    b.HasOne("Drahten_Services_UserService.Models.PublicHistory", "PublicHistory")
                        .WithMany("ViewedArticles")
                        .HasForeignKey("PublicHistoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_PublicHistory_ViewedArticlePublicHist");

                    b.Navigation("Article");

                    b.Navigation("PublicHistory");
                });

            modelBuilder.Entity("Drahten_Services_UserService.Models.ViewedUserPrivateHist", b =>
                {
                    b.HasOne("Drahten_Services_UserService.Models.PrivateHistory", "PrivateHistory")
                        .WithMany("ViewedUsers")
                        .HasForeignKey("PrivateHistoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_PrivateHistory_ViewedUserPrivateHist");

                    b.HasOne("Drahten_Services_UserService.Models.User", "User")
                        .WithOne()
                        .HasForeignKey("Drahten_Services_UserService.Models.ViewedUserPrivateHist", "ViewedUserId")
                        .OnDelete(DeleteBehavior.SetNull)
                        .IsRequired()
                        .HasConstraintName("FK_User_ViewedUserPrivateHist");

                    b.Navigation("PrivateHistory");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Drahten_Services_UserService.Models.ViewedUserPublicHist", b =>
                {
                    b.HasOne("Drahten_Services_UserService.Models.PublicHistory", "PublicHistory")
                        .WithMany("ViewedUsers")
                        .HasForeignKey("PublicHistoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_PublicHistory_ViewedUserPublicHist");

                    b.HasOne("Drahten_Services_UserService.Models.User", "User")
                        .WithOne()
                        .HasForeignKey("Drahten_Services_UserService.Models.ViewedUserPublicHist", "ViewedUserId")
                        .OnDelete(DeleteBehavior.SetNull)
                        .IsRequired()
                        .HasConstraintName("FK_User_ViewedUserPublicHist");

                    b.Navigation("PublicHistory");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Drahten_Services_UserService.Models.Article", b =>
                {
                    b.Navigation("ArticleComments");

                    b.Navigation("ArticleLikes");

                    b.Navigation("UserArticles");
                });

            modelBuilder.Entity("Drahten_Services_UserService.Models.ArticleComment", b =>
                {
                    b.Navigation("ArticleCommentThumbsDown");

                    b.Navigation("ArticleCommentThumbsUp");

                    b.Navigation("Children");
                });

            modelBuilder.Entity("Drahten_Services_UserService.Models.PrivateHistory", b =>
                {
                    b.Navigation("SearchedTopicData");

                    b.Navigation("ViewedArticles");

                    b.Navigation("ViewedUsers");
                });

            modelBuilder.Entity("Drahten_Services_UserService.Models.PublicHistory", b =>
                {
                    b.Navigation("SearchedTopicData");

                    b.Navigation("ViewedArticles");

                    b.Navigation("ViewedUsers");
                });

            modelBuilder.Entity("Drahten_Services_UserService.Models.Topic", b =>
                {
                    b.Navigation("Children");

                    b.Navigation("SearchedTopicDataPrivateHist");

                    b.Navigation("SearchedTopicDataPublicHist");

                    b.Navigation("Users");
                });

            modelBuilder.Entity("Drahten_Services_UserService.Models.User", b =>
                {
                    b.Navigation("ArticleCommentThumbsDown");

                    b.Navigation("ArticleCommentThumbsUp");

                    b.Navigation("ArticleComments");

                    b.Navigation("ArticleLikes");

                    b.Navigation("ContactRequests");

                    b.Navigation("Topics");

                    b.Navigation("UserArticles");
                });
#pragma warning restore 612, 618
        }
    }
}
