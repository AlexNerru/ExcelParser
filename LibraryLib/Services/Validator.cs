using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace LibraryLib
{
    /// <summary>
    /// Help class to validate diffent features
    /// </summary>
    public class Validator : IValidator
    {
        /// <summary>
        /// Checks if all textboxes are not empty
        /// </summary>
        /// <param name="boxes"></param>
        /// <returns></returns>
        public bool AreBoxesNotEmpty (List<TextBox> boxes) =>  boxes.TrueForAll(box => box.Text != string.Empty);

        /// <summary>
        /// Cheks if all textboxes contains int values
        /// </summary>
        /// <param name="boxes"></param>
        /// <returns></returns>
        public bool AreBoxesTextIsInt(List<TextBox> boxes) => boxes.TrueForAll(box => int.TryParse(box.Text, out int a));

        /// <summary>
        /// Cheks if all textboxes contains double values
        /// </summary>
        /// <param name="boxes"></param>
        /// <returns></returns>
        public bool AreBoxesTextIsDouble(List<TextBox> boxes) => boxes.TrueForAll(box => double.TryParse(box.Text, out double a));
    }
}
