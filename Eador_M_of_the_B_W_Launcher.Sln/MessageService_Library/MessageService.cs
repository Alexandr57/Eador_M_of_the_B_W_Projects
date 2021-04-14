using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MessageService_Library
{
    public interface IMessageService
    {
        void MessageError(string text, string caption);
    }

    public class MessageService : IMessageService
    {
        public void MessageError(string text, string caption)
        {
            MessageBox.Show(text, caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
