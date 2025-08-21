using Altinay.Meeting.MeetingRoomDtos;
using Altinay.Meeting;
using Altinay.Personel;
using Altinay.Personel.Departments;
using Altinay.Personel.Managers;
using AutoMapper;
using Altinay.Meeting.CreateUpdateDtos;
using Altinay.Projects;
using Altinay.Files;
using Altinay.ProjectGroups;


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




    }
}
