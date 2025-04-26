using System.Security.Claims;

namespace Domain.Aggregates.Users.Enums;

public struct RoleType
{
    public static Guid Programmer = Guid.Parse("C270E450-F50B-41C1-91FC-1A7B7AB2C1BC");
    public static Guid Administrator = Guid.Parse("80559B01-9629-4809-8CD7-5D2B694101C1");
    public static Guid PosCounter = Guid.Parse("B1C9EBBD-13CD-4853-9742-D55895174B0C");
    public static Guid Customer = Guid.Parse("AA58C5EB-4AC2-4128-8002-CE739757C6FB");

    public static bool Contains(Guid guid)
    {
        return guid == Programmer || guid == Administrator || guid == PosCounter || guid == Customer;
    }

    public static string[] GetAdminRoles()
    {
        return [Programmer.ToString(), Administrator.ToString()];
    }

    public static string[] GetPosRoles()
    {
        var adminRoles = GetAdminRoles();
        var posRoles = adminRoles.Append(PosCounter.ToString());
        return [.. posRoles];
    }
}
