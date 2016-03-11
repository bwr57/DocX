using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace Novacode.Charts
{

    public class SurfaceChart : Chart
    {
        public enum SurfaceChartStyle
        {
            Surface = 0,
            Grid = 1
        }

        /// <summary>
        /// Specifies the possible groupings for a bar chart.
        /// </summary>
        public SurfaceChartStyle Style
        {
            get
            {
                return (SurfaceChartStyle)XElementHelpers.GetIntValue(ChartXml, "wireframe", DocX.c.NamespaceName);
            }
            set
            {
                XElementHelpers.SetIntValueFromEnum<SurfaceChartStyle>(ChartXml, "wireframe", DocX.c.NamespaceName, value);
            }
        }

        protected override XElement CreateChartXml()
        {
            IsSurfacePlot = true;
            return XElement.Parse(
                @"<c:surface3DChart xmlns:c=""http://schemas.openxmlformats.org/drawingml/2006/chart"">
                    <c:wireframe val=""0""/>
                  </c:surface3DChart>");
        }
    }

}

