using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using System.Web.Mvc.Html;
using System.Web.Routing;
using System.Web.UI.WebControls;
using AutoMapper;
using SSM.Models;
using SSM.Services;
using Expression = System.Linq.Expressions.Expression;

namespace SSM.Common
{

    public static class ModelExtensions
    {
        public static string GetNumberFromStr(this string str)
        {
            str = str.Trim();
            Match m = Regex.Match(str, @"\d+");
            return (m.Value);
        }
        public static string RenderPartialView(this Controller controller, string viewName, object model)
        {
            if (string.IsNullOrEmpty(viewName))
                viewName = controller.ControllerContext.RouteData.GetRequiredString("action");

            controller.ViewData.Model = model;
            using (var sw = new StringWriter())
            {
                ViewEngineResult viewResult = ViewEngines.Engines.FindPartialView(controller.ControllerContext, viewName);
                var viewContext = new ViewContext(controller.ControllerContext, viewResult.View, controller.ViewData, controller.TempData, sw);
                viewResult.View.Render(viewContext, sw);

                return sw.GetStringBuilder().ToString();
            }
        }
        #region by DetailModel
        private const string idsToReuseKey = "__htmlPrefixScopeExtensions_IdsToReuse_";

        public static IDisposable BeginCollectionItem(this HtmlHelper html, string collectionName)
        {
            var idsToReuse = GetIdsToReuse(html.ViewContext.HttpContext, collectionName);
            string itemIndex = idsToReuse.Count > 0 ? idsToReuse.Dequeue() : Guid.NewGuid().ToString();

            // autocomplete="off" is needed to work around a very annoying Chrome behaviour whereby it reuses old values after the user clicks "Back", which causes the xyz.index and xyz[...] values to get out of sync.
            html.ViewContext.Writer.WriteLine(string.Format("<input type=\"hidden\" name=\"{0}.Index\" autocomplete=\"off\" value=\"{1}\" />", collectionName, html.Encode(itemIndex)));

            return BeginHtmlFieldPrefixScope(html, string.Format("{0}[{1}]", collectionName, itemIndex));
        }

        public static IDisposable BeginHtmlFieldPrefixScope(this HtmlHelper html, string htmlFieldPrefix)
        {
            return new HtmlFieldPrefixScope(html.ViewData.TemplateInfo, htmlFieldPrefix);
        }

        private static Queue<string> GetIdsToReuse(HttpContextBase httpContext, string collectionName)
        {
            // We need to use the same sequence of IDs following a server-side validation failure,  
            // otherwise the framework won't render the validation error messages next to each item.
            string key = idsToReuseKey + collectionName;
            var queue = (Queue<string>)httpContext.Items[key];
            if (queue == null)
            {
                httpContext.Items[key] = queue = new Queue<string>();
                var previouslyUsedIds = httpContext.Request[collectionName + ".index"];
                if (!string.IsNullOrEmpty(previouslyUsedIds))
                    foreach (string previouslyUsedId in previouslyUsedIds.Split(','))
                        queue.Enqueue(previouslyUsedId);
            }
            return queue;
        }

        private class HtmlFieldPrefixScope : IDisposable
        {
            private readonly TemplateInfo templateInfo;
            private readonly string previousHtmlFieldPrefix;

            public HtmlFieldPrefixScope(TemplateInfo templateInfo, string htmlFieldPrefix)
            {
                this.templateInfo = templateInfo;
                previousHtmlFieldPrefix = templateInfo.HtmlFieldPrefix;
                templateInfo.HtmlFieldPrefix = htmlFieldPrefix;
            }

            public void Dispose()
            {
                templateInfo.HtmlFieldPrefix = previousHtmlFieldPrefix;
            }
        }
        #endregion

