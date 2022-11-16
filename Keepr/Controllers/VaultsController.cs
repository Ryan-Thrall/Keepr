namespace Keepr.Controllers;

[ApiController]
[Route("api/[controller]")]

public class VaultsController : ControllerBase
{
  private readonly VaultsService _vaultsServ;
  private readonly Auth0Provider _auth0;

  public VaultsController(Auth0Provider auth0, VaultsService vaultsServ)
  {
    _auth0 = auth0;
    _vaultsServ = vaultsServ;
  }

  [Authorize]
  [HttpPost]
  public async Task<ActionResult<Vault>> CreateVault([FromBody] Vault data)
  {
    try
    {
      // Access User Info or throw error
      var userInfo = await _auth0.GetUserInfoAsync<Account>(HttpContext);
      if (userInfo == null || userInfo.Id == null)
      {
        throw new Exception("Cannot Access Account. Relogin and try again.");
      }

      Vault vault = _vaultsServ.CreateVault(data, userInfo);
      return Ok(vault);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }


  [HttpGet("{id}")]
  public async Task<ActionResult<Vault>> GetKeepById(int id)
  {
    try
    {
      // Access User Info or throw error
      var userInfo = await _auth0.GetUserInfoAsync<Account>(HttpContext);

      Vault vault = _vaultsServ.GetVaultById(id, userInfo?.Id);
      return Ok(vault);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }


  [HttpGet("{id}/keeps")]
  public async Task<ActionResult<Keep>> GetKeepsInVault(int id)
  {
    try
    {
      // Access User Info or throw error
      var userInfo = await _auth0.GetUserInfoAsync<Account>(HttpContext);

      List<Keep> keeps = _vaultsServ.GetKeepsInVault(id, userInfo?.Id);
      return Ok(keeps);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }

  [Authorize]
  [HttpPut("{id}")]
  public async Task<ActionResult<Vault>> EditVault(int id, [FromBody] Vault data)
  {
    try
    {
      // Access User Info or throw error
      var userInfo = await _auth0.GetUserInfoAsync<Account>(HttpContext);
      if (userInfo == null || userInfo.Id == null)
      {
        throw new Exception("Cannot Access Account. Relogin and try again.");
      }

      Vault newVault = _vaultsServ.EditVault(id, data, userInfo?.Id);
      return Ok(newVault);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }

  [Authorize]
  [HttpDelete("{id}")]
  public async Task<ActionResult<string>> DeleteVault(int id)
  {
    try
    {
      // Access User Info or throw error
      var userInfo = await _auth0.GetUserInfoAsync<Account>(HttpContext);
      if (userInfo == null || userInfo.Id == null)
      {
        throw new Exception("Cannot Access Account. Relogin and try again.");
      }

      _vaultsServ.DeletVault(id, userInfo.Id);
      return Ok("Vault Deleted");
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }
}