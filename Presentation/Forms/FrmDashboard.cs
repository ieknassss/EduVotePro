using ProyectoSistemaEletoralEstudiantil.DataAccess.Models;
using ProyectoSistemaEletoralEstudiantil.Security.Sessions;
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
    public partial class FrmDashboard : Form
    {
        EduVoteProEntities db =
    new EduVoteProEntities();
        public FrmDashboard()
        {
            InitializeComponent();

            lblUser.Text = UserSession.Nombre;

            lblRole.Text = UserSession.Role;

            LoadPermissions();
            LoadStatistics();
        }

        private void LoadPermissions()
        {
            if (UserSession.Role == "Estudiante")
            {
                btnUsers.Visible = false;

                btnParties.Visible = false;

                btnReports.Visible = false;

                btnElections.Visible = false;
            }

            if (UserSession.Role == "Administrador Partido")
            {
                btnUsers.Visible = false;
            }
        }

        private void LoadStatistics()
        {
            int users = db.Users.Count();

            int votes = db.Votes.Count();

            int elections = db.Elections.Count();

            MessageBox.Show(
                "Usuarios: " + users +
                "\nVotos: " + votes +
                "\nElecciones: " + elections
            );
        }
        private void FrmDashboard_Load(object sender, EventArgs e)
        {

        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            Application.Restart();
        }
    }
}
