using System.Xml.Linq;

namespace Novacode.Charts
{
    /// <summary>
    /// This element contains the 3-D surface chart series.
    /// </summary>
    public class SurfaceChart : Chart
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public SurfaceChart()
            : base()
        {

        }

        /// <summary>
        /// Constructor with defiation of axis titles
        /// </summary>
        /// <param name="catAxisTitle">Category axis title</param>
        /// <param name="valAxisTitle">Value axis title</param>
        /// <param name="serAxisTitle">Seria axis title</param>
        public SurfaceChart(string catAxisTitle, string valAxisTitle, string serAxisTitle)
            : base(catAxisTitle, valAxisTitle, serAxisTitle)
        {

        }

        /// <summary>
        /// Styles of surface chart
        /// </summary>
        public enum SurfaceChartStyle
        {
            /// <summary>
            /// Solid surface chart
            /// </summary>
            Surface = 0,
            /// <summary>
            /// Grid surface chart
            /// </summary>
            Grid = 1
        }

        /// <summary>
        /// Specifies the style for a surface chart.
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

        /// <summary>
        /// Form original xml code for surface chart
        /// </summary>
        /// <returns></returns>
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

