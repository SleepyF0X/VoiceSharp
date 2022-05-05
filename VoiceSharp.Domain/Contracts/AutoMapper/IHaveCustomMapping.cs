using AutoMapper;

namespace VoiceSharp.Domain.Contracts.AutoMapper
{
    public interface IHaveCustomMapping
    {
        void CreateMappings(Profile configuration);
    }
}
