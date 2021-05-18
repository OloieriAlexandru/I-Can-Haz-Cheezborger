using Entities;
using System;
using System.Collections.Generic;

namespace DataAccess.Seed
{
    public static class TrendsSeed
    {
        public static ICollection<Trend> Seed()
        {
            ICollection<Trend> trends = new List<Trend>();
            AddTrend(trends, new Guid("fa682095-d421-49f7-b6ed-cdf0aa68ba8a"), "Funny", "Why so serious", "1557376304.186_U5U7u5_100x100wp.webp");
            AddTrend(trends, new Guid("72609609-31a0-42cc-92d1-c60ac13e0e37"), "Random", "Just random things. Be nice.", "1481541784.8502_e8ARAR_100x100wp.webp");
            AddTrend(trends, new Guid("44610d77-1627-47ec-a6d7-03bdd3d83c32"), "Gaming", "We don't die, we respawn!", "1557286928.6604_uTYgug_100x100wp.webp");
            AddTrend(trends, new Guid("e9e2f1a2-0534-4892-a0c4-9a5718eec16b"), "Animals", "It's so fluffy I'm gonna die!", "1557391851.3248_Za4UdA_100x100wp.webp");
            AddTrend(trends, new Guid("9dd83f1d-77da-441e-9749-73eb8d88d5fc"), "Car", "Vroom vroom!", "1557311278.4297_UNEHy6_100x100wp.webp");
            AddTrend(trends, new Guid("2976abf0-3cdb-4753-b48c-d37b144c6434"), "Sport", "The sports fanatics hub", "1557286774.0983_eGARyH_100x100wp.webp");
            AddTrend(trends, new Guid("b359535d-0fcd-49c7-8647-58aae84fa456"), "WTF", "Jaw-dropping moments", "1557310702.1267_UgysAp_100x100wp.webp");
            return trends;
        }

        private static void AddTrend(ICollection<Trend> trends, Guid id, string name, string description, string imageName)
        {
            trends.Add(new Trend
            {
                Id = id,
                Name = name,
                Description = description,
                CreatorUsername = "admin",
                CreatorId = new Guid("e03f1453-9194-47e8-83c4-9eac442f216d"),
                ImageUrl = "https://miscmedia-9gag-fun.9cache.com/images/thumbnail-facebook/" + imageName,
                ApprovedImage = true,
                ApprovedText = true
            });
        }
    }
}
