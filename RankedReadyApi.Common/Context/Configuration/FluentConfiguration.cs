using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RankedReadyApi.Common.Entities;

namespace RankedReadyApi.Common.Context.Configuration;

public class User_FluentConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> modelBuilder)
    {
        modelBuilder.HasKey(u => u.Id);
    }
}

public class Announcement_FluentConfiguration : IEntityTypeConfiguration<Announcement>
{
    public void Configure(EntityTypeBuilder<Announcement> modelBuilder)
    {
        modelBuilder.HasKey(u => u.Id);
    }
}

public class CodeChangedPassword_FluentConfiguration : IEntityTypeConfiguration<CodeChangedPassword>
{
    public void Configure(EntityTypeBuilder<CodeChangedPassword> modelBuilder)
    {
        modelBuilder.HasKey(u => u.Id);
    }
}

public class Skin_FluentConfiguration : IEntityTypeConfiguration<Skin>
{
    public void Configure(EntityTypeBuilder<Skin> modelBuilder)
    {
        modelBuilder.HasKey(u => u.Id);
    }
}

public class SupportTicket_FluentConfiguration : IEntityTypeConfiguration<SupportTicket>
{
    public void Configure(EntityTypeBuilder<SupportTicket> modelBuilder)
    {
        modelBuilder.HasKey(u => u.Id);
    }
}

public class Transaction_FluentConfiguration : IEntityTypeConfiguration<Transaction>
{
    public void Configure(EntityTypeBuilder<Transaction> modelBuilder)
    {
        modelBuilder.HasKey(u => u.Id);
    }
}

public class TransactionStripe_FluentConfiguration : IEntityTypeConfiguration<TransactionStripe>
{
    public void Configure(EntityTypeBuilder<TransactionStripe> modelBuilder)
    {
        modelBuilder.HasKey(u => u.Id);
    }
}

public class ValorantAccount_FluentConfiguration : IEntityTypeConfiguration<ValorantAccount>
{
    public void Configure(EntityTypeBuilder<ValorantAccount> modelBuilder)
    {
        modelBuilder.HasKey(u => u.Id);
    }
}

public class LeagueLegendAccount_FluentConfiguration : IEntityTypeConfiguration<LeagueLegendAccount>
{
    public void Configure(EntityTypeBuilder<LeagueLegendAccount> modelBuilder)
    {
        modelBuilder.HasKey(u => u.Id);
    }
}

