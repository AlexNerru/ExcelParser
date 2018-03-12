using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace LibraryLib
{
    public class Validator
    {
        public bool AreBoxesNotEmpty (List<TextBox> boxes) =>  boxes.TrueForAll(box => box.Text != string.Empty);

        public bool AreBoxesTextIsInt(List<TextBox> boxes) => boxes.TrueForAll(box => int.TryParse(box.Text, out int a));

        public bool AreBoxesTextIsDouble(List<TextBox> boxes) => boxes.TrueForAll(box => double.TryParse(box.Text, out double a));
    }
}
