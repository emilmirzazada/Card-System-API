namespace CardSystem.Persistence
{
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;
    using CardSystem.Domain.Entities;
    using CardSystem.Application.Common.Interfaces;

    public class CardSystemDbContext : IdentityDbContext<User>,ICardSystemDbContext
    {
        public CardSystemDbContext(DbContextOptions<CardSystemDbContext> options)
            : base(options)
        {
        }

        public override DbSet<User> Users { get; set; }
        
        public DbSet<Card> Cards { get; set; }
        public DbSet<ClientCard> ClientCards { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Vendor> Vendors { get; set; }
        public DbSet<VendorAddress> VendorAddresses { get; set; }

        public DbSet<Deposit> Deposits { get; set; }

        public DbSet<Transaction> Transactions { get; set; }

        public DbSet<RefreshToken> RefreshTokens { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Deposit>()
                .HasOne(d => d.Client)
                .WithMany(c => c.Deposits)
                .HasForeignKey(d => d.ClientId);

            builder.Entity<Transaction>()
                .HasOne(t => t.Client)
                .WithMany(c => c.Transactions)
                .HasForeignKey(t => t.ClientId);

            base.OnModelCreating(builder);
        }
    }
}
