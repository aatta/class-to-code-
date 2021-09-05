using CD2C.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CD2C.Common
{
    public class DataMemberModel : ICloneable
    {
        public ScopeEnum Scope { get; set; }
        public TypeEnum Type { get; set; }
        public string Name { get; set; }

        public object Clone()
        {
            return new DataMemberModel
            {
                Scope = this.Scope,
                Type = this.Type,
                Name = this.Name
            };
        }
    }
}
