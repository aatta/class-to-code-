using System.Collections.ObjectModel;

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
