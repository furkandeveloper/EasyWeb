using System;
using System.Collections.Generic;
using System.Text;

namespace EasyWeb.Core.Models
{
    /// <summary>
    /// This is base class of Api Response models. All requests will return in this template. See BaseController for implementation.
    /// </summary>
    /// <typeparam name="T">Type of data.</typeparam>
    public class ApiResult<TData, TMeta>
    {
        /// <summary>
        /// Indicates if request completed successful or not.
        /// </summary>
        public virtual bool Success { get; set; }

        /// <summary>
        /// Key of message or request type.
        /// </summary>
        public virtual string Key { get; set; }

        /// <summary>
        /// Main data of response.
        /// </summary>
        public virtual TData Data { get; set; }

        /// <summary>
        /// Metadata about response, like pagination page counts or deprecated elements etc.
        /// </summary>
        public virtual TMeta Meta { get; set; }

        /// <summary>
        /// Message about request. Some unsuccessful requests have message about it.
        /// </summary>
        public virtual string Message { get; set; }

        /// <summary>
        /// Indicates if <see cref="Message"/> comes from resources and readable by directly users or not.
        /// </summary>
        public virtual bool? IsUserFriendlyMessage { get; set; }
    }

}
