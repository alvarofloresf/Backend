using Backend.Exceptions;
using Backend.Models;
using Backend.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private IUsersServices _usersServices;
        public UsersController(IUsersServices usersServices)
        {
            _usersServices = usersServices;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserModel>>> GetUsersAsync()
        {
            try
            {
                var users = await _usersServices.GetAllUsersAsync();
                return Ok(users);
            }
            catch (NotFoundElementException ex)
            {
                return NotFound(ex.Message);
            }
            catch (InvalidElementOperationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Simething happend.");
            }
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<UserModel>> GetUserAsync(int id)
        {
            try
            {
                var user = await _usersServices.GetUserAsync(id);
                return Ok(user);
            }
            catch (NotFoundElementException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Something happend.");
            }
        }

        [HttpPost]
        public async Task<ActionResult<UserModel>> PostUserAsync([FromBody] UserModel user)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var newUser = await _usersServices.CreateUserAsync(user);
                return Created($"/api/restaurants/{newUser.Id}", newUser);
            }
            catch (NotFoundElementException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Simething happend.");
            }
        }

        [HttpDelete("{userId:int}")]
        public async Task<ActionResult> DeleteUserAsync(int userId)
        {
            try
            {
                await _usersServices.DeleteUserAsync(userId);
                return Ok();
            }
            catch (NotFoundElementException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Simething happend.");
            }
        }

        [HttpPut("{userId:int}")]
        public async Task<ActionResult<UserModel>> PutUserAsync(int userId, [FromBody] UserModel user)
        {
            try
            {
                var updatedUser = await _usersServices.UpdateUserAsync(userId, user);
                return Ok(updatedUser);
            }
            catch (NotFoundElementException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Simething happend.");
            }
        }
    }
}

