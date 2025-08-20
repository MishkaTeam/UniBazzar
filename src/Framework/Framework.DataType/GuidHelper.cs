namespace Framework.DataType;

public static class GuidHelper
{
    public static bool IsNullOrDefault(this Guid? id) => id == null || id == Guid.Empty;
    public static bool IsEmpty(this Guid id) => id == Guid.Empty;

}
