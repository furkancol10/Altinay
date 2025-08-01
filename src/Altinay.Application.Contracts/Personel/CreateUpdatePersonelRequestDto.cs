using Altinay.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Altinay.Personel
{
    public class CreateUpdatePersonelRequestDto : IValidatableObject
    {
        //
        // Talep Edilen Bilgiler
        //
        [Required]
        public string JobTitle { get; set; }
        [Required]
        public Guid DepartmentId { get; set; }
        [Required]
        public Guid ManagerId { get; set; }
        [Required]
        public int NumberOfPersonel { get; set; } = 1;
        [Required]
        public DateTime RequestDate { get; set; } = DateTime.Today;
        public string? RequestReason { get; set; }
        //
        // Talep Türü
        //
        public RequestType RequestType { get; set; }
        public string? ReplacementPersonName { get; set; }
        public string? ReasonForLeaving { get; set; }
        public DateTime? LeavingDate { get; set; } = DateTime.Today;
        public string? ReasonForNewPosition { get; set; }
        //
        // Personel Bilgisi
        //
        [Required]
        public int MinAge { get; set; }
        [Required]
        public int MaxAge { get; set; }
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (MinAge < 18) { yield return new ValidationResult("MinAgeCanBeHigherThan18!", new[] { nameof(MinAge) }); }
            if (MaxAge > 60) { yield return new ValidationResult("MaxAgeCanBeLowerThan60!", new[] { nameof(MaxAge) }); }
            if (MinAge >= MaxAge) { yield return new ValidationResult("MinAgeCanBeLowerThanMaxAge!", new[] { nameof(MinAge), nameof(MaxAge) }); }
        }
        [Required]
        public Gender Gender { get; set; }
        [Required]
        public string Location { get; set; }
        public ExperienceStatus ExperienceStatus { get; set; }
        public List<string> OtherQualifications { get; set; } = new();
        //
        //Önerilen Aday Bilgileri
        //
        public string? SuggestedPersonelName { get; set; }
        public string? SuggestedJobTitle { get; set; }
        public string? ReasonForSuggestion { get; set; }
        //
        // Talep Eden Bilgileri
        //
        [Required]
        public string RequesterName { get; set; }
        [Required]
        public string RequesterTitle { get; set; }
    }
}
