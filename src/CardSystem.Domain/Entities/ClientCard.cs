using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardSystem.Domain.Entities
{
    public class ClientCard
    {
        public int Id { get; set; }
        public string ClientId { get; set; }
        public User Client { get; set; }
        public int CardId { get; set; }
        public int AccountId { get; set; }
        public Card Card { get; set; }
        public Account Account { get; set; }
    }
}
