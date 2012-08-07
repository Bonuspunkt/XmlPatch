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
                var targetNode = baseDoc.SelectSingleNode(node.Attributes["sel"].Value);

                switch (node.Name)
                {
                    case "add":
                        Add(baseDoc, node, targetNode);
                        break;
                    case "remove":
                        Remove(node, targetNode);
                        break;
                    case "replace":
                        Replace(baseDoc, node, targetNode);
                        break;
                }
            }

            return baseDoc;
        }

        private static void Add(XmlDocument baseDoc, XmlNode diffNode, XmlNode targetNode)
        {
            var typeAttribute = diffNode.Attributes["type"];
            var posAttribute = diffNode.Attributes["pos"];

            if (posAttribute != null)
            {
                System.Diagnostics.Debugger.Break();
            }

            if (typeAttribute == null)
            {
                foreach (XmlNode diffChild in diffNode.ChildNodes)
                {
                    var importNode = baseDoc.ImportNode(diffChild, true);
                    targetNode.AppendChild(importNode);
                }
                return;
            }

            if (typeAttribute.Value.StartsWith("@"))
            {
                var attribute = baseDoc.CreateAttribute(typeAttribute.Value.Substring(1));
                attribute.Value = diffNode.InnerXml;
                targetNode.Attributes.Append(attribute);

                return;
            }
            if (typeAttribute.Value.StartsWith("namespace::"))
            {
                var attribute = baseDoc.CreateAttribute("xmlns:" + typeAttribute.Value.Substring(11));
                attribute.Value = diffNode.InnerXml;

                targetNode.Attributes.Append(attribute);
                return;
            }

            throw new NotImplementedException();
        }

        private static void Remove(XmlNode diffNode, XmlNode targetNode)
        {
            if (targetNode.ParentNode != null)
            {
                targetNode.ParentNode.RemoveChild(targetNode);
                return;
            }
            if (targetNode is XmlAttribute)
            {
                var attribute = (XmlAttribute)targetNode;
                attribute.OwnerElement.RemoveAttributeNode(attribute);
                return;
            }
            throw new NotImplementedException();
        }

        private static void Replace(XmlDocument baseDoc, XmlNode diffNode, XmlNode targetNode)
        {
            var insertAfter = targetNode;

            if (targetNode is XmlAttribute)
            {
                targetNode.Value = diffNode.InnerXml;
                return;
            }

            foreach (XmlNode diffChild in diffNode.ChildNodes)
            {
                var importNode = baseDoc.ImportNode(diffChild, true);
                insertAfter = targetNode.ParentNode.InsertAfter(importNode, targetNode);
            }
            targetNode.ParentNode.RemoveChild(targetNode);
        }
    }
}