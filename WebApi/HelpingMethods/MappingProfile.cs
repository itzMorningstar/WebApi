using AutoMapper;
using Entities.EmployeesEntities;
using Entities.SchoolManagement;
using System;
using System.Runtime.CompilerServices;
using WebApi.Dto_s;
using WebApi.Models.SchoolManagment;

namespace WebApi.HelpingMethods
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Student, StudentDto>();
            CreateMap<Teacher, TeacherDto>();
            CreateMap<Enrollment, EnrollmentDto>();
            CreateMap<Classroom, ClassroomDto>();
            CreateMap<Section, SectionDto>();

            CreateMap<AddStudentModel, Student>()
                .ForMember(dest => dest.ProfilePicturePath, opt => opt.Ignore());

            CreateMap<Employee, EmployeeDto>()
                .ForMember(dest => dest.Sallery, opt => opt.MapFrom(src => src.Sallery.SalleryAmount))
                .ForMember(dest => dest.Department ,opt  => opt.MapFrom(src => src.Department.ToString()));
        }
    }
}
