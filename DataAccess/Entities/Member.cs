﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace DataAccess.Entities;

public partial class Member
{
    public int MemberId { get; set; }

    public string Email { get; set; }

    public string CompanyName { get; set; }

    public string City { get; set; }

    public string Country { get; set; }

    public string Password { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}