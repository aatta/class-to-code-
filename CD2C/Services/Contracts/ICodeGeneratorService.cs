using DiagramDesigner;

namespace CD2C
{
    public interface ICodeGeneratorService
    {
        string GenerateCode(DiagramViewModel diagramViewModel);
    }
}
