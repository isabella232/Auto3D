using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MediaPortal.ProcessPlugins.Auto3D;
using MediaPortal.Profile;
using System.Net;
using System.Threading;
using System.IO;
using System.Windows.Forms;
using MediaPortal.GUI.Library;
using System.Reflection;
using MediaPortal.Configuration;
using System.IO.Ports;
using SharpLib.Display;

namespace MediaPortal.ProcessPlugins.Auto3D.Devices
{
    public class SharpLibDisplay : Auto3DBaseDevice
    {
        public SharpLibDisplay()
        {
            Client = new Client();
            Client.CloseOrderEvent += OnCloseOrder;
        }

        void OnCloseOrder()
        {
            Client.Close();
        }

        public override String CompanyName
        {
            get { return "SharpLibDisplay"; }
        }

        public override String DeviceName
        {
            get { return "SharpLibDisplay"; }
        }

        Client Client;

        public override void Start()
        {
            base.Start();
            Client.Open();
            Client.SetName("Auto3D");
            // We don't want to display anything
            Client.SetPriority(0); 
        }

        public override void Stop()
        {
            base.Stop();
            Client.Close();
        }

        public override void LoadSettings()
        {
            using (Settings reader = new MPSettings())
            {
                // Not having this results in us trying to use a null pointer when opening the 3D selection dialog
                DeviceModelName = reader.GetValueAsString("Auto3DPlugin", "SharpLibDisplayModel", "SharpLibDisplay");
            }
        }

        public override void SaveSettings()
        {
            using (Settings writer = new MPSettings())
            {
                // Not having this results in us trying to use a null pointer when opening the 3D selection dialog
                writer.SetValue("Auto3DPlugin", "SharpLibDisplayModel", SelectedDeviceModel.Name);
            }
        }

        public override bool SendCommand(RemoteCommand rc)
        {
            try
            {
                Client.TriggerEventsByName(rc.Command);
                Log.Info("Auto3D: Trigger Event: " + rc.Command);
            }
            catch (Exception ex)
            {
                Auto3DHelpers.ShowAuto3DMessage("Sending code failed: " + ex.Message, false, 0);
                Log.Error("Auto3D: Sending code " + rc.IrCode + " failed: " + ex.Message);

                return false;
            }

            return true;
        }

        public override DeviceInterface GetTurnOffInterfaces()
        {
            return DeviceInterface.IR;
        }

        public override void TurnOff(DeviceInterface type)
        {
            //Not supported
        }

        public override DeviceInterface GetTurnOnInterfaces()
        {
            return DeviceInterface.IR | DeviceInterface.Network;
        }

        public override void TurnOn(DeviceInterface type)
        {
            //Not supported
        }

        public override bool IsOn()
        {
            return true;
        }

    }
}
