
namespace MediaPortal.ProcessPlugins.Auto3D.Devices
{
    partial class SharpLibDisplayKeypad
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SharpLibDisplayKeypad));
            this.labelExplainations = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // labelExplainations
            // 
            this.labelExplainations.AutoSize = true;
            this.labelExplainations.Location = new System.Drawing.Point(3, 71);
            this.labelExplainations.Name = "labelExplainations";
            this.labelExplainations.Size = new System.Drawing.Size(245, 91);
            this.labelExplainations.TabIndex = 38;
            this.labelExplainations.Text = resources.GetString("labelExplainations.Text");
            // 
            // SharpLibDisplayKeypad
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.labelExplainations);
            this.Name = "SharpLibDisplayKeypad";
            this.Size = new System.Drawing.Size(256, 256);
            this.ResumeLayout(false);
            this.PerformLayout();

        }


        #endregion

        private System.Windows.Forms.Label labelExplainations;
    }
}
