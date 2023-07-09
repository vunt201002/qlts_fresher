using System.Net;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Misa.Qlts.Solution.Common.Exceptions;
using Misa.Qlts.Solution.Common.Resources;

namespace Misa.Qlts.Solution.Controller.Middleware
{
    /// <summary>
    /// middleware kiểm soát lỗi
    /// </summary>
    public class GlobalHandlingMiddleware : IMiddleware
    {

        #region Constructors
        public GlobalHandlingMiddleware()
        {

        }
        #endregion

        #region Methods
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);        // nếu không có lỗi, đi đến middleware kế tiếp
            }
            catch (Exception ex)
            {
                // gọi hàm handle lỗi
                await HandleExceptionAsync(context, ex);
            }
        }

        /// <summary>
        /// Kiểm soát các lỗi
        /// </summary>
        /// <param name="context"></param>
        /// <param name="exception">Lỗi</param>
        /// <returns>Task</returns>
        /// created by: ntvu (28/05/2023)
        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";

            // nếu là lỗi 500
            if (exception is InternalException)
            {
                // lấy user message từ file resource
                string internalMessage = UserErrorMessage.InternalExceptionUserMessage;
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                await context.Response.WriteAsync(
                    // đưa ra lỗi
                    text: new BaseException()
                    {
                        ErrorCode = (int)HttpStatusCode.InternalServerError,
                        UserMessage = internalMessage,
                        DevMessage = exception.Message,
                        TraceId = context.TraceIdentifier,
                        MoreInfo = exception.HelpLink
                    }.ToString() ?? ""
                );
                // nếu là lỗi 404
            }
            else if (exception is NotFoundException)
            {
                // lấy user message từ file resource
                string notFoundMessage = UserErrorMessage.NotFoundExceptionUserMessage;
                context.Response.StatusCode = (int)HttpStatusCode.NotFound;

                // đưa ra lỗi
                await context.Response.WriteAsync(
                    text: new BaseException()
                    {
                        ErrorCode = (int)HttpStatusCode.NotFound,
                        UserMessage = notFoundMessage,
                        DevMessage = exception.Message,
                        TraceId = context.TraceIdentifier,
                        MoreInfo = exception.HelpLink
                    }.ToString() ?? ""
                );
                // nếu lỗi bad request (400)
            }
            else if (exception is BadRequestException)
            {
                // lấy user message từ file resource
                string badRequest = UserErrorMessage.BadRequestExceptionUserMessage;
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;

                // đưa ra lỗi
                await context.Response.WriteAsync(
                    text: new BaseException()
                    {
                        ErrorCode = (int)HttpStatusCode.BadRequest,
                        UserMessage = badRequest,
                        DevMessage = exception.Message,
                        TraceId = context.TraceIdentifier,
                        MoreInfo = exception.HelpLink
                    }.ToString() ?? ""
                );
                // nếu là lỗi forbidden (403)
            }
            else if (exception is ForbiddenException)
            {
                // lấy user message từ file resource
                string forbiddenMessage = UserErrorMessage.ForbiddenExceptionUserMessage;
                context.Response.StatusCode = (int)HttpStatusCode.Forbidden;

                // đưa ra lỗi
                await context.Response.WriteAsync(
                    text: new BaseException()
                    {
                        ErrorCode = (int)HttpStatusCode.Forbidden,
                        UserMessage = forbiddenMessage,
                        DevMessage = exception.Message,
                        TraceId = context.TraceIdentifier,
                        MoreInfo = exception.HelpLink
                    }.ToString() ?? ""
                );
                // nếu là lỗi not implement (501)
            }
            else if (exception is NotImplementException)
            {
                // lấy user message từ file resource
                string notImplement = UserErrorMessage.NotImplementExceptionUserMessage;
                context.Response.StatusCode = (int)HttpStatusCode.NotImplemented;

                // đưa ra lỗi
                await context.Response.WriteAsync(
                    text: new BaseException()
                    {
                        ErrorCode = (int)HttpStatusCode.NotImplemented,
                        UserMessage = notImplement,
                        DevMessage = exception.Message,
                        TraceId = context.TraceIdentifier,
                        MoreInfo = exception.HelpLink
                    }.ToString() ?? ""
                );
                // nếu là lỗi request time-out (408)
            }
            else if (exception is RequestTimeOutException)
            {
                // lấy user message từ file resource
                string requestTimeOut = UserErrorMessage.RequestTimeOutExceptionUserMessage;
                context.Response.StatusCode = (int)HttpStatusCode.RequestTimeout;

                // đưa ra lỗi
                await context.Response.WriteAsync(
                    text: new BaseException()
                    {
                        ErrorCode = (int)HttpStatusCode.RequestTimeout,
                        UserMessage = requestTimeOut,
                        DevMessage = exception.Message,
                        TraceId = context.TraceIdentifier,
                        MoreInfo = exception.HelpLink
                    }.ToString() ?? ""
                );
                // nếu là lỗi unauthorized (401)
            }
            else if (exception is UnauthorizedException)
            {
                // lấy user message từ file resource
                string unauthorized = UserErrorMessage.UnauthorizedExceptionUserMessage;
                context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;

                // đưa ra lỗi
                await context.Response.WriteAsync(
                    text: new BaseException()
                    {
                        ErrorCode = (int)HttpStatusCode.Unauthorized,
                        UserMessage = unauthorized,
                        DevMessage = exception.Message,
                        TraceId = context.TraceIdentifier,
                        MoreInfo = exception.HelpLink
                    }.ToString() ?? ""
                );
            }
        } 
        #endregion

    }

}
