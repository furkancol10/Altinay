using Altinay.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;

namespace Altinay.Personel
{
    public class  PersonelRequest : AuditedAggregateRoot<Guid>
    {
        //
        // Talep Edilen Bilgileri
        //
        [Required]
        public string JobTitle { get; private set; }
        public Guid DepartmentId { get; private set; }
        public Guid ManagerId { get; private set; }
        public int NumberOfPersonel { get; private set; }
        public DateTime RequestDate { get; private set; }
        public string? RequestReason { get; private set; }
        //
        // Personel Talep Nedeni
        //
        public RequestType RequestType { get; private set; }
        public string? ReplacementPersonName { get; set; }
        public string? ReasonForLeaving { get; private set; }
        public DateTime? LeavingDate { get; private set; }        
        public string? ReasonForNewPosition { get; set; }
        //
        // Talep Edilen Personel Bilgileri
        //
        [Required]
        public int MinAge { get; set; }
        [Required]
        public int MaxAge { get; set; }
        [Required]
        public Gender Gender { get; private set; }
        [Required]
        public string Location { get; private set; }
        public ExperienceStatus ExperienceStatus { get; private set; }
        [Required]
        public List<string> OtherQualifications { get; private set; }
        //
        // Önerilen Personel Bilgileri
        //
        public string? SuggestedPersonelName { get; private set; }
        public string? SuggestedJobTitle { get; private set; }
        public string? ReasonForSuggestion { get; private set; }
        //
        // Talep Eden Bilgileri
        //
        [Required]
        public string RequesterName { get; private set; }
        [Required]
        public string RequesterTitle { get; private set; }
        //
        // Talep Durumu
        //
        public Guid? ApproverId { get; private set; }
        public DateTime? ApprovedDate { get; private set; }
        public string? RejectionReason { get; private set; }
        public RequestStatus RequestStatus { get; private set; }
        
        private PersonelRequest()
        {
            // Required by EF Core
        }
        //
        // Constructer Tanımlamaları
        //
        public PersonelRequest(
            Guid id,
            string jobTitle,
            Guid departmentId,
            Guid managerId,
            int numberOfPersonel,
            DateTime requestDate,
            string replacementPersonName,
            string requestReason,
            string reasonForLeaving,
            DateTime leavingDate,
            string reasonForNewPosition,
            Gender gender,
            string location,
            ExperienceStatus experienceStatus,
            int minAge,
            int maxAge,
            List<string> otherQualifications,
            string requesterName,
            string requesterTitle,
            string suggestedPersonelName,
            string suggestedJobTitle,
            string reasonForSuggestion
            )
        {
            JobTitle = jobTitle;
            DepartmentId = departmentId;
            ManagerId = managerId;
            NumberOfPersonel = numberOfPersonel;
            RequestDate = requestDate;
            ReplacementPersonName = replacementPersonName;
            RequestReason = requestReason;
            ReasonForNewPosition = reasonForNewPosition;
            Gender = gender;
            Location = location;
            ExperienceStatus = experienceStatus;
            MinAge = minAge;
            MaxAge = maxAge;
            OtherQualifications = otherQualifications;
            RequesterName = requesterName;
            RequesterTitle = requesterTitle;
            SuggestedPersonelName = suggestedPersonelName;
            SuggestedJobTitle = suggestedJobTitle;
            ReasonForSuggestion = reasonForSuggestion;
        }        
        //
        //Birinin Yerine mi Personel Talep Ediliyor?
        //
        internal void SetAsReplacement(string leavingReason, DateTime leavingDate)
        {
            RequestType = RequestType.Replacement;
            ReasonForLeaving = leavingReason;
            LeavingDate = leavingDate;
        }
        //
        //Yeni Pozisyon mu ?
        //
        internal void SetAsNewPosition(RequestType requestType, string reasonForNewPosition)
        {
            RequestType = requestType;
            ReasonForNewPosition = reasonForNewPosition;
            
        }   

    }    
}
