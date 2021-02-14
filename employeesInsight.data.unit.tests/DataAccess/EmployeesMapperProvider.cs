using System;
using System.Linq;
using AutoMapper;
using employeesInsight.data.Dtos.Member.Mappings;

namespace employeesInsight.data.unit.tests.DataAccess
{
    public static class EmployeesMapperProvider
    {
        public static IMapper CreateAutoMapper()
        {
            var profiles = from t in typeof(EmployeeProfile).Assembly.GetTypes()
                           where typeof(Profile).IsAssignableFrom(t)
                           select (Profile)Activator.CreateInstance(t);

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