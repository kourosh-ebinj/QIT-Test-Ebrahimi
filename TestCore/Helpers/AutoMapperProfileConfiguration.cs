using AutoMapper;
using TestDAL.Core.Domains;
using Common.Dto;
using Common.Models;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace TestAPI.Helpers
{
    public class AutoMapperProfileConfiguration : Profile
    {
        public AutoMapperProfileConfiguration() : this("CustomProfile") { }
        protected AutoMapperProfileConfiguration(string profileName) : base(profileName)
        {
            //// DTOs mapper
            CreateMap<Class, ClassDto>();
            CreateMap<ClassModel, Class>();

            CreateMap<Student, StudentDto>();
            CreateMap<StudentModel, Student>();

        }
    }
}
