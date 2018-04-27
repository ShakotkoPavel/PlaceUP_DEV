namespace PlaceUp.Migrations
{
    using PlaceUp.Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<PlaceUp.Context.DataContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(PlaceUp.Context.DataContext context)
        {
            Category cat1 = new Category { CategoryId = Guid.NewGuid(), Name = "Restaurant" };
            Category cat2 = new Category { CategoryId = Guid.NewGuid(), Name = "Cafe" };
            Category cat3 = new Category { CategoryId = Guid.NewGuid(), Name = "Pub" };

            //context.Categories.AddRange(new List<Category> { cat1, cat2, cat3 });

            Tag tag1 = new Tag { TagId = Guid.NewGuid(), Name = "good" };
            Tag tag2 = new Tag { TagId = Guid.NewGuid(), Name = "bad" };
            Tag tag3 = new Tag { TagId = Guid.NewGuid(), Name = "tasty" };
            Tag tag4 = new Tag { TagId = Guid.NewGuid(), Name = "very expensive" };
            Tag tag5 = new Tag { TagId = Guid.NewGuid(), Name = "cheap" };
            Tag tag6 = new Tag { TagId = Guid.NewGuid(), Name = "terrible" };
            Tag tag7 = new Tag { TagId = Guid.NewGuid(), Name = "like" };
            Tag tag8 = new Tag { TagId = Guid.NewGuid(), Name = "dislike" };
            Tag tag9 = new Tag { TagId = Guid.NewGuid(), Name = "awesome" };
            Tag tag10 = new Tag { TagId = Guid.NewGuid(), Name = "gorgeous" };

            //context.Tags.AddRange(new List<Tag> { tag1, tag2, tag3, tag4, tag5, tag6, tag7, tag8, tag9, tag10 });

            Feedback feed1 = new Feedback { FeedbackId = Guid.NewGuid(), Review = "The best place in the world", LikeDislike = true };
            Feedback feed2 = new Feedback { FeedbackId = Guid.NewGuid(), Review = "I recommend you to visit this place", LikeDislike = true };
            Feedback feed3 = new Feedback { FeedbackId = Guid.NewGuid(), Review = "There is the awful service ", LikeDislike = false };
            Feedback feed4 = new Feedback { FeedbackId = Guid.NewGuid(), Review = "I was satisfied", LikeDislike = true };
            Feedback feed5 = new Feedback { FeedbackId = Guid.NewGuid(), Review = "I don`n want to visit this place any more", LikeDislike = false };
            Feedback feed6 = new Feedback { FeedbackId = Guid.NewGuid(), Review = "If you want to have good impressions, you must visit this place", LikeDislike = true };
            Feedback feed7 = new Feedback { FeedbackId = Guid.NewGuid(), Review = "We have spent a good time here", LikeDislike = true };
            Feedback feed8 = new Feedback { FeedbackId = Guid.NewGuid(), Review = "I will never come here again", LikeDislike = false };
            Feedback feed9 = new Feedback { FeedbackId = Guid.NewGuid(), Review = "There are good prices for each other", LikeDislike = true };
            Feedback feed10 = new Feedback { FeedbackId = Guid.NewGuid(), Review = "We will come there the one more time", LikeDislike = true };

            //context.Feedbacks.AddRange(new List<Feedback> { feed1, feed2, feed3, feed4, feed5, feed6, feed7, feed8, feed9, feed10 });

            Place place1 = new Place
            {
                PlaceId = Guid.NewGuid(),
                Description = "The number of French cafes has steeply declined over the past few decades, as the country's celebrated cafe culture has battled against legal and societal challenges – most recently a smoking ban, but also a general reluctance to adapt to a younger clientele.",
                Name = "Cafe des 2 Moulins",
                Category = cat1,
                Adress = "Wall street",
                Email = "school1@gmail.com",
                OpeningDate = DateTime.Now,
                Phone = "0991234567",
                WebSite = "http://www.contoso.com"
            };

            place1.Feedbacks.Add(feed1);
            place1.Feedbacks.Add(feed2);
            place1.Feedbacks.Add(feed5);
            place1.Feedbacks.Add(feed7);

            place1.Tags.Add(tag10);
            place1.Tags.Add(tag1);
            place1.Tags.Add(tag2);
            place1.Tags.Add(tag3);

            Place place2 = new Place
            {
                PlaceId = Guid.NewGuid(),
                Name = "Verlet",
                Description = "Just a block from the Comédie Française theatre and the Louvre, this century-old coffee shop stocks more than 20 varieties of single-origin beans and house blends, freshly roasted and gloriously aromatic. It also operates as a quaint salon de café, where you can sample the coffee of your choice (about €3) with an optional slice of cake provided by an Austrian pastry shop across town. The cosy window seats in the upstairs room are particularly enviable.",
                Category = cat2,
                Adress = "Near Tower bridge",
                Email = "cafe1@gmail.com",
                OpeningDate = DateTime.Now,
                Phone = "0661111111",
                WebSite = "http://www.google.com"
            };

            place2.Feedbacks.Add(feed10);
            place2.Feedbacks.Add(feed9);
            place2.Feedbacks.Add(feed8);

            place2.Tags.Add(tag10);
            place2.Tags.Add(tag2);
            place2.Tags.Add(tag1);
            place2.Tags.Add(tag4);
            place2.Tags.Add(tag5);
            place2.Tags.Add(tag6);


            Place place3 = new Place
            {
                PlaceId = Guid.NewGuid(),
                Name = "The Palace Bar",
                Description = "This popular and always lively, pub exudes a quaint and traditional aura and presents its patrons with an almost museum-like atmosphere. A heated outdoor beer garden was opened in 2010 and immediately became a huge hit with both locals and tourists alike. Over a century ago Toners was a favourite spot for the legendary Irish poet and playwright WB Yeats, and its charm has been maintained ever since.",
                Category = cat3,
                Adress = "Baker street, 221B",
                Email = "pub1@gmail.com",
                OpeningDate = DateTime.Now,
                Phone = "0662225222",
                WebSite = "http://www.microsoft.com"
            };

            place3.Feedbacks.Add(feed5);
            place3.Feedbacks.Add(feed6);
            place3.Feedbacks.Add(feed7);

            place3.Tags.Add(tag1);
            place3.Tags.Add(tag6);

            Place place4 = new Place
            {
                PlaceId = Guid.NewGuid(),
                Name = "Fluxus Ministerija",
                Description = "We once played a show in Lithuania in a space called called Fluxus Ministerija. It was an old shoe factory and felt like a squat party, but with about 10,000 people, loads of weird art installations and AV projections. You would walk through one entire section of the building, about the size of Fabric in London, then arrive in a massive hall with totally different music and people. It went on until about 5pm the following day",
                Category = cat3,
                Adress = "Kreschatyk, 89",
                Email = "pub2@gmail.com",
                OpeningDate = DateTime.Now,
                Phone = "0667654321",
                WebSite = "http://www.youtube.com"
            };

            place4.Tags.Add(tag7);
            place4.Tags.Add(tag9);

            place4.Feedbacks.Add(feed3);
            place4.Feedbacks.Add(feed10);
            place4.Feedbacks.Add(feed6);
            place4.Feedbacks.Add(feed1);
            place4.Feedbacks.Add(feed2);

            context.Places.AddRange(new List<Place> { place1, place2, place3, place4 });

            context.SaveChanges();
        }
    }
}
