using DrahtenWeb.Dtos;
using DrahtenWeb.Models;
using DrahtenWeb.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace DrahtenWeb.Controllers
{
    [Authorize]
    public class HistoryController : Controller
    {
        [HttpGet]
        public IActionResult ViewHistory()
        {
            return View();
        }
    }
}
