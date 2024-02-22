using Microsoft.EntityFrameworkCore.Diagnostics;
using System.Data.Common;

namespace BookStore.Infrastructure.DAL.Interceptors;

internal class SillyEFCoreInterceptor : IDbCommandInterceptor
{
    public DbDataReader ReaderExecuted(DbCommand command, CommandExecutedEventData eventData, DbDataReader result)
    {
        return result;
    }
}
