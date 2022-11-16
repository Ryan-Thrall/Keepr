namespace Keepr.Repositories;

public class VaultsRepository : BaseRepo
{
  public VaultsRepository(IDbConnection db) : base(db)
  {
  }

  public Vault CreateKeep(Vault data)
  {
    string sql = @"
    INSERT INTO vaults(creatorId, name, description, isPrivate, img)
    VALUES(@CreatorId, @Name, @Description, @IsPrivate, @Img);
        SELECT LAST_INSERT_ID()
    ;";

    // Set The Created Time for this keep
    data.CreatedAt = DateTime.Now;
    data.UpdatedAt = DateTime.Now;

    // Run the Sql and Return the Object Created
    data.Id = _db.ExecuteScalar<int>(sql, data);
    return data;
  }

  public Vault GetVaultById(int vaultId)
  {
    string sql = @"
    SELECT 
    v.*,
    a.*
    FROM vaults v
    JOIN accounts a ON a.id = v.creatorId
    WHERE v.id = @VaultId
    GROUP BY v.id
    ;";

    return _db.Query<Vault, Profile, Vault>(sql, (v, p) =>
    {
      v.Creator = p;
      return v;
    }, new { vaultId }).FirstOrDefault();
  }

  public List<Keep> GetKeepsInVault(int vaultId)
  {
    string sql = @"
    SELECT 
      k.*,
      a.*,
      vk.*
    FROM vaultkeeps vk
      JOIN keeps k ON k.id = vk.keepId
      JOIN accounts a ON a.id = k.creatorId
    WHERE vk.vaultId = @vaultId;
    ;";

    return _db.Query<Keep, Profile, VaultKeep, Keep>(sql, (k, p, vk) =>
    {
      k.Creator = p;
      k.vaultKeepId = vk.Id;
      return k;
    }, new { vaultId }).ToList();
  }

  public Vault EditVault(Vault vault)
  {
    string sql = @"
    UPDATE vaults SET
    name = @Name,
    isPrivate = @IsPrivate
    WHERE id = @Id
    ;";
    vault.UpdatedAt = DateTime.Now;
    _db.Execute(sql, vault);

    return vault;
  }

  public void DeleteKeep(int id)
  {
    string sql = @"DELETE FROM vaults WHERE id = @id;";

    _db.Execute(sql, new { id });
  }
}