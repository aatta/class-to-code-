using CD2C.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DemoApp.Persistence.Common
{
    public class DataMemberModel
    {
        public ScopeEnum Scope { get; set; }
        public TypeEnum Type { get; set; }
        public string Name { get; set; }
    }
}
