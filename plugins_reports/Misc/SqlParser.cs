using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Reports
{
    public static class SqlParser
    {
        public static List<string> ParseSqlFields(string sql)
        {
            Dictionary<string, string> map = new Dictionary<string, string>();
            List<string> list = new List<string>();

            int indexOfFromClause = IndexOfFromClause(sql);
            
            string selectClause = null;
            if (indexOfFromClause > 0)
                selectClause = sql.Substring(0, indexOfFromClause);
            else
                selectClause = sql;
            int selectIndexOf = selectClause.IndexOf("select", StringComparison.InvariantCultureIgnoreCase);
            if (selectIndexOf >= 0)
                selectClause = selectClause.Remove(selectIndexOf, "select".Length);
            int distinctIndexOf = selectClause.IndexOf("distinct", StringComparison.InvariantCultureIgnoreCase);
            if (distinctIndexOf >= 0)
                selectClause = selectClause.Remove(distinctIndexOf, "distinct".Length);

            // Удаление закомментированных блоков
            var blockComments = @"/\*(.*?)\*/";
            selectClause = Regex.Replace(selectClause,
                blockComments,
                me =>
                {
                    if (me.Value.StartsWith("/*"))
                        return string.Empty;
                    return me.Value;
                },
                RegexOptions.Singleline);

            string[] words = selectClause.Split(',');
            int brackets = 0, apostrophes = 0;
            int i = 1; string fieldPart = null;

            foreach (string word in words)
            {
                if (word.TrimStart().StartsWith("--")) continue;
                foreach (char c in word)
                {
                    if (c == '\'')
                    {
                        if (i % 2 != 0)
                            apostrophes++;
                        else
                            apostrophes--;
                        i++;
                    }
                    else if (c == '(')
                    {
                        brackets++;
                    }
                    else if (c == ')')
                    {
                        brackets--;
                    }
                }

                if (brackets == 0 && apostrophes == 0)
                {
                    list.Add(string.Concat(fieldPart, word).Trim());
                    fieldPart = null;
                }
                else
                {
                    fieldPart = string.Concat(fieldPart, word);
                }
            }

            foreach (var field in list)
                map[GetFieldAlias(field)] = field;

            return new List<string>(map.Keys);
        }


        private static string GetFieldAlias(string field)
        {
            string fieldAlias;
            int indexOfAsClause = field.LastIndexOf("as", StringComparison.InvariantCultureIgnoreCase);
            if (indexOfAsClause > 0)
                fieldAlias = field.Substring(indexOfAsClause + 3, field.Length - indexOfAsClause - 3);
            else if (field.IndexOf("count", StringComparison.InvariantCultureIgnoreCase) >= 0)
                fieldAlias = field;
            else
                fieldAlias = field.Substring(field.IndexOf('.') + 1, field.Length - field.IndexOf('.') - 1);
            return fieldAlias ?? string.Empty;
        }


        private static int IndexOfFromClause(string sql)
        {
            var blockBrackets = @"\((.*?)\)";
            string modifiedSql = Regex.Replace(sql,
                blockBrackets,
                me =>
                {
                    if (me.Value.IndexOf("from", StringComparison.InvariantCultureIgnoreCase) > 0)
                        return Regex.Replace(me.Value, "from", "xxxx", RegexOptions.IgnoreCase);
                    return me.Value;
                },
                RegexOptions.Singleline);

            return modifiedSql.IndexOf("from", StringComparison.InvariantCultureIgnoreCase);
        }
    }
}
