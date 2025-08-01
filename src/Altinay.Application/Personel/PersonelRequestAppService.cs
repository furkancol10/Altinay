using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using System.Linq.Dynamic.Core;
using Altinay.Enums;

namespace Altinay.Personel
{
    public class PersonelRequestAppService :
        CrudAppService<
            PersonelRequest,
            PersonelRequestDto,
            Guid,
            PersonelRequestInputListDto,
            CreateUpdatePersonelRequestDto
            >, IPersonelRequestAppService
    {
        private readonly PersonelRequestManager _personelRequestManager;

        public PersonelRequestAppService(
            IRepository<PersonelRequest, Guid> repository,
            PersonelRequestManager personelRequestManager
        ) : base(repository)
        {
            _personelRequestManager = personelRequestManager;
        }

        //
        // Oluşturma işlemleri
        //
        public override async Task<PersonelRequestDto> CreateAsync(CreateUpdatePersonelRequestDto input)
        {
            PersonelRequest request;

            if (input.RequestType == RequestType.Replacement)
            {
                request = await _personelRequestManager.CreateForReplacementAsync(
                    input.RequesterName,
                    input.RequesterTitle,
                    input.ManagerId,
                    input.JobTitle,
                    input.DepartmentId,
                    input.NumberOfPersonel,
                    input.RequestDate,
                    input.ReplacementPersonName ?? string.Empty,
                    input.RequestType,
                    input.ReasonForLeaving,
                    input.LeavingDate,
                    input.RequestReason,
                    input.MinAge,
                    input.MaxAge,
                    input.Gender,
                    input.OtherQualifications,
                    input.Location,
                    input.ExperienceStatus,
                    input.SuggestedPersonelName ?? string.Empty,
                    input.SuggestedJobTitle ?? string.Empty,
                    input.ReasonForSuggestion ?? string.Empty
                );
            }
            else if (input.RequestType == RequestType.NewPosition_InPlan)
            {
                request = await _personelRequestManager.CreateForNewPositionInPlanAsync(
                    input.RequesterName,
                    input.RequesterTitle,
                    input.ManagerId,
                    input.JobTitle,
                    input.DepartmentId,
                    input.NumberOfPersonel,
                    input.RequestDate,
                    input.RequestReason ?? string.Empty,
                    input.MinAge,
                    input.MaxAge,
                    input.Gender,
                    input.OtherQualifications,
                    input.Location,
                    input.ExperienceStatus,
                    input.ReasonForNewPosition ?? string.Empty,
                    input.SuggestedPersonelName ?? string.Empty,
                    input.SuggestedJobTitle ?? string.Empty,
                    input.ReasonForSuggestion ?? string.Empty
                );
            }
            else // RequestType.NewPosition_OutPlan
            {
                request = await _personelRequestManager.CreateForNewPositionOutOfPlanAsync(
                    input.RequesterName,
                    input.RequesterTitle,
                    input.ManagerId,
                    input.JobTitle,
                    input.DepartmentId,
                    input.NumberOfPersonel,
                    input.RequestDate,
                    input.RequestReason ?? string.Empty,
                    input.MinAge,
                    input.MaxAge,
                    input.Gender,
                    input.OtherQualifications,
                    input.Location,
                    input.ExperienceStatus,
                    input.ReasonForNewPosition ?? string.Empty,
                    input.SuggestedPersonelName ?? string.Empty,
                    input.SuggestedJobTitle ?? string.Empty,
                    input.ReasonForSuggestion ?? string.Empty
                );
            }

            await Repository.InsertAsync(request, autoSave: true);

            return ObjectMapper.Map<PersonelRequest, PersonelRequestDto>(request);
        }

        //
        // Listeleme işlemleri (filtreli)
        //
        public override async Task<PagedResultDto<PersonelRequestDto>> GetListAsync(PersonelRequestInputListDto input)
        {
            var queryable = await Repository.GetQueryableAsync();

            var filtered = queryable
                .WhereIf(
                    !string.IsNullOrWhiteSpace(input.Filter),
                    x => x.JobTitle.Contains(input.Filter) ||
                         x.Location.Contains(input.Filter) ||
                         x.RequesterName.Contains(input.Filter)
                )
                .WhereIf(
                    input.RequestType.HasValue,
                    x => x.RequestType == input.RequestType.Value
                )
                .WhereIf(
                    input.RequestStatus.HasValue,
                    x => x.RequestStatus == input.RequestStatus.Value
                )
                .WhereIf(
                    input.DepartmentId.HasValue,
                    x => x.DepartmentId == input.DepartmentId.Value
                );

            var totalCount = await AsyncExecuter.CountAsync(filtered);

            var items = await AsyncExecuter.ToListAsync(
                filtered
                    .OrderBy(NormalizeSorting(input.Sorting))
                    .PageBy(input)
            );

            return new PagedResultDto<PersonelRequestDto>(
                totalCount,
                ObjectMapper.Map<List<PersonelRequest>, List<PersonelRequestDto>>(items)
            );
        }

        private string NormalizeSorting(string? sorting)
        {
            if (sorting.IsNullOrWhiteSpace())
            {
                return nameof(PersonelRequest.RequestDate);
            }

            return sorting switch
            {
                "RequesterName" => nameof(PersonelRequest.RequesterName),
                "JobTitle" => nameof(PersonelRequest.JobTitle),
                "Location" => nameof(PersonelRequest.Location),
                _ => sorting
            };
        }
    }
}
