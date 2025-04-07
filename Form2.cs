using System;
using System.Linq;
using System.Windows.Forms;
using NAudio.CoreAudioApi;
using NAudio.Wave;
using System.Runtime.InteropServices;
using System.Drawing;

namespace MicroSW
{
    public partial class Form2 : Form
    {



        static MMDevice mic;
        Form1 forme;

        public Form2(Form1 forms)
        {
            forme = forms;
            InitializeComponent();

            RegisterHotKey(this.Handle, 1, 0, (int)Keys.F5);

        }

        public static void UpdateDevices(string input)
        {
            

            mic = GetMicrophoneByName(input);
        }

        private void ToggleStreaming()
        {
            bool isMuted = mic.AudioEndpointVolume.Mute;
            if (!isMuted) {
                pictureBox1.Image = Image.FromFile("off.png");
                mic.AudioEndpointVolume.Mute = true;
            }
            else { pictureBox1.Image = Image.FromFile("on.png");
                mic.AudioEndpointVolume.Mute = false;
            }
            

        }

        static MMDevice GetMicrophoneByName(string micName)
        {
            var enumerator = new MMDeviceEnumerator();
            foreach (var device in enumerator.EnumerateAudioEndPoints(DataFlow.Capture, DeviceState.Active))
            {
                if (device.FriendlyName.ToLower().Contains(micName.ToLower()))
                {
                    return device; 
                }
            }
            return null; 
        }

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == 0x0312)
            {
                ToggleStreaming();
            }
            base.WndProc(ref m);
        }
        
            
               
        

        private void Form2_Load(object sender, EventArgs e) { }

        [DllImport("user32.dll")]
        private static extern bool RegisterHotKey(IntPtr hWnd, int id, int fsModifiers, int vk);

        [DllImport("user32.dll")]
        private static extern bool UnregisterHotKey(IntPtr hWnd, int id);

        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {
           
            mic.AudioEndpointVolume.Mute = false;
            forme.Close();
        }

        private void Form2_FormClosed(object sender, FormClosedEventArgs e)
        {

            mic.AudioEndpointVolume.Mute = false;
            forme.Close();
        }
    }
}
