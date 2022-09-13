using System;
using CryptoExchange.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CryptoExchange.Persistence.EntityTypeConfigurations
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasKey(order => order.Id);
            builder.HasIndex(order => order.Id).IsUnique();
            builder.HasOne<Exchanger>("Exchanger").WithMany("Orders");
            builder.Property(order => order.ExchangeFrom).IsRequired();
            builder.Property(order => order.ExchangeTo).IsRequired();
            builder.Property(order => order.IncomeSum).IsRequired();
            builder.Property(order => order.OutcomeSum).IsRequired();
            builder.Property(order => order.MaxAmount).IsRequired();
            builder.Property(order => order.MinAmount).IsRequired();
            builder.Property(order => order.ExchangerName).IsRequired();
        }
    }
}


