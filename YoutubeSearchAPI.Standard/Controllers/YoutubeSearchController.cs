// <copyright file="YoutubeSearchController.cs" company="APIMatic">
// Copyright (c) APIMatic. All rights reserved.
// </copyright>
namespace YoutubeSearchAPI.Standard.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Dynamic;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;
    using Newtonsoft.Json.Converters;
    using YoutubeSearchAPI.Standard;
    using YoutubeSearchAPI.Standard.Authentication;
    using YoutubeSearchAPI.Standard.Exceptions;
    using YoutubeSearchAPI.Standard.Http.Client;
    using YoutubeSearchAPI.Standard.Http.Request;
    using YoutubeSearchAPI.Standard.Http.Request.Configuration;
    using YoutubeSearchAPI.Standard.Http.Response;
    using YoutubeSearchAPI.Standard.Utilities;

    /// <summary>
    /// YoutubeSearchController.
    /// </summary>
    public class YoutubeSearchController : BaseController
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="YoutubeSearchController"/> class.
        /// </summary>
        /// <param name="config"> config instance. </param>
        /// <param name="httpClient"> httpClient. </param>
        /// <param name="authManagers"> authManager. </param>
        /// <param name="httpCallBack"> httpCallBack. </param>
        internal YoutubeSearchController(IConfiguration config, IHttpClient httpClient, IDictionary<string, IAuthManager> authManagers, HttpCallBack httpCallBack = null)
            : base(config, httpClient, authManagers, httpCallBack)
        {
        }

        /// <summary>
        /// Get Search Results EndPoint.
        /// </summary>
        /// <param name="part">Required parameter: Example: snippet.</param>
        /// <param name="key">Required parameter: Example: AIzaSyBuwl45ObqR_g7Viy6S7RqHKLJDTqNs1n4.</param>
        /// <param name="q">Optional parameter: The search query with the search term for youtube.</param>
        /// <returns>Returns the dynamic response from the API call.</returns>
        public dynamic GetSearchResults(
                string part,
                string key,
                string q = null)
        {
            Task<dynamic> t = this.GetSearchResultsAsync(part, key, q);
            ApiHelper.RunTaskSynchronously(t);
            return t.Result;
        }

        /// <summary>
        /// Get Search Results EndPoint.
        /// </summary>
        /// <param name="part">Required parameter: Example: snippet.</param>
        /// <param name="key">Required parameter: Example: AIzaSyBuwl45ObqR_g7Viy6S7RqHKLJDTqNs1n4.</param>
        /// <param name="q">Optional parameter: The search query with the search term for youtube.</param>
        /// <param name="cancellationToken"> cancellationToken. </param>
        /// <returns>Returns the dynamic response from the API call.</returns>
        public async Task<dynamic> GetSearchResultsAsync(
                string part,
                string key,
                string q = null,
                CancellationToken cancellationToken = default)
        {
            // the base uri for api requests.
            string baseUri = this.Config.GetBaseUri();

            // prepare query string for API call.
            StringBuilder queryBuilder = new StringBuilder(baseUri);
            queryBuilder.Append("/search");

            // prepare specfied query parameters.
            var queryParams = new Dictionary<string, object>()
            {
                { "part", part },
                { "key", key },
                { "q", q },
            };

            // append request with appropriate headers and parameters
            var headers = new Dictionary<string, string>()
            {
                { "user-agent", this.UserAgent },
                { "accept", "application/json" },
            };

            // prepare the API call request to fetch the response.
            HttpRequest httpRequest = this.GetClientInstance().Get(queryBuilder.ToString(), headers, queryParameters: queryParams);

            if (this.HttpCallBack != null)
            {
                this.HttpCallBack.OnBeforeHttpRequestEventHandler(this.GetClientInstance(), httpRequest);
            }

            // invoke request and get response.
            HttpStringResponse response = await this.GetClientInstance().ExecuteAsStringAsync(httpRequest, cancellationToken: cancellationToken).ConfigureAwait(false);
            HttpContext context = new HttpContext(httpRequest, response);
            if (this.HttpCallBack != null)
            {
                this.HttpCallBack.OnAfterHttpResponseEventHandler(this.GetClientInstance(), response);
            }

            if (response.StatusCode == 400)
            {
                throw new BadRequestException("invalidChannelID", context);
            }

            // handle errors defined at the API level.
            this.ValidateResponse(response, context);

            return ApiHelper.JsonDeserialize<dynamic>(response.Body);
        }
    }
}