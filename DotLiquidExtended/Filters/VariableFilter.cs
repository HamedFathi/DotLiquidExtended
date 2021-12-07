namespace DotLiquidExtended.Filters
{
    public static class VariableFilter
    {
        public static string SafeVar(string input, string tagName)
        {

            if (input == null)
            {
                return $"{DotLiquidUtility._indicator}{tagName}{DotLiquidUtility._indicator}";
            }
            return input;
        }

        public static string IgnoreSafeVar(string input)
        {
            return input;
        }
    }
}
