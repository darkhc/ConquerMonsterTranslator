using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ConquerMonsterTranslator
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public static string TempData;
        public static string NameMonsters;
        public static string NameMonstersES;
        public static string[] NameMonstersSQL;
        public static string NameMonstersSQLX;
        public static string IDMonster;
        public static string OldNameMonsterForEdit;
        public static string OldLookFaceForEdit;
        public static string NameMonsterForEdit;
        public static string LookFaceForEdit;
        public static string LineNameMonsterForEdit;
        public static string LineLookFaceForEdit;
        public static string LineForSave;

        public void TranslateSQL(string Path)
        {
            string[] tempdata = System.IO.File.ReadAllLines(Path);
            foreach (string tempdatax in tempdata)
            {
                string[] readsql = tempdatax.Split(',');
                IDMonster = readsql[0].Substring(readsql[0].IndexOf("'"), readsql[0].Length - readsql[0].IndexOf("'"));
                SearchNameES(NameMonsterForEdit, LookFaceForEdit);
                OldNameMonsterForEdit = readsql[1];
                OldLookFaceForEdit = readsql[4];
                readsql[1] = "'" + NameMonsterForEdit.Trim(new Char[] { '[', ']' }) + "'";
                readsql[4] = "'" + LookFaceForEdit.Trim('M', 'a', 'x', 'L', 'i', 'f', 'e', '=') + "'";
                LineNameMonsterForEdit = readsql[1];
                LineLookFaceForEdit = readsql[4];
                LineForSave += tempdatax + Environment.NewLine;
                LineForSave = LineForSave.Replace(OldNameMonsterForEdit, LineNameMonsterForEdit);
                LineForSave = LineForSave.Replace(OldLookFaceForEdit, LineLookFaceForEdit);
            }
            System.IO.File.WriteAllText(Path, LineForSave);
        }

        public void SearchNameES(string value, string value2)
        {
            int n = 0;
            string[] tempdata = System.IO.File.ReadAllLines("Files/MonsterES.txt");
            foreach (string tempdatax in tempdata)
            {
                if (tempdatax.Contains("TypeID=" + IDMonster.Replace("'","")))
                {
                    int n2 = n - 12;
                    value = tempdata[n2];
                    value2 = tempdata[n2+3];
                }
                n++;
            }
            NameMonsterForEdit = value;
            LookFaceForEdit = value2;
        }

        public void LoadNamesSQL()
        {
            string[] tempdata = System.IO.File.ReadAllLines(textBox1.Text);
            foreach (string tempdatax in tempdata)
            {
                string[] readsql = tempdatax.Split(',');
                readsql[1] = readsql[1].Replace("'", "");
                listBox3.Items.Add(readsql[1].Trim());
            }
        }

        public void LoadNames(string Path, ListBox listBox)
        {
            string[] tempdata = System.IO.File.ReadAllLines(Path);
            foreach (string tempdatax in tempdata)
            {
                if (tempdatax.Contains("[") && tempdatax.Contains("]"))
                {
                    TempData = tempdatax;
                    TempData = TempData.Replace("[", "");
                    TempData = TempData.Replace("]", "");
                    NameMonsters = Environment.NewLine + TempData;
                    listBox.Items.Add(NameMonsters.Trim());
                    NameMonstersES += NameMonsters;
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            TranslateSQL(textBox1.Text);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LoadNames("Files/MonsterES.txt", listBox2);
            LoadNamesSQL();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            LoadNames("Files/MonsterES.txt", listBox2);
            LoadNamesSQL();
        }
    }
}
