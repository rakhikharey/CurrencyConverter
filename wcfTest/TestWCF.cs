using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Cconvert;

namespace wcfTest
{
    [TestClass]
    public class TestWCF
    {
        [TestMethod]
        public void TestLogin()
        {

            ICroweCurrencyConversionAPI c = new CroweCurrencyConversionAPI();

            //Test for the source which needs to be logged in to fetch the API. He/She will get a new token after login which will be sent to API later for calculating  the money
            string loginjson = c.LoginSource("rakhi", "rakhi");
            Assert.AreEqual("{\"status\":\"success\",\"val\":{\"token\":\"9i89hshj8t6E\",\"user\":\"rakhi\"}}", loginjson);
            string currencyConvert = c.CurrrencyConvert("USD", "INR", 80, "9i89hshj8t6E");
            Assert.AreEqual("{\"status\":\"success\",\"val\":{\"fromCur\":\"USD\",\"toCur\":\"INR\",\"amount\":80,\"rate\":80,\"convertedamount\":6400}}", currencyConvert);


            //negative tests

            currencyConvert = c.CurrrencyConvert("USD", "", 80, "9i89hshj8t6E");
            Assert.AreNotEqual("{\"status\":\"success\",\"val\":{\"fromCur\":\"USD\",\"toCur\":\"INR\",\"amount\":80,\"rate\":80,\"convertedamount\":6400}}", currencyConvert);
            Assert.AreEqual("{\"status\":\"error\",\"val\":\"Some parameters are missing or incorrect\"}", currencyConvert);

            currencyConvert = c.CurrrencyConvert("U-SD", "", 80, "9i89hshj8t6E");
            Assert.AreNotEqual("{\"status\":\"success\",\"val\":{\"fromCur\":\"USD\",\"toCur\":\"INR\",\"amount\":80,\"rate\":80,\"convertedamount\":6400}}", currencyConvert);
            Assert.AreEqual("{\"status\":\"error\",\"val\":\"Some parameters are missing or incorrect\"}", currencyConvert);

            currencyConvert = c.CurrrencyConvert("USSD", "", 80, "9i89hshj8t6E");
            Assert.AreNotEqual("{\"status\":\"success\",\"val\":{\"fromCur\":\"USD\",\"toCur\":\"INR\",\"amount\":80,\"rate\":80,\"convertedamount\":6400}}", currencyConvert);
            Assert.AreEqual("{\"status\":\"error\",\"val\":\"Some parameters are missing or incorrect\"}", currencyConvert);

            currencyConvert = c.CurrrencyConvert("USD", "INRR", 80, "9i89hshj8t6E");
            Assert.AreNotEqual("{\"status\":\"success\",\"val\":{\"fromCur\":\"USD\",\"toCur\":\"INR\",\"amount\":80,\"rate\":80,\"convertedamount\":6400}}", currencyConvert);
            Assert.AreEqual("{\"status\":\"error\",\"val\":\"Some parameters are missing or incorrect\"}", currencyConvert);


            //Test for the source which needs not to be logged in to fetch the API. 
            //He /She will be assign a static APIKey from admin to access the API

            currencyConvert = c.CurrrencyConvert("USD", "INR", 80, "emGqJ2s3cEiz");
            Assert.AreEqual("{\"status\":\"success\",\"val\":{\"fromCur\":\"USD\",\"toCur\":\"INR\",\"amount\":80,\"rate\":80,\"convertedamount\":6400}}", currencyConvert);


            //negative tests

            currencyConvert = c.CurrrencyConvert("USD", "", 80, "emGqJ2s3cEiz");
            Assert.AreNotEqual("{\"status\":\"success\",\"val\":{\"fromCur\":\"USD\",\"toCur\":\"INR\",\"amount\":80,\"rate\":80,\"convertedamount\":6400}}", currencyConvert);
            Assert.AreEqual("{\"status\":\"error\",\"val\":\"Some parameters are missing or incorrect\"}", currencyConvert);

            currencyConvert = c.CurrrencyConvert("", "INR", 80, "emGqJ2s3cEiz");
            Assert.AreNotEqual("{\"status\":\"success\",\"val\":{\"fromCur\":\"USD\",\"toCur\":\"INR\",\"amount\":80,\"rate\":80,\"convertedamount\":6400}}", currencyConvert);
            Assert.AreEqual("{\"status\":\"error\",\"val\":\"Some parameters are missing or incorrect\"}", currencyConvert);

            currencyConvert = c.CurrrencyConvert("USD", "INR", 0, "emGqJ2s3cEiz");
            Assert.AreNotEqual("{\"status\":\"success\",\"val\":{\"fromCur\":\"USD\",\"toCur\":\"INR\",\"amount\":80,\"rate\":80,\"convertedamount\":6400}}", currencyConvert);
            Assert.AreEqual("{\"status\":\"error\",\"val\":\"Some parameters are missing or incorrect\"}", currencyConvert);
        }
    }
}
