using System;
using CryptoExchange.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CryptoExchange.Persistence.EntityTypeConfigurations
{
	public class ExchangerConfiguration : IEntityTypeConfiguration<Exchanger>
	{
		public void Configure(EntityTypeBuilder<Exchanger> builder)
        {
            builder.HasKey(exchanger => exchanger.Id);
            builder.HasIndex(exchanger => exchanger.Id).IsUnique();
            builder.Property(exchanger => exchanger.Name).IsRequired();
            builder.Property(exchanger => exchanger.WebResuorceUrl).IsRequired();
            builder.HasOne<AppUser>("User").WithOne("Exchanger").HasForeignKey("Exchanger", "UserId");
            builder.HasMany<Order>("Orders").WithOne("Exchanger");
            
        }

	}
}

