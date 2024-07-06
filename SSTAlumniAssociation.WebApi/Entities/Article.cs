using System.ComponentModel.DataAnnotations;

namespace SSTAlumniAssociation.WebApi.Entities;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
public class Article
{
    public Guid Id { get; set; }
    [MaxLength(512)] public required string Title { get; set; }
    [MaxLength(1024)] public required string Description { get; set; }
    [MaxLength(512)] public required string HeroImageAlt { get; set; }
    [MaxLength(512)] public required string HeroImageUrl { get; set; }
    [MaxLength(512)] public required string CtaTitle { get; set; }
    [MaxLength(1024)] public required string CtaUrl { get; set; }
}

#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member