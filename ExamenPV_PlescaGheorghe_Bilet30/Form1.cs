using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace AplicatieCafenea
{
    public partial class Form1 : Form
    {
        private readonly DatabaseManager db = new DatabaseManager();

        private GroupBox gbFelul1, gbFelul2, gbDesert, gbDateClient;
        private RadioButton rbZeama, rbSupa, rbBors, rbCiorba;
        private RadioButton rbHrisca, rbCartofi, rbLegume;
        private CheckBox chkCafea, chkInghetata, chkFructe;

        private Label lblNrComanda, lblData, lblAdresa, lblAtiComandat;
        private TextBox txtNrComanda, txtAdresa;
        private DateTimePicker dtpData;

        private ListBox listBoxComanda;
        private Button btnAfiseaza, btnSterge, btnIstoric, btnInchide, btnCreazaDB;

        public Form1()
        {
            InitializeInterface();
        }

        private void InitializeInterface()
        {
            this.Text = "Comanda la cafenea";
            this.Size = new Size(760, 520);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;

            gbDateClient = new GroupBox { Text = "Date Identificare Client & Comandă:", Location = new Point(20, 15), Size = new Size(700, 65) };

            lblNrComanda = new Label { Text = "Nr. comandă:", Location = new Point(15, 28), Size = new Size(75, 20) };
            txtNrComanda = new TextBox { Location = new Point(95, 25), Size = new Size(80, 20) };

            lblData = new Label { Text = "Data:", Location = new Point(195, 28), Size = new Size(35, 20) };
            dtpData = new DateTimePicker { Location = new Point(235, 25), Size = new Size(115, 20), Format = DateTimePickerFormat.Custom, CustomFormat = "dd.MM.yyyy" };

            lblAdresa = new Label { Text = "Adresă client:", Location = new Point(365, 28), Size = new Size(75, 20) };
            txtAdresa = new TextBox { Location = new Point(445, 25), Size = new Size(240, 20) };

            gbDateClient.Controls.AddRange(new Control[] { lblNrComanda, txtNrComanda, lblData, dtpData, lblAdresa, txtAdresa });

            gbFelul1 = new GroupBox { Text = "Felul întâi:", Location = new Point(20, 95), Size = new Size(130, 140) };
            rbZeama = new RadioButton { Text = "Zeamă", Location = new Point(15, 22), Size = new Size(100, 20) };
            rbSupa = new RadioButton { Text = "Supă", Location = new Point(15, 50), Size = new Size(100, 20) };
            rbBors = new RadioButton { Text = "Borș", Location = new Point(15, 78), Size = new Size(100, 20) };
            rbCiorba = new RadioButton { Text = "Ciorbă", Location = new Point(15, 106), Size = new Size(100, 20), Checked = true };
            gbFelul1.Controls.AddRange(new Control[] { rbZeama, rbSupa, rbBors, rbCiorba });

            gbFelul2 = new GroupBox { Text = "Felul doi:", Location = new Point(170, 95), Size = new Size(130, 140) };
            rbHrisca = new RadioButton { Text = "Hrișcă", Location = new Point(15, 22), Size = new Size(100, 20) };
            rbCartofi = new RadioButton { Text = "Cartofi", Location = new Point(15, 50), Size = new Size(100, 20), Checked = true };
            rbLegume = new RadioButton { Text = "Legume", Location = new Point(15, 78), Size = new Size(100, 20) };
            gbFelul2.Controls.AddRange(new Control[] { rbHrisca, rbCartofi, rbLegume });

            gbDesert = new GroupBox { Text = "Desert:", Location = new Point(320, 95), Size = new Size(130, 140) };
            chkCafea = new CheckBox { Text = "Cafea", Location = new Point(15, 22), Size = new Size(100, 20), Checked = true };
            chkInghetata = new CheckBox { Text = "Înghețată", Location = new Point(15, 50), Size = new Size(100, 20) };
            chkFructe = new CheckBox { Text = "Fructe", Location = new Point(15, 78), Size = new Size(100, 20), Checked = true };
            gbDesert.Controls.AddRange(new Control[] { chkCafea, chkInghetata, chkFructe });

            btnAfiseaza = new Button { Text = "Afișează comanda", Location = new Point(480, 100), Size = new Size(120, 30) };
            btnAfiseaza.Click += BtnAfiseaza_Click;

            btnSterge = new Button { Text = "Șterge afișarea", Location = new Point(480, 135), Size = new Size(120, 30) };
            btnSterge.Click += BtnSterge_Click;

            btnIstoric = new Button { Text = "Istoric comenzi", Location = new Point(480, 170), Size = new Size(120, 30) };
            btnIstoric.Click += BtnIstoric_Click;

            btnInchide = new Button { Text = "Închide", Location = new Point(480, 205), Size = new Size(120, 30) };
            btnInchide.Click += BtnInchide_Click;

            lblAtiComandat = new Label { Text = "Ați comandat:", Location = new Point(40, 260), Size = new Size(80, 20) };
            listBoxComanda = new ListBox { Location = new Point(130, 260), Size = new Size(320, 160), Font = new Font("Microsoft Sans Serif", 11) };

            btnCreazaDB = new Button { Text = "Creaza Baza de Date", Location = new Point(40, 430), Size = new Size(120, 30) };
            btnCreazaDB.Click += BtnCreazaDB_Click;

            this.Controls.AddRange(new Control[] {
                gbDateClient, gbFelul1, gbFelul2, gbDesert, btnAfiseaza, btnSterge, btnIstoric, btnInchide, lblAtiComandat, listBoxComanda, btnCreazaDB
            });
        }
        private void BtnCreazaDB_Click(object sender, EventArgs e)
        {
            if (db.creazaBazaDeDate())
                MessageBox.Show("Baza de date creata!", "Succes", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        private void BtnAfiseaza_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNrComanda.Text) || string.IsNullOrWhiteSpace(txtAdresa.Text))
            {
                MessageBox.Show("Numărul comenzii și adresa clientului sunt obligatorii!", "Validare", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string felul1 = "";
            if (rbZeama.Checked) felul1 = "Zeamă";
            else if (rbSupa.Checked) felul1 = "Supă";
            else if (rbBors.Checked) felul1 = "Borș";
            else if (rbCiorba.Checked) felul1 = "Ciorbă";

            string felul2 = "";
            if (rbHrisca.Checked) felul2 = "Hrișcă";
            else if (rbCartofi.Checked) felul2 = "Cartofi";
            else if (rbLegume.Checked) felul2 = "Legume";

            List<string> deserturi = new List<string>();
            if (chkCafea.Checked) deserturi.Add("Cafea");
            if (chkInghetata.Checked) deserturi.Add("Înghețată");
            if (chkFructe.Checked) deserturi.Add("Fructe");
            string desert = deserturi.Count > 0 ? string.Join(" și ", deserturi) : "Fără desert";

            listBoxComanda.Items.Clear();
            listBoxComanda.Items.Add($"Felul întâi: {felul1}");
            listBoxComanda.Items.Add($"Felul doi: {felul2}");
            listBoxComanda.Items.Add($"Desert: {desert}");

            try
            {
                db.InsereazaComanda(txtNrComanda.Text.Trim(), dtpData.Value, txtAdresa.Text.Trim(), felul1, felul2, desert);
                MessageBox.Show("Comanda a fost înregistrată cu succes în tabelul Comanda!", "Succes", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Eroare SQL", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnSterge_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNrComanda.Text))
            {
                MessageBox.Show("Introduceți numărul comenzii pentru a o șterge din baza de date!", "Validare", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string nrComanda = txtNrComanda.Text.Trim();

            try
            {
                bool sters = db.StergeComanda(nrComanda);
                listBoxComanda.Items.Clear();

                if (sters)
                {
                    MessageBox.Show($"Comanda Nr. {nrComanda} a fost ștearsă din baza de date și ecranul a fost curățat!", "Succes", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Caseta a fost curățată, dar numărul comenzii nu a fost găsit în baza de date.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Eroare SQL", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnIstoric_Click(object sender, EventArgs e)
        {
            try
            {
                using (var f = new FormIstoric(db))
                {
                    f.ShowDialog(this);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void BtnInchide_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Sunteți sigur că doriți să închideți aplicația?", "Confirmare", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                this.Close();
            }
        }
    }
}