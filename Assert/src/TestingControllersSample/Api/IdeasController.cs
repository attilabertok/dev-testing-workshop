﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;

using TestingControllersSample.ClientModels;
using TestingControllersSample.Core.Interfaces;
using TestingControllersSample.Core.Model;

namespace TestingControllersSample.Api;

[Route("api/ideas")]
public class IdeasController(IBrainstormSessionRepository sessionRepository) : ControllerBase
{
    [HttpGet("forsession/{sessionId}")]
    public async Task<IActionResult> ForSession(int sessionId)
    {
        var session = await sessionRepository.GetByIdAsync(sessionId);
        if (session == null)
        {
            return NotFound(sessionId);
        }

        var result = session.Ideas.Select(idea => new IdeaDto()
        {
            Id = idea.Id,
            Name = idea.Name,
            Description = idea.Description,
            DateCreated = idea.DateCreated
        }).ToList();

        return Ok(result);
    }

    [HttpPost("create")]
    public async Task<IActionResult> Create([FromBody] NewIdeaModel model)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var session = await sessionRepository.GetByIdAsync(model.SessionId);
        if (session == null)
        {
            return NotFound(model.SessionId);
        }

        var idea = new Idea()
        {
            DateCreated = DateTimeOffset.Now,
            Description = model.Description,
            Name = model.Name
        };
        session.AddIdea(idea);

        await sessionRepository.UpdateAsync(session);

        return Ok(session);
    }

    [HttpGet("forsessionactionresult/{sessionId}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    public async Task<ActionResult<List<IdeaDto>>> ForSessionActionResult(int sessionId)
    {
        var session = await sessionRepository.GetByIdAsync(sessionId);

        if (session == null)
        {
            return NotFound(sessionId);
        }

        var result = session.Ideas.Select(idea => new IdeaDto()
        {
            Id = idea.Id,
            Name = idea.Name,
            Description = idea.Description,
            DateCreated = idea.DateCreated
        }).ToList();

        return result;
    }

    [HttpPost("createactionresult")]
    [ProducesResponseType(201)]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    public async Task<ActionResult<BrainstormSession>> CreateActionResult([FromBody] NewIdeaModel model)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var session = await sessionRepository.GetByIdAsync(model.SessionId);

        if (session == null)
        {
            return NotFound(model.SessionId);
        }

        var idea = new Idea()
        {
            DateCreated = DateTimeOffset.Now,
            Description = model.Description,
            Name = model.Name
        };
        session.AddIdea(idea);

        await sessionRepository.UpdateAsync(session);

        return CreatedAtAction(nameof(CreateActionResult), new { id = session.Id }, session);
    }
}