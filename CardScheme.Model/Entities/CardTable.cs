using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CardScheme.Domain.Entities
{
    /// <summary>
    /// The Database model with annotation
    /// </summary>
    public class CardTable
    {
        [Key]
        public int Id { get; set; }

        [StringLength(20)]
        [Required]
        public string CardNumber { get; set; }

        [StringLength(6)]
        [Required]
        public string BinCode { get; set; }

        [Required]
        public int HitCount { get; set; }

    }
}
