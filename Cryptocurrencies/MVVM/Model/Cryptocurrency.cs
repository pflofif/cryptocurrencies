using System;
using System.Linq;

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

    private readonly string _priceUsd;

    public string PriceUsd
    {
        get => _priceUsd;
        init => _priceUsd = value[..(value.IndexOf('.') + 3)];
    }

    private readonly string _changePercent24Hr;

    public string ChangePercent24Hr
    {
        get => _changePercent24Hr;
        init => _changePercent24Hr = value[0] is '-' ? $"{value[..5]}%" : $"+{value[..4]}%";
    }
}