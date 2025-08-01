using Altinay.Enums;
using System;
using System.Collections.Generic;
using Volo.Abp.Application.Dtos;

public class PersonelRequestDto : AuditedEntityDto<Guid>
{
    // Talep Edilen Bilgiler
    public string JobTitle { get; private set; }
    public Guid DepartmentId { get; private set; }
    public Guid ManagerId { get; private set; }
    public int NumberOfPersonel { get; private set; }
    public DateTime RequestDate { get; private set; }

    // Personel Talep Nedeni
    public RequestType RequestType { get; private set; }
    public string? ReasonForLeaving { get; private set; }
    public DateTime? LeavingDate { get; private set; }
    public string? RequestReason { get; private set; }
    public string? ReasonForNewPosition { get; private set; }
    public string? ReplacementPersonName { get; private set; }

    // Talep Edilen Personel Bilgileri
    public int MinAge { get; private set; }
    public int MaxAge { get; private set; }
    public Gender Gender { get; private set; }
    public string Location { get; private set; }
    public ExperienceStatus ExperienceStatus { get; private set; }
    public List<string> OtherQualifications { get; private set; }

    // Önerilen Personel Bilgileri
    public string? SuggestedPersonelName { get; private set; }
    public string? SuggestedJobTitle { get; private set; }
    public string? ReasonForSuggestion { get; private set; }

    // Talep Eden Bilgiler
    public string RequesterName { get; set; }
    public string RequesterTitle { get; set; }

    // Talep Durumu
    public Guid? ApproverId { get; private set; }
    public DateTime? ApprovedDate { get; private set; }
    public string? RejectionReason { get; private set; }
    public RequestStatus RequestStatus { get; private set; }
}
