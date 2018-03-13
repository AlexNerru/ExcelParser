using System.Collections.Generic;
using System.Windows.Controls;

namespace LibraryLib
{
    /// <summary>
    /// Express validator to check interface objects
    /// </summary>
    public interface IValidator
    {
        bool AreBoxesNotEmpty(List<TextBox> boxes);
        bool AreBoxesTextIsDouble(List<TextBox> boxes);
        bool AreBoxesTextIsInt(List<TextBox> boxes);
    }
}