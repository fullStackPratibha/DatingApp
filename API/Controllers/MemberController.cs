using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

    [Route("api/[controller]")] //localhost:5001/api/member
    [ApiController]

    public class MemberController(AppDbContext context) : ControllerBase
{
        [HttpGet]
        public ActionResult<IReadOnlyList<AppUser>> GetMembers()
    {
        var members = context.Users.ToList();
        return members;
    }

    [HttpGet("{id}")] //localhost:5001/api/member/bob-id
    public async Task<ActionResult<AppUser>> GetMember(string id)
    {
        var member = await context.Users.FindAsync(id);
        if(member == null) return NotFound();
        return member;
        
    }

}