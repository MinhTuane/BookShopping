﻿namespace BookShopping.Contents
{
    public enum Ranks
    {
        Silver, Gold, Platinum, Diamond
    }

    public class Rank
    {
        public Ranks rankUser { get; set; }

        private readonly Dictionary<Ranks, double> values = new Dictionary<Ranks, double>
        {
            { Ranks.Silver, 0 },
            { Ranks.Gold, 0.05 },
            { Ranks.Platinum, 0.08 },
            { Ranks.Diamond, 0.12 }
        };

        public double GetValue()
        {
            return values[rankUser];
        }
        public Rank(double TotalSpend)
        {
            if (TotalSpend < 0) { throw new Exception("Error total spend"); }
            if (TotalSpend >= 0 && TotalSpend < 100) { rankUser = Ranks.Silver; }
            else if (TotalSpend >= 100 && TotalSpend < 150) { rankUser = Ranks.Gold; }
            else if (TotalSpend >= 150 && TotalSpend < 200) { rankUser = Ranks.Platinum; }
            else { rankUser = Ranks.Diamond; }
        }
    }

}
