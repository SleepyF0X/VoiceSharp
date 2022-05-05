using System.Reflection;
using VoiceSharp.Domain.Contracts.AutoMapper;

namespace VoiceSharp.API.AutoMapper
{
    public class WebUiAutoMapperProfile : AutoMapperProfile
    {
        public WebUiAutoMapperProfile()
            : base(Assembly.GetExecutingAssembly())
        {
        }
    }
}