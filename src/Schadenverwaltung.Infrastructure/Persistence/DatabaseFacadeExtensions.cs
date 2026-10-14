using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Schadenverwaltung.Infrastructure.Persistence;

internal static class DatabaseFacadeExtensions
{
    public static Task<int> NextSequenceValueAsync(this DatabaseFacade database, string sequenceName)
    {
        var quotedName = $"\"{sequenceName}\"";
        return database
            .SqlQuery<int>($"Select nextval({quotedName}::regclass) AS \"Value\"")
            .SingleAsync();
    }
}