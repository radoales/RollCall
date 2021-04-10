#pragma checksum "C:\Programming\SDL\RollCall\RollCall.MVC\Views\Shared\_AddUserToSubjectPartial.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "36dc75beb1668a64db948fa502724f584f19e359"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Shared__AddUserToSubjectPartial), @"mvc.1.0.view", @"/Views/Shared/_AddUserToSubjectPartial.cshtml")]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#nullable restore
#line 1 "C:\Programming\SDL\RollCall\RollCall.MVC\Views\_ViewImports.cshtml"
using RollCall.MVC;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Programming\SDL\RollCall\RollCall.MVC\Views\_ViewImports.cshtml"
using RollCall.MVC.Data.Models;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "C:\Programming\SDL\RollCall\RollCall.MVC\Views\_ViewImports.cshtml"
using RollCall.MVC.ViewModels.Identity;

#line default
#line hidden
#nullable disable
#nullable restore
#line 4 "C:\Programming\SDL\RollCall\RollCall.MVC\Views\_ViewImports.cshtml"
using RollCall.MVC.ViewModels.SchoolClass;

#line default
#line hidden
#nullable disable
#nullable restore
#line 5 "C:\Programming\SDL\RollCall\RollCall.MVC\Views\_ViewImports.cshtml"
using RollCall.MVC.ViewModels.Subjects;

#line default
#line hidden
#nullable disable
#nullable restore
#line 6 "C:\Programming\SDL\RollCall\RollCall.MVC\Views\_ViewImports.cshtml"
using RollCall.MVC.ViewModels.Users;

#line default
#line hidden
#nullable disable
#nullable restore
#line 7 "C:\Programming\SDL\RollCall\RollCall.MVC\Views\_ViewImports.cshtml"
using RollCall.MVC.Helpers;

#line default
#line hidden
#nullable disable
#nullable restore
#line 8 "C:\Programming\SDL\RollCall\RollCall.MVC\Views\_ViewImports.cshtml"
using static RollCall.MVC.WebConstants.Roles;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"36dc75beb1668a64db948fa502724f584f19e359", @"/Views/Shared/_AddUserToSubjectPartial.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"4129ce4d84aafe438469a1293ed6145a7dee1812", @"/Views/_ViewImports.cshtml")]
    public class Views_Shared__AddUserToSubjectPartial : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<IEnumerable<AddUsersToSubjectVM>>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("<div id=\"user-subject-partial\">\r\n\r\n    <table class=\"table\">\r\n        <thead>\r\n            <tr>\r\n                <th>Student Number</th>\r\n                <th>Name</th>\r\n                <th></th>\r\n            </tr>\r\n        </thead>\r\n        <tbody>\r\n\r\n");
#nullable restore
#line 14 "C:\Programming\SDL\RollCall\RollCall.MVC\Views\Shared\_AddUserToSubjectPartial.cshtml"
             foreach (var item in Model)
            {

#line default
#line hidden
#nullable disable
            WriteLiteral("                <tr>\r\n                    <td>");
#nullable restore
#line 17 "C:\Programming\SDL\RollCall\RollCall.MVC\Views\Shared\_AddUserToSubjectPartial.cshtml"
                   Write(item.User.StudentNumber);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                    <td>");
#nullable restore
#line 18 "C:\Programming\SDL\RollCall\RollCall.MVC\Views\Shared\_AddUserToSubjectPartial.cshtml"
                   Write(item.User.FirstName);

#line default
#line hidden
#nullable disable
            WriteLiteral(" ");
#nullable restore
#line 18 "C:\Programming\SDL\RollCall\RollCall.MVC\Views\Shared\_AddUserToSubjectPartial.cshtml"
                                        Write(item.User.LastName);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n\r\n                    <td>\r\n                        <button id=\"Add\" type=\"button\"  class=\"btn btn-success\" onclick=\"Add(this)\" data-id=\"");
#nullable restore
#line 21 "C:\Programming\SDL\RollCall\RollCall.MVC\Views\Shared\_AddUserToSubjectPartial.cshtml"
                                                                                                        Write(item.User.Id);

#line default
#line hidden
#nullable disable
            WriteLiteral("\">Add</button>\r\n                    </td>\r\n\r\n                </tr>\r\n");
#nullable restore
#line 25 "C:\Programming\SDL\RollCall\RollCall.MVC\Views\Shared\_AddUserToSubjectPartial.cshtml"
            }

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </tbody>\r\n    </table>\r\n</div>\r\n\r\n<script type=\"text/javascript\">\r\n    \r\n\r\n\r\n</script>");
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<IEnumerable<AddUsersToSubjectVM>> Html { get; private set; }
    }
}
#pragma warning restore 1591
