using ProyectoSistemaEletoralEstudiantil.DataAccess.Models;
using ProyectoSistemaEletoralEstudiantil.Security.Encryption;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ProyectoSistemaEletoralEstudiantil.Security.Sessions;


namespace ProyectoSistemaEletoralEstudiantil.Presentation.Forms
{
    public partial class FrmLogin : Form
    {
        EduVoteProEntities db = new EduVoteProEntities();
        public FrmLogin()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string email = txtEmail.Text.Trim();

            string password =
                PasswordEncryptor.Encrypt(
                    txtPassword.Text.Trim());

            var user = db.Users.FirstOrDefault(u =>
                u.Email == email &&
                u.PasswordHash == password);

            if (user != null)
            {
                UserSession.Id = user.Id;

                UserSession.Nombre = user.Nombre;

                UserSession.Role = user.Role;

                MessageBox.Show(
                    "Bienvenido " + user.Nombre,
                    "EduVote Pro",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                this.Hide();
                FrmDashboard dashboard = new FrmDashboard();

                dashboard.Show();
            }
            else
            {
                MessageBox.Show(
                    "Correo o contraseña incorrectos.",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }
    }
    }

