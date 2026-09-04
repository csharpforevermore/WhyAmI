using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using WhyAmI.Web.Models;

namespace WhyAmI.Web.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Customer> Customers => Set<Customer>();
    public DbSet<Vendor> Vendors => Set<Vendor>();
    public DbSet<ContactDetails> ContactDetails => Set<ContactDetails>();
    public DbSet<Questionnaire> Questionnaires => Set<Questionnaire>();
    public DbSet<Question> Questions => Set<Question>();
    public DbSet<Answer> Answers => Set<Answer>();
    public DbSet<CustomerQuestionnaireResults> CustomerQuestionnaireResults => Set<CustomerQuestionnaireResults>();
    public DbSet<Survey> Surveys => Set<Survey>();
    public DbSet<SurveyQuestion> SurveyQuestions => Set<SurveyQuestion>();
    public DbSet<SurveyResponse> SurveyResponses => Set<SurveyResponse>();
    public DbSet<SurveyResponseAnswer> SurveyResponseAnswers => Set<SurveyResponseAnswer>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        ConfigureBaseProperties<Customer>(modelBuilder);
        ConfigureBaseProperties<Vendor>(modelBuilder);
        ConfigureBaseProperties<Questionnaire>(modelBuilder);
        ConfigureBaseProperties<Question>(modelBuilder);
        ConfigureBaseProperties<Answer>(modelBuilder);
        ConfigureBaseProperties<CustomerQuestionnaireResults>(modelBuilder);
        ConfigureBaseProperties<Survey>(modelBuilder);
        ConfigureBaseProperties<SurveyQuestion>(modelBuilder);
        ConfigureBaseProperties<SurveyResponse>(modelBuilder);
        ConfigureBaseProperties<SurveyResponseAnswer>(modelBuilder);

        modelBuilder.Entity<ContactDetails>(entity =>
        {
            entity.HasKey(c => c.ID);
            entity.Property(c => c.Title).IsRequired().HasMaxLength(10);
            entity.Property(c => c.FirstName).IsRequired().HasMaxLength(200);
            entity.Property(c => c.LastName).IsRequired().HasMaxLength(200);
            entity.Property(c => c.Email).IsRequired().HasMaxLength(200);
            entity.Property(c => c.PhoneNumber).IsRequired().HasMaxLength(200);
            entity.Property(c => c.AlternatePhoneNumber).HasMaxLength(200);
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasOne(c => c.ContactDetails)
                .WithMany()
                .HasForeignKey("ContactDetailsId")
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<Vendor>(entity =>
        {
            entity.Property(v => v.Name).IsRequired().HasMaxLength(200);
            entity.Property(v => v.About).IsRequired().HasMaxLength(200);
            entity.Property(v => v.Website).HasMaxLength(200);

            entity.HasOne(v => v.ContactDetails)
                .WithMany()
                .HasForeignKey("ContactDetailsId")
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<Questionnaire>(entity =>
        {
            entity.Property(q => q.Title).IsRequired().HasMaxLength(200);
            entity.Property(q => q.SubTitle).HasMaxLength(500);
            entity.Property(q => q.Source).HasMaxLength(200).HasAnnotation("Source URL", "URL or source of the questionnaire.");
            entity.Property(q => q.Description).HasMaxLength(500);
            entity.Property(q => q.HasQuestionsWithTrueFalse).HasDefaultValue(false);
            entity.Property(q => q.IsEnabled).HasDefaultValue(true);

            var scoreOptionsComparer = new ValueComparer<IList<ScoreOption>>(
                (left, right) => ScoreOptionsConverter.Serialize(left!) == ScoreOptionsConverter.Serialize(right!),
                value => ScoreOptionsConverter.Serialize(value).GetHashCode(),
                value => ScoreOptionsConverter.Deserialize(ScoreOptionsConverter.Serialize(value)));

            entity.Property(q => q.ScoreOptions)
                .HasConversion(
                    value => ScoreOptionsConverter.Serialize(value),
                    value => ScoreOptionsConverter.Deserialize(value))
                .HasColumnType("TEXT")
                .Metadata.SetValueComparer(scoreOptionsComparer);

            entity.HasMany(q => q.Questions)
                .WithOne()
                .HasForeignKey("QuestionnaireId")
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<Question>(entity =>
        {
            entity.Property(q => q.Id).IsRequired();
            entity.Property(q => q.Text).IsRequired().HasMaxLength(200);
            entity.Property(q => q.HasTrueFalse);
        });

        modelBuilder.Entity<CustomerQuestionnaireResults>(entity =>
        {
            entity.Property(r => r.TotalScore);

            entity.HasOne(r => r.Customer)
                .WithMany()
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasMany(r => r.Questions)
                .WithMany();

            entity.HasMany(r => r.Answers)
                .WithMany();

            entity.HasMany(r => r.Vendors)
                .WithMany();
        });

        var surveyScoreOptionsComparer = new ValueComparer<IList<SurveyScoreOption>>(
            (left, right) => SurveyScoreOptionsConverter.Serialize(left!) == SurveyScoreOptionsConverter.Serialize(right!),
            value => SurveyScoreOptionsConverter.Serialize(value).GetHashCode(),
            value => SurveyScoreOptionsConverter.Deserialize(SurveyScoreOptionsConverter.Serialize(value)));

        modelBuilder.Entity<Survey>(entity =>
        {
            entity.Property(s => s.Title).IsRequired().HasMaxLength(200);
            entity.Property(s => s.SubTitle).HasMaxLength(500);
            entity.Property(s => s.Source).HasMaxLength(200);
            entity.Property(s => s.Description).HasMaxLength(500);
            entity.Property(s => s.IsEnabled).HasDefaultValue(true);

            entity.OwnsMany(s => s.ScoreOptions, options =>
            {
                options.ToTable("SurveyScoreOptions", table =>
                    table.HasCheckConstraint("CK_SurveyScoreOptions_Score", "\"Score\" >= 1 AND \"Score\" <= 5"));
                options.Property<int>("Id");
                options.HasKey("SurveyId", "Id");
                options.WithOwner().HasForeignKey("SurveyId");
                options.Property(o => o.Score).IsRequired();
                options.Property(o => o.Text).IsRequired().HasMaxLength(200);
                options.HasIndex("SurveyId", nameof(SurveyScoreOption.Score)).IsUnique();
            });

            entity.HasMany(s => s.Questions)
                .WithOne(q => q.Survey)
                .HasForeignKey(q => q.SurveyId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<SurveyQuestion>(entity =>
        {
            entity.Property(q => q.Text).IsRequired().HasMaxLength(200);
            entity.HasIndex(q => new { q.SurveyId, q.Number }).IsUnique();
        });

        modelBuilder.Entity<SurveyResponse>(entity =>
        {
            entity.Property(r => r.SurveyTitle).IsRequired().HasMaxLength(200);
            entity.Property(r => r.SurveyDescription).HasMaxLength(500);
            entity.Property(r => r.ScoreOptionsSnapshot)
                .HasConversion(
                    value => SurveyScoreOptionsConverter.Serialize(value),
                    value => SurveyScoreOptionsConverter.Deserialize(value))
                .HasColumnType("TEXT")
                .Metadata.SetValueComparer(surveyScoreOptionsComparer);

            entity.HasOne(r => r.Customer)
                .WithMany()
                .HasForeignKey(r => r.CustomerId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(r => r.Survey)
                .WithMany()
                .HasForeignKey(r => r.SurveyId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.SetNull);

            entity.HasMany(r => r.Answers)
                .WithOne(a => a.SurveyResponse)
                .HasForeignKey(a => a.SurveyResponseId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<SurveyResponseAnswer>(entity =>
        {
            entity.Property(a => a.QuestionText).IsRequired().HasMaxLength(200);
            entity.Property(a => a.AnswerText).IsRequired().HasMaxLength(200);
            entity.HasIndex(a => new { a.SurveyResponseId, a.QuestionNumber }).IsUnique();
            entity.ToTable(table =>
                table.HasCheckConstraint("CK_SurveyResponseAnswers_Score", "\"Score\" >= 1 AND \"Score\" <= 5"));

            entity.HasOne(a => a.SourceQuestion)
                .WithMany()
                .HasForeignKey(a => a.SourceQuestionId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.SetNull);
        });
    }

    private static void ConfigureBaseProperties<TEntity>(ModelBuilder modelBuilder)
        where TEntity : Base
    {
        modelBuilder.Entity<TEntity>(entity =>
        {
            entity.HasKey(e => e.Id);
        });
    }

    private static class ScoreOptionsConverter
    {
        private static readonly JsonSerializerOptions JsonOptions = new();

        public static string Serialize(IList<ScoreOption> value) =>
            JsonSerializer.Serialize(value, JsonOptions);

        public static IList<ScoreOption> Deserialize(string value) =>
            JsonSerializer.Deserialize<List<ScoreOption>>(value, JsonOptions)
            ?? [];
    }

    private static class SurveyScoreOptionsConverter
    {
        private static readonly JsonSerializerOptions JsonOptions = new();

        public static string Serialize(IList<SurveyScoreOption> value) =>
            JsonSerializer.Serialize(value, JsonOptions);

        public static IList<SurveyScoreOption> Deserialize(string value) =>
            JsonSerializer.Deserialize<List<SurveyScoreOption>>(value, JsonOptions)
            ?? [];
    }
}
