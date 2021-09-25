using CD2C.Common;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace CD2C.Common
{
    public class MethodModel
    {
        public MethodModel()
        {
            InputParameters = new ObservableCollection<InputParameterModel>();
        }

        public ScopeEnum Scope { get; set; }
        public TypeEnum ReturnType { get; set; }
        public string Name { get; set; }

        public ObservableCollection<InputParameterModel> InputParameters { get; set; }
    }
}
