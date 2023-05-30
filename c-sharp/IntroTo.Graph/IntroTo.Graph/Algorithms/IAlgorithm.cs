namespace IntroTo.Graph.Algorithms;

public interface IAlgorithm<in TIn, out TOut, TArgs>
{
    TOut Execute(TIn input, TArgs args);
}