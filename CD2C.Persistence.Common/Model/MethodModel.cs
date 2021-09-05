using CD2C.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CD2C.Common
{
    public class MethodModel : ICloneable
    {
        public ScopeEnum Scope { get; set; }
        public TypeEnum ReturnType { get; set; }
        public string Name { get; set; }

        public Dictionary<string, TypeEnum> InputParameters { get; set; }

        public object Clone()
        {
            var iParams = this.InputParameters;

            if (iParams != null)
            {
                iParams = iParams.ToDictionary(k => k.Key, v => v.Value);
            }


            return new MethodModel
            {
                Scope = this.Scope,
                ReturnType = this.ReturnType,
                Name = this.Name,
                InputParameters = iParams
            };
        }
    }
}
