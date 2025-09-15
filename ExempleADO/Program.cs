// See https://aka.ms/new-console-template for more information
// 1. Me connnecter à la db
/* Définir le chemin contenant l'IP, les infos d'authentification, etc.*/
using Microsoft.Data.SqlClient;
using System;
using System.Data;
using System.Data.Common;
using static System.Net.Mime.MediaTypeNames;
using static System.Runtime.InteropServices.JavaScript.JSType;

// clic droit sur db, properties, string connection
string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=ExempleADO;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";

DbConnection connexion = new SqlConnection(connectionString);
try
{
    //connexion.ConnectionString = connectionString;
    /** Créer un objet permettant de se connecter
    * Appeler la méthode qui permettra de se connecter
    * Vérifier que l'on est connecté
    * */
    connexion.Open();


}

catch (SqlException ex)
{
    Console.WriteLine($"Erreur de connexion DB{ex.Message}");
}


if (connexion.State == System.Data.ConnectionState.Open)

{
    // 2. Exécuter un requête select

    try
    {
        string sqlQuery = @"SELECT Id, Title, Body 
                             From Jokes";
        DbCommand command = connexion.CreateCommand();
        command.CommandText = sqlQuery;

        //3. Afficher le résultat de ma requête
        DbDataReader reader = command.ExecuteReader();

        while (reader.Read())
        {

            int? Id = reader["Id"] == DBNull.Value ? null : (int?)reader["Id"];
            string? titre = reader["Title"] == DBNull.Value ? "" : reader
            ["Title"].ToString();

            Console.WriteLine(titre);
        }

    }

    catch (SqlException ex)
    {

        Console.WriteLine(ex.Message);


    }


    //string sqlQuery = @"GetJokes";
    //DbCommand command = connexion.CreateCommand();
    //// 3. Afficher le résultat de ma requête
    //command.CommandText = sqlQuery;
    //command.CommandType=System.Data.CommandType.Text;
    //command.CommandType = System.Data.CommandType.TableDirect;
    //command.CommandType = System.Data.CommandType.StoredProcedure;
    //command.Parameters.Add(new SqlParameter("motcle", "vache"));
}
//On ferme la connexion
try
{

    connexion.Close();

}

catch (SqlException ex)

{
    Console.WriteLine($"Erreur de fermeture de connexion DB {ex.Message}");
}

