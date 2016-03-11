using System;
using System.Linq;
using System.Reflection;
using System.Xml.Linq;

namespace Novacode
{
    internal static class XElementHelpers
    {
        /// <summary>
        /// Get value from XElement and convert it to enum
        /// </summary>
        /// <typeparam name="T">Enum type</typeparam>        
        internal static T GetValueToEnum<T>(XElement element)
        {
            if (element == null)
                throw new ArgumentNullException("element");

            String value = element.Attribute(XName.Get("val")).Value;
            foreach (T e in Enum.GetValues(typeof(T)))
            {
                FieldInfo fi = typeof(T).GetField(e.ToString());
                if (fi.GetCustomAttributes(typeof(XmlNameAttribute), false).Count() == 0)
                    throw new Exception(String.Format("Attribute 'XmlNameAttribute' is not assigned to {0} fields!", typeof(T).Name));
                XmlNameAttribute a = (XmlNameAttribute)fi.GetCustomAttributes(typeof(XmlNameAttribute), false).First();
                if (a.XmlName == value)
                    return e;
            }
            throw new ArgumentException("Invalid element value!");
        }

        /// <summary>
        /// Convert value to xml string and set it into XElement
        /// </summary>
        /// <typeparam name="T">Enum type</typeparam> 
        internal static void SetValueFromEnum<T>(XElement element, T value)
        {
            if (element == null)
                throw new ArgumentNullException("element");
            element.Attribute(XName.Get("val")).Value = GetXmlNameFromEnum<T>(value);
        }

        /// <summary>
        /// Return xml string for this value
        /// </summary>
        /// <typeparam name="T">Enum type</typeparam> 
        internal static String GetXmlNameFromEnum<T>(T value)
        {
            if (value == null)
                throw new ArgumentNullException("value");

            FieldInfo fi = typeof(T).GetField(value.ToString());
            if (fi.GetCustomAttributes(typeof(XmlNameAttribute), false).Count() == 0)
                throw new Exception(String.Format("Attribute 'XmlNameAttribute' is not assigned to {0} fields!", typeof(T).Name));
            XmlNameAttribute a = (XmlNameAttribute)fi.GetCustomAttributes(typeof(XmlNameAttribute), false).First();
            return a.XmlName;
        }

        /// <summary>
        /// Convert value to integer value and set it into XElement
        /// </summary>
        public static int GetIntValue(XElement element, string tagName, string nameSpace)
        {
            if (element == null)
                return 0;
            element = element.Element(XName.Get(tagName, nameSpace));
            if (element == null)
                return 0;
            int result = 0;
            if (Int32.TryParse(element.Attribute(XName.Get("val")).Value, out result))
                return result;
            return 0;
        }


        /// <summary>
        /// Convert value to integer value and set it into XElement
        /// </summary>
        /// <typeparam name="T">Enum type</typeparam> 
        public static void SetIntValueFromEnum<T>(XElement element, string tagName, string nameSpace, T value)
        {
            if (element == null)
                throw new ArgumentNullException("element");
            element = element.Element(XName.Get(tagName, nameSpace));
            if (element == null)
                throw new ArgumentNullException("element");
            element.Attribute(XName.Get("val")).Value = Convert.ToInt32(value).ToString();
        }

    }

    /// <summary>
    /// This attribute applied to enum's fields for definition their's real xml names in DocX file.
    /// </summary>
    /// <example>
    /// public enum MyEnum
    /// {
    ///    [XmlName("one")] // This means, that xml element has 'val="one"'
    ///    ValueOne,
    ///    [XmlName("two")] // This means, that xml element has 'val="two"'
    ///    ValueTwo
    /// }
    /// </example>
    [AttributeUsage(AttributeTargets.Field, Inherited = false, AllowMultiple = false)]
    internal sealed class XmlNameAttribute : Attribute
    {
        /// <summary>
        /// Real xml name
        /// </summary>
        public String XmlName { get; private set; }

        public XmlNameAttribute(String xmlName)
        {
            XmlName = xmlName;
        }
    }
}
