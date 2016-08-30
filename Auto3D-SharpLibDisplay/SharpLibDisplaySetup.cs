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
  public partial class SharpLibDisplaySetup : UserControl, IAuto3DSetup
  {
    SharpLibDisplay _device;

	public SharpLibDisplaySetup(IAuto3D device)
    {
      InitializeComponent();

	  if (!(device is SharpLibDisplay))
        throw new Exception("Auto3D: Device is no SharpLibDisplay");

	  _device = (SharpLibDisplay)device;

	  //checkAllowIRCommandsForOtherDevices.Checked = Auto3DBaseDevice.AllowIrCommandsForAllDevices;
    }

    public IAuto3D GetDevice()
    {
      return _device;
    }

    public void LoadSettings()
    {
	   _device.SelectedDeviceModel = _device.DeviceModels[0];


	  //checkBoxPingCheck.Checked = _device.PingCheck;

    }

    public void SaveSettings()
    {
		_device.SaveSettings();
    }


	private void buttonPingGenericDevice_Click(object sender, EventArgs e)
	{
		if (_device.IsOn())
		{
			Auto3DHelpers.ShowAuto3DMessage("Ping was returned. TV seems to be on.", false, 0);
		}
		else
			Auto3DHelpers.ShowAuto3DMessage("Ping was not returned. TV seems to be off.", false, 0);
	}
  }
}
