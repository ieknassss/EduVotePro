using ProyectoSistemaEletoralEstudiantil.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProyectoSistemaEletoralEstudiantil.Presentation.Forms
{
    public partial class FrmParties : Form
    {
        EduVoteProEntities db =
    new EduVoteProEntities();

        string selectedLogoPath = "";

        int selectedPartyId = 0;
        public FrmParties()
        {
            InitializeComponent();

            LoadParties();
        }
        private void LoadParties()
        {
            dgvParties.Columns["Votes"]
    .Visible = false;
        }
        private void FrmParties_Load(object sender, EventArgs e)
        {

        }

        private void btnUploadLogo_Click(object sender, EventArgs e)
        {
            OpenFileDialog open =
       new OpenFileDialog();

            open.Filter =
                "Imagenes|*.jpg;*.png;*.jpeg";

            if (open.ShowDialog() == DialogResult.OK)
            {
                selectedLogoPath = open.FileName;

                picLogo.ImageLocation =
                    selectedLogoPath;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(
     txtNumber.Text,
     out int partyNumber))
            {
                MessageBox.Show(
                    "Ingrese un número válido."
                );

                return;
            }

            bool numberExists =
    db.Parties.Any(p =>
        p.Number.ToString() ==
        txtNumber.Text);

            if (numberExists)
            {
                MessageBox.Show(
                    "Ese número ya existe."
                );

                return;
            }

            if (txtName.Text.Trim() == "")
            {
                MessageBox.Show(
                    "Ingrese nombre."
                );

                return;
            }

            string logoFileName = "";

            if (selectedLogoPath != "")
            {
                logoFileName =
                    Guid.NewGuid().ToString()
                    + Path.GetExtension(selectedLogoPath);

                string destination =
                    Application.StartupPath +
                    "\\Uploads\\" +
                    logoFileName;

                File.Copy(
                    selectedLogoPath,
                    destination,
                    true);
            }

            Parties party = new Parties()
            {
                Name = txtName.Text,
                Number = partyNumber,

                Motto = txtMotto.Text,

                ThemeColor =
                    txtThemeColor.Text,

                Description =
                    txtDescription.Text,

                LogoPath = logoFileName
            };

            db.Parties.Add(party);

            db.SaveChanges();

            MessageBox.Show(
                "Plancha registrada."
            );

            LoadParties();

            ClearFields();
        }

        private void ClearFields()
        {
            txtName.Clear();

            txtNumber.Clear();

            txtMotto.Clear();

            txtThemeColor.Clear();

            txtDescription.Clear();

            picLogo.Image = null;

            selectedLogoPath = "";

            selectedPartyId = 0;
        }

        private void dgvParties_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row =
                    dgvParties.Rows[e.RowIndex];

                selectedPartyId =
                    Convert.ToInt32(
                        row.Cells["Id"].Value);

                txtName.Text =
                    row.Cells["Name"].Value.ToString();

                txtNumber.Text =
                    row.Cells["Number"].Value.ToString();

                txtMotto.Text =
                    row.Cells["Motto"].Value.ToString();

                txtThemeColor.Text =
                    row.Cells["ThemeColor"]
                    .Value.ToString();

                txtDescription.Text =
                    row.Cells["Description"]
                    .Value.ToString();

                string logoName =
    row.Cells["LogoPath"]
    .Value.ToString();

                string fullPath =
                    Application.StartupPath +
                    "\\Uploads\\" +
                    logoName;

                if (File.Exists(fullPath))
                {
                    picLogo.ImageLocation = fullPath;
                }
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            var party =
      db.Parties.Find(selectedPartyId);

            if (party != null)
            {
                party.Name = txtName.Text;

                party.Number =
                    Convert.ToInt32(txtNumber.Text);

                party.Motto = txtMotto.Text;

                party.ThemeColor =
                    txtThemeColor.Text;

                party.Description =
                    txtDescription.Text;

                db.SaveChanges();

                LoadParties();

                ClearFields();

                MessageBox.Show(
                    "Plancha actualizada."
                );
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            var party =
       db.Parties.Find(selectedPartyId);

            if (party != null)
            {
                DialogResult result =
                    MessageBox.Show(
                        "¿Eliminar plancha?",
                        "Confirmar",
                        MessageBoxButtons.YesNo);

                if (result == DialogResult.Yes)
                {
                    db.Parties.Remove(party);

                    db.SaveChanges();

                    LoadParties();

                    ClearFields();
                }
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearFields();
        }
    }
}
