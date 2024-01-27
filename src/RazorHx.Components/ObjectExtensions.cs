using System.ComponentModel;

namespace RazorHx.Components;

public static class ObjectExtensions {
    public static Dictionary<string, object?> ToDictionary(this object? values) {
        return values is null
            ? new Dictionary<string, object?>()
            : TypeDescriptor.GetProperties(values).Cast<PropertyDescriptor>().ToDictionary(property => property.Name, property => property.GetValue(values));
    }
}