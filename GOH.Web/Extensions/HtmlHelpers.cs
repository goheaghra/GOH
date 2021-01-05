using System;
using System.IO;
using System.Web;
using System.Web.Hosting;
using System.Web.WebPages.Html;
using System.Xml;
using System.Xml.Xsl;


namespace WebUI.HtmlHelpers
{
    public static class WebUIHtmlHelpers
    {
        /// <summary>  
        /// Applies an XSL transformation to an XML document  
        /// </summary>  
        /// <param name="helper"></param>  
        /// <param name="xml"></param>  
        /// <param name="xsltPath"></param>  
        /// <returns></returns>  
        public static HtmlString RenderXml(string xml, string xsltPath)
        {
            XsltArgumentList args = new XsltArgumentList();

            // Create XslCompiledTransform object to loads and compile XSLT file.  
            XslCompiledTransform tranformer = new XslCompiledTransform();
            tranformer.Load(HostingEnvironment.MapPath(xsltPath));

            // Create XMLReaderSetting object to assign DtdProcessing, Validation type  
            XmlReaderSettings xmlSettings = new XmlReaderSettings();
            xmlSettings.DtdProcessing = DtdProcessing.Parse;
            xmlSettings.ValidationType = ValidationType.DTD;

            // Create XMLReader object to Transform xml value with XSLT setting   
            using (XmlReader reader = XmlReader.Create(new StringReader(xml), xmlSettings))
            {
                StringWriter writer = new StringWriter();
                tranformer.Transform(reader, args, writer);

                // Generate HTML string from StringWriter  
                HtmlString htmlString = new HtmlString(writer.ToString());
                return htmlString;
            }
        }


        public static string MyLabel(this HtmlHelper helper, string target, string text)
        {
            return String.Format("<label for='{0}'>{1}</label>", target, text);
        }
    }
}
