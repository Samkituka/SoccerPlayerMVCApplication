using System.Configuration;
using System.Data.SqlClient;
using SoccerPlayer.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web.Mvc;

namespace SoccerPlayer.DataAcess.Controllers
{
    public abstract class Controller
    {
        protected string sqlConnectionString = ConfigurationManager.ConnectionStrings["SoccerPlayer"].ConnectionString;
    }

    public class PlayerController : Controller 
    {

        public static int CreatePlayer(string FirstName, string LastName, string Age, string Team, string Position, ISoccerPlayerConfigManager configManager )
        {
            string sqlConnectionString = configManager.SoccerPlayerConnection;
            int PlayerID = 0;
            string insertsqlCommand = @"INSERT INTO PLAYERS 
                                       (FIRSTNAME,
                                        LASTNAME,
                                         AGE,
                                        TEAM,
                                         POSITION)
                                      OUTPUT INSERTED.PLAYERID
                                        VALUES 
                                       (@FIRSTNAME,
                                        @LASTNAME,
                                        @AGE,
                                         @TEAM,
                                         @POSITION)";

            using (SqlConnection sqlConnection = new SqlConnection(sqlConnectionString))
            {
                using (SqlCommand sqlCommand = new SqlCommand(insertsqlCommand, sqlConnection))
                {
                    sqlCommand.Parameters.Add(new SqlParameter("@FIRSTNAME", FirstName));
                    sqlCommand.Parameters.Add(new SqlParameter("@LASTNAME", LastName));
                    sqlCommand.Parameters.Add(new SqlParameter("@AGE", Age));
                    sqlCommand.Parameters.Add(new SqlParameter("@TEAM", Team));
                    sqlCommand.Parameters.Add(new SqlParameter("@POSITION", Position));


                    sqlCommand.Connection.Open();
                    PlayerID = (int)sqlCommand.ExecuteScalar();
                    sqlCommand.Connection.Close();


                }
            }

            return PlayerID;
        }

        public static int UpdatePlayer(int PlayerID, string FirstName, string LastName, string Age, string Team, string Position, ISoccerPlayerConfigManager configManager)
        {
            string sqlConnectionString = configManager.SoccerPlayerConnection;
            string updatesqlCommand = @"UPDATE PLAYERS
                                       SET FIRSTNAME = @FIRSTNAME,
                                           LASTNAME = @LASTNAME,
                                             AGE = @AGE,
                                            TEAM = @TEAM,
                                            POSITION = @POSITION
                                       WHERE PLAYERID = @PLAYERID";

            using (SqlConnection sqlConnection = new SqlConnection(sqlConnectionString))
            {
                using (SqlCommand sqlCommand = new SqlCommand(updatesqlCommand, sqlConnection))
                {
                 
                    sqlCommand.Parameters.Add(new SqlParameter("@FIRSTNAME", FirstName));
                    sqlCommand.Parameters.Add(new SqlParameter("@LASTNAME", LastName));
                    sqlCommand.Parameters.Add(new SqlParameter("@AGE", Age));
                    sqlCommand.Parameters.Add(new SqlParameter("@TEAM", Team));
                    sqlCommand.Parameters.Add(new SqlParameter("@POSITION", Position));
                    sqlCommand.Parameters.Add(new SqlParameter("@PLAYERID", PlayerID));

                    sqlConnection.Open();
                    sqlCommand.ExecuteNonQuery();
                    sqlConnection.Close();
                }


            }

            return PlayerID;
        }

        public static bool DeletePlayer( int PlayerID, ISoccerPlayerConfigManager configManager)
        {
            string sqlConnectionString = configManager.SoccerPlayerConnection;
            string deletesqlCommand = @"DELETE FROM PLAYERS WHERE PLAYERID = @PLAYERID";

            using (SqlConnection sqlConnection = new SqlConnection (sqlConnectionString ))
            {
                using (SqlCommand sqlCommand = new SqlCommand (deletesqlCommand , sqlConnection ))
                {
                    sqlCommand.Parameters.Add(new SqlParameter("@PLAYERID", PlayerID));

                    sqlConnection.Open();
                    sqlCommand.ExecuteNonQuery();
                    sqlConnection.Close();
                }
            }
            return true;
        }

        public static PlayerModel? GetPlayerById (int PlayerID, ISoccerPlayerConfigManager configManager)
        {
            string sqlConnectionString = configManager.SoccerPlayerConnection;
            PlayerModel player = new PlayerModel();

            string querySql = "SELECT * FROM PLAYERS WHERE PLAYERID = @PLAYERID";

            using (SqlConnection sqlConnection = new SqlConnection (sqlConnectionString ))
            {
                using (SqlCommand sqlCommand = new SqlCommand(querySql , sqlConnection ))
                {
                    sqlCommand.Parameters.Add(new SqlParameter("@PLAYERID", PlayerID));

                    sqlConnection.Open();
                    using (SqlDataReader reader = sqlCommand.ExecuteReader ())
                    {
                        if (reader.HasRows )
                        {
                            reader.Read();

                            player.PlayerID = Convert.ToInt32(reader["PLAYERID"]);
                            player.FirstName = reader["FIRSTNAME"]?.ToString() ?? "";
                            player.LastName = reader["LASTNAME"]?.ToString() ?? "";
                            player.Age = Convert.ToInt32(reader["AGE"]);
                            player.Team = reader["TEAM"]?.ToString() ?? "";
                            player.Position = reader["POSITION"]?.ToString() ?? "";

                        }
                        else
                        {
                            throw new Exception("No Rows Found!");
                        }

                    }
                    sqlConnection.Close();
                }
            }
            return player;
        }

        public static List<PlayerModel> GetAllPlayers(ISoccerPlayerConfigManager configManager)
        {
            string sqlConnectionString = configManager.SoccerPlayerConnection;
            List<PlayerModel> playerList = new List<PlayerModel>();
            string querySql = "SELECT * FROM PLAYERS ORDER BY PLAYERID DESC ";

            using (SqlConnection sqlConnection = new SqlConnection (sqlConnectionString ))
            {
                using (SqlCommand sqlCommand = new SqlCommand (querySql , sqlConnection ))
                {
                    using (SqlDataAdapter sqlDataAdapter = new SqlDataAdapter (sqlCommand ))
                    {
                        using (DataTable dataTable = new DataTable ())
                        {
                            sqlDataAdapter.Fill(dataTable);
                            foreach (DataRow dataRow in dataTable.Rows  )
                            {
                                PlayerModel playerModel = new PlayerModel();

                                playerModel.PlayerID = Convert.ToInt32(dataRow["PLAYERID"]);
                                playerModel.FirstName = dataRow["FIRSTNAME"]?.ToString() ??"";
                                playerModel.LastName = dataRow["LASTNAME"]?.ToString() ??"";
                                playerModel.Age = Convert.ToInt32(dataRow["AGE"]);
                                playerModel.Team = dataRow["TEAM"]?.ToString() ?? "";
                                playerModel.Position = dataRow["POSITION"]?.ToString() ?? "";

                                playerList.Add(playerModel);
                            }
                        }
                    }
                }
            }
            return playerList;
        }
    }
}
