using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace AplicatieCafenea
{
    public class FormIstoric : Form
    {
        private readonly DatabaseManager db;
        private DataGridView dgv;

        public FormIstoric(DatabaseManager database)
        {
            db = database;
            InitializeInterface();
            LoadData();
        }

        private void InitializeInterface()
        {
            this.Text = "Istoric comenzi";
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.ShowInTaskbar = false;
            this.Width = 700;
            this.Height = 320; // max height ~300 plus borders

            dgv = new DataGridView
            {
                Location = new Point(10, 10),
                Size = new Size(this.ClientSize.Width - 20, this.ClientSize.Height - 20),
                Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right,
                ReadOnly = true,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
            };

            dgv.Scroll += (s, e) => { /* keep default scrolling */ };

            this.Controls.Add(dgv);
        }

        private void LoadData()
        {
            try
            {
                DataTable dt = db.ToateComenzile();
                dgv.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
