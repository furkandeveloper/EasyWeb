﻿using System;
using System.Linq;
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
        }

        /// <summary>
        /// Creates a <see cref="AcceptedResult"/> object that produces an <see cref="StatusCodes.Status202Accepted"/> response.
        /// </summary>
        /// <param name="value">The optional content value to format in the entity body; may be null.</param>
        /// <returns>The created <see cref="AcceptedResult"/> for the response.</returns>
        [NonAction]
        public override AcceptedResult Accepted(object value)
        {
            return base.Accepted(new ApiResult
            {
                Success = true,
                Key = "Accepted",
                Data = value
            });
        }

        /// <summary>
        /// Creates a <see cref="AcceptedResult"/> object that produces an <see cref="StatusCodes.Status202Accepted"/> response.
        /// </summary>
        /// <param name="uri">The URI with the location at which the status of requested content can be monitored.</param>
        /// <param name="value">The optional content value to format in the entity body; may be null.</param>
        /// <returns>The created <see cref="AcceptedResult"/> for the response.</returns>
        [NonAction]
        public override AcceptedResult Accepted(Uri uri, object value)
        {
            return base.Accepted(uri, new ApiResult
            {
                Success = true,
                Key = "Accepted",
                Data = value
            });
        }

        /// <summary>
        /// Creates a <see cref="AcceptedResult"/> object that produces an <see cref="StatusCodes.Status202Accepted"/> response.
        /// </summary>
        /// <param name="uri">The URI with the location at which the status of requested content can be monitored.</param>
        /// <param name="value">The optional content value to format in the entity body; may be null.</param>
        /// <returns>The created <see cref="AcceptedResult"/> for the response.</returns>
        [NonAction]
        public override AcceptedResult Accepted(string uri, object value)
        {
            return base.Accepted(uri, new ApiResult
            {
                Success = true,
                Key = "Accepted",
                Data = value
            });
        }

        /// <summary>
        /// Creates a <see cref="CreatedResult"/> object that produces a <see cref="StatusCodes.Status201Created"/> response.
        /// </summary>
        /// <param name="uri">The URI at which the content has been created.</param>
        /// <param name="value">The content value to format in the entity body.</param>
        /// <returns>The created <see cref="CreatedResult"/> for the response.</returns>
        [NonAction]
        public override CreatedResult Created(Uri uri, object value)
        {
            return base.Created(uri, new ApiResult
            {
                Success = true,
                Data = value,
                Key = "Created"
            });
        }

        /// <summary>
        /// Creates a <see cref="CreatedResult"/> object that produces a <see cref="StatusCodes.Status201Created"/> response.
        /// </summary>
        /// <param name="uri">The URI at which the content has been created.</param>
        /// <param name="value">The content value to format in the entity body.</param>
        /// <returns>The created <see cref="CreatedResult"/> for the response.</returns>
        [NonAction]
        public override CreatedResult Created(string uri, object value)
        {
            return base.Created(uri, new ApiResult
            {
                Success = true,
                Key = "Created",
                Data = value
            });
        }

        /// <summary>
        /// Creates an <see cref="OkObjectResult"/> object that produces an <see cref="StatusCodes.Status200OK"/> response.
        /// </summary>
        /// <param name="value">The content value to format in the entity body.</param>
        /// <returns>The created <see cref="OkObjectResult"/> for the response.</returns>
        [NonAction]
        public override OkObjectResult Ok(object value)
        {
            if (value == null)
                return base.Ok(new ApiResult
                {
                    Success = true,
                    Data = new object(),
                    Message = "Data is empty",
                    Key = "OK"
                });
            return base.Ok(new ApiResult
            {
                Success = true,
                Data = value,
                Key = "OK"
            });
        }

        /// <summary>
        /// Creates an <see cref="OkObjectResult"/> object that produces an <see cref="StatusCodes.Status200OK"/> response.
        /// </summary>
        /// <param name="value">The content value to format in the entity body.</param>
        /// <param name="meta">The content value to format in the entity meta.</param>
        /// <returns>The created <see cref="OkObjectResult"/> for the response.</returns>
        [NonAction]
        protected virtual OkObjectResult Ok(object value, object meta)
        {
            return base.Ok(new ApiResult
            {
                Success = true,
                Data = value,
                Meta = meta,
                Key = "OK"
            });
        }

        /// <summary>
        /// Creates a <see cref="CreatedAtActionResult"/> object that produces a <see cref="StatusCodes.Status201Created"/> response.
        /// </summary>
        /// <param name="actionName">The name of the action to use for generating the URL.</param>
        /// <param name="controllerName">The name of the controller to use for generating the URL.</param>
        /// <param name="routeValues">The route data to use for generating the URL.</param>
        /// <param name="value">The content value to format in the entity body.</param>
        /// <returns>The created <see cref="CreatedAtActionResult"/> for the response.</returns>
        [NonAction]
        public override CreatedAtActionResult CreatedAtAction(string actionName, string controllerName, object routeValues, object value)
        {
            return base.CreatedAtAction(actionName, controllerName, routeValues, new ApiResult
            {
                Success = true,
                Data = value,
                Key = "Created"
            });
        }

        /// <summary>
        /// Creates a <see cref="CreatedAtActionResult"/> object that produces a <see cref="StatusCodes.Status201Created"/> response.
        /// </summary>
        /// <param name="actionName">The name of the action to use for generating the URL.</param>
        /// <param name="value">The content value to format in the entity body.</param>
        /// <returns>The created <see cref="CreatedAtActionResult"/> for the response.</returns>
        [NonAction]
        public override CreatedAtActionResult CreatedAtAction(string actionName, object value)
        {
            return base.CreatedAtAction(actionName, new ApiResult
            {
                Success = true,
                Data = value,
                Key = "Created"
            });
        }

        /// <summary>
        /// Creates a <see cref="CreatedAtActionResult"/> object that produces a <see cref="StatusCodes.Status201Created"/> response.
        /// </summary>
        /// <param name="actionName">The name of the action to use for generating the URL.</param>
        /// <param name="routeValues">The route data to use for generating the URL.</param>
        /// <param name="value">The content value to format in the entity body.</param>
        /// <returns>The created <see cref="CreatedAtActionResult"/> for the response.</returns>
        [NonAction]
        public override CreatedAtActionResult CreatedAtAction(string actionName, object routeValues, object value)
        {
            return base.CreatedAtAction(actionName, routeValues, new ApiResult
            {
                Success = true,
                Data = value,
                Key = "Created"
            });
        }

        /// <summary>
        /// Creates a <see cref="CreatedAtRouteResult"/> object that produces a <see cref="StatusCodes.Status201Created"/> response.
        /// </summary>
        /// <param name="routeValues">The route data to use for generating the URL.</param>
        /// <param name="value">The content value to format in the entity body.</param>
        /// <returns>The created <see cref="CreatedAtRouteResult"/> for the response.</returns>
        [NonAction]
        public override CreatedAtRouteResult CreatedAtRoute(object routeValues, object value)
        {
            return base.CreatedAtRoute(routeValues, new ApiResult
            {
                Success = true,
                Data = value,
                Key = "Created"
            });
        }

        /// <summary>
        /// Creates a <see cref="CreatedAtRouteResult"/> object that produces a <see cref="StatusCodes.Status201Created"/> response.
        /// </summary>
        /// <param name="routeName">The name of the route to use for generating the URL.</param>
        /// <param name="value">The content value to format in the entity body.</param>
        /// <returns>The created <see cref="CreatedAtRouteResult"/> for the response.</returns>
        [NonAction]
        public override CreatedAtRouteResult CreatedAtRoute(string routeName, object value)
        {
            return base.CreatedAtRoute(routeName, new ApiResult
            {
                Success = true,
                Data = value,
                Key = "Created"
            });
        }

        /// <summary>
        /// Creates a <see cref="CreatedAtRouteResult"/> object that produces a <see cref="StatusCodes.Status201Created"/> response.
        /// </summary>
        /// <param name="routeName">The name of the route to use for generating the URL.</param>
        /// <param name="routeValues">The route data to use for generating the URL.</param>
        /// <param name="value">The content value to format in the entity body.</param>
        /// <returns>The created <see cref="CreatedAtRouteResult"/> for the response.</returns>
        [NonAction]
        public override CreatedAtRouteResult CreatedAtRoute(string routeName, object routeValues, object value)
        {
            return base.CreatedAtRoute(routeName, routeValues, new ApiResult
            {
                Success = true,
                Data = value,
                Key = "Created"
            });
        }
    }
}