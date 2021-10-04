using CD2C.Common;
using DiagramDesigner;
using System.Collections.Generic;

namespace CD2C
{
    public interface ICodeGeneratorService
    {
        string GenerateCode(DiagramViewModel diagramViewModel);
        string GenerateClass(string className, string parentClassName, IEnumerable<MethodModel> methods, IEnumerable<DataMemberModel> dataMembers);
    }
}
