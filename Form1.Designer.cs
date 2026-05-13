namespace FotoOrganizerPro
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.TextBox txtPasta;
        private System.Windows.Forms.Button btnProcessar;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.ListBox lstLog;
        private System.Windows.Forms.Label lblStatus;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.txtPasta = new System.Windows.Forms.TextBox();
            this.btnProcessar = new System.Windows.Forms.Button();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.lstLog = new System.Windows.Forms.ListBox();
            this.lblStatus = new System.Windows.Forms.Label();
            this.SuspendLayout();

            this.txtPasta.Location = new System.Drawing.Point(12, 12);
            this.txtPasta.Size = new System.Drawing.Size(400, 23);

            this.btnProcessar.Location = new System.Drawing.Point(420, 12);
            this.btnProcessar.Text = "Processar";
            this.btnProcessar.Click += new System.EventHandler(this.btnProcessar_Click);

            this.progressBar.Location = new System.Drawing.Point(12, 50);
            this.progressBar.Size = new System.Drawing.Size(500, 23);

            this.lstLog.Location = new System.Drawing.Point(12, 80);
            this.lstLog.Size = new System.Drawing.Size(500, 200);

            this.lblStatus.Location = new System.Drawing.Point(12, 290);
            this.lblStatus.Size = new System.Drawing.Size(200, 23);

            this.ClientSize = new System.Drawing.Size(530, 320);
            this.Controls.Add(this.txtPasta);
            this.Controls.Add(this.btnProcessar);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.lstLog);
            this.Controls.Add(this.lblStatus);
            this.Text = "Foto Organizer Pro";

            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
