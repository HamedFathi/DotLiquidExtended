using DotLiquid;
using DotLiquidExtended.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace DotLiquidExtended
{
    public static class Extenions
    {
        private static readonly string _seperator = "%@%";
        public static RenderResult RenderWithValidation(string templateText, object data)
        {
            Template.RegisterFilter(typeof(VariableFilter));
            var tmpl = Template.Parse(templateText);

            if (tmpl.Errors.Any())
            {
                return new RenderResult
                {
                    Template = tmpl,
                    Result = null,
                    Errors = tmpl.Errors
                };
            }

            var vars = tmpl.GetAllNodes()
                .Where(x => x is Variable);

            foreach (Variable item in vars)
            {
                var filters = item.Filters.Select(x => x.Name);

                if (!filters.Contains("ignore_safe_var"))
                {
                    templateText = templateText.Replace(item.Name, $"{item.Name} | safe_var:'{item.Name}'");
                }
            }
            var template2 = Template.Parse(templateText);
            var result = template2.RenderAnonymousObject(data);
            var matches = Regex.Matches(result, $"{_seperator}(.+){_seperator}", RegexOptions.Compiled)
                .Cast<Match>()
                .Select(x => x.Value.Replace(_seperator, "")).Distinct();

            if (matches.Any())
            {
                var exceptions = new List<Exception>();
                foreach (var match in matches)
                {
                    exceptions.Add(new ArgumentNullException($"'{match}' is null or does not exist."));
                }
                return new RenderResult
                {
                    Template = tmpl,
                    Result = null,
                    Errors = exceptions
                };
            }

            return new RenderResult
            {
                Template = tmpl,
                Result = result,
                Errors = null
            };

        }
        public static string RenderAnonymousObject(this Template template, object obj, bool inclueBaseClassProperties = false, IFormatProvider formatProvider = null)
        {
            return template.Render(Hash.FromAnonymousObject(obj, inclueBaseClassProperties), formatProvider);
        }
        public static string RenderObject(this Template template, object obj, bool inclueBaseClassProperties = false, IFormatProvider formatProvider = null)
        {
            return template.Render(Hash.FromAnonymousObject(new { RootObject = obj }, inclueBaseClassProperties), formatProvider);
        }
        public static IEnumerable<object> GetAllNodes(this Template template)
        {
            return ProcessNodes(template.Root.NodeList);

            static IEnumerable<object> ProcessNodes(IEnumerable<object> nodes)
            {
                var result = new List<object>();

                if (nodes != null && nodes.Any())
                {
                    foreach (var node in nodes)
                    {
                        if (node is Tag tag)
                        {
                            var newResult = ProcessNodes(tag.NodeList);
                            result.AddRange(newResult);
                        }
                        result.Add(node);
                    }
                }

                return result;
            }
        }
    }
}
