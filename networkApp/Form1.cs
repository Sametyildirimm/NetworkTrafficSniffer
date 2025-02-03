using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PacketDotNet;
using PacketDotNet.Ieee80211;
using SharpPcap;

namespace networkApp
{
    public partial class Form1 : Form
    {
        private CaptureDeviceList devices;
        private ICaptureDevice device;

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            devices = CaptureDeviceList.Instance;
            if (devices.Count < 1)
            {
                MessageBox.Show("there is no network adapter");
                return;
            }

            for (int i = 0; i < devices.Count; i++)
            {
                listBox1.Items.Add($"Device {i}: {devices[i].Description}");
            }

            device = devices[3]; // İlk ağı kullan
            device.OnPacketArrival += new PacketArrivalEventHandler(Device_OnPacketArrival);
            device.Open(DeviceModes.Promiscuous);
            device.StartCapture();
            listBox1.Items.Add("Start...");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (device != null)
            {
                device.StopCapture();
                device.Close();
                listBox1.Items.Add("Packet capture stopped.");
            }
        }


        private void Device_OnPacketArrival(object sender, PacketCapture e)
        {
            MessageBox.Show("Packet arrived!");
            var rawPacket = e.GetPacket();
            var packet = Packet.ParsePacket(rawPacket.LinkLayerType, rawPacket.Data);
            var ipPacket = packet.Extract<IPv4Packet>();

            if (ipPacket != null)
            {
                string sourceIP = ipPacket.SourceAddress.ToString();
                string destinationIP = ipPacket.DestinationAddress.ToString();

                var tcpPacket = packet.Extract<TcpPacket>();

                if (tcpPacket != null)
                {
                    
                    string sourcePort = tcpPacket.SourcePort.ToString();
                    string destinationPort = tcpPacket.DestinationPort.ToString();

                 
                    string log = $"Source IP: {sourceIP}, Source Port: {sourcePort} → Destination IP: {destinationIP}, Destination Port: {destinationPort}";
                    Invoke(new Action(() => listBox1.Items.Add(log)));
                }
            }
        }
    }
}
