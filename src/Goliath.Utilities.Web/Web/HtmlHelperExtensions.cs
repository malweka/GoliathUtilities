using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using Goliath.Models;
using Goliath.Web.Authorization;
using Nancy.ModelBinding;
using Nancy.ViewEngines.Razor;

namespace Goliath.Web
{
    /// <summary>
    /// 
    /// </summary>
    public static class HtmlHelperExtensions
    {
        
        /// <summary>
        /// Determines whether this instance [can perform action] the specified HTML helper.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="htmlHelper">The HTML helper.</param>
        /// <param name="resourceType">Type of the resource.</param>
        /// <param name="action">The action.</param>
        /// <returns></returns>
        public static bool CanPerformAction<T>(this HtmlHelpers<T> htmlHelper, int resourceType, int action)
        {
            var currentUser = htmlHelper.CurrentUser;
            if (currentUser == null) return false;
            var userSession = currentUser as UserSession;
            if (userSession == null) return false;

            var permissionValidator = userSession.GetPermissionValidator();
            if (permissionValidator == null) return false;

            return permissionValidator.CanPerformAction(resourceType, action);
        }

        /// <summary>
        /// Determines whether this instance [can perform action] the specified HTML helper.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="htmlHelper">The HTML helper.</param>
        /// <param name="resourceType">Type of the resource.</param>
        /// <param name="action">The action.</param>
        /// <returns></returns>
        public static bool CanPerformAction<T>(this HtmlHelpers<T> htmlHelper, Type resourceType, int action)
        {
            var currentUser = htmlHelper.CurrentUser;
            var userSession = currentUser as UserSession;
            if (userSession == null) return false;

            var permissionValidator = userSession.GetPermissionValidator();
            if (permissionValidator == null) return false;

            return permissionValidator.CanPerformAction(resourceType, action);
        }

        /// <summary>
        /// Hiddens for.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="helpers">The helpers.</param>
        /// <param name="name">The name.</param>
        /// <param name="value">The value.</param>
        /// <param name="htmlAttributes">The HTML attributes.</param>
        /// <returns></returns>
        public static IHtmlString HiddenFor<T>(this HtmlHelpers<T> helpers, string name, object value, IDictionary<string, object> htmlAttributes)
        {
            var attributes = new StringBuilder();

            if (htmlAttributes != null)
            {
                foreach (var attribute in htmlAttributes)
                {
                    attributes.AppendFormat("{0}='{1}' ", attribute.Key, attribute.Value);
                }
            }

            if (value != null)
            {
                if (value is DateTime)
                {
                    var dateValue = (DateTime)value;
                    if (dateValue.Equals(DateTime.MinValue))
                    {
                        attributes.Append("value='' ");
                    }
                    else
                    {
                        attributes.AppendFormat("value='{0}' ", value);
                    }
                }
                else
                    attributes.AppendFormat("value='{0}' ", value);
            }



            var markup = string.Concat("<input type='hidden' id='", name, "' name='", name, "' value='", value, "' ", attributes.ToString(), "/>");

            return new NonEncodedHtmlString(markup);
        }

        /// <summary>
        /// Hiddens for.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="helpers">The helpers.</param>
        /// <param name="expression">The expression.</param>
        /// <param name="value">The value.</param>
        /// <param name="htmlAttributes">The HTML attributes.</param>
        /// <returns></returns>
        public static IHtmlString HiddenFor<T>(this HtmlHelpers<T> helpers, Expression<Func<T, object>> expression, object value, IDictionary<string, object> htmlAttributes)
        {
            var name = expression.GetTargetMemberInfo().Name;
            return HiddenFor(helpers, name, value, htmlAttributes);
        }

