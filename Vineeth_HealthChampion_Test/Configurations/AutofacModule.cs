using Autofac;
using Autofac.Extras.DynamicProxy;
using AutoMapper;
using EmployeeManagement.Abstraction.Behaviours;
using EmployeeManagement.Business.Behaviours;
using EmployeeManagement.Data;
using EmployeeManagement.Data.EF;
using EmployeeManagement.Data.EF.Models;
using EmployeeManagement.Data.Interfaces;
using EmployeeManagement.Data.Repositories;
using EmployeeManagement.Service.Interceptors;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.Service.Configurations
{
    public class AutofacModule : Module
    {
        private IConfiguration configuration;

        /// <summary>
        /// Initializes a new instance of the <see cref="AutofacModule"/> class.
        /// </summary>
        /// <param name="configuration">Configuration.</param>
        public AutofacModule(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        /// <inheritdoc/>
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterInstance(configuration).As<IConfiguration>();
            ConfigureAutoMapper(builder);
            builder.RegisterType<EmployeeDirector>()
               .As<IEmployeeDirector>()
               .EnableClassInterceptors()
               .InterceptedBy(typeof(ServiceActionLogger));

            builder.RegisterType<DepartmentDirector>()
               .As<IDepartmentDirector>()
               .EnableClassInterceptors()
               .InterceptedBy(typeof(ServiceActionLogger));

            builder.RegisterType<HttpContextAccessor>()
                .As<IHttpContextAccessor>()
                .EnableClassInterceptors()
                .InterceptedBy(typeof(ServiceActionLogger));


            builder.RegisterType<UnitOfWork>()
                .As<IUnitOfWork>()
                .WithParameter(
                    (pi, ctx) => pi.ParameterType == typeof(IGenericRepository<Employee>),
                    (pi, ctx) => new GenericRepository<Employee>(ctx.Resolve<EmployeeContext>()))
                .WithParameter(
                    (pi, ctx) => pi.ParameterType == typeof(IGenericRepository<Department>),
                    (pi, ctx) => new GenericRepository<Department>(ctx.Resolve<EmployeeContext>()))
                .EnableClassInterceptors()
                .InterceptedBy(typeof(ServiceActionLogger))
                .InstancePerLifetimeScope();

            builder.Register(c => new ServiceActionLogger());
        }

        private static void ConfigureAutoMapper(ContainerBuilder builder)
        {
            builder.Register(c =>
            {
                var mappingConfig = new MapperConfiguration(mc =>
                {
                    mc.AddProfile(new MappingProfile());
                });

                return mappingConfig.CreateMapper();
            })
                .SingleInstance()
                .As<IMapper>();
        }
    }
}
