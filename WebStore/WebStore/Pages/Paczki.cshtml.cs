using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace WebStore.Pages
{
    public class PaczkiModel : PageModel
    {
        public List<PaczkiInfo> ListPaczki = new List<PaczkiInfo>();
        public void OnGet()
        {
            try
            {

                string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\Admin\\Desktop\\TO TO\\WebStore\\WebStore\\bin\\Debug\\net6.0\\database.mdf;Integrated Security=True;Connect Timeout=30";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "SELECT * FROM Paczki";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while(reader.Read())
                            {
                              
                                PaczkiInfo paczki = new PaczkiInfo();
                                paczki.id ="" + reader.GetInt32(0); 
                                paczki.nrpaczki = reader.GetString(1);
                                paczki.imie = reader.GetString(2);
                                paczki.nazwisko = reader.GetString(3);
                                paczki.telefon = reader.GetString(4);
                                paczki.created_at = reader.GetDateTime(5).ToString();
                                paczki.photoName= reader.GetString(6);
                               
                                
                            ListPaczki.Add(paczki);
                            }
                        }
                    }    
                }
            }
            catch (Exception ex)
            {
                
            }
        }
    }
    public class PaczkiInfo
    {
        public string id;
        public string nrpaczki;
        public string imie;
        public string nazwisko;
        public string telefon;
        public string created_at;
        public string photoName;



    }

}
