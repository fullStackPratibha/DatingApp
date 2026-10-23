using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;

    public class MemberController(AppDbContext context) : BaseApiController
{
    [AllowAnonymous]
    [HttpGet] //localhost:5001/api/member
    public async Task<ActionResult<IReadOnlyList<AppUser>>> GetMembers()
    {
        var members = await context.Users.ToListAsync();
        return Ok(members);
    }

    [Authorize]
    [HttpGet("{id}")] //localhost:5001/api/member/{id}
    public async Task<ActionResult<AppUser>> GetMember(string id)
    {
        var member = await context.Users.FindAsync(id);
        if(member == null) return NotFound();
        return Ok(member);
        
    }

}