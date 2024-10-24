﻿using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;

using TestingControllersSample.Core.Interfaces;
using TestingControllersSample.ViewModels;

namespace TestingControllersSample.Controllers;

public class SessionController(IBrainstormSessionRepository sessionRepository) : Controller
{
    public static class ErrorMessage
    {
        public static string SessionNotFound => "Session not found.";
    }

    public async Task<IActionResult> Index(int? id)
    {
        if (!id.HasValue)
        {
            return RedirectToAction(
                actionName: nameof(Index),
                controllerName: "Home");
        }

        var session = await sessionRepository.GetByIdAsync(id.Value);
        if (session == null)
        {
            return Content(ErrorMessage.SessionNotFound);
        }

        var viewModel = new StormSessionViewModel()
        {
            DateCreated = session.DateCreated,
            Name = session.Name,
            Id = session.Id
        };

        return View(viewModel);
    }
}