        /// <summary>
        /// Renders a textbox for the given property on the model
        /// </summary>
        /// <typeparam name="T">The model type</typeparam>
        /// <param name="helpers">The object that the extension was called on</param>
        /// <param name="expression">The expression that is used to extract the member name from</param>
        /// <returns>
        /// Markup that will not be encoded by the viewengine
        /// </returns>
        public static IHtmlString TextBoxFor<T>(this HtmlHelpers<T> helpers, Expression<Func<T, object>> expression)
        {
            var name = expression.GetTargetMemberInfo();

            var markup =
                string.Concat("<input type='textbox' id='", name, "' name='", name, "' />");

            return new NonEncodedHtmlString(markup);
        }

        /// <summary>
        /// Bools to checked.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="helpers">The helpers.</param>
        /// <param name="val">if set to <c>true</c> [value].</param>
        /// <returns></returns>
        public static string BoolToChecked<T>(this HtmlHelpers<T> helpers, bool val)
        {
            return val ? "checked" : string.Empty;
        }

        /// <summary>
        /// Renders a textbox for the given property on the model
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="helpers">The helpers.</param>
        /// <param name="expression">The expression.</param>
        /// <param name="value">The value.</param>
        /// <param name="htmlAttributes">The HTML attributes.</param>
        /// <returns></returns>
        public static IHtmlString TextBoxFor<T>(this HtmlHelpers<T> helpers, Expression<Func<T, object>> expression, object value, IDictionary<string, object> htmlAttributes)
        {
            var name = expression.GetTargetMemberInfo().Name;
            return TextBoxFor(helpers, name, value, htmlAttributes);
        }



        /// <summary>
        /// Renders a textbox for the given property on the model
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="helpers">The helpers.</param>
        /// <param name="name">The name.</param>
        /// <param name="value">The value.</param>
        /// <param name="htmlAttributes">The HTML attributes.</param>
        /// <returns></returns>
        public static IHtmlString TextBoxFor<T>(this HtmlHelpers<T> helpers, string name, object value, IDictionary<string, object> htmlAttributes)
        {
            var attributes = new StringBuilder();

            if (htmlAttributes != null)
            {
                foreach (var attribute in htmlAttributes)
                {
                    attributes.AppendFormat("{0}='{1}' ", attribute.Key, attribute.Value);
                }
            }

            if (value != null)
            {
                if (value is DateTime)
                {
                    var dateValue = (DateTime)value;
                    if (dateValue.Equals(DateTime.MinValue))
                    {
                        attributes.Append("value='' ");
                    }
                    else
                    {
                        attributes.AppendFormat("value='{0}' ", value);
                    }
                }
                else
                    attributes.AppendFormat("value='{0}' ", value);
            }

            var markup = string.Concat("<input type='textbox' id='", name, "' name='", name, "' ", attributes.ToString(), "/>");

            return new NonEncodedHtmlString(markup);
        }

        /// <summary>
        /// Texts the area for.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="helpers">The helpers.</param>
        /// <param name="expression">The expression.</param>
        /// <param name="value">The value.</param>
        /// <param name="htmlAttributes">The HTML attributes.</param>
        /// <returns></returns>
        public static IHtmlString TextAreaFor<T>(this HtmlHelpers<T> helpers, Expression<Func<T, object>> expression, object value, IDictionary<string, object> htmlAttributes)
        {
            var name = expression.GetTargetMemberInfo().Name;
            return TextAreaFor(helpers, name, value, htmlAttributes);
        }

        /// <summary>
        /// Texts the area for.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="helpers">The helpers.</param>
        /// <param name="name">The name.</param>
        /// <param name="value">The value.</param>
        /// <param name="htmlAttributes">The HTML attributes.</param>
        /// <returns></returns>
        public static IHtmlString TextAreaFor<T>(this HtmlHelpers<T> helpers, string name, object value, IDictionary<string, object> htmlAttributes)
        {
            var attributes = new StringBuilder();

            if (htmlAttributes != null)
            {
                foreach (var attribute in htmlAttributes)
                {
                    attributes.AppendFormat("{0}='{1}' ", attribute.Key, attribute.Value);
                }
            }

            var markup = string.Concat("<textarea type='textbox' rows='5' id='", name, "' name='", name, "' ", attributes.ToString(), ">", value, "</textarea>");

            return new NonEncodedHtmlString(markup);
        }

