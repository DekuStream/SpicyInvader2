using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;


namespace Space_Invader_Réparé
{
    internal class DatabaseHandler
    {
        private string connectionString;

        /// <summary>
        /// Constructor that initializes the connection string for the MySQL database.
        /// </summary>
        public DatabaseHandler()
        {
            connectionString = "server=localhost;user=root;database=db_space_invaders;port=6033;password=root";
        }

        /// <summary>
        /// Retrieves the top scores from the database and displays them.
        /// </summary>
        public void GetTopScores()
        {
            Console.Clear();
            using MySqlConnection connection = new MySqlConnection(connectionString);
            connection.Open();

            // SQL query to select the top 10 scores
            string query = "SELECT jouPseudo, jouNombrePoints FROM t_joueur ORDER BY jouNombrePoints DESC LIMIT 5";
            MySqlCommand command = new MySqlCommand(query, connection);


            // Executing the query and reading the results
            using MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                string pseudo = reader["jouPseudo"].ToString();
                int score = Convert.ToInt32(reader["jouNombrePoints"]);

                Console.WriteLine($"Pseudo: {pseudo}, Score: {score}");
            }

            Console.WriteLine("\nAppuyer sur 'Entrée' pour continuer");
            Console.ReadLine();
            Console.Clear();

            connection.Close();
        }



        /// <summary>
        /// Saves a player's score to the database.
        /// </summary>
        /// <param name="pseudo">The player's pseudonym.</param>
        /// <param name="score">The player's score.</param>
        public void SavePlayerScore(string pseudo, int score)
        {
            using MySqlConnection connection = new MySqlConnection(connectionString);
            connection.Open();

            // SQL query to insert the player's score
            string query = "INSERT INTO t_joueur (jouPseudo, jouNombrePoints) VALUES (@pseudo, @score)";
            MySqlCommand command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("@pseudo", pseudo);
            command.Parameters.AddWithValue("@score", score);
            command.ExecuteNonQuery();

            connection.Close();
        }
    }
}
