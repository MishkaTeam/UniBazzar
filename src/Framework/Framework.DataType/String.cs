using System.Text.RegularExpressions;

namespace Framework.DataType;

public static class String 
{
    static String()
    {
    }
    public static bool IsValidNationalCode(this string nationalCode)
    {
       
        if (string.IsNullOrWhiteSpace(nationalCode) || nationalCode.Length != 10)
            return false;

        if (!nationalCode.All(char.IsDigit))
            return false;

        var allSame = nationalCode.Distinct().Count() == 1;
        if (allSame)
            return false;

        int checksum = 0;
        for (int i = 0; i < 9; i++)
        {
            checksum += (int)char.GetNumericValue(nationalCode[i]) * (10 - i);
        }
        int remainder = checksum % 11;
        int controlDigit = (int)char.GetNumericValue(nationalCode[9]);

        return (remainder < 2 && controlDigit == remainder) || (remainder >= 2 && controlDigit == 11 - remainder);
    }
    public static bool IsValidPostalCode(this string postalCode)
    {
       
        if (string.IsNullOrWhiteSpace(postalCode) || postalCode.Length != 10)
            return false;
      
        if (!postalCode.All(char.IsDigit))
            return false;
        
        var allSame = postalCode.Distinct().Count() == 1;
        if (allSame)
            return false;

        return true;
    }
    public static bool IsValidEmail(this string email)
    {
        if (string.IsNullOrWhiteSpace(email))
            return false;

        var emailRegex = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
        return Regex.IsMatch(email, emailRegex);
    }
    public static string? Fix(this string? value)
    {
        if (string.IsNullOrWhiteSpace(value: value))
        {
            return null;
        }

        value =
            value.Trim();

        while (value.Contains(value: "  "))
        {
            value = value.Replace
                (oldValue: "  ", newValue: " ");
        }

        return value;
    }
    public static bool IsValidMobile(this string mobile)
    {
        var mobileRegex = @"^09\d{9}$";
        return Regex.IsMatch(mobile, mobileRegex);
    }
    public static bool IsValidPassword(this string password)
    {
        if (string.IsNullOrWhiteSpace(password) || password.Length < 8)
            return false;

        var passwordRegex = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&#])[A-Za-z\d@$!%*?&#]{8,}$";
        return Regex.IsMatch(password, passwordRegex);
    }

    public static string? ConvertDigitsToUnicode(this object? value)
    {
        if (value is null)
        {
            return null;
        }

        var valueString =
            value.ToString();

        if (valueString is null)
        {
            return null;
        }

        var currentUICultureName =
            System.Threading.Thread.CurrentThread
            .CurrentUICulture.Parent.Name.ToUpper();

        switch (currentUICultureName)
        {
            case "FA":
            case "AR":
                {
                    valueString =
                        valueString
                        .Replace(oldChar: '0', newChar: '۰')
                        .Replace(oldChar: '1', newChar: '۱')
                        .Replace(oldChar: '2', newChar: '۲')
                        .Replace(oldChar: '3', newChar: '۳')
                        .Replace(oldChar: '4', newChar: '۴')
                        .Replace(oldChar: '5', newChar: '۵')
                        .Replace(oldChar: '6', newChar: '۶')
                        .Replace(oldChar: '7', newChar: '۷')
                        .Replace(oldChar: '8', newChar: '۸')
                        .Replace(oldChar: '9', newChar: '۹')
                        ;

                    return valueString;
                }

            default:
                {
                    return valueString;
                }
        }
    }
}
