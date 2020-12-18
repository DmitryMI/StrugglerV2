using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StrugglerV2
{
    public class NotSelectableButton : Button
    {
        public NotSelectableButton()
        {
            SetStyle(ControlStyles.Selectable, false);
        }
    }
}