        public static string StockButtonAction(User user, VoucherStatus status, long id, User owner)
        {
            /* VoucherStatus status = VoucherStatus.Hold;
             Enum.TryParse(statusInput, true, out status);*/
            string name = "Cancel";
            string nameCancel = "Cancel";
            string adStatus = "";
            const string bt =
                "<input type=\"button\" class=\"btn btn-primary \" id=\"btnAppval\" value=\"{0}\"  onclick=\"return stockCradAction('{1}',{2});\"/>";
            var button = string.Empty;
            switch (status)
            {
                case VoucherStatus.Pending:
                    if (user.IsAdmin() || user.Id == owner.Id)
                    {
                        name = "Submit";
                        adStatus = VoucherStatus.Submited.ToString();
                        button = string.Format(bt, name, adStatus, id);
                    }

                    break;
                case VoucherStatus.Submited:
                    if (user.IsAccountant())
                    {
                        name = "Check";
                        adStatus = VoucherStatus.Checked.ToString();
                        button = string.Format(bt, name, adStatus, id);
                        name = nameCancel;
                        adStatus = VoucherStatus.Pending.ToString();
                        button += string.Format(bt, name, adStatus, id);
                    }
                    else
                        if (user.IsAdmin() || user.Id == owner.Id)
                        {
                            name = nameCancel;
                            adStatus = VoucherStatus.Pending.ToString();
                            button = string.Format(bt, name, adStatus, id);
                        }

                    break;
                case VoucherStatus.Checked:
                    if (user.IsAdmin() || (user.IsDirecter() && user.AllowApprovedStockCard))
                    {
                        name = "Approval";
                        adStatus = VoucherStatus.Approved.ToString();
                        button = string.Format(bt, name, adStatus, id);
                        name = nameCancel;
                        adStatus = VoucherStatus.Submited.ToString();
                        button += string.Format(bt, name, adStatus, id);
                    }
                    else if (UsersModel.isAccountant(user))
                    {
                        name = nameCancel;
                        adStatus = VoucherStatus.Submited.ToString();
                        button = string.Format(bt, name, adStatus, id);
                    }
                    break;
                case VoucherStatus.Approved:
                    if (user.IsAdmin() || (user.IsDirecter() && user.AllowApprovedStockCard))
                    {
                        name = nameCancel;
                        adStatus = VoucherStatus.Pending.ToString();
                        button = string.Format(bt, name, adStatus, id);
                    }
                    break;
            }

            return button;
        }

        public static List<SelectListItem> PagList
        {
            get
            {
                var pageSizes = new List<SelectListItem>
                {
                    new SelectListItem() { Value = "0",  Text = "--All--" },
                    //new SelectListItem() { Value = "5",  Text = "5" },
                    new SelectListItem() { Value = "10",  Text = "10" },
                    new SelectListItem() { Value = "20",  Text = "20",Selected = true},
                    new SelectListItem() { Value = "30",  Text = "30" },
                    new SelectListItem() { Value = "50",  Text = "50" },
                    new SelectListItem() { Value = "100",  Text = "100" },
                
                };
                return pageSizes;
            }
        }


        private static Type GetNonNullableModelType(ModelMetadata modelMetadata)
        {
            Type realModelType = modelMetadata.ModelType;
            Type underlyingType = Nullable.GetUnderlyingType(realModelType);
            if (underlyingType != null)
            {
                realModelType = underlyingType;
            }
            return realModelType;
        }

        private static readonly SelectListItem[] SingleEmptyItem = new[] { new SelectListItem { Text = "", Value = "" } };

        public static string GetEnumDescription<TEnum>(TEnum value)
        {
            FieldInfo fi = value.GetType().GetField(value.ToString());
            DescriptionAttribute[] attributes = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);

            if ((attributes != null) && (attributes.Length > 0))
                return attributes[0].Description;
            else
                return value.ToString();
        }

