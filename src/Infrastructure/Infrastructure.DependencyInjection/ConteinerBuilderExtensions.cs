using System;
using System.Linq;
using System.Reflection;
using Autofac;
using CleanArchitecture.Base.DependencyManagement;

namespace Infrastructure.DependencyInjection
{
    public static class ContainerBuilderExtenstions
    {
        public static void RegisterAssembliesDependencies(this ContainerBuilder builder,
            params Assembly[] assemblies)
        {
            foreach (var assembly in assemblies)
            {
                builder.RegisterAssemblyDependencies(assembly);
            }
        }

        public static void RegisterAssemblyDependencies(this ContainerBuilder builder, Assembly assembly)
        {
            var types = assembly.GetTypes()
                .Where(x => x.GetCustomAttribute<DependencyAttribute>() != null);

            foreach (var type in types)
            {
                var attribute = type.GetCustomAttribute<DependencyAttribute>();
                
                switch (attribute)
                {
                    case NamedDependencyAttribute namedDependencyAttribute:
                        RegisterNamed(namedDependencyAttribute, type);
                        break;
                    case DependencyAttribute dependencyAttribute:
                        RegisterNormal(dependencyAttribute, type);
                        break;
                }
            }


            void RegisterNormal(DependencyAttribute dependencyAttribute, Type type)
            {
                switch (dependencyAttribute.Lifetime)
                {
                    case DependencyLifetime.NEW_INSTANCE_PERDEPENDENCY:
                        builder.RegisterType(type).AsImplementedInterfaces().InstancePerDependency();
                        break;
                    case DependencyLifetime.NEW_PER_EXECUTION:
                        builder.RegisterType(type).AsImplementedInterfaces();
                        break;
                    case DependencyLifetime.SINGLETON:
                        builder.RegisterType(type).AsImplementedInterfaces().SingleInstance();
                        break;
                }
                
               
            }

            void RegisterNamed(NamedDependencyAttribute namedDependencyAttribute, Type type)
            {
                switch (namedDependencyAttribute.Lifetime)
                {
                    case DependencyLifetime.NEW_INSTANCE_PERDEPENDENCY:
                        builder.RegisterType(type).AsImplementedInterfaces().InstancePerDependency().Named(namedDependencyAttribute.Name, type);
                        break;
                    case DependencyLifetime.NEW_PER_EXECUTION:
                        builder.RegisterType(type).AsImplementedInterfaces().Named(namedDependencyAttribute.Name, type);
                        break;
                    case DependencyLifetime.SINGLETON:
                        builder.RegisterType(type).AsImplementedInterfaces().SingleInstance().Named(namedDependencyAttribute.Name, type);;
                        break;
                }
            }
        }
    }
}