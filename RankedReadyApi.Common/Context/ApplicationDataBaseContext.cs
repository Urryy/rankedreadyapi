using Microsoft.EntityFrameworkCore;
using RankedReadyApi.Common.Context.Configuration;
using RankedReadyApi.Common.Entities;

namespace RankedReadyApi.Common.Context;

public class ApplicationDataBaseContext : DbContext
{
    public DbSet<User> Users { get; set; } = null!;
    public DbSet<Skin> Skins { get; set; } = null!;
    public DbSet<LeagueLegendAccount> LeagueLegendAccounts { get; set; } = null!;
    public DbSet<ValorantAccount> ValorantAccounts { get; set; } = null!;
    public DbSet<Announcement> Announcements { get; set; } = null!;
    public DbSet<Transaction> Transactions { get; set; } = null!;
    public DbSet<SupportTicket> SupportTickets { get; set; } = null!;
    public DbSet<CodeChangedPassword> CodeChangedPasswords { get; set; } = null!;
    public DbSet<TransactionStripe> TransactionsStripe { get; set; } = null!;

    public ApplicationDataBaseContext(DbContextOptions<ApplicationDataBaseContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new User_FluentConfiguration());
        modelBuilder.ApplyConfiguration(new Announcement_FluentConfiguration());
        modelBuilder.ApplyConfiguration(new CodeChangedPassword_FluentConfiguration());
        modelBuilder.ApplyConfiguration(new Skin_FluentConfiguration());
        modelBuilder.ApplyConfiguration(new SupportTicket_FluentConfiguration());
        modelBuilder.ApplyConfiguration(new Transaction_FluentConfiguration());
        modelBuilder.ApplyConfiguration(new TransactionStripe_FluentConfiguration());
        modelBuilder.ApplyConfiguration(new ValorantAccount_FluentConfiguration());
        modelBuilder.ApplyConfiguration(new LeagueLegendAccount_FluentConfiguration());
    }
}
