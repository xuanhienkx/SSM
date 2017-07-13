using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using WebGrease.Css.Extensions;

namespace SSM.Common
{
    public static class ModelStateErrorHandler
    {
        /// <summary>
        /// Returns a Key/Value pair with all the errors in the model
        /// according to the data annotation properties.
        /// </summary>
        /// <param name="errDictionary"></param>
        /// <returns>
        /// Key: Name of the property
        /// Value: The error message returned from data annotation
        /// </returns>
        public static Dictionary<string, string> GetModelErrors(this ModelStateDictionary errDictionary)
        {
            var errors = new Dictionary<string, string>();
            errDictionary.Where(k => k.Value.Errors.Count > 0).ForEach(i =>
            {
                var er = string.Join(", ", i.Value.Errors.Select(e => e.ErrorMessage).ToArray());
                errors.Add(i.Key, er);
            });
            return errors;
        }

        public static string StringifyModelErrors(this ModelStateDictionary errDictionary)
        {
            var errorsBuilder = new StringBuilder();
            var errors = errDictionary.GetModelErrors();
            errors.ForEach(key => errorsBuilder.AppendFormat("{0}: {1} -", key.Key, key.Value));
            return errorsBuilder.ToString();
        }
    }
}