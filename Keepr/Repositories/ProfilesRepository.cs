namespace Keepr.Repositories;

public class ProfilesRepository : BaseRepo
{
  // SECTION Constructor
  public ProfilesRepository(IDbConnection db) : base(db)
  {
  }

  public Profile GetProfileById(string id)
  {
    string sql = @"
    SELECT 
    a.*
    FROM accounts a
    WHERE a.id = @id
    ;";

    return _db.Query<Profile>(sql, new { id }).FirstOrDefault();
  }

  public List<Keep> GetKeepsByProfile(string id)
  {
    string sql = @"
    SELECT
    k.*,
    a.*
    FROM keeps k
    JOIN accounts a ON a.id = k.creatorId
    WHERE k.creatorId = @id
    ;";

    return _db.Query<Keep, Profile, Keep>(sql, (k, p) =>
    {
      k.Creator = p;
      return k;
    }, new { id }).ToList();
  }

  public List<Vault> GetVaultsByProfile(string id)
  {
    string sql = @"
    SELECT 
    v.*,
    a.*
    FROM vaults v
    JOIN accounts a ON a.id = v.creatorId
    WHERE v.creatorId = @id
    ;";

    return _db.Query<Vault, Profile, Vault>(sql, (v, p) =>
    {
      v.Creator = p;
      return v;
    }, new { id }).ToList();
  }
}