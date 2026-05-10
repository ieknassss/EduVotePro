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

namespace ProyectoSistemaEletoralEstudiantil.Presentation.Forms
{
    public partial class FrmRegister : Form
    {
        EduVoteProEntities db = new EduVoteProEntities();
        public FrmRegister()
        {
            InitializeComponent();
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            string firstName = txtFirstName.Text.Trim();
            string lastName = txtLastName.Text.Trim();
            string studentCode = txtStudentCode.Text.Trim();
            string email = txtEmail.Text.Trim();
            string password = txtPassword.Text.Trim();
            string role = cbRole.Text;

            if (
                firstName == "" ||
                lastName == "" ||
                studentCode == "" ||
                email == "" ||
                password == "" ||
                role == ""
            )
            {
                MessageBox.Show(
                    "Complete todos los campos."
                );

                return;
            }

            bool emailExists = db.Users.Any(u =>
                u.Email == email);

            if (emailExists)
            {
                MessageBox.Show(
                    "Ese correo ya existe."
                );

                return;
            }

            Users user = new Users()
            {
                Nombre = firstName,
                LastName = lastName,
                StudentCode = studentCode,
                Email = email,
                PasswordHash =
        PasswordEncryptor.Encrypt(password),
                Role = role,
                HasVoted = false,
                CreatedAt = DateTime.Now

            };

            db.Users.Add(user);

            db.SaveChanges();

            MessageBox.Show(
                "Usuario registrado correctamente."
            );

            this.Close();
        }
    }
    }