        /// <summary>
        /// Reads the only for.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="helpers">The helpers.</param>
        /// <param name="expression">The expression.</param>
        /// <param name="value">The value.</param>
        /// <param name="htmlAttributes">The HTML attributes.</param>
        /// <returns></returns>
        public static IHtmlString ReadOnlyFor<T>(this HtmlHelpers<T> helpers, Expression<Func<T, object>> expression, object value,
            IDictionary<string, object> htmlAttributes)
        {
            var name = expression.GetTargetMemberInfo().Name;
            return ReadOnlyFor(helpers, name, value, htmlAttributes);
        }

        /// <summary>
        /// Reads the only for.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="helpers">The helpers.</param>
        /// <param name="name">The name.</param>
        /// <param name="value">The value.</param>
        /// <param name="htmlAttributes">The HTML attributes.</param>
        /// <returns></returns>
        public static IHtmlString ReadOnlyFor<T>(this HtmlHelpers<T> helpers, string name, object value, IDictionary<string, object> htmlAttributes)
        {
            var readOnlyVal = string.Empty;
            if (value != null)
            {
                if (value is DateTime)
                {
                    var dateValue = (DateTime)value;
                    if (!dateValue.Equals(DateTime.MinValue))
                    {
                        readOnlyVal = value.ToString();
                    }
                }
                else
                    readOnlyVal = value.ToString();
            }

            var markup = string.Concat("<div class='well well-sm' id='", name, "'>", readOnlyVal, "</div>");

            return new NonEncodedHtmlString(markup);
        }

        /// <summary>
        /// Ticks up down for.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="helpers">The helpers.</param>
        /// <param name="expression">The expression.</param>
        /// <param name="value">The value.</param>
        /// <param name="htmlAttributes">The HTML attributes.</param>
        /// <returns></returns>
        public static IHtmlString TickUpDownFor<T>(this HtmlHelpers<T> helpers, Expression<Func<T, object>> expression, object value, IDictionary<string, object> htmlAttributes)
        {
            var name = expression.GetTargetMemberInfo().Name;
            return TextBoxFor(helpers, name, value, htmlAttributes);
        }

        /// <summary>
        /// Passwords for.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="helpers">The helpers.</param>
        /// <param name="expression">The expression.</param>
        /// <param name="value">The value.</param>
        /// <param name="htmlAttributes">The HTML attributes.</param>
        /// <returns></returns>
        public static IHtmlString PasswordFor<T>(this HtmlHelpers<T> helpers, Expression<Func<T, object>> expression, object value, IDictionary<string, object> htmlAttributes)
        {
            var name = expression.GetTargetMemberInfo().Name;
            return PasswordFor(helpers, name, value, htmlAttributes);
        }

        /// <summary>
        /// Passwords for.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="helpers">The helpers.</param>
        /// <param name="name">The name.</param>
        /// <param name="value">The value.</param>
        /// <param name="htmlAttributes">The HTML attributes.</param>
        /// <returns></returns>
        public static IHtmlString PasswordFor<T>(this HtmlHelpers<T> helpers, string name, object value, IDictionary<string, object> htmlAttributes)
        {
            var attributes = new StringBuilder();

            if (htmlAttributes != null)
            {
                foreach (var attribute in htmlAttributes)
                {
                    attributes.AppendFormat("{0}='{1}' ", attribute.Key, attribute.Value);
                }
            }

            var markup = string.Concat("<input type='password' id='", name, "' name='", name, "' value='", value, "' ", attributes.ToString(), ">");

            return new NonEncodedHtmlString(markup);
        }


