using System.Collections.Concurrent;
using System.Net;
using TopicArticleService.Domain.Exceptions;
using TopicArticleService.Infrastructure.Exceptions.Interfaces;

namespace TopicArticleService.Infrastructure.Exceptions
{
    internal sealed class ExceptionToResponseMapper : IExceptionToResponseMapper
    {
        private static readonly ConcurrentDictionary<Type, string> _errorCodes = new ConcurrentDictionary<Type, string>();

        public ExceptionResponse Map(Exception exception)
            => exception switch
            {
                DomainException ex => new ExceptionResponse(response: new { ErrorCode = GetExceptionCode(ex), 
                    Reason = ex.Message }, statusCode: HttpStatusCode.BadRequest),

                TopicArticleService.Application.Exceptions.ApplicationException ex => new ExceptionResponse(
                    response: new { ErrorCode = GetExceptionCode(ex), Reason = ex.Message }, 
                    statusCode: HttpStatusCode.BadRequest),

                InfrastructureException ex => new ExceptionResponse(response: new { ErrorCode = GetExceptionCode(ex), 
                    Reason = ex.Message }, statusCode: HttpStatusCode.BadRequest),

                _ => new ExceptionResponse(response: new { ErrorCode = "Error", Reason = "There was an server error!" }, 
                        statusCode: HttpStatusCode.InternalServerError)
            };

        private static string GetExceptionCode(Exception ex)
        {
            var exceptionType = ex.GetType();

            if(_errorCodes.TryGetValue(exceptionType, out var code))
            {
                return code;
            }

            var exceptionCode = ex switch
            {
                DomainException domainException when !string.IsNullOrWhiteSpace(domainException.ErrorCode) => domainException.ErrorCode,

                TopicArticleService.Application.Exceptions.ApplicationException applicationException 
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
 