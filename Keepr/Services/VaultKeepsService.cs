namespace Keepr.Services;

public class VaultKeepsService
{
  private readonly VaultKeepsRepository _vkRepo;
  private readonly VaultsService _vaultsServ;

  public VaultKeepsService(VaultKeepsRepository vkRepo, VaultsService vaultsServ)
  {
    _vkRepo = vkRepo;
    _vaultsServ = vaultsServ;
  }

  public VaultKeep CreateVaultKeep(VaultKeep data, string userId)
  {
    data.CreatorId = userId;

    Vault vault = _vaultsServ.GetVaultById(data.VaultId, userId);
    if (vault.CreatorId != userId)
    {
      throw new Exception("You may only add to your own vaults!!");
    }

    VaultKeep vaultKeep = _vkRepo.CreateVaultKeep(data);
    return vaultKeep;
  }

  private VaultKeep GetVaultKeepById(int vkId)
  {
    VaultKeep vaultKeep = _vkRepo.GetVaultKeepById(vkId);
    if (vaultKeep == null)
    {
      throw new Exception("Bad VaultKeep Id!");
    }
    return vaultKeep;
  }

  public void DeleteVaultKeep(int vkId, string userId)
  {
    VaultKeep vaultKeep = GetVaultKeepById(vkId);
    if (vaultKeep.CreatorId != userId)
    {
      throw new Exception("This isn't your vault to edit!");
    }

    _vkRepo.DeleteVaultKeep(vkId);
  }
}