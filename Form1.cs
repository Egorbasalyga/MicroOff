using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using NAudio.CoreAudioApi;

namespace MicroSW
{
    public partial class Form1 : Form
    {
        public static string SelectedInput { get; private set; }
        public static string SelectedOutput { get; private set; }

        public Form1()
        {
            InitializeComponent();
            LoadDevices();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
           
        }

        private void LoadDevices()
        {
            var enumerator = new MMDeviceEnumerator();


            var inputDevices = enumerator.EnumerateAudioEndPoints(DataFlow.Capture, DeviceState.Active)
                .Select(d => d.FriendlyName)
                .ToArray();
            comboBoxInput.Items.AddRange(inputDevices);


            var outputDevices = enumerator.EnumerateAudioEndPoints(DataFlow.Render, DeviceState.Active)
                .Where(d => d.FriendlyName.Contains("Virtual") || d.FriendlyName.Contains("Cable") || d.DataFlow == DataFlow.Capture)
                .Select(d => d.FriendlyName)
                .ToArray();


            if (comboBoxInput.Items.Count > 0) comboBoxInput.SelectedIndex = 0;

        }

        private void button1_Click(object sender, EventArgs e)
        {


            SelectedInput = comboBoxInput.SelectedItem.ToString();


            Form2 form2 = new Form2(this);
            form2.Show();
            Form2.UpdateDevices(SelectedInput);
            this.Hide();
           
        }

    }
}
