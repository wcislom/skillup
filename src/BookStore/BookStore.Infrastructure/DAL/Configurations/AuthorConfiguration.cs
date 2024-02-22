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
        builder.ComplexProperty(a => a.Name);
      
        builder.Property<DateTime>("LastUpdated");

        builder.HasMany(a => a.Books)
            .WithOne(b => b.Author)
            .HasForeignKey("AuthorId")
            .OnDelete(DeleteBehavior.Cascade);
        var a = new Author("John", "Smith", 1);
        a.AddBook(new Book("1984", DateOnly.Parse("2020-12-01"), 19.99M));
        //builder.HasData(a);
    }
}
