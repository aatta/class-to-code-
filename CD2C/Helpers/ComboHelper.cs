using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CD2C.Helpers.Common
{
    public class ComboData
    {
        public int Id { get; set; }
        public string Text { get; set; }
    }

    public static class ComboHelper
    {
        public static List<ComboData> FromEnum(Type enumType)
        {
            var values = new List<ComboData>();

            foreach (int val in Enum.GetValues(enumType))
            {
                values.Add(new ComboData
                {
                    Id = val,
                    Text = Enum.GetName(enumType, val)
                }) ;
            }

            return values;
        }
    }
}
