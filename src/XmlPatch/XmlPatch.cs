using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace XmlPatch
{
    public class XmlPatch
    {
        public XmlDocument Patch(string baseFile, string diffFile)
        {
            var baseDoc = new XmlDocument();
            baseDoc.Load(baseFile);
            var diffDoc = new XmlDocument();
            diffDoc.Load(diffFile);

            return Patch(baseDoc, diffDoc);
        }

        public XmlDocument Patch(XmlDocument baseDoc, XmlDocument diffDoc)
        {
            foreach (XmlNode node in diffDoc.SelectSingleNode("/diff").ChildNodes)
            {
                //System.Diagnostics.Debugger.Break();
                var workNode = baseDoc.SelectSingleNode(node.Attributes["sel"].Value);

                switch (node.Name)
                {
                    case "add":
                        Add(baseDoc, node, workNode);
                        break;
                    case "remove":
                        workNode.ParentNode.RemoveChild(workNode);
                        break;
                    case "replace":
                        break;
                }
            }

            return baseDoc;
        }

        private static void Add(XmlDocument baseDoc, XmlNode diffNode, XmlNode workNode)
        {
            var typeAttribute = diffNode.Attributes["type"];
            //var posAttribute = diffNode.Attributes["pos"];

            if (typeAttribute == null)
            {
                workNode.InnerXml += diffNode.InnerXml;
                return;
            }

            if (typeAttribute.Value.StartsWith("@"))
            {
                var attribute = baseDoc.CreateAttribute(typeAttribute.Value.Substring(1));
                attribute.Value = diffNode.InnerXml;
                workNode.Attributes.Append(attribute);

                return;
            }
            if (typeAttribute.Value.StartsWith("namespace::"))
            {
                // <add sel="doc" type="namespace::pref">urn:ns:xxx</add>
                // -- <doc xmlns:pref="urn:ns:xxx">
                var attribute = baseDoc.CreateAttribute("xmlns:" + typeAttribute.Value.Substring(11));
                attribute.Value = diffNode.InnerXml;

                workNode.Attributes.Append(attribute);
                return;
            }

            throw new NotImplementedException();
        }
    }
}