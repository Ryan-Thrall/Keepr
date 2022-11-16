namespace Keepr.Controllers;

[ApiController]
[Route("api/[controller]")]

public class ProfilesController : ControllerBase
{
  private readonly ProfilesService _profilesServ;
  private readonly Auth0Provider _auth0;
  public ProfilesController(Auth0Provider auth0, ProfilesService profilesServ)
  {
    _auth0 = auth0;
    _profilesServ = profilesServ;
  }

  [HttpGet("{id}")]
  public ActionResult<Profile> GetProfileById(string id)
  {
    try
    {
      Profile profile = _profilesServ.GetProfileById(id);
      return Ok(profile);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }

  [HttpGet("{id}/keeps")]
  public ActionResult<Keep> GetKeepsByProfile(string id)
  {
    try
    {
      List<Keep> keeps = _profilesServ.GetKeepsByProfile(id);
      return Ok(keeps);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }

  [HttpGet("{id}/vaults")]
  public async Task<ActionResult<Keep>> GetVaultsByProfileAsync(string id)
  {
    try
    {
      // Access User Info or throw error
      var userInfo = await _auth0.GetUserInfoAsync<Account>(HttpContext);

      List<Vault> vaults = _profilesServ.GetVaultsByProfile(id, userInfo?.Id);
      return Ok(vaults);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }
}