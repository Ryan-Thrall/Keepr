namespace Keepr.Repositories;

public class VaultKeepsRepository : BaseRepo
{
  public VaultKeepsRepository(IDbConnection db) : base(db)
  {
  }

  public VaultKeep CreateVaultKeep(VaultKeep data)
  {
    string sql = @"
    INSERT INTO vaultkeeps(creatorId, vaultId, keepId)
    VALUES(@CreatorId, @vaultId, @keepId);
        SELECT LAST_INSERT_ID()
    ;";

    // Set The Created Time for this keep
    data.CreatedAt = DateTime.Now;
    data.UpdatedAt = DateTime.Now;

    // Run the Sql and Return the Object Created
    data.Id = _db.ExecuteScalar<int>(sql, data);
    return data;
  }

  public VaultKeep GetVaultKeepById(int vkId)
  {
    string sql = @"
    SELECT
    vk.*
    FROM vaultkeeps vk
    WHERE vk.id = @vkId
    ;";

    return _db.Query<VaultKeep>(sql, new { vkId }).FirstOrDefault();
  }

  public void DeleteVaultKeep(int vkId)
  {
    string sql = @"DELETE FROM vaultkeeps WHERE id = @vkId;";

    _db.Execute(sql, new { vkId });
  }
}