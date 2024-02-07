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

        builder.HasData(new Author(1, "George", "Orwell"));
        builder.HasData(new Author(2, "Adam", "Smith"));
    }
}
