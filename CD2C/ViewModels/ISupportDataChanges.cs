using System.Windows.Input;

namespace CD2C
{
    public interface ISupportDataChanges
    {
        ICommand ShowDataChangeWindowCommand { get; }
    }
}
