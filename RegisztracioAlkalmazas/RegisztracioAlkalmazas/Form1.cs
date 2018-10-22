﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RegisztracioAlkalmazas
{
    public partial class FormMain : System.Windows.Forms.Form
    {
        public string szulDat;

        public FormMain()
        {
            InitializeComponent();
            szulDat = dtpSzulDatum.Value.ToString();
        }

        private void btnUjHobbiAd_Click(object sender, EventArgs e)
        {
            if (tbUjHobbi.Text.ToString() != "")
            {
                if (VanEIlyenHobbi(tbUjHobbi.Text.ToString()) == false)
                {
                    lbHobbik.Items.Add(tbUjHobbi.Text.ToString());
                    tbUjHobbi.Text = "";
                }
                else
                {
                    MessageBox.Show("Már létezik ilyen hobbi.", "Hiba");
                }

            }
            else
            {
                MessageBox.Show("Nem adtál meg értéket a hobbi felvételéhez.", "Hiba");
            }
        }

        public bool VanEIlyenHobbi(string vanEIlyen)
        {
            int i = 0;
            bool igazE = false;
            while (lbHobbik.Items.Count > i && igazE == false)
            {
                if (lbHobbik.Items[i].ToString() == vanEIlyen)
                {
                    igazE = true;
                }
                i++;
            }
            return igazE;
        }

        private void btnMentes_Click(object sender, EventArgs e)
        {
            string adatMentes = "";
            if (tbNev.Text.ToString() != "" && dtpSzulDatum.Value.ToString() != szulDat && (rbFerfi.Checked == true || rbNo.Checked == true) && lbHobbik.SelectedItem != null)
            {
                adatMentes += "Név: " + tbNev.Text.ToString() + "\nSzületési dátum:";
                string[] adat = dtpSzulDatum.Value.ToString().Split(' ');

                for (int i = 0; i < 3; i++)
                {
                    adatMentes += " " + adat[i];
                }
                adatMentes += "\nNem: ";
                if (rbFerfi.Checked == true)
                {
                    adatMentes += "Férfi";
                }
                else
                {
                    adatMentes += "Nő";
                }
                if (lbHobbik.Items.Count > 1)
                {
                    adatMentes += "\nHobbik: " + lbHobbik.Items[0];
                    for (int i = 1; i < lbHobbik.Items.Count; i++)
                    {
                        adatMentes += ", " + lbHobbik.Items[i];
                    }
                }
                adatMentes += "\nKedvenc hobbi: " + lbHobbik.SelectedItem.ToString();

                try
                {
                    StreamWriter kiir = new StreamWriter("reg_adatok.txt");
                    kiir.WriteLine(adatMentes);
                    kiir.Close();
                    MessageBox.Show("Sikeres mentés!\n");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Hiba: " + ex);
                }
            }
            else
            {
                List<string> hianyok = new List<string>();
                if (tbNev.Text.ToString() == "")
                {
                    hianyok.Add("a nevet");
                }
                if (dtpSzulDatum.Value.ToString() == szulDat)
                {
                    hianyok.Add("a születési dátumot");
                }
                if (rbFerfi.Checked == false && rbNo.Checked == false)
                {
                    hianyok.Add("a nemet");
                }
                if (lbHobbik.SelectedItem == null)
                {
                    hianyok.Add("a hobbit");
                }

                string nincsKitoltve = "Nem adtad meg ";

                nincsKitoltve += hianyok[0];
                if (hianyok.Count > 2)
                {
                    for (int i = 1; i < hianyok.Count - 1; i++)
                    {
                        nincsKitoltve += ", " + hianyok[i];
                    }
                    nincsKitoltve += " és " + hianyok[hianyok.Count-1];
                }
                else if (hianyok.Count==2)
                {
                    nincsKitoltve += " és " + hianyok[1];
                }
                
                MessageBox.Show(nincsKitoltve + ".", "Hiba");
            }
        }
    }
}
