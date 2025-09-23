using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApi.Domain.Entities
{
    public class Transactions
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public decimal Balance { get; set; }
        public int SenderId { get; set; }
        public int ReceiverId { get; set; }
        public DateTime Date { get; set; }
    }
}
