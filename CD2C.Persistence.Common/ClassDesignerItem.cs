using System.Collections.Generic;

namespace CD2C.Common
{
    public class ClassDesignerItem : DesignerItemBase
    {
        public ClassDesignerItem(int id, double left, double top, double itemWidth, double itemHeight, string setting1)
            : base(id, left, top, itemWidth, itemHeight)
        {
            this.Setting1 = setting1;

            Methods = new List<string>();

            for (int i = 1; i <= 10; i++)
            {
                Methods.Add(string.Format("Method{0}", i));
            }
        }

        public string Setting1 { get; set; }

        public List<string> Methods { get; set; }
    }
}
