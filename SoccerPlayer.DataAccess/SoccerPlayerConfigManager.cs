using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SoccerPlayer.DataAcess;

namespace SoccerPlayer.DataAcess 
{
    public class SoccerPlayerConfigManager : ISoccerPlayerConfigManager
    {
        private readonly IConfiguration _configuration;

        public SoccerPlayerConfigManager(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public SoccerPlayerConfigManager () 
        {

        }

        public string SoccerPlayerConnection
        {
            get
            {
                return _configuration["ConnectionStrings:SoccerPlayer"];
            }
        }

        public string GetConnectionString(string connectionName)
        {
            return _configuration.GetConnectionString(connectionName);
        }
    }
}
