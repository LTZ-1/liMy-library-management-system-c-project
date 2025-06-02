using System;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using MyLibraryPro;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

namespace MyLibraryPro
{
    public partial class Form1 : Form
    {
        private const string ConnectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=LibraryDB;Integrated Security=True;";

        public Form1()
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;
            this.Paint += new PaintEventHandler(Form1_Paint);
            ApplyModernStyling();
        }

        private void ApplyModernStyling()
        {
            // Panel styling
            panelLogin.BackColor = Color.FromArgb(240, 240, 240);
            panelLogin.BorderStyle = BorderStyle.None;

            // Button styling
            btnLogin.FlatAppearance.MouseOverBackColor = Color.FromArgb(70, 100, 200);
            btnLogin.FlatAppearance.MouseDownBackColor = Color.FromArgb(50, 80, 180);
            btnLogin.Cursor = Cursors.Hand;

            // Textbox styling
            txtUsername.BorderStyle = BorderStyle.FixedSingle;
            txtPassword.BorderStyle = BorderStyle.FixedSingle;

            // Label styling
            lblTitle.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
            lblTitle.ForeColor = Color.FromArgb(50, 50, 50);

            // Set rounded corners
            SetRoundedCorners();
        }

        private void SetRoundedCorners()
        {
            int panelRadius = 10;
            panelLogin.Region = CreateRoundedRegion(panelLogin.ClientRectangle, panelRadius);

            int buttonRadius = 5;
            btnLogin.Region = CreateRoundedRegion(btnLogin.ClientRectangle, buttonRadius);
        }

        private Region CreateRoundedRegion(Rectangle rect, int radius)
        {
            GraphicsPath path = new GraphicsPath();
            path.AddArc(rect.X, rect.Y, radius * 2, radius * 2, 180, 90);
            path.AddArc(rect.Right - radius * 2, rect.Y, radius * 2, radius * 2, 270, 90);
            path.AddArc(rect.Right - radius * 2, rect.Bottom - radius * 2, radius * 2, radius * 2, 0, 90);
            path.AddArc(rect.X, rect.Bottom - radius * 2, radius * 2, radius * 2, 90, 90);
            path.CloseAllFigures();
            return new Region(path);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            CenterPanel();
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            CenterPanel();
            this.Invalidate();
        }

        private void CenterPanel()
        {
            panelLogin.Left = (this.ClientSize.Width - panelLogin.Width) / 2;
            panelLogin.Top = (this.ClientSize.Height - panelLogin.Height) / 2;
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            using (LinearGradientBrush brush = new LinearGradientBrush(
                this.ClientRectangle,
                Color.FromArgb(230, 230, 230),
                Color.FromArgb(210, 210, 210),
                LinearGradientMode.Vertical))
            {
                e.Graphics.FillRectangle(brush, this.ClientRectangle);
            }
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text;

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                ShowErrorMessage("Please enter both username and password.");
                return;
            }

            if (AuthenticateUser(username, password))
            {
                lblErrorMessage.Visible = false;
                MessageBox.Show("Login Successful!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                this.Hide();
                FormMain mainForm = new FormMain();
                mainForm.Show();
            }
            else
            {
                ShowErrorMessage("Invalid username or password.");
            }
        }

        private void ShowErrorMessage(string message)
        {
            lblErrorMessage.Text = message;
            lblErrorMessage.Visible = true;
            lblErrorMessage.ForeColor = Color.FromArgb(200, 0, 0);
        }

        private bool AuthenticateUser(string username, string password)
        {
            bool isAuthenticated = false;
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                try
                {
                    connection.Open();
                    string query = "SELECT COUNT(1) FROM Users WHERE Username = @Username AND PasswordHash = @PasswordHash";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Username", username);
                        command.Parameters.AddWithValue("@PasswordHash", password);
                        int count = (int)command.ExecuteScalar();
                        isAuthenticated = count > 0;
                    }
                }
                catch (SqlException ex)
                {
                    ShowErrorMessage($"Database error: {ex.Message}");
                }
                catch (Exception ex)
                {
                    ShowErrorMessage($"An unexpected error occurred: {ex.Message}");
                }
            }
            return isAuthenticated;
        }

       
    }
}