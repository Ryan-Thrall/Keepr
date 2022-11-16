namespace Keepr.Controllers;

[ApiController]
[Route("api/[controller]")]

public class KeepsController : ControllerBase
{
  private readonly KeepsService _keepsServ;
  private readonly Auth0Provider _auth0;
  public KeepsController(Auth0Provider auth0, KeepsService keepsServ)
  {
    _auth0 = auth0;
    _keepsServ = keepsServ;
  }

  [Authorize]
  [HttpPost]
  public async Task<ActionResult<Keep>> CreateKeep([FromBody] Keep data)
  {
    try
    {
      // Access User Info or throw error
      var userInfo = await _auth0.GetUserInfoAsync<Account>(HttpContext);
      if (userInfo == null || userInfo.Id == null)
      {
        throw new Exception("Cannot Access Account. Relogin and try again.");
      }

      // Create and return a Keep
      Keep keep = _keepsServ.CreateKeep(data, userInfo);
      return Ok(keep);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }

  [HttpGet]
  public ActionResult<Keep> GetAllKeeps()
  {
    try
    {
      List<Keep> keeps = _keepsServ.GetAllKeeps();
      return Ok(keeps);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }

  [HttpGet("{id}")]
  public ActionResult<Keep> GetKeepById(int id)
  {
    try
    {
      Keep keep = _keepsServ.GetKeepById(id, true);
      return Ok(keep);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }

  [Authorize]
  [HttpPut("{id}")]
  public async Task<ActionResult<Keep>> EditKeep(int id, [FromBody] Keep data)
  {
    try
    {
      // Access User Info or throw error
      var userInfo = await _auth0.GetUserInfoAsync<Account>(HttpContext);
      if (userInfo == null || userInfo.Id == null)
      {
        throw new Exception("Cannot Access Account. Relogin and try again.");
      }

      Keep newKeep = _keepsServ.EditKeep(id, data, userInfo?.Id);
      return Ok(newKeep);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }

  [Authorize]
  [HttpDelete("{id}")]
  public async Task<ActionResult<string>> DeleteKeep(int id)
  {
    try
    {
      // Access User Info or throw error
      var userInfo = await _auth0.GetUserInfoAsync<Account>(HttpContext);
      if (userInfo == null || userInfo.Id == null)
      {
        throw new Exception("Cannot Access Account. Relogin and try again.");
      }

      _keepsServ.DeleteKeep(id, userInfo?.Id);
      return Ok("Keep Deleted");
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }
}