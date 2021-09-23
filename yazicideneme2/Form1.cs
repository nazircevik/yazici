using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace yazicideneme2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        List<string> parca = new List<string>();
        List<string> sira = new List<string>();
        private void Form1_Load(object sender, EventArgs e)
        {
            for (int i = 1; i < 9; i++)
            {
                sira.Add("PARCA " + i);
            }
        }
        
      
        private void button1_Click(object sender, EventArgs e)
        {
            parca.Add("deneme1");
            parca.Add("sadasd");
            parca.Add("dsadsa");
            int a = parca.Count;
            for (int i = 0; i < sira.Count-a; i++)
            {
                parca.Add("");
            }
          
            string str = File.ReadAllText("sablon.prn");
            for (int i = 0; i < sira.Count; i++)
            {
                str = str.Replace(sira[i], parca[i]);
            }
           
            File.WriteAllText("cikti.prn",str);
       //     RawPrinterHelper.SendFileToPrinter("TSC TE210", "cikti.prn");
            parca.Clear();
            
        }
    }
}
