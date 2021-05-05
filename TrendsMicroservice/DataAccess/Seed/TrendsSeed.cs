using Entities;
using System;
using System.Collections.Generic;

namespace DataAccess.Seed
{
    public static class TrendsSeed
    {
        public static ICollection<Trend> Seed()
        {
            return new List<Trend>
            {
                new Trend
                {
                    Id = new Guid("fa682095-d421-49f7-b6ed-cdf0aa68ba8a"),
                    Name = "Funny",
                    Description = "Why so serious",
                    CreatorUsername = "admin",
                    CreatorId = new Guid("e03f1453-9194-47e8-83c4-9eac442f216d"),
                    ImageUrl = "https://miscmedia-9gag-fun.9cache.com/images/thumbnail-facebook/1557376304.186_U5U7u5_100x100wp.webp"
                },
                new Trend
                {
                    Id = new Guid("72609609-31a0-42cc-92d1-c60ac13e0e37"),
                    Name = "Random",
                    Description = "Just random things. Be nice.",
                    CreatorUsername = "admin",
                    CreatorId = new Guid("e03f1453-9194-47e8-83c4-9eac442f216d"),
                    ImageUrl = "https://miscmedia-9gag-fun.9cache.com/images/thumbnail-facebook/1481541784.8502_e8ARAR_100x100wp.webp"
                },
                new Trend
                {
                    Id = new Guid("44610d77-1627-47ec-a6d7-03bdd3d83c32"),
                    Name = "Gaming",
                    Description = "We don't die, we respawn!",
                    CreatorUsername = "admin",
                    CreatorId = new Guid("e03f1453-9194-47e8-83c4-9eac442f216d"),
                    ImageUrl = "https://miscmedia-9gag-fun.9cache.com/images/thumbnail-facebook/1557286928.6604_uTYgug_100x100wp.webp"
                },
                new Trend
                {
                    Id = new Guid("e9e2f1a2-0534-4892-a0c4-9a5718eec16b"),
                    Name = "Animals",
                    Description = "It's so fluffy I'm gonna die!",
                    CreatorUsername = "admin",
                    CreatorId = new Guid("e03f1453-9194-47e8-83c4-9eac442f216d"),
                    ImageUrl = "https://miscmedia-9gag-fun.9cache.com/images/thumbnail-facebook/1557391851.3248_Za4UdA_100x100wp.webp"
                },
                new Trend
                {
                    Id = new Guid("9dd83f1d-77da-441e-9749-73eb8d88d5fc"),
                    Name = "Car",
                    Description = "Vroom vroom!",
                    CreatorUsername = "admin",
                    CreatorId = new Guid("e03f1453-9194-47e8-83c4-9eac442f216d"),
                    ImageUrl = "https://miscmedia-9gag-fun.9cache.com/images/thumbnail-facebook/1557311278.4297_UNEHy6_100x100wp.webp"
                },
                new Trend
                {
                    Id = new Guid("2976abf0-3cdb-4753-b48c-d37b144c6434"),
                    Name = "Sport",
                    Description = "The sports fanatics hub",
                    CreatorUsername = "admin",
                    CreatorId = new Guid("e03f1453-9194-47e8-83c4-9eac442f216d"),
                    ImageUrl = "https://miscmedia-9gag-fun.9cache.com/images/thumbnail-facebook/1557286774.0983_eGARyH_100x100wp.webp"
                },
                new Trend
                {
                    Id = new Guid("b359535d-0fcd-49c7-8647-58aae84fa456"),
                    Name = "WTF",
                    Description = "Jaw-dropping moments",
                    CreatorUsername = "admin",
                    CreatorId = new Guid("e03f1453-9194-47e8-83c4-9eac442f216d"),
                    ImageUrl = "https://miscmedia-9gag-fun.9cache.com/images/thumbnail-facebook/1557310702.1267_UgysAp_100x100wp.webp"
                }
            };
        }
    }
}
