using DiagramDesigner;
using System.Linq;
using System.Text;

namespace CD2C
{
    public class CodeGeneratorService : ICodeGeneratorService
    {
        public string GenerateCode(DiagramViewModel diagramViewModel)
        {
            var sbCode = new StringBuilder();

            foreach (var persistItemVM in diagramViewModel.Items.OfType<ClassDesignerItemViewModel>())
            {
                //PersistDesignerItem persistDesignerItem = new PersistDesignerItem(persistItemVM.Id, persistItemVM.Left, persistItemVM.Top, persistItemVM.ItemWidth, persistItemVM.ItemHeight, persistItemVM.HostUrl);
                //wholeDiagram.DesignerItems.Add(new DiagramItemData(persistDesignerItem.Id, typeof(PersistDesignerItem)));
            }

            return sbCode.ToString();
        }
    }
}
