using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
    using SoccerPlayer.DataAcess;
using SoccerPlayer.DataAcess.Controllers;
using SoccerPlayer.DataAccess.Models;

namespace SoccerPlayerMVCApplication.Models
{
    public class PlayerViewModel
    {
        private ISoccerPlayerConfigManager  _configuration;

        public List<PlayerModel> PlayerList { get; set; }

        public PlayerModel CurrentPlayer { get; set; }

        public bool IsActionSuccess { get; set; }

        public string ActionMessage { get; set; }

        public PlayerViewModel(ISoccerPlayerConfigManager configuration)
        {
            _configuration = configuration;
            PlayerList = GetAllPlayers();
            CurrentPlayer = PlayerList.FirstOrDefault();
        }

        public PlayerViewModel(ISoccerPlayerConfigManager configuration, int PlayerID)
        {
            _configuration = configuration;
            PlayerList = new List<PlayerModel>();

            if (PlayerID > 0)
            {
                CurrentPlayer = GetPlayer(PlayerID);
            }
            else
            {
                CurrentPlayer = new PlayerModel();
            }
        }

        public List<PlayerModel> GetAllPlayers()
        {
            return PlayerController.GetAllPlayers(_configuration);
        }

        public PlayerModel GetPlayer(int PlayerID)
        {
            return PlayerController.GetPlayerById(PlayerID , _configuration);
        }
    }
}
