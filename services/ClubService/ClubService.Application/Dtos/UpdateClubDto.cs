﻿namespace ClubService.Application.Dtos;

public class UpdateClubDto
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string LogoUrl { get; set; } = string.Empty;
}
