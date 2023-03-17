using ProCardsNew.Domain.CardAggregate.ValueObjects;
using ProCardsNew.Domain.Common.Models;

namespace ProCardsNew.Domain.CardAggregate.Entities;

public class Grade: Entity<GradeId>
{
    public int GradeValue { get; }
    public DateTime GradedAtDateTime { get; }
    
    public Grade(
        GradeId id,
        int gradeValue,
        DateTime gradedAtDateTime)
        : base(id)
    {
        GradeValue = gradeValue;
        GradedAtDateTime = gradedAtDateTime;
    }

    public static Grade Create(
        int gradeValue)
    {
        return new(
            GradeId.CreateUnique(), 
            gradeValue,
            DateTime.UtcNow);
    }
}