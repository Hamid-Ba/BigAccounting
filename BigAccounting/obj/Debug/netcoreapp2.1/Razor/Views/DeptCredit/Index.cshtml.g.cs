#pragma checksum "C:\Users\Danesh\Documents\Visual Studio 2019\Projects\Asp Core Pro\BigAccounting\BigAccounting\Views\DeptCredit\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "abafcfe22994a851368b4543e087c8e8585ec804"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_DeptCredit_Index), @"mvc.1.0.view", @"/Views/DeptCredit/Index.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/DeptCredit/Index.cshtml", typeof(AspNetCore.Views_DeptCredit_Index))]
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
#line 1 "C:\Users\Danesh\Documents\Visual Studio 2019\Projects\Asp Core Pro\BigAccounting\BigAccounting\Views\_ViewImports.cshtml"
using BigAccounting;

#line default
#line hidden
#line 2 "C:\Users\Danesh\Documents\Visual Studio 2019\Projects\Asp Core Pro\BigAccounting\BigAccounting\Views\_ViewImports.cshtml"
using BigAccounting.Models;

#line default
#line hidden
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"abafcfe22994a851368b4543e087c8e8585ec804", @"/Views/DeptCredit/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"dc32455a1807c4c7226ff13f47b4c9fd73b1529e", @"/Views/_ViewImports.cshtml")]
    public class Views_DeptCredit_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<IEnumerable<DataLayer.Models.Member>>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "SingleDeptCredit", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        #line hidden
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
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
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#line 3 "C:\Users\Danesh\Documents\Visual Studio 2019\Projects\Asp Core Pro\BigAccounting\BigAccounting\Views\DeptCredit\Index.cshtml"
  
    ViewData["Title"] = "Index";
    Layout = "~/Views/Shared/_AdminLayout.cshtml";

#line default
#line hidden
            BeginContext(190, 34, true);
            WriteLiteral("\r\n<h2>طلبکاران و بدهکاران</h2>\r\n\r\n");
            EndContext();
#line 10 "C:\Users\Danesh\Documents\Visual Studio 2019\Projects\Asp Core Pro\BigAccounting\BigAccounting\Views\DeptCredit\Index.cshtml"
   int i = 1; 

#line default
#line hidden
            BeginContext(241, 356, true);
            WriteLiteral(@"<table class=""table table-striped"" style="" direction:rtl"">
    <thead>
        <tr>
            <th style=""color:cadetblue"">ردیف</th>
            <th style=""color:cadetblue"">نام</th>
            <th style=""color:cadetblue"">پولی که میخواد</th>
            <th style=""color:cadetblue"">پولی که باید بده</th>
        </tr>
    </thead>

    <tbody>
");
            EndContext();
#line 22 "C:\Users\Danesh\Documents\Visual Studio 2019\Projects\Asp Core Pro\BigAccounting\BigAccounting\Views\DeptCredit\Index.cshtml"
         foreach (var mem in Model)
        {

            var creditor = UW.BaseRepository<DataLayer.Models.Creditor>().GetEntityByID(mem.CreditorID);
            var deptor = UW.BaseRepository<DataLayer.Models.Deptor>().GetEntityByID(mem.DeptorID);


#line default
#line hidden
            BeginContext(855, 38, true);
            WriteLiteral("            <tr>\r\n                <td>");
            EndContext();
            BeginContext(894, 1, false);
#line 29 "C:\Users\Danesh\Documents\Visual Studio 2019\Projects\Asp Core Pro\BigAccounting\BigAccounting\Views\DeptCredit\Index.cshtml"
               Write(i);

#line default
#line hidden
            EndContext();
            BeginContext(895, 28, true);
            WriteLiteral("</td>\r\n                <td> ");
            EndContext();
            BeginContext(923, 83, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "96dc7c95d545480296fe7643bf6737a0", async() => {
                BeginContext(991, 1, true);
                WriteLiteral(" ");
                EndContext();
                BeginContext(993, 8, false);
#line 30 "C:\Users\Danesh\Documents\Visual Studio 2019\Projects\Asp Core Pro\BigAccounting\BigAccounting\Views\DeptCredit\Index.cshtml"
                                                                                     Write(mem.Name);

#line default
#line hidden
                EndContext();
                BeginContext(1001, 1, true);
                WriteLiteral(" ");
                EndContext();
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_0.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
            if (__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues == null)
            {
                throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-memberID", "Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper", "RouteValues"));
            }
            BeginWriteTagHelperAttribute();
#line 30 "C:\Users\Danesh\Documents\Visual Studio 2019\Projects\Asp Core Pro\BigAccounting\BigAccounting\Views\DeptCredit\Index.cshtml"
                                                              WriteLiteral(mem.MemberID);

#line default
#line hidden
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["memberID"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-memberID", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["memberID"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(1006, 28, true);
            WriteLiteral(" </td>\r\n                <td>");
            EndContext();
            BeginContext(1035, 17, false);
#line 31 "C:\Users\Danesh\Documents\Visual Studio 2019\Projects\Asp Core Pro\BigAccounting\BigAccounting\Views\DeptCredit\Index.cshtml"
               Write(creditor.GetMoney);

#line default
#line hidden
            EndContext();
            BeginContext(1052, 33, true);
            WriteLiteral(" تومان</td>\r\n                <td>");
            EndContext();
            BeginContext(1086, 16, false);
#line 32 "C:\Users\Danesh\Documents\Visual Studio 2019\Projects\Asp Core Pro\BigAccounting\BigAccounting\Views\DeptCredit\Index.cshtml"
               Write(deptor.DeptMoney);

#line default
#line hidden
            EndContext();
            BeginContext(1102, 13, true);
            WriteLiteral(" تومان</td>\r\n");
            EndContext();
#line 33 "C:\Users\Danesh\Documents\Visual Studio 2019\Projects\Asp Core Pro\BigAccounting\BigAccounting\Views\DeptCredit\Index.cshtml"
                  i++;

#line default
#line hidden
            BeginContext(1140, 19, true);
            WriteLiteral("            </tr>\r\n");
            EndContext();
#line 35 "C:\Users\Danesh\Documents\Visual Studio 2019\Projects\Asp Core Pro\BigAccounting\BigAccounting\Views\DeptCredit\Index.cshtml"
        }

#line default
#line hidden
            BeginContext(1170, 30, true);
            WriteLiteral("\r\n    </tbody>\r\n\r\n</table>\r\n\r\n");
            EndContext();
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public DataLayer.Models.UnitOfWork.UnitOfWork UW { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<IEnumerable<DataLayer.Models.Member>> Html { get; private set; }
    }
}
#pragma warning restore 1591