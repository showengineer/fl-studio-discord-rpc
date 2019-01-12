using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;
using FLRPC;
using FLRPC.Helpers;

namespace FLRPC_GUI
{
    public partial class settings : Form
    {
        public XmlSettings xml;
        private void GetSettingsAndFillControls()
        {
            xml = FL_RPC.ReadSettings();
            noPrj_txt.Text = xml.NoNameMessage;
            SecMode_txt.Text = xml.SecretMessage;
            if (xml.Secret)
            {
                sec_mode_sel.Checked = true;
                def_mode_selector.Checked = false;
            }
            else if (!xml.Secret)
            {
                sec_mode_sel.Checked = false;
                def_mode_selector.Checked = true;
            }
            debugLevel_sel.Value = (int)xml.logLevel;
            numericUpDown1.Value = xml.RefeshInterval/1000;
            textBox1.Text = xml.ClientID;
            numericUpDown2.Value = xml.Pipe;
            
        }
        public settings()
        {
            InitializeComponent();
            GetSettingsAndFillControls();
        }
        private void SaveSettings()
        {
            XmlSettings settings = new XmlSettings();
            settings.ClientID = textBox1.Text;
            settings.RefeshInterval = (int)numericUpDown1.Value * 1000;
            settings.Pipe = (int)numericUpDown2.Value;
            settings.logLevel = (DiscordRPC.Logging.LogLevel)debugLevel_sel.Value;
            settings.SecretMessage = SecMode_txt.Text;
            settings.NoNameMessage = noPrj_txt.Text;
            if(sec_mode_sel.Checked && !def_mode_selector.Checked)
                settings.Secret = true;
            else if(!sec_mode_sel.Checked && def_mode_selector.Checked)
                settings.Secret = false;
            settings.AcceptedWarning = dangerZone_enable.Checked;
            FL_RPC.SaveToXml(settings);
        }

        private void dangerZone_enable_CheckedChanged(object sender, EventArgs e)
        {
            if (dangerZone_enable.Checked){
                if(!xml.AcceptedWarning)
                {
                    DialogResult r = MessageBox.Show("You are entering the Danger Zone! Changing these settings without knowing what they do can break the program! Do you accept the risks?", "DANGER ZONE!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (r != DialogResult.Yes)
                    {
                        dangerZone_enable.Checked = false;
                        return;
                    }
                }
                groupBox3.Enabled = true;
                xml.AcceptedWarning = true;
                SaveSettings();
            }
            else
            {
                groupBox3.Enabled = false;
                xml.AcceptedWarning = false;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try {
                SaveSettings();
                DialogResult r = MessageBox.Show("Settings were saved! Do you wish to restart the RPC now?", "Success!", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if(r == DialogResult.Yes)
                {
                    FL_RPC.Stop();
                    //Give it some time to rest
                    //Thread.Sleep(2000);

                    //Reboot
                    Thread t = new Thread(FL_RPC.Init);
                    t.Start();
                }
            } catch(Exception exx)
            {
                MessageBox.Show("An error occured: " + exx.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
