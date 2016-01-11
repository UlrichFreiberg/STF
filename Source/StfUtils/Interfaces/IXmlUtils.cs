// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IXmlUtils.cs" company="Mir Software">
//   Copyright governed by Artistic license as described here:
//          http://www.perlfoundation.org/artistic_license_2_0
// </copyright>
// <summary>
//   The XmlUtils interface.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Mir.Stf.Utilities.Interfaces
{
    /// <summary>
    /// The XmlUtils interface.
    /// </summary>
    public interface IXmlUtils
    {
        /// <summary>
        /// Gets a value indicating whether error.
        /// </summary>
        bool Error { get; }

        /// <summary>
        /// Gets the error message.
        /// </summary>
        string ErrorMessage { get; }

        /// <summary>
        /// The pretty print.
        /// </summary>
        /// <param name="xml">
        /// The xml.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        string PrettyPrint(string xml);

        /// <summary>
        /// The pretty print.
        /// </summary>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        string PrettyPrint();

        /// <summary>
        /// The load xml from file.
        /// </summary>
        /// <param name="filename">
        /// The filename.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        bool LoadXmlFromFile(string filename);

        /// <summary>
        /// The load xml from string.
        /// </summary>
        /// <param name="xml">
        /// The xml.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        bool LoadXmlFromString(string xml);

        /// <summary>
        /// The get value.
        /// </summary>
        /// <param name="xpath">
        /// The xpath.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        string GetValue(string xpath);

        /// <summary>
        /// The get values.
        /// </summary>
        /// <param name="xpath">
        /// The xpath.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        string GetValues(string xpath);

        /// <summary>
        /// The get tag value.
        /// </summary>
        /// <param name="tag">
        /// The tag.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        string GetTagValue(string tag);

        /// <summary>
        /// The validate schema.
        /// </summary>
        /// <param name="xsdFilename">
        /// The xsd filename.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        bool ValidateSchema(string xsdFilename);

        /// <summary>
        /// The well formed xml.
        /// </summary>
        /// <param name="xml">
        /// The xml.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        bool WellFormedXml(string xml);

        /// <summary>
        /// The well formed xml file.
        /// </summary>
        /// <param name="filename">
        /// The filename.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        bool WellFormedXmlFile(string filename);

        /// <summary>
        /// The transform.
        /// </summary>
        /// <param name="xsltFilename">
        /// The xslt filename.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        string Transform(string xsltFilename);

        /// <summary>
        /// The xml split.
        /// </summary>
        /// <param name="xpath">
        /// The xpath.
        /// </param>
        /// <param name="destinationDir">
        /// The destination dir.
        /// </param>
        /// <param name="filenamePrefix">
        /// The filename prefix.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        int XmlSplit(string xpath, string destinationDir, string filenamePrefix);

        /// <summary>
        /// The xml diff.
        /// </summary>
        /// <param name="referenceXml">
        /// The reference xml.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        bool XmlDiff(IXmlUtils referenceXml);
    }
}
