using System.Web.Mvc;
using Unity;
using Unity.Mvc5;
using University.DAL;
using University.Infrastructure;
using University.Repositories;

namespace University
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers

            // e.g. container.RegisterType<ITestService, TestService>();
            container.RegisterSingleton<SchoolContext>();
            container.RegisterType<IStudentRepository, StudentRepository>();
            container.RegisterType<ICourseRepository, CourseRepository>();
            container.RegisterType<IInstructorRepository, InstructorRepository>();
            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}