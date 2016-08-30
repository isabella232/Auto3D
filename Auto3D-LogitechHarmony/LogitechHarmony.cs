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
using System.Threading.Tasks;

namespace MediaPortal.ProcessPlugins.Auto3D.Devices
{
    public class LogitechHarmony : Auto3DBaseDevice
    {

        public HarmonyHub.Client Client;
        public HarmonyHub.Config Config;

        public LogitechHarmony()
        {
        }

        public override String CompanyName
        {
            get { return "Logitech"; }
        }

        public override String DeviceName
        {
            get { return "Harmony Hub"; }
        }


        public string HostName
        {
            get;
            set;
        }

        public string UserName
        {
            get;
            set;
        }

        public string AuthToken
        {
            get;
            set;
        }

        public string Password
        {
            get;
            set;
        }

        void ConnectionClosedByServerHandler(object aSender, bool aRequestWasCancelled)
        {
            // We know this notification is not coming from the UI thread.
            // Therefore we Invoke to be able to modifiy our tree view control.
            // Try opening our connection again to keep it alive.
            
        }

        async Task HarmonyOpen()
        {
            await Client.TryOpenAsync(AuthToken);

            if (Client.IsReady)
            {
                Config = await Client.GetConfigAsync();
            }
        }

        async Task HarmonyClose()
        {
            await Client.CloseAsync();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task HarmonyAuthenticate()
        {
            await Client.CloseAsync();
            await Client.TryOpenAsync(UserName,Password);
            if (Client.IsReady)
            {
                //Save our authentication token then
                AuthToken = Client.Token;
            }

            await Client.SendKeyPressAsync("37058142", "PowerToggle");
        }



        public override async void Start()
        {
            base.Start();
            Client = new HarmonyHub.Client(HostName);
            Client.OnConnectionClosedByServer += ConnectionClosedByServerHandler;
            await HarmonyOpen();
        }

        public override async void Stop()
        {
            base.Stop();
            await HarmonyClose();
            Client = null;
        }

        public override void LoadSettings()
        {
            using (Settings reader = new MPSettings())
            {
                HostName = reader.GetValueAsString("Auto3DPlugin", "LogitechHarmonyHostName", "HarmonyHub");
                UserName = reader.GetValueAsString("Auto3DPlugin", "LogitechHarmonyUserName", "");
                AuthToken = reader.GetValueAsString("Auto3DPlugin", "LogitechHarmonyAuthToken", "");
            }
        }

        public override void SaveSettings()
        {
            using (Settings writer = new MPSettings())
            {
                writer.SetValue("Auto3DPlugin", "LogitechHarmonyHostName", HostName);
                writer.SetValue("Auto3DPlugin", "LogitechHarmonyUserName", UserName);
                writer.SetValue("Auto3DPlugin", "LogitechHarmonyAuthToken", AuthToken);
            }
        }

        public override bool SendCommand(RemoteCommand rc)
        {
            try
            {
                IrToy.Send(rc.IrCode);
                Log.Info("Auto3D: Code sent: " + rc.IrCode);
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
            // Not supported
        }

        public override DeviceInterface GetTurnOnInterfaces()
        {
            return DeviceInterface.IR | DeviceInterface.Network;
        }

        public override void TurnOn(DeviceInterface type)
        {
            // Not supported
        }

        public override bool IsOn()
        {
            return Auto3DHelpers.Ping(HostName);
        }

    }
}
