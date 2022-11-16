namespace Keepr.Models;

public class Vault : IHasCreator
{
  public string Name { get; set; }
  public string Description { get; set; }
  public string Img { get; set; }
  public bool isPrivate { get; set; }

  #region IHasCreator Additions
  public string CreatorId { get; set; }
  public Profile Creator { get; set; }

  #endregion

  #region IDbItem Additions
  public int Id { get; set; }
  public DateTime CreatedAt { get; set; }
  public DateTime UpdatedAt { get; set; }
  Account IHasCreator.Creator { get; set; }

  #endregion
}