using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using yazicideneme2.Properties;

namespace yazicideneme2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        string sqlCon = @"server=192.168.10.211; Database=PEUGEOTDMS ;Connect Timeout=60; User Id=EFES;Password=EFES;";
        private void Form1_Load(object sender, EventArgs e)
        {
            richTextBox1.MaxLength = 147;
          
            for (int i = 0; i < 10; i++)
            {
                comboBox1.Items.Add((Convert.ToInt32(DateTime.Now.Year)-i).ToString());
            }
            comboBox1.SelectedIndex = 0;

            foreach (String yazici in PrinterSettings.InstalledPrinters)       
{
                
                comboBox2.Items.Add(yazici);
                
}
            comboBox2.Text = Settings.Default.Setting;
        }
        string isy = "";
        public string StringReplace(string text)
        {
            text = text.Replace("İ", @"I");
            text = text.Replace("ı", @"i");
            text = text.Replace("Ğ", @"G");
            text = text.Replace("ğ", @"g");
            text = text.Replace("Ö", @"O");
            text = text.Replace("ö", @"o");
            text = text.Replace("Ü", @"U");
            text = text.Replace("ü", @"u");
            text = text.Replace("Ş", "S");
            text = text.Replace("ş", "s");
            text = text.Replace("Ç", @"\80");
            text = text.Replace("ç", @"\87");

            return text;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            
            //  MessageBox.Show(dataGridView1.CurrentRow.Cells[0].Value.ToString());

            var asd = File.ReadAllText("25.prn");

            string str = asd;
            KleimModel kleim = new KleimModel();
            SqlConnection con;
            con = new SqlConnection(sqlCon);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "exec dbo.kleim_Etiket " + textBox1.Text + ",'" + dataGridView1.CurrentRow.Cells[0].Value.ToString() + "','"+comboBox1.SelectedItem.ToString()+"'";
            cmd.Connection = con;
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                kleim.IsemriNo = dr[0].ToString();
                kleim.IsEmriAcilisTarihi = dr[1].ToString();
                kleim.GNO = dr[2].ToString();
                kleim.GarantiGonderimTarihi= dr[3].ToString();
                kleim.Stokkod= dr[4].ToString();
                kleim.StokAdi= dr[5].ToString();
                kleim.StokAdet= dr[6].ToString();
                kleim.GelKm= dr[7].ToString();
                kleim.SasiNO= dr[8].ToString();
                kleim.MotorNo= dr[9].ToString();
                kleim.GarantiBasTar= dr[10].ToString();
                kleim.MAdi= dr[12].ToString();
                kleim.ModelNo= dr[13].ToString();
                isy = dr[14].ToString();
            }

            con.Close();
           // kleim.IsemriNo = "56815561366";
            //kleim.MAdi = "Nazir Çevik";
            //kleim.GNO = "asdsa3424";
            //kleim.ModelNo = "Yeni 5008 GTI 1.5Blue HDi 130hp";
            //kleim.SasiNO = "SŞKD9ASSD8SA978DSA98DF";
            //kleim.GarantiBasTar = "150,3432,532";
            //kleim.IsEmriAcilisTarihi = "123412413431";
            //kleim.GelKm = "153535";
            //kleim.GarantiGonderimTarihi = "garinti g tarih";
            //kleim.Stokkod = "asdadass";
            //kleim.StokAdet = "1";
            //kleim.StokAdi = "Deneme";
            //kleim.Sebep = "asdasdasdsadasas";


            str = str.Replace("1505256412", kleim.IsemriNo);
            str = str.Replace("MADI", kleim.MAdi);
            str = str.Replace("GNO",kleim.GNO);
            str = str.Replace("GT",kleim.GarantiGonderimTarihi);
            str = str.Replace("ModelNo", kleim.ModelNo);
            str = str.Replace("SNO", kleim.SasiNO);
            str = str.Replace("GBT", kleim.GarantiBasTar);
            str = str.Replace("IEA", kleim.IsEmriAcilisTarihi);
            str = str.Replace("GK", kleim.GelKm);
            str = str.Replace("MotorNo", kleim.MotorNo);
            str = str.Replace("GT", kleim.GarantiGonderimTarihi);
            str = str.Replace("SADI", isy);
            str = str.Replace("Parca4","");
            str = str.Replace("Parca3","");
            str = str.Replace("Parca2","");
            str = str.Replace("Parca1","");
            //    str = str.Replace("Parca", kleim.Stokkod + " " + kleim.StokAdi  );
            str = str.Replace("ADET1", "");
            str = str.Replace("ADET2", "");
            str = str.Replace("ADET3", "");
            str = str.Replace("ADET4", "");
            str = str.Replace("ADET", kleim.StokAdet);
            if (richTextBox1.Text.Length > 1)
            {
                if (richTextBox1.TextLength < 49)
                {
                    str = str.Replace("Neden1", richTextBox1.Text.Substring(0, richTextBox1.TextLength));
                
                }
                else
                {
                  
                    str = str.Replace("Neden1", richTextBox1.Text.Substring(0, 49));
                }

            }
            else
            {
                str = str.Replace("Neden1", richTextBox1.Text);
               
            }
            if (richTextBox1.Text.Length > 47)
            {
                str = str.Replace("Neden2", richTextBox1.Text.Substring(49, 50));
              

            }
            else
            {
                str = str.Replace("Neden2", "");
        
            }
            if (richTextBox1.Text.Length > 94)
            {
                str = str.Replace("Neden3", richTextBox1.Text.Substring(99, richTextBox1.TextLength-99));
            

            }
            else
            {


                str = str.Replace("Neden3", "");
           

            }
            str = StringReplace(str);



            int toplengtc = kleim.Stokkod.Length + kleim.StokAdi.Length;
            if (toplengtc > 37)
            {
                int a = 37 - kleim.Stokkod.Length;
                str = str.Replace("Parca"  , kleim.Stokkod.Substring(0, a - 2) + "-" + kleim);
            }
            else
            {
                str = str.Replace("Parca" , kleim.Stokkod + "-" + kleim.StokAdi);
            }

      
            str = StringReplace(str);


            File.WriteAllText("cikti.prn", str);

            for (int i = 0; i < numericUpDown1.Value; i++)
            {
                RawPrinterHelper.SendFileToPrinter(comboBox2.Text, "cikti.prn");
            }
            

            richTextBox1.Text = "";


        }
        int sayac = 0;
        List<Parcalar> parcalars = new List<Parcalar>();
        private void button2_Click(object sender, EventArgs e)
        {
            isy = "";
         
            parcalars = new List<Parcalar>();
            sayac = 0;
            SqlConnection con;
            con = new SqlConnection(sqlCon);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "select AISMLZ0.STOKKOD,USTOK0.ACIKLAMA,AISMLZ0.KULMIK from AISMLZ0,USTOK0 where AISMLZ0.STOKKOD=USTOK0.STOKKOD and ISEMRINO="+textBox1.Text+ " and TAHTIP='G' and USTOK0.SIRKOD=1 and YIL='"+comboBox1.SelectedItem.ToString()+"' ";
            cmd.Connection = con;
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                sayac++;
                Parcalar pa = new Parcalar();
              pa.Stokkod    = dr[0].ToString();
              pa.Aciklama   = dr[1].ToString();
              pa.KulMiktar = dr[2].ToString();
              parcalars.Add(pa);

            }
            dataGridView1.DataSource = parcalars;
            con.Close();
        }
       
        private void button3_Click(object sender, EventArgs e)
        {
            var asd = File.ReadAllText("25.prn");
           
            string str = asd;
            KleimModel kleim = new KleimModel();
            SqlConnection con;
            con = new SqlConnection(sqlCon);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "exec dbo.kleim_Etiket " + textBox1.Text + ",'" + dataGridView1.CurrentRow.Cells[0].Value.ToString() + "','"+comboBox1.SelectedItem.ToString()+"'";
            cmd.Connection = con;
            con.Open();
         
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                kleim.IsemriNo = dr[0].ToString();
                kleim.IsEmriAcilisTarihi = dr[1].ToString();
                kleim.GNO = dr[2].ToString();
                kleim.GarantiGonderimTarihi = dr[3].ToString();
                kleim.Stokkod = dr[4].ToString();
                kleim.StokAdi = dr[5].ToString();
                kleim.StokAdet = dr[6].ToString();
                kleim.GelKm = dr[7].ToString();
                kleim.SasiNO = dr[8].ToString();
                kleim.MotorNo = dr[9].ToString();

                kleim.GarantiBasTar = dr[10].ToString();
                kleim.MAdi = dr[12].ToString();
                kleim.ModelNo = dr[13].ToString();
                isy = dr[14].ToString();

            }
 
            con.Close();
           

            str = str.Replace("1505256412", kleim.IsemriNo);
            str = str.Replace("MADI", kleim.MAdi);
            str = str.Replace("GNO", kleim.GNO);
            str = str.Replace("GT", kleim.GarantiGonderimTarihi);
            str = str.Replace("ModelNo", kleim.ModelNo);
            str = str.Replace("SNO", kleim.SasiNO);
            str = str.Replace("GBT", kleim.GarantiBasTar);

            str = str.Replace("IEA", kleim.IsEmriAcilisTarihi);
            str = str.Replace("GK", kleim.GelKm);
            str = str.Replace("MotorNo", kleim.MotorNo);
            str = str.Replace("GT", kleim.GarantiGonderimTarihi);
            str = str.Replace("SADI",isy);
            str = StringReplace(str);
            richTextBox1.Text = StringReplace(richTextBox1.Text);
           string str2 = str;
            for (int i = 0; i < sayac-5; i++)
            {
                if(i!=0)
                {
                    int toplengtc = parcalars[i].Stokkod.Length + parcalars[i].Aciklama.Length;
                    if(toplengtc>32)
                    {
                        int a = 32 - parcalars[i].Stokkod.Length;
                    str2 = str2.Replace("Parca" + i.ToString(), parcalars[i].Stokkod.Substring(0,a-1) + "-" + parcalars[i].Aciklama);
                    }
                    else
                    {
                        str2 = str2.Replace("Parca" + i.ToString(), parcalars[i].Stokkod + "-" + parcalars[i].Aciklama);
                    }
                    str2 = str2.Replace("ADET" + i.ToString(), parcalars[i].KulMiktar);
                  


                }
            }
            if(sayac>5)
            {
                int toplengtb = parcalars[5].Stokkod.Length + parcalars[5].Aciklama.Length;
                if (toplengtb > 32)
                {
                    int a = 32 - parcalars[5].Stokkod.Length;
                    str2 = str2.Replace("Parca" + parcalars[5].ToString(), parcalars[5].Stokkod.Substring(0, a-1) + "-" + parcalars[5].Aciklama);
                }
                else
                {
                    str2 = str2.Replace("Parca", parcalars[5].Stokkod + "-" + parcalars[5].Aciklama);
                }
               
            str2 = str2.Replace("ADET", parcalars[5].KulMiktar);
            }
            File.WriteAllText("cikti2.prn", str2);
            for (int i = 1; i < sayac; i++)
            {
                if (i <5) 
                {
                    int toplengta = parcalars[i].Stokkod.Length + parcalars[i].Aciklama.Length;
                    if (toplengta > 35)
                    {
                        int a = 35 - parcalars[i].Stokkod.Length;
                        str = str.Replace("Parca" + parcalars[i].ToString(), parcalars[i].Stokkod + "-" + parcalars[i].Aciklama.Substring(0, a - 1));
                    }
                    else
                    {
                        str = str.Replace("Parca" + i.ToString(), parcalars[i].Stokkod + "-" + parcalars[i].Aciklama);
                    }
                    
                str = str.Replace("ADET" + i.ToString(),parcalars[i].KulMiktar);
                }
            }
            str2 = StringReplace(str2);
            str = str.Replace("Parca1", "");
            str = str.Replace("Parca2", "");
            str = str.Replace("Parca3", "");
            str = str.Replace("Parca4", "");


            str = str.Replace("ADET1", "");
            str = str.Replace("ADET2", "");
            str = str.Replace("ADET3", "");
            str = str.Replace("ADET4", "");

            int toplengt = kleim.Stokkod.Length + kleim.StokAdi.Length;
            if (toplengt > 32)
            {
                int a = 32 - kleim.Stokkod.Length;
                str2 = str2.Replace("Parca" + kleim.ToString(), kleim.Stokkod.Substring(0, a-1) + "-" + kleim.StokAdi);
            }
            else
            {
                str = str.Replace("Parca", kleim.Stokkod + "-" + kleim.StokAdi);
            }
        
            str = str.Replace("ADET",kleim.StokAdet);

            if (richTextBox1.Text.Length > 1)
            {
              
                if(richTextBox1.TextLength<49)
                {
                    str = str.Replace("Neden1", richTextBox1.Text.Substring(0, richTextBox1.TextLength));
                    str2 = str2.Replace("Neden1", richTextBox1.Text.Substring(0, richTextBox1.TextLength));
                }
                else
                {
                    str2 = str2.Replace("Neden1", richTextBox1.Text.Substring(0, 49));
                    str = str.Replace("Neden1", richTextBox1.Text.Substring(0, 49));
                }
               
            }
            else
            {
                str = str.Replace("Neden1", "");
                str2 = str2.Replace("Neden1", "");
            }
            if (richTextBox1.Text.Length > 47)
            {
                str = str.Replace("Neden2", richTextBox1.Text.Substring(46, richTextBox1.TextLength - 48));
                str2 = str2.Replace("Neden2", richTextBox1.Text.Substring(46, richTextBox1.TextLength - 48));

            }
            else
            {
                str = str.Replace("Neden2", "");
                str2 = str2.Replace("Neden2", "");
            }
            if (richTextBox1.Text.Length > 94)
            {
                str = str.Replace("Neden3", richTextBox1.Text.Substring(94, richTextBox1.TextLength - 95));
                str2 = str2.Replace("Neden3", richTextBox1.Text.Substring(94, richTextBox1.TextLength - 95));
            }
            else
            {
                str = str.Replace("Neden3", "");
                str2 = str2.Replace("Neden3", "");
            }
            str = StringReplace(str);
            str2= StringReplace(str2);
            File.WriteAllText("cikti2.prn", str2);
            File.WriteAllText("cikti.prn", str);
            for (int i = 0; i < numericUpDown1.Value; i++)
            {
               RawPrinterHelper.SendFileToPrinter(comboBox2.Text, "cikti.prn");
                if(sayac>5)
                {
                    RawPrinterHelper.SendFileToPrinter(comboBox2.Text, "cikti2.prn");
                }
            }
             
            richTextBox1.Text = "";
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            ResourceManager resource = new ResourceManager(typeof(Form1));
            string str = resource.GetString("String1");

           
   


            //Read
            String settingValue = Settings.Default.Setting;
            //Write
            Settings.Default.Setting = comboBox2.SelectedItem.ToString();
            //Write settings to disk
            Settings.Default.Save();
        }
    }

}