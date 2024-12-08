using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using SiradigCalc.Core.Entities;

namespace SiradigCalc.Infra.Persistence;

public static class LogBuilder
{
    internal static ChangeLog BuildLog(EntityEntry entry, DateTime datetime, string user = "system")
    {
        var entityName = entry.Entity.GetType().Name;

        var changes = entry.State == EntityState.Deleted
            ? $"Entity deleted. Original values: {SerializeObject(PropertyValuesToDict(entry, entry.OriginalValues))}"
            : SerializeObject(entry.Properties.ToDictionary(p => p.Metadata.Name, p => new
            {
                Original = entry.State == EntityState.Added ? null : entry.OriginalValues[p.Metadata.Name],
                Current = PropertyValuesToDict(entry, entry.CurrentValues)
            }));


        var log = new ChangeLog
        {
            EntityName = entityName,
            Action = entry.State.ToString(),
            PrimaryKey = GetPrimaryKey(entry),
            Changes = changes,
            Timestamp = datetime,
            User = user
        };

        return log;
    }

    private static object PropertyValuesToDict(EntityEntry entry, PropertyValues currentValues)
        => currentValues.Properties.ToDictionary(p => p.Name, p => entry.OriginalValues[p.Name]);

    internal static string SerializeObject(object obj)
        => System.Text.Json.JsonSerializer.Serialize(obj);

    internal static string GetPrimaryKey(EntityEntry entry)
    {
        var key = entry.Metadata.FindPrimaryKey();
        if (key == null) return "Unknown";

        var keyValues = key.Properties.Select(p => entry.Property(p.Name).CurrentValue).ToArray();
        return string.Join(",", keyValues);
    }
}