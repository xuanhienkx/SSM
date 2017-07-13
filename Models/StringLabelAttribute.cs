using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Reflection;
namespace SSM.Models
{
    public class StringLabelAttribute : Attribute
    {

        #region Properties

        /// <summary>
        /// Holds the stringvalue for a value in an enum.
        /// </summary>
        public string StringLabel { get; protected set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor used to init a StringValue Attribute
        /// </summary>
        /// <param name="value"></param>
        public StringLabelAttribute(string label)
        {
            this.StringLabel = label;
        }

        #endregion
        
    }
    public static class EnumUtils {
        /// <summary>
        /// Will get the string value for a given enums value, this will
        /// only work if you assign the StringValue attribute to
        /// the items in your enum.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string GetStringLabel(this Enum value)
        {
            // Get the type
            Type type = value.GetType();

            // Get fieldinfo for this type
            FieldInfo fieldInfo = type.GetField(value.ToString());

            // Get the stringvalue attributes
            StringLabelAttribute[] attribs = fieldInfo.GetCustomAttributes(
                typeof(StringLabelAttribute), false) as StringLabelAttribute[];

            // Return the first if there was a match.
            return attribs.Length > 0 ? attribs[0].StringLabel : null;
        } 
    }
}