using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApi.Domain.Entities
{
    public class Cards
    {
        public int Id { get; set; }
        public string CardHoLderName { get; set; } = string.Empty;
        public string Number { get; set; } = string.Empty;
        public int CCV { get; set; }
        public int UserId { get; set; }
        public DateTime ExpirationDate { get; set; }
        public DateTime AddedAt { get; set; }
    }
}
