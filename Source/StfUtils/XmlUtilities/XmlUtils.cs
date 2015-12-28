// --------------------------------------------------------------------------------------------------------------------
// <copyright file="XmlUtils.cs" company="Mir Software">
//   Copyright governed by Artistic license as described here:
//          http://www.perlfoundation.org/artistic_license_2_0
// </copyright>
// <summary>
//   Defines the XmlUtils type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Mir.Stf.Utilities.XmlUtilities
{
    using System.IO;
    using System.Text;
    using System.Xml;
    using System.Xml.Linq;

    /// <summary>
    /// The xml utils.
    /// </summary>
    public class XmlUtils
    {
        /// <summary>
        /// The pretty xml.
        /// </summary>
        /// <param name="xml">
        /// The xml.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public string PrettyXml(string xml)
        {
            var stringBuilder = new StringBuilder();

            var element = XDocument.Parse(xml);

            var settings = new XmlWriterSettings
            {
                OmitXmlDeclaration = true,
                Indent = true,
                NewLineOnAttributes = true,
                IndentChars = "     ",
                NewLineHandling = NewLineHandling.None,
                WriteEndDocumentOnClose = false
            };

            using (var xmlWriter = XmlWriter.Create(stringBuilder, settings))
            {
                element.Save(xmlWriter);
            }

            return stringBuilder.ToString();
        }

        /// <summary>
        /// The pretty print.
        /// </summary>
        /// <param name="xml">
        /// The xml.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public string PrettyPrint(string xml)
        {
            var result = string.Empty;

            var memoryStream = new MemoryStream();
            var xmlTextWriter = new XmlTextWriter(memoryStream, Encoding.Unicode);
            var xmlDocument = new XmlDocument();

            try
            {
                // Load the XmlDocument with the XML.
                xmlDocument.LoadXml(xml);

                xmlTextWriter.Formatting = Formatting.Indented;

                // Write the XML into a formatting XmlTextWriter
                xmlDocument.WriteContentTo(xmlTextWriter);
                xmlTextWriter.Flush();
                memoryStream.Flush();

                // Have to rewind the MemoryStream in order to read
                // its contents.
                memoryStream.Position = 0;

                // Read MemoryStream contents into a StreamReader.
                var streamReader = new StreamReader(memoryStream);

                // Extract the text from the StreamReader.
                var formattedXml = streamReader.ReadToEnd();

                result = formattedXml;
            }
            catch (XmlException)
            {
            }

            memoryStream.Close();
            xmlTextWriter.Close();

            return result;
        }
    }
}
