using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AssetwiseApi.Context;
using AssetwiseApi.Models;
// using AssetwiseApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace AssetwiseApi.Controllers;
[Authorize(Roles = UserRoles.Admin)]
[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase {
    private readonly ILogger<UserController> _logger;
    // private readonly EmailSenderService _emailService;
    private readonly UserManager<User> _userManager;


    public UserController(ILogger<UserController?> logger, AWContext context,UserManager<User> userManager)
    {
        _logger = logger;
        // _emailService = emailService;
        _userManager = userManager;
    }

    [HttpGet()]
    public Task<List<User>> Get()
    {
        return _userManager.Users.ToListAsync();
    }

    [HttpGet("{id}")]
    public Task<User?> GetUser(string id)
    {
        return _userManager.FindByIdAsync(id);
    }

    // [HttpPost("create")]
    // public async Task<IActionResult> Post(User newUser){
    //     // await _userManager.Users.Add(newUser);
    //     // await _userManager.Users.SaveChanges();
    //     return CreatedAtAction(nameof(Get),new {id = newUser.Id },newUser);
    // }

    // [HttpPost("createEmail")]
    // public async Task<IActionResult> PostEmail(){
    //     //Service to send email
    //     //remove cast
    //     return (IActionResult)_emailService.SendEmailAsync();
    //     // return ;
    //     // return CreatedAtAction(nameof(Get),);
    // }

    [HttpPatch("update/{id}")]
    public async Task<IActionResult> Put(int id,JsonPatchDocument<User> patchDoc){
        // var user = await _userManager.GetAsync(id);
        if(patchDoc is null){
            return BadRequest();
        }
        
        // updatedUser.Id = id;
        // await _userManager.UpdateAsync(id,updatedUser);
        return NoContent();
    }

}