        /// <summary>
        /// Dates the picker for.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="helpers">The helpers.</param>
        /// <param name="expression">The expression.</param>
        /// <param name="value">The value.</param>
        /// <param name="htmlAttributes">The HTML attributes.</param>
        /// <returns></returns>
        public static IHtmlString DatePickerFor<T>(this HtmlHelpers<T> helpers, Expression<Func<T, object>> expression, object value, IDictionary<string, object> htmlAttributes)
        {
            var name = expression.GetTargetMemberInfo().Name;
            return TextBoxFor(helpers, name, value, htmlAttributes);
        }

        /// <summary>
        /// Prints the error form group class.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="helpers">The helpers.</param>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        public static IHtmlString PrintErrorFormGroupClass<T>(this HtmlHelpers<T> helpers, string name)
        {
            string clname = string.Empty;
            var vmodel = helpers.Model as ViewModel;
            if (vmodel != null && vmodel.ExecutionInfo.ExecutionState == ViewExecutionState.HasError)
            {
                if (vmodel.ExecutionInfo.Errors.ContainsKey(name))
                {
                    clname = "has-error";
                }
            }

            return new NonEncodedHtmlString(clname);
        }

        /// <summary>
        /// Renders a checkbox for the given property on the model
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="helpers">The helpers.</param>
        /// <param name="expression">The expression.</param>
        /// <param name="value">The value.</param>
        /// <param name="htmlAttributes">The HTML attributes.</param>
        /// <returns></returns>
        public static IHtmlString CheckBoxFor<T>(this HtmlHelpers<T> helpers, Expression<Func<T, object>> expression, object value, IDictionary<string, object> htmlAttributes)
        {
            var membInfo = expression.GetTargetMemberInfo();
            var name = membInfo.Name;

            var isChecked = false;

            if (htmlAttributes == null)
                htmlAttributes = new Dictionary<string, object>();

            if (membInfo.ReflectedType == typeof(bool))
            {
                if (value != null)
                    isChecked = (bool)value;
            }

            else if (value is string)
            {
                if (!string.IsNullOrWhiteSpace(value.ToString()))
                    bool.TryParse(value.ToString(), out isChecked);
            }

            return CheckBoxFor(helpers, name, value, isChecked, htmlAttributes);
        }

        /// <summary>
        /// Renders a checkbox for the given property on the model
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="helpers">The helpers.</param>
        /// <param name="name">The name.</param>
        /// <param name="value">The value.</param>
        /// <param name="isChecked">The is checked.</param>
        /// <param name="htmlAttributes">The HTML attributes.</param>
        /// <returns></returns>
        public static IHtmlString CheckBoxFor<T>(this HtmlHelpers<T> helpers, string name, object value, bool? isChecked, IDictionary<string, object> htmlAttributes)
        {
            var attributes = new StringBuilder();

            if (htmlAttributes != null)
            {
                foreach (var attribute in htmlAttributes)
                {
                    attributes.AppendFormat("{0}='{1}' ", attribute.Key, attribute.Value);
                }
            }

            if (value != null)
            {
                attributes.AppendFormat("value='{0}' ", value);
            }

            if (isChecked.HasValue && isChecked.Value)
            {
                attributes.Append("checked='checked'");
            }

            var markup = string.Concat("<input type='checkbox' id='", name, "' name='", name, "' ", attributes.ToString(), "/>");

            return new NonEncodedHtmlString(markup);
        }


        //public static IHtmlString CheckBoxListFor<T>(this HtmlHelpers<T> helpers, Expression<Func<T, object>> expression,
        //    object value, IDictionary<string, object> htmlAttributes)
        //{
        //    var membInfo = expression.GetTargetMemberInfo();
        //    var name = membInfo.Name;

        //    if (membInfo.ReflectedType != null && membInfo.ReflectedType.IsEnum)
        //    {
        //        var enumValues = Enum.GetNames(membInfo.ReflectedType);
        //        var controls = new StringBuilder();
        //        var enumFullName = membInfo.ReflectedType.FullName.Replace(".", "_").ToLower();
        //        foreach (var en in enumValues)
        //        {
        //            if (value != null && en.Equals(value.ToString()))
        //                htmlAttributes.Add("checked", "true");

