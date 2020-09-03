﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TicketHubDataLibrary.Models
{
    public class Shop
    {
        [Display(Name = "商家代號")]
        public Guid Id { get; set; }
        public string ShopName { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string Website { get; set; }
        public string BannerImg { get; set; }
        public DateTime AppliedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public DateTime ValidatedDate { get; set; }
        public Guid? ReviewerId { get; set; }
        [ForeignKey("ReviewerId")]
        public Admin Admin { get; set; }
        public ICollection<ShopEmployee> ShopEmployees { get; set; }
        public ICollection<ShopTag> ShopTags { get; set; }
    }
}