using Microsoft.AspNetCore.Mvc;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Web.Mvc;
using Microsoft.Extensions.Logging;
using SoccerPlayerMVCApplication.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using SoccerPlayer.DataAcess.Controllers;
using SoccerPlayer.DataAcess;
using SoccerPlayer.DataAccess.Models;
using Microsoft.AspNetCore.Mvc.Controllers;
using Controller = Microsoft.AspNetCore.Mvc.Controller;
using HttpPostAttribute = Microsoft.AspNetCore.Mvc.HttpPostAttribute;

namespace SoccerPlayerMVCApplication.Controllers
{
    public class PlayersController : Controller 
    {
        private readonly ISoccerPlayerConfigManager  _configuration;

        public PlayersController(ISoccerPlayerConfigManager  configuration)
        {
            _configuration = configuration;
        }

        public IActionResult Index()
        {
            PlayerViewModel model = new PlayerViewModel(_configuration);
            return View(model);
        }

        [HttpPost]
        public IActionResult Index(int PlayerID, string FirstName, string LastName, string Age, string Team, string Position)
        {
            if (PlayerID  > 0)
            {
                PlayerController.UpdatePlayer(PlayerID , FirstName, LastName, Age, Team, Position,  _configuration);
            }
            else
            {
                PlayerController.CreatePlayer(FirstName, LastName, Age, Team, Position, _configuration);
            }

            PlayerViewModel model = new PlayerViewModel(_configuration);
            model.IsActionSuccess = true;
            model.ActionMessage = "Player has been saved successfully";

            return View(model);
        }

        public IActionResult Update(int id)
        {
            PlayerViewModel model = new PlayerViewModel(_configuration, id);
            return View(model);
        }

        public IActionResult Delete(int id)
        {
            if (id > 0)
            {
                PlayerController.DeletePlayer(id, _configuration);
            }

            PlayerViewModel model = new PlayerViewModel(_configuration);
            model.IsActionSuccess = true;
            model.ActionMessage = "Player has been deleted successfully";
            return View("Index", model);
        }
    }
}
