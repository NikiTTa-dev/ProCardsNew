using ProCardsNew.Domain.CardAggregate.ValueObjects;
using ProCardsNew.Domain.Common.Models;

namespace ProCardsNew.Domain.CardAggregate.Entities;

public class Side: Entity<SideId>
{
    public string SideName { get; private set; }
    
    private Side(
        SideId id,
        string sideName)
        : base(id)
    {
        SideName = sideName;
    }

    public static Side Create(string sideName)
    {
        return new(
            SideId.CreateUnique(),
            sideName);
    }
    
#pragma warning disable CS8618
    // ReSharper disable once UnusedMember.Local
    private Side()
    {
    }
#pragma warning restore CS8618
}