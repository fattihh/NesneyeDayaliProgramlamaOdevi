using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NDPÖDEV
{
    public partial class Form1 : Form
    {
        private string SecilenSekil1; //Secilecek sekil icin stringler
        private string SecilenSekil2;


        private Sekil sekil1;//Classlardan secicelecek nesneler için degiskenler
        private Sekil sekil2;


        private List<string> SekilListesi = new List<string>();//Stringleri tutmak için bir liste

        public Form1()
        {
            InitializeComponent();

            Text = "Çarpışma Uygulaması";


            panel1.Paint += panel1_Paint;
            //Combobox için liste
            SekilListesi = new List<string> { "Nokta", "Cember", "Küre", "Kare", "Küp", "Yüzey", "Dikdörtgen", "Dikdörtgen Prizma" };

            groupBox1.Enter += groupBox1_Enter;
            groupBox2.Enter += groupBox2_Enter;

            comboBox1.SelectedIndexChanged += comboBox1_SelectedIndexChanged;

            comboBox2.SelectedIndexChanged += comboBox2_SelectedIndexChanged;

            //Comboboxa liste ekleme
            comboBox1.Items.AddRange(SekilListesi.ToArray());

            comboBox2.Items.AddRange(SekilListesi.ToArray());

            SecilenSekil1 = "";

            SecilenSekil2 = " ";

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            //Eger sekil1 ve sekil2 null degilse sekil1 ve sekil2 yi cizdir
            if (sekil1 != null)
            {
                sekil1.Cizdir(g);
            }

            if (sekil2 != null)
            {
                sekil2.Cizdir(g);
            }

            Invalidate();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {
            //hem comboboxa hem buraya ekledigimiz icin bir clear metodu.
            comboBox1.Items.Clear();
            //Listeyi grupbox'a eklemek
            comboBox1.Items.AddRange(SekilListesi.ToArray());

        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {
            //hem comboboxa hem buraya ekledigimiz icin bir clear metodu.
            comboBox2.Items.Clear();
            //Listeyi comboboxa eklemek
            comboBox2.Items.AddRange(SekilListesi.ToArray());
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

            //sekil1'i secilen sekile esitledim ve secilen sekili comboboxtan ona göre seçtim.
            sekil1 = SekilCiz(SecilenSekil1);

            SecilenSekil1 = comboBox1.SelectedItem.ToString();

            panel1.Invalidate();

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

            //sekil1'i secilen sekile esitledim ve secilen sekili comboboxtan ona göre seçtim.

            sekil2 = SekilCiz(SecilenSekil2);

            SecilenSekil2 = comboBox2.SelectedItem.ToString();

            panel1.Invalidate();


        }

        private Sekil SekilCiz(string sekil)
        {
            //secilen sekil stringini bulmak icin ve ona göre nesne döndürmek icin switch case yapısı.
            switch (sekil)
            {
                case "Kare":
                    return new Kare();
                case "Küp":
                    return new Küp();
                case "Cember":
                    return new Cember();
                case "Küre":
                    return new Küre();
                case "Dikdörtgen":
                    return new Dikdörtgen();
                case "Dikdörtgen Prizma":
                    return new DikdörtgenPrizma();
                case "Yüzey":
                    return new Yüzey();
                case "Nokta":
                    return new Nokta();
                default:
                    return null;

            }


        }

        private void button1_Click(object sender, EventArgs e)
        {

            if (sekil1.Carpisma(sekil2))
            {
                MessageBox.Show("Şekiller Çarpıştı!");
            }
            else 
            {
                MessageBox.Show("Şekiller Çarpışmadı.");
            }
        }
    }
}
