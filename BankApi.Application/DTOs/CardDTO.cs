using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApi.Application.DTOs
{
    public class CardDto
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string CardHoLderName { get; set; } = string.Empty;
        [Required]
        [StringLength(16, MinimumLength = 16)]
        public string Number { get; set; } = string.Empty;
        [Required]
        public int CCV { get; set; }
        [Required]
        public int UserId { get; set; }
        [Required]
        public DateTime ExpirationDate { get; set; }
    }
}
