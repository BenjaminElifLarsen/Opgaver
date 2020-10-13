using System;
using Xunit;
using StorageSystemCore;

namespace StroageSystemTest
{
    public class UnitTest1
    {
        [Fact]
        public void WareAmountTest()
        {
            WareInformation.AddWareDefault();
            Assert.Equal(6,WareInformation.Ware.Count);
            WareModifier.RemoveWareTesting("ID-55t");
            Assert.Equal(5, WareInformation.Ware.Count);
            WareModifier.RemoveWareTesting("id-55t");
            Assert.Equal(5, WareInformation.Ware.Count);
            WareInformation.Ware = null;
        }

        [Fact]
        public void WareInformationTest()
        {
            Assert.Equal(3, WareInformation.FindWareTypes().Count);
        }

        [Fact]
        public void RegexTest()
        {
            Assert.True(RegexControl.IsValidLength("StringLength"));
            Assert.False(RegexControl.IsValidLength("Str"));
            Assert.False(RegexControl.IsValidLength("ThisIsALongString"));
            Assert.True(RegexControl.IsValidLettersLower("str"));
            Assert.False(RegexControl.IsValidLettersLower("STR"));
            Assert.True(RegexControl.IsValidLettersLower("STr"));
            Assert.True(RegexControl.IsValidLettersUpper("STR"));
            Assert.False(RegexControl.IsValidLettersUpper("str"));
            Assert.True(RegexControl.IsValidLettersUpper("stR"));
            Assert.True(RegexControl.IsValidSpecial("-"));
            Assert.False(RegexControl.IsValidSpecial("%"));
            Assert.True(RegexControl.IsValidSpecial("b-nm"));
            Assert.False(RegexControl.IsValidSpecial("b%"));
            Assert.True(RegexControl.IsValidCharsOnly("awdw123."));
            Assert.False(RegexControl.IsValidCharsOnly("123123dø"));
        }

        [Fact]
        public void SupportTest()
        {
            dynamic result = Support.GetDefaultValueFromValueType("Int32");
            Assert.Equal(0, result);
            result = Support.GetDefaultValueFromValueType("String");
            Assert.Equal(null, result);
            result = Support.GetDefaultValueFromValueType("Single");
            Assert.Equal(0, result);
            dynamic result2 = Support.GetDefaultValueFromValueType("SByte");
            Assert.Equal(0, result2);
        }
    }
}
