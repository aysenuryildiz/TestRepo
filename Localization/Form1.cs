using Localization.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Localization
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            var cultureInfo = System.Globalization.CultureInfo.CurrentCulture;
            var lang = cultureInfo.Name;
            Localization.Culture = new CultureInfo(lang);
            writeText();
        }

        private void writeText()
        {
            lblUsername.Text = Localization.lblUsername;
            lblPassword.Text = Localization.lblPassword;
            btnSelectLang.Text = Localization.btnSelectLang;

        }

        private void button1_Click(object sender, EventArgs e)
        {

            Localization.Culture = new CultureInfo("en-US");
            writeText();
        }
    }
}
