﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static Tuya.CreditCard.Api.DTO.Models.Enums;

namespace Tuya.CreditCard.Api.DAL.Contracts.Entities
{
    public class TransactionEntity
    {
        public Guid Id { get; set; }

        public Guid SaleId { get; set; }

        public Guid CardId { get; set; }

        [StringLength(100)]
        public string TransactionReference { get; set; } = string.Empty;

        [Column(TypeName = "numeric(18,2)")]
        public decimal Value { get; set; }

        public DateTime CreationDate { get; set; }

        public TransactionState State { get; set; }

        [StringLength(500)]
        public string? ResponseMessage { get; set; }

        public virtual SaleEntity Sale { get; set; } = null!;

        public virtual CardEntity Card { get; set; } = null!;
    }
}
