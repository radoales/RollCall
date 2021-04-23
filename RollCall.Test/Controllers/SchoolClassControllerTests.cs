namespace RollCall.Test.Controllers
{
    using FluentAssertions;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using RollCall.MVC.Controllers;
    using System.Linq;
    using System.Reflection;
    using Xunit;
    using static RollCall.MVC.WebConstants;
    public class SchoolClassControllerTests
    {
        //Accessability Tests
        [Fact]
        public void CreateSchoolClassGet_ShouldBeAccessableOnly_ByAdministrators()
        {
            //Arrange
            var method = GetGetMethodInfo("Create");

            // Act
            var atributes = method.GetCustomAttributes(true);
            var result = atributes
                 .Where(x => x.GetType() == typeof(AuthorizeAttribute))
                .SelectMany(attrib => ((AuthorizeAttribute)attrib)
                .Roles.Split(new[] { ',' }))
                .ToList();

            //Assert
            result
                .Should().Contain(Roles.AdminRole)
                .And
                .NotContain(Roles.StudentRole)
                .And
                .NotContain(Roles.TeacherRole);
        }

        [Fact]
        public void CreateSchoolClassPost_ShouldBeAccessableOnly_ByAdministrators()
        {
            //Arrange
            var method = GetPostMethodInfo("Create");

            // Act
            var atributes = method.GetCustomAttributes(true);
            var result = atributes
                 .Where(x => x.GetType() == typeof(AuthorizeAttribute))
                .SelectMany(attrib => ((AuthorizeAttribute)attrib)
                .Roles.Split(new[] { ',' }))
                .ToList();

            //Assert
            result
                .Should().Contain(Roles.AdminRole)
                .And
                .NotContain(Roles.StudentRole)
                .And
                .NotContain(Roles.TeacherRole);
        }

        //Helpers
        private MethodInfo GetGetMethodInfo(string methodName)
        {
            return typeof(SchoolClassesController)
                .GetMethods()
                .Where(x => x.CustomAttributes
                .Any(y => y.AttributeType == typeof(HttpGetAttribute)))
                .ToList()
            .FirstOrDefault(x => x.Name == methodName);
        }

        private MethodInfo GetPostMethodInfo(string methodName)
        {
            return typeof(SchoolClassesController)
                .GetMethods()
                .Where(x => x.CustomAttributes
                .Any(y => y.AttributeType == typeof(HttpPostAttribute)))
                .ToList()
            .FirstOrDefault(x => x.Name == methodName);
        }
    }
}
