namespace Keepr.Services;

public class KeepsService
{

  private readonly KeepsRepository _keepsRepo;

  public KeepsService(KeepsRepository keepsRepo)
  {
    _keepsRepo = keepsRepo;
  }

  public Keep CreateKeep(Keep data, Account userInfo)
  {
    data.CreatorId = userInfo.Id;

    Keep keep = _keepsRepo.CreateKeep(data);
    keep.Creator = userInfo;
    return keep;
  }

  public List<Keep> GetAllKeeps()
  {
    return _keepsRepo.GetAllKeeps();
  }

  public Keep GetKeepById(int keepId, bool viewed)
  {
    Keep keep = _keepsRepo.GetKeepById(keepId);
    if (keep == null)
    {
      throw new Exception("Bad Keep Id!");
    }
    if (viewed)
    {
      keep.Views++;
      _keepsRepo.AddView(keep);
    }
    return keep;
  }

  public Keep EditKeep(int id, Keep data, string userId)
  {
    Keep keep = GetKeepById(id, false);

    if (keep.CreatorId != userId)
    {
      throw new Exception("This is not your Keep to Edit!");
    }

    keep.Name = data.Name;
    keep.Description = data.Description;
    return _keepsRepo.EditKeep(keep);
  }

  public void DeleteKeep(int id, string userId)
  {
    Keep keep = GetKeepById(id, false);
    if (keep.CreatorId != userId)
    {
      throw new Exception("This is not your Keep to Delete!");
    }

    _keepsRepo.DeleteKeep(id);
  }
}