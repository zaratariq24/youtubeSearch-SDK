// <copyright file="BadRequestException.cs" company="APIMatic">
// Copyright (c) APIMatic. All rights reserved.
// </copyright>
namespace YoutubeSearchAPI.Standard.Exceptions
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using JsonSubTypes;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;
    using YoutubeSearchAPI.Standard;
    using YoutubeSearchAPI.Standard.Http.Client;
    using YoutubeSearchAPI.Standard.Utilities;

    /// <summary>
    /// BadRequestException.
    /// </summary>
    public class BadRequestException : ApiException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BadRequestException"/> class.
        /// </summary>
        /// <param name="reason"> The reason for throwing exception.</param>
        /// <param name="context"> The HTTP context that encapsulates request and response objects.</param>
        public BadRequestException(string reason, HttpContext context)
            : base(reason, context)
        {
        }

        /// <summary>
        /// The channelId parameter specified an invalid channel ID.
        /// </summary>
        [JsonProperty("invalidChannelID")]
        public string InvalidChannelID { get; set; }
    }
}