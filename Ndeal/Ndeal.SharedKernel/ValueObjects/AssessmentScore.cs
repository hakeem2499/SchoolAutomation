using System;
using Ndeal.SharedKernel; // Or your shared kernel namespace

namespace Ndeal.SharedKernel.ValueObjects;

public class AssessmentScore : ValueObject
{
    public decimal Score { get; }
    public decimal MaxScore { get; } // Add MaxScore for context and validation

    public AssessmentScore(decimal score, decimal maxScore)
    {
        if (maxScore <= 0)
        {
            throw new ArgumentOutOfRangeException(
                nameof(maxScore),
                "Max score must be greater than zero."
            );
        }

        if (score < 0 || score > maxScore) // Score cannot be negative or more than MaxScore
        {
            throw new ArgumentOutOfRangeException(
                nameof(score),
                $"Score must be between 0 and {maxScore}."
            );
        }

        Score = score;
        MaxScore = maxScore;
    }

    public decimal Percentage => MaxScore == 0 ? 0 : Score / MaxScore * 100; // Calculate percentage

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Score;
        yield return MaxScore;
    }

    public override string ToString()
    {
        return $"{Score} / {MaxScore} ({Percentage:F1}%)"; // Example: "85 / 100 (85.0%)"
    }

    // Optional: Add a method to create a copy with updated values (if needed)
    public AssessmentScore WithScore(decimal newScore) => new(newScore, MaxScore);

    public AssessmentScore WithMaxScore(decimal newMaxScore) => new(Score, newMaxScore);
}
