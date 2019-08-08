using System;
using ZendeskCustom.Models.Ticket;
using System.Collections.Generic;
using System.Threading;
using System.Windows.Forms;

namespace ZendeskCustom
{
    public class SelectFile
    {
        public string fileSelected { get; set; }
        public void FileSelect()
        {
            using (OpenFileDialog openFileDialog1 = new OpenFileDialog())
            {
                var t = new Thread((ThreadStart)(() =>
                {
                    openFileDialog1.InitialDirectory = "c:\\";
                    openFileDialog1.Filter = "png files (*.png)|*.png|All files (*.*)|*.*";
                    openFileDialog1.FilterIndex = 2;
                    openFileDialog1.RestoreDirectory = true;
                    if (openFileDialog1.ShowDialog() == DialogResult.OK)
                    {
                        fileSelected = openFileDialog1.FileName;
                    }
                    else
                    {
                        fileSelected = String.Empty;
                    }

                }));
                t.SetApartmentState(ApartmentState.STA);
                t.Start();
                t.Join();
                fileSelected = openFileDialog1.FileName;
            }
        }
    }
}