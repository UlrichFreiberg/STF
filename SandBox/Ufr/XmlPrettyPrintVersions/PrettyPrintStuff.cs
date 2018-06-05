        private string PrettyPrint3(string xml)
        {
            try
            {
                var doc = XDocument.Parse(xml);
                var retVal = doc.ToString();
                return retVal;
            }
            catch (Exception)
            {
                return xml;
            }
        }

        private string PrettyPrint2(string xml)
        {
            var xmlDoc = new XmlDocument();
            var sw = new StringWriter();

            xmlDoc.LoadXml(xml);
            xmlDoc.Save(sw);

            var retVal = sw.ToString();

            return retVal;
        }

        /// <summary>
        /// The pretty print.
        /// See http://stackoverflow.com/questions/1123718/format-xml-string-to-print-friendly-xml-string
        /// </summary>
        /// <param name="xml">
        /// The xml.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        private string PrettyPrint1(string xml)
        {
            var result = string.Empty;
            var memoryStream = new MemoryStream();
            var xmlTextWriter = new XmlTextWriter(memoryStream, Encoding.UTF8);
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
            catch (XmlException ex)
            {
                LogError("You tried to log an XML message - something is not right: [{0}]", ex.Message);
            }

            memoryStream.Close();
            xmlTextWriter.Close();

            return result;
        }

		        /// <summary>
        /// The pretty print 5.
        /// </summary>
        /// <param name="xml">
        /// The xml.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        private string PrettyPrint5(string xml)
        {
            var retVal = xml;

            try
            {
                retVal = System.Xml.Linq.XElement.Parse(xml).ToString();
            }
            catch(System.Xml.XmlException ex)
            {
            }

            return retVal;
        }
