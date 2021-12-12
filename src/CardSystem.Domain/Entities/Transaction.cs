using CardSystem.Domain.Enums;
using System;

namespace CardSystem.Domain.Entities
{
    public class Transaction
    {
        public int Id { get; set; }

        public DateTime CreatedAt { get; set; }

        public decimal Amount { get; set; }
        public int CardId { get; set; }
        public Card Card { get; set; }
        public int VendorId { get; set; }
        public Vendor Vendor { get; set; }
        public TransactionType Type { get; set; }
        public TransactionStatus Status { get; set; }

        public string ClientId { get; set; }
        public User Client { get; set; }

        public string TypeName => Type.ToString();
        public string StatusName => Status.ToString();
    }
}
