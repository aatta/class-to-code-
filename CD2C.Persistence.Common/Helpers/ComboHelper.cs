using System;
using System.Collections.Generic;

namespace CD2C.Common.Helpers
{
    public class ComboData<T>
    {
        public T Id { get; set; }
        public string Text { get; set; }
    }

    public static class ComboHelper
    {
        public static List<ComboData<T>> FromEnum<T>() where T : Enum
        {
            var values = new List<ComboData<T>>();

            foreach (T val in Enum.GetValues(typeof(T)))
            {
                values.Add(new ComboData<T>
                {
                    Id = val,
                    Text = Enum.GetName(typeof(T), val)
                });
            }

            return values;
        }
    }
}
