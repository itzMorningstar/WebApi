using AutoMapper;
using Entities.SchoolManagement;
using System;
using System.Runtime.CompilerServices;
using WebApi.Dto_s;

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
        }
    }
}
