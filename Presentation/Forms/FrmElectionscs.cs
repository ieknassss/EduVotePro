using ProyectoSistemaEletoralEstudiantil.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProyectoSistemaEletoralEstudiantil.Presentation.Forms
{
    public partial class FrmElectionscs : Form
    {
        EduVoteProEntities db =
    new EduVoteProEntities();

        int selectedElectionId = 0;
        public FrmElectionscs()
        {
            InitializeComponent();

            LoadElections();
        }

        private void LoadElections()
        {
            dgvElections.DataSource =
                db.Elections.ToList();
        }
        private void FrmElectionscs_Load(object sender, EventArgs e)
        {

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtName.Text.Trim() == "")
            {
                MessageBox.Show(
                    "Ingrese un nombre."
                );

                return;
            }

            Elections election = new Elections()
            {
                Name = txtName.Text.Trim(),
                Description = txtDescription.Text,
                StartDate = dtStartDate.Value,
                EndDate = dtEndDate.Value,
                Status = cbStatus.Text
            };

            db.Elections.Add(election);

            db.SaveChanges();

            MessageBox.Show(
                "Elección guardada correctamente."
            );

            LoadElections();

            ClearFields();
        }

        private void ClearFields()
        {
            txtName.Clear();

            txtDescription.Clear();

            cbStatus.SelectedIndex = -1;

            selectedElectionId = 0;
        }

        private void dgvElections_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row =
                    dgvElections.Rows[e.RowIndex];

                selectedElectionId =
                    Convert.ToInt32(row.Cells["Id"].Value);

                txtName.Text =
                    row.Cells["Name"].Value.ToString();

                txtDescription.Text =
                    row.Cells["Description"].Value.ToString();

                cbStatus.Text =
                    row.Cells["Status"].Value.ToString();

                dtStartDate.Value =
                    Convert.ToDateTime(
                        row.Cells["StartDate"].Value);

                dtEndDate.Value =
                    Convert.ToDateTime(
                        row.Cells["EndDate"].Value);
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            var election =
        db.Elections.Find(selectedElectionId);

            if (election != null)
            {
                election.Name = txtName.Text;

                election.Description =
                    txtDescription.Text;

                election.StartDate =
                    dtStartDate.Value;

                election.EndDate =
                    dtEndDate.Value;

                election.Status =
                    cbStatus.Text;

                db.SaveChanges();

                MessageBox.Show(
                    "Elección actualizada."
                );

                LoadElections();

                ClearFields();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            var election =
        db.Elections.Find(selectedElectionId);

            if (election != null)
            {
                DialogResult result =
                    MessageBox.Show(
                        "¿Eliminar elección?",
                        "Confirmar",
                        MessageBoxButtons.YesNo);

                if (result == DialogResult.Yes)
                {
                    db.Elections.Remove(election);

                    db.SaveChanges();

                    LoadElections();

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
