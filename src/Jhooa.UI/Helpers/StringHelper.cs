using System.Text.RegularExpressions;

namespace Jhooa.UI.Helpers;

public static partial class StringHelper
{
    public static string StripHtmlTags(this string input)
    {
        return MyRegex().Replace(input, String.Empty);
    }

    [GeneratedRegex("<.*?>")]
    private static partial Regex MyRegex();
}