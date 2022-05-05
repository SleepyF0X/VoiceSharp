using System.Reflection;
using AutoMapper;

namespace VoiceSharp.Domain.Contracts.AutoMapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile(Assembly assembly)
        {
            LoadStandardMappings(assembly);
            LoadCustomMappings(assembly);
            LoadConverters(assembly);
        }

#pragma warning disable CC0091
        private void LoadConverters(Assembly assembly)
        {
        }
#pragma warning restore CC0091

        private void LoadStandardMappings(Assembly assembly)
        {
            var maps = MapperProfileHelper.LoadStandardMappings(assembly);
            foreach (var map in maps)
            {
                CreateMap(map.Source, map.Destination).ReverseMap();
            }
        }

        private void LoadCustomMappings(Assembly assembly)
        {
            var customMappings = MapperProfileHelper.LoadCustomMappings(assembly);
            foreach (var map in customMappings)
            {
                map.CreateMappings(this);
            }
        }
    }
}