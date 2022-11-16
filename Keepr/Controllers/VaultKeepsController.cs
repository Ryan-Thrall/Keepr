namespace Keepr.Controllers;

[ApiController]
[Route("api/[controller]")]

public class VaultKeepsController : ControllerBase
{
  private readonly VaultKeepsService _vkServ;
  private readonly Auth0Provider _auth0;

  public VaultKeepsController(Auth0Provider auth0, VaultKeepsService vkServ)
  {
    _auth0 = auth0;
    _vkServ = vkServ;
  }

  [Authorize]
  [HttpPost]
  public async Task<ActionResult<VaultKeep>> CreateVaultKeep([FromBody] VaultKeep data)
  {
    try
    {
      // Access User Info or throw error
      var userInfo = await _auth0.GetUserInfoAsync<Account>(HttpContext);
      if (userInfo == null || userInfo.Id == null)
      {
        throw new Exception("Cannot Access Account. Relogin and try again.");
      }

      VaultKeep vaultKeep = _vkServ.CreateVaultKeep(data, userInfo.Id);
      return Ok(vaultKeep);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }

  [Authorize]
  [HttpDelete("{id}")]
  public async Task<ActionResult<string>> DeleteVaultKeep(int id)
  {
    try
    {
      // Access User Info or throw error
      var userInfo = await _auth0.GetUserInfoAsync<Account>(HttpContext);
      if (userInfo == null || userInfo.Id == null)
      {
        throw new Exception("Cannot Access Account. Relogin and try again.");
      }

      _vkServ.DeleteVaultKeep(id, userInfo.Id);
      return Ok("Vault Keep Deleted.");
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }
}