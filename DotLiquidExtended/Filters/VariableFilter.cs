namespace DotLiquidExtended.Filters
{
    public static class VariableFilter
    {
        public static string SafeVar(string input, string tagName)
        {
            if (input == null)
            {
                return $"%@%{tagName}%@%";
            }
            return input;
        }

        public static string IgnoreSafeVar(string input)
        {
            return input;
        }
    }
}
