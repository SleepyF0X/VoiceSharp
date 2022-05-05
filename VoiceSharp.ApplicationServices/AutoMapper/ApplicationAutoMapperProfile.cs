using System.Reflection;
using VoiceSharp.Domain.Contracts.AutoMapper;

namespace VoiceSharp.ApplicationServices.AutoMapper
{
    public class ApplicationAutoMapperProfile : AutoMapperProfile
    {
        public ApplicationAutoMapperProfile()
            : base(Assembly.GetExecutingAssembly())
        {
        }
    }
}