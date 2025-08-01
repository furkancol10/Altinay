using Altinay.Enums;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Domain.Services;

namespace Altinay.Personel
{
    public interface IPersonelRequestManager : IDomainService
    {
        //
        // 1.//Talep Edilen Personel Başka Birinin Yerine Mi ?
        //
        Task<PersonelRequest> CreateForReplacementAsync(
            string requesterName,
            string requesterTitle,
            Guid managerId,
            string jobTitle,
            Guid departmentId,
            int numberOfPersonel,
            DateTime requestDate,
            string replacementPersonName,
            RequestType requestType,
            string? reasonForLeaving,
            DateTime? leavingDate,
            string? requestReason,
            int minAge,
            int maxAge,
            Gender gender,
            List<string> otherQualifications,
            string location,
            ExperienceStatus experienceStatus,
            string suggestedPersonelName,
            string suggestedJobTitle,
            string reasonForSuggestion
        );

        //
        // 2.//Yeni Pozisyon Plan Dahilinde Mi ?
        //
        Task<PersonelRequest> CreateForNewPositionInPlanAsync(
            string requesterName,
            string requesterTitle,
            Guid managerId,
            string jobTitle,
            Guid departmentId,
            int numberOfPersonel,
            DateTime requestDate,
            string requestReason,
            int minAge,
            int maxAge,
            Gender gender,
            List<string> otherQualifications,
            string location,
            ExperienceStatus experienceStatus,
            string reasonForNewPosition,
            string suggestedPersonelName,
            string suggestedJobTitle,
            string reasonForSuggestion
        );

        //
        // 3.//Yeni Pozisyon Plan Haricinde Mi ?
        //
        Task<PersonelRequest> CreateForNewPositionOutOfPlanAsync(
            string requesterName,
            string requesterTitle,
            Guid managerId,
            string jobTitle,
            Guid departmentId,
            int numberOfPersonel,
            DateTime requestDate,
            string requestReason,
            int minAge,
            int maxAge,
            Gender gender,
            List<string> otherQualifications,
            string location,
            ExperienceStatus experienceStatus,
            string reasonForNewPosition,
            string suggestedPersonelName,
            string suggestedJobTitle,
            string reasonForSuggestion
        );
    }
}
