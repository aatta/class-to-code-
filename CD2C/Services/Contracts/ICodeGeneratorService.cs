using CD2C.Common;
using DiagramDesigner;
using System.Collections.Generic;

namespace CD2C
{
    public interface ICodeGeneratorService
    {
        string GenerateCode(DiagramViewModel diagramViewModel);
        string GenerateClass(string className, string parentClassName, ConnectionTypeEnum connectionType, MultiplicityTypeEnum multiplicityType, IEnumerable<MethodModel> methods, IEnumerable<DataMemberModel> dataMembers);
    }
}
