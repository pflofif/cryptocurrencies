namespace Cryptocurrencies.MVVM.Model;

public class Cryptocurrency
{
    public string Id { get; init; }
    public string Rank { get; init; }
    public string Symbol { get; init; }
    public string Name { get; init; }
    public string Supply { get; init; }
    public string MaxSupply { get; init; }
    public string MarketCapUsd { get; init; }
    public string VolumeUsd24Hr { get; init; }
    public string PriceUsd { get; init; }
    public string ChangePercent24Hr { get; init; }
}