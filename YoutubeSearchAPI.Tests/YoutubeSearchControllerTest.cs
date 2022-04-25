// <copyright file="YoutubeSearchControllerTest.cs" company="APIMatic">
// Copyright (c) APIMatic. All rights reserved.
// </copyright>
namespace YoutubeSearchAPI.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.IO;
    using System.Threading.Tasks;
    using Newtonsoft.Json.Converters;
    using NUnit.Framework;
    using YoutubeSearchAPI.Standard;
    using YoutubeSearchAPI.Standard.Controllers;
    using YoutubeSearchAPI.Standard.Exceptions;
    using YoutubeSearchAPI.Standard.Http.Client;
    using YoutubeSearchAPI.Standard.Http.Response;
    using YoutubeSearchAPI.Standard.Utilities;
    using YoutubeSearchAPI.Tests.Helpers;

    /// <summary>
    /// YoutubeSearchControllerTest.
    /// </summary>
    [TestFixture]
    public class YoutubeSearchControllerTest : ControllerTestBase
    {
        /// <summary>
        /// Controller instance (for all tests).
        /// </summary>
        private YoutubeSearchController controller;

        /// <summary>
        /// Setup test class.
        /// </summary>
        [OneTimeSetUp]
        public void SetUpDerived()
        {
            this.controller = this.Client.YoutubeSearchController;
        }

        /// <summary>
        /// This test checks the validity of the keys returned in the "get search results" endpoint..
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous unit test.</returns>
        [Test]
        public async Task TestSearchTest()
        {
            // Parameters for the API call
            string part = string.Empty;
            string key = string.Empty;
            string q = string.Empty;

            // Perform API call
            dynamic result = null;
            try
            {
                result = await this.controller.GetSearchResultsAsync(part, key, q);
            }
            catch (ApiException)
            {
            }

            // Test response code
            Assert.AreEqual(200, this.HttpCallBackHandler.Response.StatusCode, "Status should be 200");

            // Test whether the captured response is as we expected
            Assert.IsNotNull(result, "Result should exist");
            Assert.IsTrue(
                    TestHelper.IsJsonObjectProperSubsetOf(
                    "{\n\"kind\": \"youtube#searchListResponse\",\n}",
                    TestHelper.ConvertStreamToString(this.HttpCallBackHandler.Response.RawBody),
                    false,
                    true,
                    false),
                    "Response body should have matching keys");
        }
    }
}