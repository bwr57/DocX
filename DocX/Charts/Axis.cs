using System;
using System.Xml.Linq;

namespace Novacode
{
    /// <summary>
    /// Axis base class
    /// </summary>
    public abstract class Axis
    {
        /// <summary>
        /// ID of this Axis 
        /// </summary>
        public String Id
        {
            get
            {
                return Xml.Element(XName.Get("axId", DocX.c.NamespaceName)).Attribute(XName.Get("val")).Value;
            }
        }

        /// <summary>
        /// Return true if this axis is visible
        /// </summary>
        public Boolean IsVisible
        {
            get
            {
                return Xml.Element(XName.Get("delete", DocX.c.NamespaceName)).Attribute(XName.Get("val")).Value == "0";
            }
            set
            {
                if (value)
                    Xml.Element(XName.Get("delete", DocX.c.NamespaceName)).Attribute(XName.Get("val")).Value = "0";
                else
                    Xml.Element(XName.Get("delete", DocX.c.NamespaceName)).Attribute(XName.Get("val")).Value = "1";
            }

        }

        /// <summary>
        /// Axis xml element
        /// </summary>
        internal XElement Xml { get; set; }

        internal Axis(XElement xml)
        {
            Xml = xml;
        }

        public Axis(String id)
        { }

        /// <summary>
        /// Form xml code of axis title
        /// </summary>
        /// <param name="title">Axis title</param>
        /// <returns>Xml code</returns>
        protected virtual string GetTitleXml(string title)
        {
            if (title == null)
                return "";
            return String.Format(@"<c:title>
                        <c:tx>
                            <c:rich>
                                <a:bodyPr/>
                                <a:lstStyle/>
                                <a:p>
                                    <a:pPr>
                                        <a:defRPr/>
                                    </a:pPr>
                                    <a:r>
                                        <a:t>{0}</a:t>
                                    </a:r>
                                </a:p>
                            </c:rich>
                        </c:tx>
                        <c:overlay val=""0""/>
                    </c:title>", title);
        }
    }

    /// <summary>
    /// Represents Category Axes
    /// </summary>
    public class CategoryAxis : Axis
    {
        internal CategoryAxis(XElement xml)
            : base(xml)
        { }

        public CategoryAxis(String id, String valAxisId)
            : this(id, valAxisId, null)
        { }

        public CategoryAxis(String id, String valAxisId, string title)
            : base(id)
        {
            Xml = XElement.Parse(String.Format(
              @"<c:catAx xmlns:c=""http://schemas.openxmlformats.org/drawingml/2006/chart"" xmlns:a=""http://schemas.openxmlformats.org/drawingml/2006/main""> 
                <c:axId val=""{0}""/>
                <c:scaling>
                  <c:orientation val=""minMax""/>
                </c:scaling>
                <c:delete val=""0""/>
                <c:axPos val=""b""/>
                {2}
                <c:majorTickMark val=""out""/>
                <c:minorTickMark val=""none""/>
                <c:tickLblPos val=""nextTo""/>
                <c:crossAx val=""{1}""/>
                <c:crosses val=""autoZero""/>
                <c:auto val=""1""/>
                <c:lblAlgn val=""ctr""/>
                <c:lblOffset val=""100""/>
                <c:noMultiLvlLbl val=""0""/>
              </c:catAx>", id, valAxisId, GetTitleXml(title)));
        }
    }

    /// <summary>
    /// Represents Values Axes
    /// </summary>
    public class ValueAxis : Axis
    {
        internal ValueAxis(XElement xml)
            : base(xml)
        { }

        public ValueAxis(String id, String valAxisId)
            : this(id, valAxisId, null)
        { }

        public ValueAxis(String id, String catAxisId, string title)
            : base(id)
        {
            Xml = XElement.Parse(String.Format(
              @"<c:valAx xmlns:c=""http://schemas.openxmlformats.org/drawingml/2006/chart"" xmlns:a=""http://schemas.openxmlformats.org/drawingml/2006/main"">
                <c:axId val=""{0}""/>
                <c:scaling>
                  <c:orientation val=""minMax""/>
                </c:scaling>
                <c:delete val=""0""/>
                <c:axPos val=""l""/>
                {2}
                <c:numFmt sourceLinked=""0"" formatCode=""General""/>
                <c:majorGridlines/>
                <c:majorTickMark val=""out""/>
                <c:minorTickMark val=""none""/>
                <c:tickLblPos val=""nextTo""/>
                <c:crossAx val=""{1}""/>
                <c:crosses val=""autoZero""/>
                <c:crossBetween val=""between""/>
              </c:valAx>", id, catAxisId, GetTitleXml(title)));
        }
    }

    /// <summary>
    /// Represents Seria Axes
    /// </summary>
    public class SeriaAxis : Axis
    {
        internal SeriaAxis(XElement xml)
            : base(xml)
        { }

        public SeriaAxis(String id, String valAxisId)
            : this(id, valAxisId, null)
        { }

        public SeriaAxis(String id, String valAxisId, string title)
            : base(id)
        {
            Xml = XElement.Parse(String.Format(
              @"<c:serAx xmlns:c=""http://schemas.openxmlformats.org/drawingml/2006/chart"" xmlns:a=""http://schemas.openxmlformats.org/drawingml/2006/main"">
                <c:axId val=""{0}""/>
                <c:scaling>
                  <c:orientation val=""minMax""/>
                </c:scaling>
                <c:delete val=""0""/>
                <c:axPos val=""b""/>
                {2}
                <c:majorTickMark val=""out""/>
                <c:minorTickMark val=""none""/>
                <c:tickLblPos val=""nextTo""/>
                <c:crossAx val=""{1}""/>
                <c:crosses val=""autoZero""/>
              </c:serAx>", id, valAxisId, GetTitleXml(title)));
        }
    }

}
