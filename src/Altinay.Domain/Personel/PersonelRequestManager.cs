using System;
using System.Threading.Tasks;
using Volo.Abp.Domain.Services;
using System.Collections.Generic;
using Altinay.Enums;

namespace Altinay.Personel
{
    public class PersonelRequestManager : DomainService, IPersonelRequestManager
    {
        private readonly IPersonelRequestRepository _personelRequestRepository;

        public PersonelRequestManager(IPersonelRequestRepository personelRequestRepository)
        {
            _personelRequestRepository = personelRequestRepository;
        }

        //
        // 1.//Talep Edilen Personel Başka Birinin Yerine Mi ?
        //
        public async Task<PersonelRequest> CreateForReplacementAsync(
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
            string reasonForSuggestion)
        {
            if (string.IsNullOrWhiteSpace(reasonForLeaving))
                throw new ArgumentException("ReasonForLeavingCannotBeNull.", nameof(reasonForLeaving));

            if (!leavingDate.HasValue)
                throw new ArgumentException("DateOfLeavingCannotBeNull", nameof(leavingDate));

            //
            // 2.//Entity instance oluşturuluyor
            //
            var request = new PersonelRequest(
                Guid.NewGuid(),
                jobTitle,
                departmentId,
                managerId,
                numberOfPersonel,
                requestDate,
                replacementPersonName,
                requestReason ?? "",
                reasonForLeaving!,
                leavingDate.Value,
                "",
                gender,
                location,
                experienceStatus,
                minAge,
                maxAge,
                otherQualifications,
                requesterName,
                requesterTitle,
                suggestedPersonelName ?? "",
                suggestedJobTitle ?? "",
                reasonForSuggestion ?? ""
            );

            request.SetAsReplacement(reasonForLeaving!, leavingDate.Value);

            return await Task.FromResult(request);
        }

        //
        // 2.//Yeni Pozisyon Plan Dahilinde Mi ?
        //
        public async Task<PersonelRequest> CreateForNewPositionInPlanAsync(
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
            string reasonForSuggestion)
        {
            var request = new PersonelRequest(
                Guid.NewGuid(),
                jobTitle,
                departmentId,
                managerId,
                numberOfPersonel,
                requestDate,
                "",
                requestReason,
                "",
                DateTime.MinValue,
                reasonForNewPosition,
                gender,
                location,
                experienceStatus,
                minAge,
                maxAge,
                otherQualifications,
                requesterName,
                requesterTitle,
                suggestedPersonelName,
                suggestedJobTitle,
                reasonForSuggestion
            );

            request.SetAsNewPosition(RequestType.NewPosition_InPlan, reasonForNewPosition);

            return await Task.FromResult(request);
        }
        //
        // 3.//Yeni Pozisyon Plan Haricinde Mi ?
        //
        public async Task<PersonelRequest> CreateForNewPositionOutOfPlanAsync(
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
            string reasonForSuggestion)
        {
            var request = new PersonelRequest(
                Guid.NewGuid(),
                jobTitle,
                departmentId,
                managerId,
                numberOfPersonel,
                requestDate,
                "",
                requestReason,
                "",
                DateTime.MinValue,
                reasonForNewPosition,
                gender,
                location,
                experienceStatus,
                minAge,
                maxAge,
                otherQualifications,
                requesterName,
                requesterTitle,
                suggestedPersonelName,
                suggestedJobTitle,
                reasonForSuggestion
            );

            request.SetAsNewPosition(RequestType.NewPosition_OutPlan, reasonForNewPosition);

            return await Task.FromResult(request);
        }
    }
}
