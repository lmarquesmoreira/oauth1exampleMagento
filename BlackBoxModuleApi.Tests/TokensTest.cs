using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BlackBoxModuleApi.Models;
using BlackBoxModuleApi.Services;

namespace BlackBoxModuleApi.Tests
{
    [TestClass]
    public class TokensTest
    {
        [TestMethod]
        public void Should_Be_Request_Token()
        {
            string data = "oauth_token=4cqw0r7vo0s5goyyqnjb72sqj3vxwr0h&oauth_token_secret=rig3x3j5a9z5j6d4ubjwyf9f1l21itrr";
            var expected = new Token()
            {
                oauth_token = "4cqw0r7vo0s5goyyqnjb72sqj3vxwr0h",
                oauth_token_secret = "rig3x3j5a9z5j6d4ubjwyf9f1l21itrr"
            };

            var result = OAuthService.Generate(data);

            Assert.AreEqual(expected.oauth_token, result.oauth_token);
            Assert.AreEqual(expected.oauth_token_secret, result.oauth_token_secret);
        }
    }
}
