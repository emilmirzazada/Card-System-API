namespace CardSystem.Domain.Entities
{
    using CardSystem.Domain.Entities;
    using Microsoft.AspNetCore.Identity;
    using System;
    using System.Collections.Generic;

    public class User : IdentityUser
    {
        public User()
        {
            this.Deposits = new List<Deposit>();
            this.Transactions = new List<Transaction>();
            this.Accounts = new List<Account>();
        }
        public string FirstName { get; set; }

        public string LastName { get; set; }
        public DateTime LastLoginTime { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime LastPasswordChangeDate { get; set; }

        public int? RefreshTokenId { get; set; }
        public RefreshToken RefreshToken { get; set; }

        public ICollection<Account> Accounts { get; set; }

        public ICollection<Deposit> Deposits { get; set; }

        public ICollection<Transaction> Transactions { get; set; }
    }
}
