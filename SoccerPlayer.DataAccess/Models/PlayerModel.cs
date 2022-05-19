namespace SoccerPlayer.DataAccess.Models
{
    public class PlayerModel
    {
        public int PlayerID { get; set; }
        public string FirstName { get; set; } = "";
        public string LastName { get; set; } = "";
        public int Age { get; set; }
        public string Team { get; set; } = "";
        public string Position { get; set; } = "";
    }
}
