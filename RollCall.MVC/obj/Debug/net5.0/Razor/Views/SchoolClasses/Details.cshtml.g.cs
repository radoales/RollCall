#pragma checksum "C:\Programming\SDL\RollCall\RollCall.MVC\Views\SchoolClasses\Details.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "ef0e1f02873b79eb15588c6b9dae050209a3ef51"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_SchoolClasses_Details), @"mvc.1.0.view", @"/Views/SchoolClasses/Details.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"ef0e1f02873b79eb15588c6b9dae050209a3ef51", @"/Views/SchoolClasses/Details.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"501c5d1622a7a6048273834a5290681d4b4e6adc", @"/Views/_ViewImports.cshtml")]
    public class Views_SchoolClasses_Details : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<DetailsSchoolClassVM>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("name", "_CheckInPartial", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        #line hidden
        #pragma warning disable 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        #pragma warning restore 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __backed__tagHelperScopeManager = null;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __tagHelperScopeManager
        {
            get
            {
                if (__backed__tagHelperScopeManager == null)
                {
                    __backed__tagHelperScopeManager = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager(StartTagHelperWritingScope, EndTagHelperWritingScope);
                }
                return __backed__tagHelperScopeManager;
            }
        }
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.PartialTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
#nullable restore
#line 3 "C:\Programming\SDL\RollCall\RollCall.MVC\Views\SchoolClasses\Details.cshtml"
  
    ViewData["Title"] = "Details";
    Layout = "~/Views/Shared/_Layout.cshtml";

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n<div>\r\n    <div class=\"text-center\">\r\n        <h2>");
#nullable restore
#line 10 "C:\Programming\SDL\RollCall\RollCall.MVC\Views\SchoolClasses\Details.cshtml"
       Write(Model.Subject.Name);

#line default
#line hidden
#nullable disable
            WriteLiteral("</h2>\r\n        <h5>");
#nullable restore
#line 11 "C:\Programming\SDL\RollCall\RollCall.MVC\Views\SchoolClasses\Details.cshtml"
       Write(Model.Date);

#line default
#line hidden
#nullable disable
            WriteLiteral("</h5>\r\n        <h6>");
#nullable restore
#line 12 "C:\Programming\SDL\RollCall\RollCall.MVC\Views\SchoolClasses\Details.cshtml"
       Write(Model.Time);

#line default
#line hidden
#nullable disable
            WriteLiteral("</h6>\r\n");
            WriteLiteral("            <a class=\"btn btn-success\"");
            BeginWriteAttribute("onclick", " onclick=\"", 378, "\"", 411, 3);
            WriteAttributeValue("", 388, "generateCode(", 388, 13, true);
#nullable restore
#line 15 "C:\Programming\SDL\RollCall\RollCall.MVC\Views\SchoolClasses\Details.cshtml"
WriteAttributeValue("", 401, Model.Id, 401, 9, false);

#line default
#line hidden
#nullable disable
            WriteAttributeValue("", 410, ")", 410, 1, true);
            EndWriteAttribute();
            WriteLiteral("> Genarete Room Code</a>\r\n        \r\n        <h2>");
#nullable restore
#line 17 "C:\Programming\SDL\RollCall\RollCall.MVC\Views\SchoolClasses\Details.cshtml"
       Write(Model.Code);

#line default
#line hidden
#nullable disable
            WriteLiteral("</h2>\r\n");
            WriteLiteral("        <div id=\"timeLeft\"></div>\r\n    </div>\r\n    <p id=\"time\" hidden>");
#nullable restore
#line 21 "C:\Programming\SDL\RollCall\RollCall.MVC\Views\SchoolClasses\Details.cshtml"
                   Write(Model.CodeGeneratedTime);

