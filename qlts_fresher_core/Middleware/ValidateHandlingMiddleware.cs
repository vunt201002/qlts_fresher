using Misa.Qlts.Solution.Common.Enums;
using Misa.Qlts.Solution.Common.Exceptions;
using Misa.Qlts.Solution.Common.Exceptions.ValidateException;
using Misa.Qlts.Solution.Common.Resources;
using System.Net;

namespace Misa.Qlts.Solution.Controller.Middleware
{
    public class ValidateHandlingMiddleware : IMiddleware
    {
        #region Constructors
        public ValidateHandlingMiddleware()
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
        /// Kiểm soát các lỗi được trả ra
        /// </summary>
        /// <param name="context"></param>
        /// <param name="exception">lỗi</param>
        /// <returns></returns>
        /// created by: ntvu (29/05/2023)
        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";

            // nếu lỗi tỷ hệ hao mòn
            if (exception is DepreciationRateException)
            {
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;

                // đưa ra lỗi
                await context.Response.WriteAsync(
                    text: new BaseException()
                    {
                        ErrorCode = (int)ErrorCode.DepreciationRateExceptionCode,
                        UserMessage = "Depreciation Rate Exception",
                        DevMessage = exception.Message,
                        TraceId = context.TraceIdentifier,
                        MoreInfo = exception.HelpLink
                    }.ToString() ?? ""
                );
                // nếu lỗi hao mòn năm
            }
            else if (exception is AmortizationOfYearException)
            {
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;

                // đưa ra lỗi
                await context.Response.WriteAsync(
                    text: new BaseException()
                    {
                        ErrorCode = (int)ErrorCode.AmortizationOfYearExceptionCode,
                        UserMessage = "Amortization Of Year Exception",
                        DevMessage = exception.Message,
                        TraceId = context.TraceIdentifier,
                        MoreInfo = exception.HelpLink
                    }.ToString() ?? ""
                );
            }
            // nếu là lỗi trùng mã
            else if (exception is DuplicateCodeException)
            {
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;

                // đưa ra lỗi
                await context.Response.WriteAsync(
                    text: new BaseException()
                    {
                        ErrorCode = (int)ErrorCode.DuplicateCodeExceptionCode,
                        UserMessage = "Duplicate code Exception",
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