        public static MvcHtmlString EnumDropDownListFor<TModel, TEnum>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TEnum>> expression)
        {
            return EnumDropDownListFor(htmlHelper, expression, null, null);
        }
        public static MvcHtmlString EnumDropDownListFor<TModel, TEnum>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TEnum>> expression, object htmlAttributes, string attributes = null)
        {
            ModelMetadata metadata = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);
            Type enumType = GetNonNullableModelType(metadata);
            IEnumerable<TEnum> values = Enum.GetValues(enumType).Cast<TEnum>();

            IEnumerable<SelectListItem> items = from value in values
                                                select new SelectListItem
                                                {
                                                    Text = GetEnumDescription(value),
                                                    Value = value.ToString(),
                                                    Selected = value.Equals(metadata.Model)
                                                };

            // If the enum is nullable, add an 'empty' item to the collection
            if (metadata.IsNullableValueType)
                items = SingleEmptyItem.Concat(items);

            return htmlHelper.DropDownListFor(expression, items, htmlAttributes);
        }
        public static List<string> GetErrorListFromModelState(ModelStateDictionary modelState)
        {
            var query = from state in modelState.Values
                        from error in state.Errors
                        select error.ErrorMessage;

            var errorList = query.ToList();
            return errorList;
        }
        public static string CheckBoxList(this HtmlHelper helper, string name, IEnumerable<SelectListItem> items)
        {
            var output = new StringBuilder();
            output.Append(@"<div class=""checkboxList"">");

            foreach (var item in items)
            {
                output.Append(@"<input type=""checkbox"" name=""");
                output.Append(name);
                output.Append("\" value=\"");
                output.Append(item.Value);
                output.Append("\"");
                if (item.Selected)
                    output.Append(@" checked=""chekced""");
                output.Append(" />");
                output.Append(item.Text);
                output.Append("<br />");
            }
            output.Append("</div>");

            return output.ToString();
        }
        public static string BindImangeNew(User user, NewsModel model)
        {


            var check = model.UserView != null &&
                        model.UserView.Contains(string.Format(";{0};", user.Id));
            if (check) return string.Empty;
            var checkDate = (DateTime.Now - model.DateCreate).Days >= Helpers.DaysView;
            if (checkDate) return string.Empty;
            var output = new StringBuilder();
            output.Append(@"<img class=""newAlertinfo"" alt=""newInfo"" src=""/Content/new-17434.png"" />");
            return output.ToString();
        }

        public static string ToDateDisplay(this HtmlHelper helper, DateTime? date, bool nearTime = false)
        {
            if (date == null)
                return string.Empty;
            if (nearTime)
                return date.Value.ToString("dd-MMM-yyyy HH:mm");
            return string.Format("{0:dd-MMM-yyy}", date.Value);
        }

        public static IHtmlString LinkIcon(this AjaxHelper ajax, string title, string action, string controller,
            object routeValues,
            MyAjaxOptions ajaxOptions, object htmlAttributes = null, object icon = null)
        {
            var attIcom = HtmlHelper.AnonymousObjectToHtmlAttributes(icon) ?? new RouteValueDictionary();
            var iBuilder = new TagBuilder("i");
            iBuilder.MergeAttributes(attIcom);
            // return Link(ajax,title,action,controller,routeValues,ajaxOptions,htmlAttributes);
            var attributes = HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes) ?? new RouteValueDictionary();
            var routes = HtmlHelper.AnonymousObjectToHtmlAttributes(routeValues);
            var targetUrl = UrlHelper.GenerateUrl(null, action, controller, routes, ajax.RouteCollection, ajax.ViewContext.RequestContext, true);

            var builder = new TagBuilder("a")
            {
                InnerHtml = HttpUtility.HtmlEncode(title + iBuilder.ToString(TagRenderMode.Normal))
            };
            builder.MergeAttributes(attributes);
            // builder.MergeAttribute("href", targetUrl);
            builder.MergeAttribute("data-ajax-url", targetUrl);
            if (ajax.ViewContext.UnobtrusiveJavaScriptEnabled)
            {
                var options = ajaxOptions ?? new MyAjaxOptions();
                var optionAttributes = options.ToUnobtrusiveHtmlAttributes();
                optionAttributes.Remove("data-ajax");
                optionAttributes.Add("data-ajax-contentType", options.ContentType);
                optionAttributes.Add("data-ajax-dataType", options.DataType);
                builder.MergeAttributes(optionAttributes);
            }
            builder.MergeAttribute("onclick", "return jQuery(this).mbqAjax();");

            return MvcHtmlString.Create(builder.ToString(TagRenderMode.Normal));
        }
        public static IHtmlString Link(this AjaxHelper ajax, string title, string action, string controller, object routeValues,
            MyAjaxOptions ajaxOptions, object htmlAttributes = null)
        {
            var attributes = HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes) ?? new RouteValueDictionary();
            var routes = HtmlHelper.AnonymousObjectToHtmlAttributes(routeValues);
            var targetUrl = UrlHelper.GenerateUrl(null, action, controller, routes, ajax.RouteCollection, ajax.ViewContext.RequestContext, true);

            var builder = new TagBuilder("a")
            {
                InnerHtml = HttpUtility.HtmlEncode(title)
            };
            builder.MergeAttributes(attributes);
            // builder.MergeAttribute("href", targetUrl);
            builder.MergeAttribute("data-ajax-url", targetUrl);
            if (ajax.ViewContext.UnobtrusiveJavaScriptEnabled)
            {
                var options = ajaxOptions ?? new MyAjaxOptions();
                var optionAttributes = options.ToUnobtrusiveHtmlAttributes();
                optionAttributes.Remove("data-ajax");
                if (!string.IsNullOrEmpty(options.DataType))
                    optionAttributes.Add("data-ajax-contentType", options.ContentType);
                if (!string.IsNullOrEmpty(options.DataType))
                    optionAttributes.Add("data-ajax-dataType", options.DataType);
                optionAttributes.Add("data-ajax-classDialog", options.ClassDialog);
                builder.MergeAttributes(optionAttributes);
            }
            builder.MergeAttribute("onclick", "return jQuery(this).mbqAjax();");

            return MvcHtmlString.Create(builder.ToString(TagRenderMode.Normal));
        }
        public static IEnumerable<Enum> GetListOfDescription<T>() where T : struct
        {
            Type t = typeof(T);
            return !t.IsEnum ? null : Enum.GetValues(t).Cast<Enum>().ToList();
        }

    }

    public class MyAjaxOptions : AjaxOptions
    {
        public string DataType { get; set; }
        public string ContentType { get; set; }
        public string ClassDialog { get; set; }
    }
}