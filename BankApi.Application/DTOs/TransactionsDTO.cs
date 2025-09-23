using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApi.Application.DTOs
{
    public class TransactionsDTO
    {
        [Required]
        public decimal Amount { get; set; }
        [Required]
        public decimal Balance { get; set; }
        [Required]
        public int SenderId { get; set; }
        [Required]
        public int ReceiverId { get; set; }
    }
}
