﻿using System;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;

using TestingControllersSample.Core.Interfaces;
using TestingControllersSample.Core.Model;
using TestingControllersSample.ViewModels;

namespace TestingControllersSample.Controllers;

public class HomeController(IBrainstormSessionRepository sessionRepository) : Controller
{
    private readonly IBrainstormSessionRepository sessionRepository = sessionRepository ?? throw new ArgumentNullException(nameof(sessionRepository));

    public async Task<IActionResult> Index()
    {
        var sessionList = await sessionRepository.ListAsync();

        var model = sessionList.Select(session => new StormSessionViewModel()
        {
            Id = session.Id,
            DateCreated = session.DateCreated,
            Name = session.Name,
            IdeaCount = session.Ideas.Count
        });

        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Index(NewSessionModel model)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        await sessionRepository.AddAsync(new BrainstormSession()
        {
            DateCreated = DateTimeOffset.Now,
            Name = model.SessionName
        });

        return RedirectToAction(actionName: nameof(Index));
    }
}
