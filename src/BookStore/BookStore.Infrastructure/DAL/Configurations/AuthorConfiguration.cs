using BookStore.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookStore.Infrastructure.DAL.Configurations;

internal class AuthorConfiguration : IEntityTypeConfiguration<Author>
{
    public void Configure(EntityTypeBuilder<Author> builder)
    {
        builder.HasKey(a => a.Id);
        builder.Property(a => a.Id)
            .HasColumnName("AuthorId")
            .UseIdentityColumn();
        builder.Property(a => a.FirstName)
            .IsRequired()
            .HasMaxLength(100);
        builder.Property(a => a.LastName)
            .IsRequired()
            .HasMaxLength(100);
        builder.HasMany(a => a.Books)
            .WithOne(b => b.Author)
            .HasForeignKey(b => b.AuthorId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasData(new Author(1, "George", "Orwell"));
        builder.HasData(new Author(2, "Adam", "Smith"));
    }
}
