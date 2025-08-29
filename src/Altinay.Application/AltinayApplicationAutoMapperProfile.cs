using Altinay.Files;
using Altinay.Meeting;
using Altinay.Meeting.CreateUpdateDtos;
using Altinay.Meeting.MeetingRoomDtos;
using Altinay.Personel;
using Altinay.Personel.Departments;
using Altinay.Personel.Managers;
using Altinay.ProjectGroups;
using Altinay.Projects;
using AutoMapper;
using System.Linq;
using Volo.Abp.Identity;


namespace Altinay;

public class AltinayApplicationAutoMapperProfile : Profile
{
    public AltinayApplicationAutoMapperProfile()
    {
        CreateMap<PersonelRequest, PersonelRequestDto>();
        CreateMap<CreateUpdatePersonelRequestDto, PersonelRequest>();
        CreateMap<PersonelRequest, CreateUpdatePersonelRequestDto>();
        CreateMap<Manager, LookupDto>();
        CreateMap<Department, LookupDto>();

        CreateMap<Room, RoomDto>();
        CreateMap<CreateUpdateRoomDto, Room>();
        CreateMap<Room, CreateUpdateRoomDto>();

        CreateMap<Floor, FloorDto>();
        CreateMap<CreateUpdateFloorDto, Floor>();
        CreateMap<Floor, CreateUpdateFloorDto>();
        
        CreateMap<Floor, BookingDto>();
        CreateMap<CreateUpdateBookingDto, Floor>();
        CreateMap<Floor, CreateUpdateBookingDto>();

        CreateMap<Project, ProjectDto>();
        CreateMap<CreateUpdateProjectDto, Project>();
        CreateMap<Project, CreateUpdateProjectDto>();
        CreateMap<ProjectDto, CreateUpdateProjectDto>();
      
        CreateMap<CreateUpdateProjectDto, ProjectDto>();
        CreateMap<File, FileDto>();
        CreateMap<CreateUpdateFileDto, File>();
        CreateMap<File, CreateUpdateFileDto>();
        CreateMap<File, FileDto>();

        CreateMap<ProjectGroup, ProjectGroupDto>();
        CreateMap<ProjectGroupDto, ProjectGroup>();
        CreateMap<CreateUpdateProjectGroupDto, ProjectGroup>();
        CreateMap<ProjectGroup, CreateUpdateProjectGroupDto>();
        CreateMap<ProjectGroupDto, CreateUpdateProjectGroupDto>();
        CreateMap<CreateUpdateProjectGroupDto, ProjectGroupDto>();

        CreateMap<ProjectGroup, ProjectGroupDto>()
            .ForMember(dest => dest.Users, opt => opt.MapFrom(src =>
                src.Users.Select(u => new IdentityUserDto
                {
                    Id = u.IdentityUserId
                    // You may need to map more properties if available
                }).ToList()
            ));




    }
}
