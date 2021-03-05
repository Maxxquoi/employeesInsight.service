using System;
using System.Linq;
using AutoMapper;
using employeesInsight.webapi.v1.ViewModels.Member.Mappings;

namespace employeesInsight.webapi.unit.tests.v1.Controllers
{
    public static class EmployeesMapperProvider
    {
        public static IMapper CreateAutoMapper()
        {
            var profiles = from t in typeof(CreateEmployeeProfile).Assembly.GetTypes()
                where typeof(Profile).IsAssignableFrom(t)
                select (Profile) Activator.CreateInstance(t);

            var configuration = new MapperConfiguration(config =>
            {
                foreach (var profile in profiles)
                {
                    config.AddProfile(profile);
                }
            });

            return configuration.CreateMapper();
        }
    }
}