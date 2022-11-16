namespace Keepr.Services;

public class VaultsService
{
  private readonly VaultsRepository _vaultsRepo;

  public VaultsService(VaultsRepository vaultsRepo)
  {
    _vaultsRepo = vaultsRepo;
  }

  public Vault CreateVault(Vault data, Account userInfo)
  {
    data.CreatorId = userInfo.Id;

    Vault vault = _vaultsRepo.CreateKeep(data);
    vault.Creator = userInfo;
    return vault;
  }

  public Vault GetVaultById(int vaultId, string userId)
  {

    Vault vault = _vaultsRepo.GetVaultById(vaultId);
    if (vault == null)
    {
      throw new Exception("Bad Vault Id!");
    }

    if (vault.isPrivate)
    {
      if (userId == null)
      {
        throw new Exception("This Vault is Private!!!");
      }

      if (vault.CreatorId != userId)
      {
        throw new Exception("This Vault Is Private!!!");
      }
    }
    return vault;
  }

  public List<Keep> GetKeepsInVault(int vaultId, string userId)
  {
    Vault vault = GetVaultById(vaultId, userId);
    return _vaultsRepo.GetKeepsInVault(vaultId);
  }

  public Vault EditVault(int id, Vault data, string userId)
  {
    Vault vault = GetVaultById(id, userId);

    if (vault.CreatorId != userId)
    {
      throw new Exception("This is not your Vault to Edit!");
    }
    vault.Name = data.Name;
    vault.isPrivate = data.isPrivate;
    return _vaultsRepo.EditVault(vault);

  }

  public void DeletVault(int id, string userId)
  {
    Vault vault = GetVaultById(id, userId);
    if (vault.CreatorId != userId)
    {
      throw new Exception("This is not your Vault to Delete!");
    }

    _vaultsRepo.DeleteKeep(id);
  }


}