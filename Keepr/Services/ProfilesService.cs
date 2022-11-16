namespace Keepr.Services;

public class ProfilesService
{
  private readonly ProfilesRepository _profilesRepo;

  public ProfilesService(ProfilesRepository profilesRepo)
  {
    _profilesRepo = profilesRepo;
  }

  public Profile GetProfileById(string id)
  {
    Profile profile = _profilesRepo.GetProfileById(id);
    if (profile == null)
    {
      throw new Exception("Bad Profile Id!");
    }

    return profile;
  }

  public List<Keep> GetKeepsByProfile(string id)
  {
    Profile profile = GetProfileById(id);
    return _profilesRepo.GetKeepsByProfile(id);
  }

  public List<Vault> GetVaultsByProfile(string id, string userId)
  {
    Profile profile = GetProfileById(id);
    List<Vault> vaults = _profilesRepo.GetVaultsByProfile(id);

    // If Profile is not the users
    if (id != userId)
    {
      // Filter the private vaults out
      List<Vault> filteredVaults = vaults.Where(v => v.isPrivate == false).ToList();
      return filteredVaults;
    }

    // Else Return all Vaults
    return vaults;
  }
}