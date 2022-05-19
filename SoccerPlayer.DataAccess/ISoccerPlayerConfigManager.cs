using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoccerPlayer.DataAcess
{
    public interface ISoccerPlayerConfigManager
    {
        string SoccerPlayerConnection { get; }

        string GetConnectionString(string connectionName);
       
    }
}
