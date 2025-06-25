using AutoMapper;
using MI.Service.TestEngine.Business.Models;
using MI.Service.TestEngine.Domain.Entities;

namespace MI.Service.TestEngine.Infrastructure.AutoMapper;

/// <summary>
/// Mapper profile for application.
/// </summary>
public class ApplicationProfile : Profile
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ApplicationProfile"/> class.
    /// </summary>
    public ApplicationProfile()
    {
        CreateMap<Application, NameModel>()
            .ForMember(x => x.Name, o => o.MapFrom(x => x.Name));

        CreateMap<Application, ExternalReferenceModel>()
            .ForMember(x => x.SystemName, o => o.MapFrom(x => x.SystemName))
            .ForMember(x => x.DisplayName, o => o.MapFrom(x => x.Name));
    }
}