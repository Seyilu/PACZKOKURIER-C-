using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace WebStore.Pages
{
    public class EditModel : PageModel
    {
        public PaczkiInfo paczkiinfo = new PaczkiInfo();
        public String errorMessage = "";
        public String successMessage = "";
        public void OnGet()
        {
            String id = Request.Query["id"];

            try
            {
                String connectionString = "Data Source = (LocalDB)\\MSSQLLocalDB; AttachDbFilename = C:\\Users\\Admin\\Desktop\\TO TO\\WebStore\\WebStore\\bin\\Debug\\net6.0\\database.mdf; Integrated Security = True; Connect Timeout = 30";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "SELECT * FROM Paczki WHERE id=@id";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@id", id);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                paczkiinfo.id ="" + reader.GetInt32(0);
                                paczkiinfo.nrpaczki = reader.GetString(1);
                                paczkiinfo.imie = reader.GetString(2);
                                paczkiinfo.nazwisko = reader.GetString(3);
                                paczkiinfo.telefon = reader.GetString(4);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
            }
        }

        public void OnPost()
        {
            paczkiinfo.id = Request.Form["id"];
            paczkiinfo.nrpaczki = Request.Form["nrpaczki"];
            paczkiinfo.imie = Request.Form["imie"];
            paczkiinfo.nazwisko = Request.Form["nazwisko"];
            paczkiinfo.telefon = Request.Form["telefon"];

            if (paczkiinfo.id.Length == 0|| paczkiinfo.nrpaczki.Length == 0 ||
                paczkiinfo.imie.Length == 0 || paczkiinfo.nazwisko.Length == 0 ||
                paczkiinfo.telefon.Length == 0)
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
                    String sql = "UPDATE Paczki " +
                                 "SET nrpaczki=@nrpaczki, imie=@imie, nazwisko=@nazwisko, telefon=@telefon " +
                                 "WHERE id=@id";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@nrpaczki", paczkiinfo.nrpaczki);
                        command.Parameters.AddWithValue("@imie", paczkiinfo.imie);
                        command.Parameters.AddWithValue("@nazwisko", paczkiinfo.nazwisko);
                        command.Parameters.AddWithValue("@telefon", paczkiinfo.telefon);
                        command.Parameters.AddWithValue("@id", paczkiinfo.id);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }

            Response.Redirect("/Paczki");
        }
    }
}
