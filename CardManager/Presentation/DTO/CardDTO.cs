﻿using CardManager.Domain.Enums;

namespace CardManager.Presentation.DTO
{
    public class CardDto
    {
        public string? CardSerial { get; set; }
        public string? CardOwnerName { get; set; }
        public string? CardOwnerCpf { get; set; }
        public string? CardType { get; set; }
    }
}
