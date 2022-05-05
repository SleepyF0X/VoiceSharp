using System.Reflection;

namespace VoiceSharp.Domain.Contracts.AutoMapper
{
    public static class MapperProfileHelper
    {
        public static IList<Map> LoadStandardMappings(Assembly rootAssembly)
        {
            var maps = new List<Map>();
            foreach (var type in rootAssembly.GetExportedTypes().Where(t => !t.IsAbstract && !t.IsInterface))
            {
                foreach (var instance in type.GetInterfaces().Where(i => i.IsGenericType))
                {
                    var hasMapFrom = instance.GetGenericTypeDefinition() == typeof(IMapFrom<>);
                    var hasMapTo = instance.GetGenericTypeDefinition() == typeof(IMapTo<>);
                    if (!(hasMapFrom || hasMapTo))
                    {
                        continue;
                    }

                    Type source;
                    Type destination;
                    if (hasMapFrom)
                    {
                        source = instance.GetGenericArguments().First();
                        destination = type;
                    }
                    else
                    {
                        source = type;
                        destination = instance.GetGenericArguments().Last();
                    }

                    maps.Add(new Map
                    {
                        Source = source,
                        Destination = destination,
                    });
                }
            }

            return maps;
        }

        public static IList<IHaveCustomMapping> LoadCustomMappings(Assembly rootAssembly)
        {
            var maps = new List<IHaveCustomMapping>();

            foreach (var type in rootAssembly.GetExportedTypes()
                .Where(t => typeof(IHaveCustomMapping).IsAssignableFrom(t) && !t.IsAbstract && !t.IsInterface))
            {
                try
                {
                    maps.Add((IHaveCustomMapping)Activator.CreateInstance(type));
                }
                catch (MissingMethodException mme)
                {
                    throw new MissingMethodException(type.FullName, mme);
                }
            }

            return maps;
        }
    }

    public sealed class Map
    {
        public Type Source { get; set; }
        public Type Destination { get; set; }
    }
}
