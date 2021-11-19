﻿using SnakeTBs.Identity.Api.Authentications.Models.Constants;
using SnakeTBs.Identity.LogicLayer.Models.Requests.Entities;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace SnakeTBs.Identity.Api.Controllers.UserControllers.Requests.Entities
{
    public class EditGuidRequest : GuidRequest
    {
        /// <summary>
        /// 
        /// </summary>
        [JsonPropertyName("firstName")]
        [StringLength(256)]
        public string FirstName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [JsonPropertyName("middleName")]
        [StringLength(256)]
        public string MiddleName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [JsonPropertyName("lastName")]
        [StringLength(256)]
        public string LastName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [JsonPropertyName("phone")]
        [RegularExpression(RegularExpressionConstant.PHONE)]
        public string Phone { get; set; }
    }
}
