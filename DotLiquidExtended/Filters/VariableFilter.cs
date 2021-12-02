namespace DotLiquidExtended.Filters
{
    internal static class VariableFilter
    {
        internal static string SafeVar(string input, string tagName)
        {
            if (input == null)
            {
                return $"%@%{tagName}%@%";
            }
            return input;
        }

        internal static string IgnoreSafeVar(string input)
        {
            return input;
        }
    }
}
