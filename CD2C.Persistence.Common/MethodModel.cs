using CD2C.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DemoApp.Persistence.Common
{
    public class MethodModel
    {
        public ScopeEnum Scope { get; set; }
        public TypeEnum ReturnType { get; set; }
        public string Name { get; set; }

        public List<InputParam> Parameters { get; set; }
    }

    public class InputParam
    {
        public TypeEnum Type { get; set; }
        public string Name { get; set; }
    }
}
