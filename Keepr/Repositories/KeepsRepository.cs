namespace Keepr.Repositories;

public class KeepsRepository : BaseRepo
{
  // SECTION Constructor
  public KeepsRepository(IDbConnection db) : base(db)
  {
  }

  public Keep CreateKeep(Keep data)
  {
    string sql = @"
    INSERT INTO keeps(creatorId, name, description, img)
    VALUES(@CreatorId, @Name, @Description, @Img);
    SELECT LAST_INSERT_ID()
    ;";

    // Set The Created Time for this keep
    data.CreatedAt = DateTime.Now;
    data.UpdatedAt = DateTime.Now;

    // Run the Sql and Return the Object Created
    data.Id = _db.ExecuteScalar<int>(sql, data);
    return data;
  }

  public List<Keep> GetAllKeeps()
  {
    string sql = @"
    SELECT 
    k.*,
    COUNT(vk.id) AS kept,
    a.*
    FROM keeps k
    JOIN accounts a ON a.id = k.creatorId
    LEFT JOIN vaultkeeps vk ON vk.keepId = k.id
    GROUP BY k.id
    ;";

    return _db.Query<Keep, Profile, Keep>(sql, (k, p) =>
    {
      k.Creator = p;
      return k;
    }).ToList();

  }

  public Keep GetKeepById(int keepId)
  {
    string sql = @"
    SELECT 
    k.*,
    COUNT(vk.id) AS kept,
    a.*
    FROM keeps k
    JOIN accounts a ON a.id = k.creatorId
    LEFT JOIN vaultkeeps vk ON vk.keepId = k.id
    WHERE k.id = @keepId
    GROUP BY k.id
    ;";

    return _db.Query<Keep, Profile, Keep>(sql, (k, p) =>
    {
      k.Creator = p;
      return k;
    }, new { keepId }).FirstOrDefault();
  }

  public void AddView(Keep keep)
  {
    string sql = @"
      UPDATE keeps SET
      views = @Views
      WHERE id = @Id
    ;";

    _db.Execute(sql, keep);
  }

  public Keep EditKeep(Keep keep)
  {
    string sql = @"
    UPDATE keeps SET
    name = @Name,
    description = @Description
    WHERE id = @Id
    ;";
    keep.UpdatedAt = DateTime.Now;
    _db.Execute(sql, keep);

    return keep;
  }

  public void DeleteKeep(int id)
  {
    string sql = @"DELETE FROM keeps WHERE id = @id;";

    _db.Execute(sql, new { id });
  }
}