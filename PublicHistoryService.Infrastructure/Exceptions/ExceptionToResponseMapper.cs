using PublicHistoryService.Domain.Exceptions;
using PublicHistoryService.Infrastructure.Exceptions.Interfaces;
using System.Collections.Concurrent;
using System.Net;

namespace PublicHistoryService.Infrastructure.Exceptions
{
    internal sealed class ExceptionToResponseMapper : IExceptionToResponseMapper
    {
        private static readonly ConcurrentDictionary<Type, string> _errorCodes = new ConcurrentDictionary<Type, string>();

        public ExceptionResponse Map(Exception exception)
            => exception switch
            {
                DomainException ex => new ExceptionResponse(response: new
                {
                    ErrorCode = GetExceptionCode(ex),
                    Reason = ex.Message
                }, statusCode: HttpStatusCode.BadRequest),

                PublicHistoryService.Application.Exceptions.ApplicationException ex => new ExceptionResponse(
                    response: new { ErrorCode = GetExceptionCode(ex), Reason = ex.Message },
                    statusCode: HttpStatusCode.BadRequest),

                InfrastructureException ex => new ExceptionResponse(response: new
                {
                    ErrorCode = GetExceptionCode(ex),
                    Reason = ex.Message
                }, statusCode: HttpStatusCode.BadRequest),

                _ => new ExceptionResponse(response: new { ErrorCode = "Error", Reason = "There was an server error!" },
                        statusCode: HttpStatusCode.InternalServerError)
            };

        private static string GetExceptionCode(Exception ex)
        {
            var exceptionType = ex.GetType();

            if (_errorCodes.TryGetValue(exceptionType, out var code))
            {
                return code;
            }

            var exceptionCode = ex switch
            {
                DomainException domainException when !string.IsNullOrWhiteSpace(domainException.ErrorCode) => domainException.ErrorCode,

                PublicHistoryService.Application.Exceptions.ApplicationException applicationException
                    when !string.IsNullOrWhiteSpace(applicationException.ErrorCode) => applicationException.ErrorCode,

                InfrastructureException infrastructureException when !string.IsNullOrWhiteSpace(infrastructureException.ErrorCode)
                    => infrastructureException.ErrorCode,

                _ => exceptionType.Name.Replace("Exception", string.Empty)
            };

            _errorCodes.TryAdd(exceptionType, exceptionCode);

            return exceptionCode;
        }
    }
}