        //            var txtResource = string.Concat(enumFullName, "_", en.ToLower());
        //            var displayText = EnumText.ResourceManager.GetString(txtResource);
        //            var ctrl = BuildInputMarkup("checkbox", name, en, displayText, htmlAttributes);
        //            controls.Append(ctrl);
        //        }
        //        return new NonEncodedHtmlString(controls.ToString());
        //    }

        //    var markup = string.Concat("<div class='ajx_ctrl ajx_checklist' id='", name, "' ", HtmlAttributesString(htmlAttributes), "></div>");
        //    return new NonEncodedHtmlString(markup);
        //}

        //public static IHtmlString RadioButtonListFor<T>(this HtmlHelpers<T> helpers, Expression<Func<T, object>> expression,
        //    object value, IDictionary<string, object> htmlAttributes)
        //{
        //    var membInfo = expression.GetTargetMemberInfo();
        //    var propInfo = membInfo as PropertyInfo;
        //    var name = membInfo.Name;

        //    if (propInfo != null && propInfo.PropertyType.IsEnum)
        //    {
        //        var enumValues = Enum.GetNames(propInfo.PropertyType);
        //        var controls = new StringBuilder();
        //        var enumFullName = propInfo.PropertyType.FullName.Replace(".", "_").ToLower();
        //        foreach (var en in enumValues)
        //        {
        //            bool isChecked = false;
        //            if (value != null && en.Equals(value.ToString()))
        //            {
        //                htmlAttributes.Add("checked", "checked");
        //                isChecked = true;
        //            }

        //            var txtResource = string.Concat(enumFullName, "_", en.ToLower());
        //            var displayText = EnumText.ResourceManager.GetString(txtResource);
        //            var ctrl = BuildInputMarkup("radio", name, en, displayText, htmlAttributes);
        //            if (isChecked)
        //            {
        //                htmlAttributes.Remove("checked");
        //            }
        //            controls.Append(ctrl);
        //        }
        //        return new NonEncodedHtmlString(controls.ToString());
        //    }

        //    var markup = string.Concat("<div class='ajx_ctrl ajx_radiolist' id='", name, "' ", HtmlAttributesString(htmlAttributes), " ></div>");
        //    return new NonEncodedHtmlString(markup);
        //}

        public static IHtmlString DropDownFor<T>(this HtmlHelpers<T> helpers, Expression<Func<T, object>> expression,
            object value, IDictionary<string, object> htmlAttributes)
        {
            var membInfo = expression.GetTargetMemberInfo();
            var propInfo = membInfo as PropertyInfo;

            var name = membInfo.Name;

            var attribs = HtmlAttributesString(htmlAttributes);

            if (propInfo != null && propInfo.PropertyType.IsEnum)
            {
                var enumValues = Enum.GetNames(propInfo.PropertyType);
                var controls = new StringBuilder(string.Concat("<select id='", name, "' name='", name, "' ", attribs, " >\n\r"));
                var enumFullName = propInfo.PropertyType.FullName.Replace(".", "_").ToLower();

                foreach (var en in enumValues)
                {
                    string selected = string.Empty;
                    if (value != null && en.Equals(value.ToString()))
                        selected = " selected='selected'";

                    //var txtResource = string.Concat(enumFullName, "_", en.ToLower());
                    var displayText = en;//EnumText.ResourceManager.GetString(txtResource);
                    var ctrl = string.Concat("<option value='", en, "'", selected, ">", displayText, "</option>\n\r");
                    controls.Append(ctrl);
                }
                controls.Append("</select>");
                return new NonEncodedHtmlString(controls.ToString());
            }

