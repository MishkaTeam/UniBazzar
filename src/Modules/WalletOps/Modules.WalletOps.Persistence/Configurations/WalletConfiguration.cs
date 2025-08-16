using BuildingBlocks.Persistence.Configurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Modules.WalletOps.Domain.Aggregates.WalletTrx;

namespace Modules.WalletOps.Persistence.Configurations
{
    internal class WalletConfiguration : BaseConfiguration<Wallet>
    {

        public override void Configure(EntityTypeBuilder<Wallet> builder)
        {
            base.Configure(builder);

            builder.ToTable("Wallets");

            builder.Ignore(x => x.TotalBalance);
            builder.Ignore(x => x.AvailableBalance);

            builder.Property(x => x.CurrencyCode).HasMaxLength(3);
            builder.OwnsOne(x => x.WithdrawableBalance, dBuilder =>
            {
                dBuilder.Property(x => x.Amount).HasColumnName("WithdrawableBalance");
                dBuilder.Property(x => x.Currency).HasColumnName("WithdrawableBalanceCurrency").HasMaxLength(3);
            });

            builder.OwnsOne(x => x.NonWithdrawableBalance, dBuilder =>
            {
                dBuilder.Property(x => x.Amount).HasColumnName("NonWithdrawableBalance");
                dBuilder.Property(x => x.Currency).HasColumnName("NonWithdrawableBalanceCurrency").HasMaxLength(3);
            });

            builder.OwnsOne(x => x.HeldBalance, dBuilder =>
            {
                dBuilder.Property(x => x.Amount).HasColumnName("HeldBalance");
                dBuilder.Property(x => x.Currency).HasColumnName("HeldBalanceCurrency").HasMaxLength(3);
            });


            builder.OwnsMany(x => x.HeldFunds, itemBuilder =>
            {
                itemBuilder.ToTable("HeldFunds");

                itemBuilder.HasKey(t => t.Id);

                itemBuilder.Property(x => x.Ordering)
                       .HasDefaultValue(10_000);

                itemBuilder.Property(s => s.OwnerId)
                       .IsRequired();

                itemBuilder.Property(s => s.InsertedBy)
                       .IsRequired();

                itemBuilder.Property(s => s.UpdatedBy)
                       .IsRequired();

                itemBuilder.Property(s => s.InsertDateTime)
                       .IsRequired();

                itemBuilder.Property(s => s.UpdateDateTime)
                       .IsRequired();   
                
                itemBuilder.Property(s => s.Reason)
                       .HasMaxLength(255)
                       .IsRequired(false);

                itemBuilder.OwnsOne(x => x.Amount, dBuilder =>
                {
                    dBuilder.Property(x => x.Amount).HasColumnName("Amount");
                    dBuilder.Property(x => x.Currency).HasColumnName("AmountCurrency").HasMaxLength(3);
                });
            });


            builder.OwnsMany(x => x.Transactions, itemBuilder =>
            {
                itemBuilder.ToTable("Transactions");

                itemBuilder.HasKey(t => t.Id);

                itemBuilder.Property(x => x.Ordering)
                       .HasDefaultValue(10_000);

                itemBuilder.Property(s => s.OwnerId)
                       .IsRequired();

                itemBuilder.Property(s => s.InsertedBy)
                       .IsRequired();

                itemBuilder.Property(s => s.UpdatedBy)
                       .IsRequired();

                itemBuilder.Property(s => s.InsertDateTime)
                       .IsRequired();

                itemBuilder.Property(s => s.UpdateDateTime)
                       .IsRequired();

                itemBuilder.Property(s => s.Description)
                       .HasMaxLength(255)
                       .IsRequired(false);

                itemBuilder.OwnsOne(x => x.Amount, dBuilder =>
                {
                    dBuilder.Property(x => x.Amount).HasColumnName("Amount");
                    dBuilder.Property(x => x.Currency).HasColumnName("AmountCurrency").HasMaxLength(3);
                });
            });
        }

    }
}
