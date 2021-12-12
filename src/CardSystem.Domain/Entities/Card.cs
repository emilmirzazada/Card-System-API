namespace CardSystem.Domain.Entities
{
    using CardSystem.Domain.Enums;
    using System;

    public class Card
    {
        public int Id { get; set; }

        public string Number { get; set; }

        public string CVV { get; set; }
        public bool Valid { get; set; }
        public CardState State { get; set; }
        public CardType Type { get; set; }
        public DateTime DateRegistered { get; set; }

        public DateTime ExpirationDate { get; set; }

        public string StateName => State.ToString();
        public string TypeName => Type.ToString();
    }
}
