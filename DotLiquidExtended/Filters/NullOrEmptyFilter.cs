namespace DotLiquidExtended.Filters
{
    public static class DotLiquidCustomFilters
    {
        public static string IsNullOrEmpty(string input)
        {
            return string.IsNullOrEmpty(input) ? "true" : "false";
        }
    }
}
