using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace Application.Contracts.Response
{
    // Copyright (c) Microsoft Corporation, Inc. All rights reserved.
    // Licensed under the MIT License, Version 2.0. See License.txt in the project root for license information.
    // https://github.com/aspnet/AspNetIdentity/blob/master/src/Microsoft.AspNet.Identity.Core/IdentityResult.cs
    public class ServiceResponse<T>
    {
        public bool Succeeded { get; private set; }

        public bool Failed => Errors.Any() || !Succeeded;

        public T Content { get; private set; }

        public IEnumerable<string> Errors { get; private set; }

        public string ErrorMessage => Errors.Any() ? string.Join("\n", Errors.ToArray()) : string.Empty;

        public ErrorDetails ErrorDetails => new ErrorDetails(Errors.ToList());

        /// <summary>
        /// Failure constructor that takes error messages
        /// </summary>
        /// <param name="errors"></param>
        protected ServiceResponse(params string[] errors) : this((IEnumerable<string>)errors)
        {
        }

        /// <summary>
        /// Failure constructor that takes error messages
        /// </summary>
        /// <param name="errors"></param>
        protected ServiceResponse(IEnumerable<string> errors)
        {
            if (errors == null) {
                errors = new[] { "An error occured" };
            }
            Succeeded = false;
            Errors = errors;
        }

        /// <summary>
        /// Constructor that takes whether the result is successful
        /// </summary>
        /// <param name="content"></param>
        protected ServiceResponse(T content)
        {
            Content = content;
            Succeeded = true;
            Errors = new string[0];
        }

        /// <summary>
        ///  Static success result
        /// </summary>
        /// <returns></returns>
        public static ServiceResponse<T> Success(T content) => new ServiceResponse<T>(content);

        /// <summary>
        /// Failed helper method
        /// </summary>
        /// <param name="errors"></param>
        /// <returns></returns>
        public static ServiceResponse<T> Fail(params string[] errors) => new ServiceResponse<T>(errors);
    }

    /// <summary>
    /// https://tools.ietf.org/html/rfc7807 complaint error details
    /// </summary>
    public class ErrorDetails : ProblemDetails
    {
        public ErrorDetails(List<string> errors)
        {
            Title = errors.Any() ? string.Join("\n", errors.ToArray()) : string.Empty;
        }
    }
}
