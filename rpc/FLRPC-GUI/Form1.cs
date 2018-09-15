using System;
using System.Windows.Forms;

namespace FLRPC_GUI
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            
            textBox1.AppendText("Thread Started! \n");
            if (FL_RPC.Csecret)
            {
                button1.Text = "Toggle Secret Mode (on)";
                textBox1.AppendText("Secret mode is on \n");
            }
            else if (!FL_RPC.Csecret)
            {
                button1.Text = "Toggle Secret Mode (off)";
                textBox1.AppendText("Secret mode is off \n");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.AppendText("Stopping thread... \n");
            FL_RPC.Stop();
            Environment.Exit(0);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (FL_RPC.Psecret)
            {
                FL_RPC.Csecret = false;
                button1.Text = "Toggle Secret Mode (off)";
                textBox1.AppendText("Secret mode is off \n");
            }
            else if (!FL_RPC.Csecret) {
                FL_RPC.Csecret = true;
                button1.Text = "Toggle Secret Mode (on)";
                textBox1.AppendText("Secret mode is on \n");
            }

        }
    }
}
