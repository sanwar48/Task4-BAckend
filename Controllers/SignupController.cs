using ChatApp.Models;
using ChatApp.Services;
using ChatApp.Validations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using System.Text;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ChatApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SignupController : ControllerBase
    {
        private readonly ISignupService _signupservice;
        private readonly ISignupValidations _signupValidations;

        public SignupController(ISignupService signupservice, ISignupValidations signupValidations)
        {
            this._signupservice = signupservice;
            this._signupValidations = signupValidations;
        }
        // GET: api/<SignupController>
        [HttpGet("{pageIndex:int}")]
        [AllowAnonymous]
        public ActionResult<(List<Signup>, string)> Get(int pageIndex)
        {

            var temp = _signupservice.Get(pageIndex);

            return Ok(new { data = temp.Item1, total = temp.Item2 });
        } 

        // GET api/<SignupController>/5
        [HttpGet("{id}")]
        public ActionResult<Signup> Get(string id)
        {
            Signup User =  _signupservice.Get(id);

            if(User== null)
            {
                return NotFound("User not found");
            }
            return User;
        }

        // POST api/<SignupController>
        [HttpPost]
        public ActionResult Post([FromBody] Signup newUser)
        {
            bool uniquEmail = _signupValidations.IsUniqueEamil(newUser.Email);

            bool uniqueUserName = _signupValidations.IsUniqueUsername(newUser.Name);

            if(uniquEmail==false && uniqueUserName==false) return NotFound("email and userName already exists");

            if(uniqueUserName==false) return NotFound("Username already exists");

            if (uniquEmail==false) return NotFound("email already exists");


            if(_signupValidations.IsStrongPassword(newUser.Password)==false) return NotFound("weak password. Please enter uppercase, lowercase, digit and specialchar. Minimum lengtyh 8");
            newUser.Password = _signupValidations.PasswordEncryp256(newUser.Password);





            _signupservice.Create(newUser);

            return CreatedAtAction(nameof(Get), new { id = newUser.Id}, newUser);
        }

        // PUT api/<SignupController>/5
        [HttpPut("{id}")]
        public ActionResult Put(string id, [FromBody] Signup newUserData)
        {
            var ExistingUser = _signupservice.Get(id);

            if (ExistingUser == null)
            {
                return NotFound($"student with {id} not found");
            }

            _signupservice.Update(id, newUserData);
            return NoContent();
        }

        // DELETE api/<SignupController>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(string id)
        {
            var targetUser = _signupservice.Get(id);

            if(targetUser == null)
            {
                return NotFound("user not Exists");
            }
            _signupservice.Delete(id);
            return Ok("target user deleted");
        }
    }
}
