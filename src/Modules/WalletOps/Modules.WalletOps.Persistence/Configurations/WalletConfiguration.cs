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


            builder.OwnsMany(x => x.HeldFunds, heldBuilder =>
            {
                heldBuilder.ToTable("HeldFunds");

                heldBuilder.HasKey(t => t.Id);

                heldBuilder.Property(x => x.Ordering)
                       .HasDefaultValue(10_000);

                heldBuilder.Property(s => s.OwnerId)
                       .IsRequired();

                heldBuilder.Property(s => s.InsertedBy)
                       .IsRequired();

                heldBuilder.Property(s => s.UpdatedBy)
                       .IsRequired();

                heldBuilder.Property(s => s.InsertDateTime)
                       .IsRequired();

                heldBuilder.Property(s => s.UpdateDateTime)
                       .IsRequired();   
                
                heldBuilder.Property(s => s.Reason)
                       .HasMaxLength(255)
                       .IsRequired(false);

                heldBuilder
                .Property(x => x.OperationId)
                .HasMaxLength(255)
                .IsRequired(false);

                heldBuilder.HasIndex(x => x.OperationId)
                .HasFilter("\"OperationId\" IS NOT NULL")
                .IsUnique(true);

                heldBuilder.OwnsOne(x => x.Amount, dBuilder =>
                {
                    dBuilder.Property(x => x.Amount).HasColumnName("Amount");
                    dBuilder.Property(x => x.Currency).HasColumnName("AmountCurrency").HasMaxLength(3);
                });
            });


            builder.OwnsMany(x => x.Transactions, trxBuilder =>
            {
                trxBuilder.ToTable("Transactions");

                trxBuilder.HasKey(t => t.Id);

                trxBuilder.Property(x => x.Ordering)
                       .HasDefaultValue(10_000);

                trxBuilder.Property(s => s.OwnerId)
                       .IsRequired();

                trxBuilder.Property(s => s.InsertedBy)
                       .IsRequired();

                trxBuilder.Property(s => s.UpdatedBy)
                       .IsRequired();

                trxBuilder.Property(s => s.InsertDateTime)
                       .IsRequired();

                trxBuilder.Property(s => s.UpdateDateTime)
                       .IsRequired();

                trxBuilder.Property(s => s.Description)
                       .HasMaxLength(255)
                       .IsRequired(false);

                trxBuilder
                .Property(x => x.OperationId)
                .HasMaxLength(255)
                .IsRequired(false);

                trxBuilder.HasIndex(x => x.OperationId)
                .HasFilter("\"OperationId\" IS NOT NULL")
                .IsUnique(true);


                trxBuilder.OwnsOne(x => x.Amount, dBuilder =>
                {
                    dBuilder.Property(x => x.Amount).HasColumnName("Amount");
                    dBuilder.Property(x => x.Currency).HasColumnName("AmountCurrency").HasMaxLength(3);
                });
            });
        }

    }
}
