﻿
namespace UserService.Infrastructure.Exceptions.Interfaces
{
    public interface IExceptionToResponseMapper
    {
        ExceptionResponse Map(Exception exception);
    }
}
