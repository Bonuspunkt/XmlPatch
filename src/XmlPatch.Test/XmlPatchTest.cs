using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;
using System.Xml;

namespace XmlPatch.Test
{
    public class XmlPatchTest
    {
        private void Run(int no)
        {
            var patcher = new XmlPatch();
            var sourceFile = string.Format(@".\A{0}\Base.xml", no);
            var patchFile = string.Format(@".\A{0}\Diff.xml", no);
            var actual = patcher.Patch(sourceFile, patchFile);

            var expectedFile = string.Format(@".\A{0}\Result.xml", no);
            var expectedDoc = new XmlDocument();
            expectedDoc.Load(expectedFile);

            Assert.Equal(expectedDoc.InnerXml, actual.InnerXml);
        }

        [Fact]
        public void A1_Adding_an_Element()
        {
            Run(1);
        }
        
        [Fact]
        public void A2_Adding_an_Attribute()
        {
            Run(2);
        }

        [Fact]
        public void A3_Adding_a_Prefixed_Namespace_Declaration()
        {
            Run(3);
        }

        [Fact(Skip="not done yet")]
        public void A4_Adding_a_Comment_Node_with_the_pos_Attribute()
        {
            Run(4);
        }
        
        [Fact(Skip="not done yet")]
        public void A5_Adding_Multiple_Nodes()
        {
            Run(5);
        }
        
        [Fact(Skip="not done yet")]
        public void A6_Replacing_an_Element()
        {
            Run(6);
        }
        
        [Fact(Skip="not done yet")]
        public void A7_Replacing_an_Attribute_Value()
        {
            Run(7);
        }
        
        [Fact(Skip="not done yet")]
        public void A8_Replacing_a_Namespace_Declaration_URI()
        {
            Run(8);
        }
        
        [Fact(Skip="not done yet")]
        public void A9_Replacing_a_Comment_Node()
        {
            Run(9);
        }
        
        [Fact(Skip="not done yet")]
        public void A10_Replacing_a_Processing_Instruction_Node()
        {
            Run(10);
        }
        
        [Fact(Skip="not done yet")]
        public void A11_Replacing_a_Text_Node()
        {
            Run(11);
        }
        
        [Fact(Skip="not done yet")]
        public void A12_Removing_an_Element()
        {
            Run(12);
        }
        
        [Fact(Skip="not done yet")]
        public void A13_Removing_an_Attribute()
        {
            Run(13);
        }
        
        [Fact(Skip="not done yet")]
        public void A14_Removing_a_Prefixed_Namespace_Declaration()
        {
            Run(14);
        }
        
        [Fact(Skip="not done yet")]
        public void A15_Removing_a_Comment_Node()
        {
            Run(15);
        }
        
        [Fact(Skip="not done yet")]
        public void A16_Removing_a_Processing_Instruction_Node()
        {
            Run(16);
        }
        
        [Fact(Skip="not done yet")]
        public void A17_Removing_a_Text_Node()
        {
            Run(17);
        }
        
        [Fact(Skip="not done yet")]
        public void A18_Several_Patches_With_Namespace_Mangling()
        {
            Run(18);
        }
    }
}
