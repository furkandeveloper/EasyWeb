using EasyWeb.Core.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace EasyWeb.AspNetCore.Controllers
{
    /// <summary>
    /// A base class for an MVC controller without view support.
    /// </summary>
    [ApiController]
    public abstract class BaseApiController : ControllerBase
    {
        private const string BadRequestMessage = "An error occurred while executing the operation.";

        /// <summary>
        /// Creates an Microsoft.AspNetCore.Mvc.BadRequestObjectResult that produces a Microsoft.AspNetCore.Http.StatusCodes.Status400BadRequest
        /// response.
        /// </summary>
        /// <param name="modelState">
        /// The Microsoft.AspNetCore.Mvc.ModelBinding.ModelStateDictionary containing errors
        /// to be returned to the client.
        /// </param>
        /// <returns>
        /// The created Microsoft.AspNetCore.Mvc.BadRequestObjectResult for the response.
        /// See <see cref="BadRequestResult"/>
        /// </returns>
        public override BadRequestObjectResult BadRequest(ModelStateDictionary modelState)
        {
            return BadRequest(modelState, false);
        }

        /// <summary>
        /// Creates an Microsoft.AspNetCore.Mvc.BadRequestObjectResult that produces a Microsoft.AspNetCore.Http.StatusCodes.Status400BadRequest
        /// response.
        /// </summary>
        /// <param name="modelState">
        /// /// The Microsoft.AspNetCore.Mvc.ModelBinding.ModelStateDictionary containing errors
        /// to be returned to the client.
        /// </param>
        /// <param name="isUserFriendlyMessage">
        /// Indicates if <see>
        ///     <cref>Message</cref>
        /// </see>
        /// comes from resources and readable by directly users or not.
        /// </param>
        /// <returns>
        /// The created Microsoft.AspNetCore.Mvc.BadRequestObjectResult for the response.
        /// See <see cref="BadRequestResult"/>
        /// </returns>
        [NonAction]
        protected virtual BadRequestObjectResult BadRequest(ModelStateDictionary modelState, bool isUserFriendlyMessage)
        {
            var mainErrors = modelState.Where(x => !string.IsNullOrEmpty(x.Key))
                .ToDictionary(k => k.Key, v => v.Value.Errors.Select(x => x.ErrorMessage));

            return base.BadRequest(new ApiResult
            {
                Success = false,
                Data = mainErrors,
                Key = "ModelState",
                Message = string.Join("\n", mainErrors.SelectMany(x => x.Value)),
                IsUserFriendlyMessage = isUserFriendlyMessage
            });
        }

        /// <summary>
        /// Creates an Microsoft.AspNetCore.Mvc.BadRequestObjectResult that produces a Microsoft.AspNetCore.Http.StatusCodes.Status400BadRequest
        /// response.
        /// </summary>
        /// <param name="error">
        /// An error object to be returned to the client.
        /// </param>
        /// <returns>
        /// The created Microsoft.AspNetCore.Mvc.BadRequestObjectResult for the response.
        /// See <see cref="BadRequestResult"/>
        /// </returns>
        public override BadRequestObjectResult BadRequest(object error)
        {
            return base.BadRequest(new ApiResult
            {
                Success = false,
                Data = error,
                Key = "BadRequest",
                Message = BadRequestMessage
            });
        }

        /// <summary>
        /// Creates an Microsoft.AspNetCore.Mvc.BadRequestObjectResult that produces a Microsoft.AspNetCore.Http.StatusCodes.Status400BadRequest
        /// response.
        /// </summary>
        /// <param name="error">
        /// An error object to be returned to the client.
        /// </param>
        /// <param name="key">
        /// Key of message or request type.
        /// </param>
        /// <returns>
        /// The created Microsoft.AspNetCore.Mvc.BadRequestObjectResult for the response.
        /// See <see cref="BadRequestResult"/>
        /// </returns>
        [NonAction]
        protected virtual BadRequestObjectResult BadRequest(object error, string key)
        {
            return base.BadRequest(new ApiResult
            {
                Success = false,
                Data = error,
                Key = key,
                Message = BadRequestMessage
            });
        }

        /// <summary>
        /// Creates an Microsoft.AspNetCore.Mvc.BadRequestObjectResult that produces a Microsoft.AspNetCore.Http.StatusCodes.Status400BadRequest
        /// response.
        /// </summary>
        /// <param name="error">
        /// An error object to be returned to the client.
        /// </param>
        /// <param name="key">
        /// Key of message or request type.
        /// </param>
        /// <param name="message">
        /// Message about request. Some unsuccessful requests have message about it.
        /// </param>
        /// <returns>
        /// The created Microsoft.AspNetCore.Mvc.BadRequestObjectResult for the response.
        /// See <see cref="BadRequestResult"/>
        /// </returns>
        [NonAction]
        protected virtual BadRequestObjectResult BadRequest(object error, string key, string message)
        {
            return base.BadRequest(new ApiResult
            {
                Success = false,
                Data = error,
                Key = key,
                Message = message,
                IsUserFriendlyMessage = true
            });
        }

        /// <summary>
        /// Creates an <see cref="NotFoundObjectResult"/> that produces a <see cref="StatusCodes.Status404NotFound"/> response.
        /// </summary>
        /// <returns>The created <see cref="NotFoundObjectResult"/> for the response.</returns>
        [NonAction]
        public override NotFoundObjectResult NotFound(object value)
        {
            return base.NotFound(new ApiResult
            {
                Success = false,
                Key = "NotFound",
                Data = value
            });
            return base.NotFound(value);
        }
    }
}