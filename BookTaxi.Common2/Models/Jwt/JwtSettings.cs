﻿namespace BookTaxi.Common2.Models.Jwt;

public class JwtSettings
{
    public string Issuer { get; set; }
    public string Audience { get; set; }
    public string Key { get; set; }
}