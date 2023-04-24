using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace WebStore.Pages
{
    public class CreateModel : PageModel
    {
        public PaczkiInfo paczkiinfo = new PaczkiInfo();
        public string errorMessage = "";
        public string successMessage = "";
        string photoPath;
        private void btnUpload_click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                photoPath = openFileDialog.FileName;
                Uri fileUri = new Uri(openFileDialog.FileName);
                image.Source = new BitmapImage(fileUri);
            }
        }
        public void OnGet()
        {
        }
        public void OnPost()
        {
            paczkiinfo.nrpaczki = Request.Form["nrpaczki"];
            paczkiinfo.imie = Request.Form["imie"];
            paczkiinfo.nazwisko = Request.Form["nazwisko"];
            paczkiinfo.telefon = Request.Form["telefon"];

            if (paczkiinfo.nrpaczki.Length == 0 || paczkiinfo.imie.Length == 0 ||
                paczkiinfo.nazwisko.Length == 0 || paczkiinfo.telefon.Length == 0)
            {
                errorMessage = "Wszystkie pola s¹ wymagane";
                return;
            }

            try
            {
                String connectionString = "Data Source = (LocalDB)\\MSSQLLocalDB; AttachDbFilename = C:\\Users\\Admin\\Desktop\\TO TO\\WebStore\\WebStore\\bin\\Debug\\net6.0\\database.mdf; Integrated Security = True; Connect Timeout = 30";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "INSERT INTO Paczki " +
                                 "(nrpaczki, imie, nazwisko, telefon) VALUES " +
                                 "(@nrpaczki, @imie, @nazwisko, @telefon);";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@nrpaczki", paczkiinfo.nrpaczki);
                        command.Parameters.AddWithValue("@imie", paczkiinfo.imie);
                        command.Parameters.AddWithValue("@nazwisko", paczkiinfo.nazwisko);
                        command.Parameters.AddWithValue("@telefon", paczkiinfo.telefon);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }

            paczkiinfo.nrpaczki = ""; paczkiinfo.imie = ""; paczkiinfo.nazwisko = ""; paczkiinfo.telefon = "";
            successMessage = "New Client Added Correctly";
            
            Response.Redirect("/Paczki");

        }
    }
}
