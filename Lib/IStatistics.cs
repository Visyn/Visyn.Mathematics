namespace Visyn.Mathematics
{
    public interface IStatistics
    {
        double Mean { get; }
        double Variance { get; }
        double StandardDeviation { get; }

        double Min { get; }
        double Max { get; }
        int SampleSize { get; }
    }
}