#line default
#line hidden
#nullable disable
            WriteLiteral(@"</p>
    <div>
        <hr />
        <table class=""table"">
            <thead>
                <tr style=""font-weight:bold"">
                    <td>Name</td>
                    <td>Check In 1</td>
                    <td>Check In 2</td>
                    <td>Check In 3</td>
                    <td>Attendance %</td>
                </tr>
            </thead>
            <tbody>
");
#nullable restore
#line 35 "C:\Programming\SDL\RollCall\RollCall.MVC\Views\SchoolClasses\Details.cshtml"
                 foreach (var attendance in Model.Attendances)
                {

#line default
#line hidden
#nullable disable
            WriteLiteral("                    <tr>\r\n                        <td>");
#nullable restore
#line 38 "C:\Programming\SDL\RollCall\RollCall.MVC\Views\SchoolClasses\Details.cshtml"
                       Write(attendance.User.FirstName);

#line default
#line hidden
#nullable disable
            WriteLiteral(" ");
#nullable restore
#line 38 "C:\Programming\SDL\RollCall\RollCall.MVC\Views\SchoolClasses\Details.cshtml"
                                                  Write(attendance.User.LastName);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                        <td>\r\n                            ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("partial", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "ef0e1f02873b79eb15588c6b9dae050209a3ef517332", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.PartialTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper.Name = (string)__tagHelperAttribute_0.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
#nullable restore
#line 40 "C:\Programming\SDL\RollCall\RollCall.MVC\Views\SchoolClasses\Details.cshtml"
__Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper.Model = attendance.CheckIn_Start;

#line default
#line hidden
#nullable disable
            __tagHelperExecutionContext.AddTagHelperAttribute("model", __Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper.Model, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n                        </td>\r\n                        <td>\r\n                            ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("partial", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "ef0e1f02873b79eb15588c6b9dae050209a3ef519007", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.PartialTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper.Name = (string)__tagHelperAttribute_0.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
#nullable restore
#line 43 "C:\Programming\SDL\RollCall\RollCall.MVC\Views\SchoolClasses\Details.cshtml"
__Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper.Model = attendance.CheckIn_Middle;

#line default
#line hidden
#nullable disable
            __tagHelperExecutionContext.AddTagHelperAttribute("model", __Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper.Model, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n                        </td>\r\n                        <td>\r\n                            ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("partial", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "ef0e1f02873b79eb15588c6b9dae050209a3ef5110683", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.PartialTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper.Name = (string)__tagHelperAttribute_0.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
#nullable restore
#line 46 "C:\Programming\SDL\RollCall\RollCall.MVC\Views\SchoolClasses\Details.cshtml"
__Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper.Model = attendance.CheckIn_End;

#line default
#line hidden
#nullable disable
            __tagHelperExecutionContext.AddTagHelperAttribute("model", __Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper.Model, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n                        </td>\r\n                        <td style=\"font-weight:bold\">66%</td>\r\n                    </tr>\r\n");
#nullable restore
#line 50 "C:\Programming\SDL\RollCall\RollCall.MVC\Views\SchoolClasses\Details.cshtml"
                }

#line default
#line hidden
#nullable disable
            WriteLiteral(@"            </tbody>
        </table>
    </div>
</div>

<script type=""text/javascript"">
function generateCode(id) {
    $.ajax({
        type: 'GET',
        url: ""/schoolclasses/GenerateCode/"",
        data: { id : id },
        success: function (result) {
            $(""body"").html(result);
        }
    });
    }

    var x = document.getElementById(""time"").innerHTML
    var countDownDate = new Date(x).getTime();
    console.log(countDownDate)
    // Update the count down every 1 second
    var x = setInterval(function () {

        // Get today's date and time
        var now = new Date().getTime();

        // Find the distance between now and the count down date
        var distance = countDownDate - now;

        // Time calculations for days, hours, minutes and seconds
        var minutes = Math.floor((distance % (1000 * 60 * 60)) / (1000 * 60));
        var seconds = Math.floor((distance % (1000 * 60)) / 1000);

        // Output the result in an element with id=""de");
            WriteLiteral(@"mo""
        document.getElementById(""timeLeft"").innerHTML = minutes + ""m "" + seconds + ""s "";

        // If the count down is over, write some text 
        //if (distance < 0) {
        //    clearInterval(x);
        //    document.getElementById(""demo"").innerHTML = ""EXPIRED"";
        //}
    }, 1000);

</script>");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<DetailsSchoolClassVM> Html { get; private set; }
    }
}
#pragma warning restore 1591
