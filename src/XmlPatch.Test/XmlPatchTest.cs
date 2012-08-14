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
        private void Run(string test)
        {
            var patcher = new XmlPatch();
            var sourceFile = string.Format(@".\{0}\Base.xml", test);
            var patchFile = string.Format(@".\{0}\Diff.xml", test);
            var actual = patcher.Patch(sourceFile, patchFile);

            var expectedFile = string.Format(@".\{0}\Result.xml", test);
            var expectedDoc = new XmlDocument();
            expectedDoc.Load(expectedFile);

            Assert.Equal(expectedDoc.InnerXml, actual.InnerXml);
        }

        [Fact]
        public void A1_Adding_an_Element()
        {
            Run("A1");
        }
        
        [Fact]
        public void A2_Adding_an_Attribute()
        {
            Run("A2");
        }

        [Fact]
        public void A3_Adding_a_Prefixed_Namespace_Declaration()
        {
            Run("A3");
        }

        [Fact]
        public void A4_Adding_a_Comment_Node_with_the_pos_Attribute()
        {
            Run("A4");
        }
        
        [Fact]
        public void A5_Adding_Multiple_Nodes()
        {
            Run("A5");
        }
        
        [Fact]
        public void A6_Replacing_an_Element()
        {
            Run("A6");
        }
        
        [Fact]
        public void A7_Replacing_an_Attribute_Value()
        {
            Run("A7");
        }
        
        [Fact]
        public void A8_Replacing_a_Namespace_Declaration_URI()
        {
            Run("A8");
        }
        
        [Fact]
        public void A9_Replacing_a_Comment_Node()
        {
            Run("A9");
        }
        
        [Fact]
        public void A10_Replacing_a_Processing_Instruction_Node()
        {
            Run("A10");
        }
        
        [Fact]
        public void A11_Replacing_a_Text_Node()
        {
            Run("A11");
        }
        
        [Fact]
        public void A12_Removing_an_Element()
        {
            Run("A12");
        }
        
        [Fact]
        public void A13_Removing_an_Attribute()
        {
            Run("A13");
        }
        
        [Fact]
        public void A14_Removing_a_Prefixed_Namespace_Declaration()
        {
            Run("A14");
        }
        
        [Fact]
        public void A15_Removing_a_Comment_Node()
        {
            Run("A15");
        }
        
        [Fact]
        public void A16_Removing_a_Processing_Instruction_Node()
        {
            Run("A16");
        }
        
        [Fact]
        public void A17_Removing_a_Text_Node()
        {
            Run("A17");
        }
        
        [Fact]
        public void A18_Several_Patches_With_Namespace_Mangling()
        {
            Run("A18");
        }
    }
}
