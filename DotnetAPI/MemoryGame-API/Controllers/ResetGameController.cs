using MemoryGame_API.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MemoryGame_API.Controllers
{
    [Route("/[controller]")]
    [ApiController]
    public class ResetGameController : ControllerBase
    {
        
        private IConfiguration Configuration { get; set; }

        public ResetGameController(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // GET: api/<ResetGameController>
        [HttpGet]
        public IEnumerable<ResetGame> Get()
        {
            MemorygameContext context = new MemorygameContext(Configuration);
            return context.ResetGame;
        }

        //// GET api/<ResetGameController>/5
        //[HttpGet("{id}")]
        //public string Get(int id)
        //{
        //    return "value";
        //}

        // POST api/<ResetGameController>
        [HttpPost]
        public ResetGame Post([FromBody] ResetGame resetGame)
        {
            MemorygameContext context = new MemorygameContext(Configuration);
            context.ResetGame.Add(resetGame);
            context.SaveChanges();
            return resetGame;
        }

        //// PUT api/<ResetGameController>/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        //// DELETE api/<ResetGameController>/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