            var markup = string.Concat("<select class='ajx_ctrl ajx_dropdown' id='", name, "' ", attribs, " ></select>");
            return new NonEncodedHtmlString(markup);
        }

        //public static IHtmlString DropDownFor<T>(this HtmlHelpers<T> helpers, Expression<Func<T, object>> expression,
        //    object value, IDictionary<string, object> htmlAttributes)
        //{
        //    var membInfo = expression.GetTargetMemberInfo();
        //    var propInfo = membInfo as PropertyInfo;

        //    var name = membInfo.Name;

        //    var attribs = HtmlAttributesString(htmlAttributes);

        //    if (propInfo != null && propInfo.PropertyType.IsEnum)
        //    {
        //        var enumValues = Enum.GetNames(propInfo.PropertyType);
        //        var controls = new StringBuilder(string.Concat("<select id='", name, "' name='", name, "' ", attribs, " >\n\r"));
        //        var enumFullName = propInfo.PropertyType.FullName.Replace(".", "_").ToLower();

        //        foreach (var en in enumValues)
        //        {
        //            string selected = string.Empty;
        //            if (value != null && en.Equals(value.ToString()))
        //                selected = " selected='selected'";

        //            //var txtResource = string.Concat(enumFullName, "_", en.ToLower());
        //            var displayText = enumFullName;//EnumText.ResourceManager.GetString(txtResource);
        //            var ctrl = string.Concat("<option value='", en, "'", selected, ">", displayText, "</option>\n\r");
        //            controls.Append(ctrl);
        //        }
        //        controls.Append("</select>");
        //        return new NonEncodedHtmlString(controls.ToString());
        //    }

        //    var markup = string.Concat("<select class='ajx_ctrl ajx_dropdown' id='", name, "' ", attribs, " ></select>");
        //    return new NonEncodedHtmlString(markup);
        //}



        static string BuildInputMarkup(string type, string controlName, string value, string displayText, IDictionary<string, object> htmlAttributes)
        {
            var attributes = new StringBuilder();

            attributes.AppendFormat("type='{0}' ", type);
            attributes.AppendFormat("id='{0}' ", controlName);
            attributes.AppendFormat("name='{0}' ", controlName);

            if (htmlAttributes != null)
            {
                foreach (var attribute in htmlAttributes)
                {
                    attributes.AppendFormat("{0}='{1}' ", attribute.Key, attribute.Value);
                }
            }

            if (value != null)
            {
                attributes.AppendFormat("value='{0}' ", value);
            }

            return string.Concat("<input ", attributes.ToString(), "/>", displayText);
        }

        public static IHtmlString LinkButton<T>(this HtmlHelpers<T> helpers, string id, string href, string icon, string value, IDictionary<string, object> htmlAttributes)
        {
            var attributes = new StringBuilder();
            string iconMarkup = string.Empty;
            if (!string.IsNullOrWhiteSpace(id))
            {
                attributes.AppendFormat("id='{0}' ", value);
            }
            if (!string.IsNullOrWhiteSpace(href))
            {
                attributes.AppendFormat("href='{0}' ", href);
            }
            if (htmlAttributes != null)
            {
                foreach (var attribute in htmlAttributes)
                {
                    attributes.AppendFormat("{0}='{1}' ", attribute.Key, attribute.Value);
                }
            }
            if (!string.IsNullOrWhiteSpace(icon))
            {
                iconMarkup = string.Concat("<i class='", icon, "'></i> ");
            }

            var markup = string.Concat("<a ", attributes.ToString(), " >", iconMarkup, value, "</a>");

            return new NonEncodedHtmlString(markup);
        }

        static string HtmlAttributesString(IDictionary<string, object> htmlAttributes)
        {
            var attributes = new StringBuilder();
            if (htmlAttributes != null)
            {
                foreach (var attribute in htmlAttributes)
                {
                    attributes.AppendFormat("{0}='{1}' ", attribute.Key, attribute.Value);
                }
            }

            return attributes.ToString();
        }
    }
}