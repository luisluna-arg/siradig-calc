using AutoMapper;

namespace SiradigCalc.Application.Mappings.Profiles.Base;

public abstract class BaseProfile<TSource, TTarget> : Profile {
    
    public BaseProfile()
    {
        CreateMap<TSource, TTarget>();
    }
}