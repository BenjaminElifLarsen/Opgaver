using System;
using Xunit;
using Regex_Assignment;

namespace Password_Test
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            // INVALID PASSWORDS
            Assert.False(Password.PasswordChecker("P1zz@")); //5 characters
            Assert.False(Password.PasswordChecker("P1zz@P1zz@P1zz@P1zz@P1zz@")); //25 characters
            Assert.False(Password.PasswordChecker("mypassword11")); //No uppercase
            Assert.False(Password.PasswordChecker("MYPASSWORD11")); //No lowercase
            Assert.False(Password.PasswordChecker("iLoveYou")); //No numbers
            Assert.False(Password.PasswordChecker("Pè7$areLove"));
            Assert.False(Password.PasswordChecker("Repeeea7!"));
            // VALID PASSWORDS
            Assert.True(Password.PasswordChecker("H4(k+x0"));
            Assert.True(Password.PasswordChecker("Fhg93@"));
            Assert.True(Password.PasswordChecker("aA0!@#$%^&*()+=_-{}[]:;\""));
            Assert.True(Password.PasswordChecker("zZ9'?<>,."));
        }
    }
}
