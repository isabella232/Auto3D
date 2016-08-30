using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MediaPortal.Profile;
using System.IO;
using System.Reflection;
using MediaPortal.Configuration;
using System.IO.Ports;
using System.Xml;
using System.Text.RegularExpressions;
using System.Management;

namespace MediaPortal.ProcessPlugins.Auto3D.Devices
{
    public partial class LogitechHarmonySetup : UserControl, IAuto3DSetup
    {
        LogitechHarmony _device;

        public LogitechHarmonySetup(IAuto3D device)
        {
            InitializeComponent();

            if (!(device is LogitechHarmony))
                throw new Exception("Auto3D: Device is no Generic Device");

            _device = (LogitechHarmony)device;

        }

        public IAuto3D GetDevice()
        {
            return _device;
        }

        public void LoadSettings()
        {
            _device.SelectedDeviceModel = _device.DeviceModels[0];

            textBoxUserName.Text = _device.UserName;
            textBoxHostName.Text = _device.HostName;
        }

        public void SaveSettings()
        {
            _device.SaveSettings();
        }

        //TODO: Implement that maybe?
        private void buttonPingGenericDevice_Click(object sender, EventArgs e)
        {
            if (_device.IsOn())
            {
                Auto3DHelpers.ShowAuto3DMessage("Ping was returned. TV seems to be on.", false, 0);
            }
            else
                Auto3DHelpers.ShowAuto3DMessage("Ping was not returned. TV seems to be off.", false, 0);
        }

        private void textBoxHostName_TextChanged(object sender, EventArgs e)
        {
            _device.HostName = textBoxHostName.Text;
        }

        private void textBoxUserName_TextChanged(object sender, EventArgs e)
        {
            _device.UserName = textBoxUserName.Text;
        }

        private void buttonAuthenticate_Click(object sender, EventArgs e)
        {
            _device.HarmonyAuthenticate();
        }
    }
}
