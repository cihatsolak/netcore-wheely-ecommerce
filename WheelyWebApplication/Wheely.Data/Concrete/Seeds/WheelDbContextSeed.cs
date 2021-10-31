using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Wheely.Core.Entities.Concrete.Categories;
using Wheely.Core.Entities.Concrete.Colors;
using Wheely.Core.Entities.Concrete.Comments;
using Wheely.Core.Entities.Concrete.Dimensions;
using Wheely.Core.Entities.Concrete.Pictures;
using Wheely.Core.Entities.Concrete.Producers;
using Wheely.Core.Entities.Concrete.Tags;
using Wheely.Core.Entities.Concrete.Wheels;
using Wheely.Data.Concrete.Contexts;

namespace Wheely.Data.Concrete.Seeds
{
    public class WheelDbContextSeed
    {
        public static async Task SeedAsync(WheelDbContext wheelDbContext, ILoggerFactory loggerFactory, int? retry = 0)
        {
            int retryForAvailability = retry.Value;

            try
            {
                wheelDbContext.Database.Migrate();

                if (!wheelDbContext.Wheels.Any())
                {
                    var wheel = GetPreConfiguredWheel();
                    var categories = GetPreConfiguredCategories();
                    var colors = GetPreConfiguredColors();
                    var dimensions = GetPreConfiguredDimensions();
                    var tags = GetPreConfiguredTags();
                    var comments = GetPreConfiguredComments();
                    var pictures = GetPreConfiguredPictures();

                    wheelDbContext.Wheels.Add(wheel);
                    wheelDbContext.Categories.AddRange(categories);
                    wheelDbContext.Colors.AddRange(colors);
                    wheelDbContext.Dimensions.AddRange(dimensions);
                    wheelDbContext.Tags.AddRange(tags);
                    wheelDbContext.Comments.AddRange(comments);
                    wheelDbContext.Pictures.AddRange(pictures);

                }

                await wheelDbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                if (retryForAvailability < 10)
                {
                    retryForAvailability++;
                    var contextLogger = loggerFactory.CreateLogger<WheelDbContextSeed>();
                    contextLogger.LogError(ex.Message);
                    Thread.Sleep(2000);
                    await SeedAsync(wheelDbContext, loggerFactory, retryForAvailability);
                }
            }
        }

        private static Wheel GetPreConfiguredWheel()
        {
            return new Wheel
            {
                StarCount = 4,
                Name = "LX2-Lato synthesize users",
                ShortDescription = "Credibly mesh technically sound results whereas cost effective leadership skills.",
                StockCode = "529",
                Description = "Assertively conceptualize parallel process improvements through user friendly action items. Interactively cultivate interdependent customer service without clicks-and-mortar e-services. Proactively cultivate go forward testing procedures with accurate quality vectors. Globally embrace ethical functionalities via empowered scenarios.",
                Price = 989,
                CampaignPrice = 799,
                Producer = new Producer
                {
                    Name = "Hueiler Inc.LTD"
                }
            };
        }

        private static List<Category> GetPreConfiguredCategories()
        {
            return new List<Category>
            {
                new Category()
                {
                    Name = "Cleanup"
                },
                new Category()
                {
                    Name = "Repire"
                }
            };
        }

        private static List<Color> GetPreConfiguredColors()
        {
            return new List<Color>
            {
                new Color()
                {
                     Name = "Purple",
                     HexCode ="A541F2"
                },
                new Color()
                {
                    Name = "Orange",
                    HexCode ="E70D3C"
                }
            };
        }

        private static List<Dimension> GetPreConfiguredDimensions()
        {
            return new List<Dimension>
            {
                new Dimension()
                {
                    Size = 16
                },
                new Dimension()
                {
                   Size = 18
                },
                new Dimension()
                {
                   Size = 20
                }
            };
        }

        private static List<Tag> GetPreConfiguredTags()
        {
            return new List<Tag>
            {
                new Tag()
                {
                    Name = "trendy"
                }
            };
        }

        private static List<Comment> GetPreConfiguredComments()
        {
            return new List<Comment>
            {
                new Comment
                {
                    StarCount = 5,
                    Content ="Progressively procrastinate mission-critical action items before team building ROI. Interactively provide access to cross functional quality vectors for client-centric catalysts for change.",
                    FullName = "Mark Jack",
                    ImageUrl = "comment-author-1.jpg"
                }
            };
        }

        private static List<Picture> GetPreConfiguredPictures()
        {
            return new List<Picture>
            {
                new Picture
                {
                   Order = 1,
                   Url = "shop-d-1-1.jpg"
                }
            };
        }
    }
}
