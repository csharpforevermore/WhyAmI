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

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        ConfigureBaseProperties<Customer>(modelBuilder);
        ConfigureBaseProperties<Vendor>(modelBuilder);
        ConfigureBaseProperties<Questionnaire>(modelBuilder);
        ConfigureBaseProperties<Question>(modelBuilder);
        ConfigureBaseProperties<Answer>(modelBuilder);
        ConfigureBaseProperties<CustomerQuestionnaireResults>(modelBuilder);

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
}
