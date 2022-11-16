namespace Keepr.Interfaces;

public interface IHasCreator : IDbItem<int>
{
  string CreatorId { get; set; }
  Account Creator { get; set; }
}