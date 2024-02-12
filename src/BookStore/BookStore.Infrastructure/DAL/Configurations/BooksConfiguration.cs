using BookStore.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookStore.Infrastructure.DAL.Configurations;

internal class BooksConfiguration : IEntityTypeConfiguration<Book>
{
    public void Configure(EntityTypeBuilder<Book> builder)
    {
        builder.HasKey(b => b.Id);
        builder.Property(b => b.Id)
            .HasColumnName("BookId")
            .UseIdentityColumn();
        builder.Property(b => b.Title)
            .IsRequired()
            .HasMaxLength(100);
        builder.Property(b => b.BasePrice)
            .IsRequired()
            .HasColumnType("decimal(18,2)");

        builder.HasOne(b => b.Author)
            .WithMany(a => a.Books)
            .HasForeignKey("AuthorId");
    }
}
