using System;
using CryptoExchange.Domain;
using CryptoExchange.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CryptoExchange.Tests.Common
{
    public class CryptoExchangeContextFactory
    {
        public static CryptoExchangeDbContext Create()
        {
            var options = new DbContextOptionsBuilder<CryptoExchangeDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            var context = new CryptoExchangeDbContext(options);
            context.Database.EnsureCreated();

            context.Users.AddRange(
                new AppUser
                {
                    Id = "86a9d249-fdd4-4b5e-8ccc-b86d1abb764f",
                    Email = "sometest1@gmail.com",
                    UserName = "Bob",
                    Password = "1234Qwe.",
                    ExchangerId = Guid.Parse("629fd77f-7569-4daf-b800-98b1c5ffb5c9")
                },
                new AppUser
                {
                    Id = "e8e8593b-c443-4686-8d8a-76fcc5c8be56",
                    Email = "sometest@gmail.com",
                    UserName="Thomas",
                    Password="Thomas123.",
                    ExchangerId = Guid.Parse("af744fd4-0579-4704-b941-e07b03f9e441")
                },
                new AppUser
                {
                    Id = "12349f16-26b2-479d-a5ba-f2e62a91bb00",
                    Email = "sometest3@gmail.com",
                    UserName = "Billy",
                    Password = "Billy123.",
                    ExchangerId = Guid.Parse("d0bd707a-3a92-4ac7-b7ce-682d0f1fb314")
                }
            );

            context.Exchangers.AddRange(
                new Exchanger
                {
                    Id = Guid.Parse("629fd77f-7569-4daf-b800-98b1c5ffb5c9"),
                    Name = "STExchange",
                    WebResuorceUrl = "https://stexchange.com",
                    UserId = "86a9d249-fdd4-4b5e-8ccc-b86d1abb764f"
                },
                new Exchanger
                {
                    Id = Guid.Parse("af744fd4-0579-4704-b941-e07b03f9e441"),
                    Name = "OnExchange",
                    WebResuorceUrl = "https://onexchange.com",
                    UserId = "e8e8593b-c443-4686-8d8a-76fcc5c8be56"
                },
                new Exchanger
                {
                    Id = Guid.Parse("d0bd707a-3a92-4ac7-b7ce-682d0f1fb314"),
                    Name = "UaExchange",
                    WebResuorceUrl = "https://uaexchange.ua",
                    UserId = "12349f16-26b2-479d-a5ba-f2e62a91bb00"
                }
            );

            context.Orders.AddRange(
                new Order
                {
                    Id = Guid.Parse("c76e7da0-ff36-45c5-a572-154f07573801"),
                    ExchangerId = Guid.Parse("629fd77f-7569-4daf-b800-98b1c5ffb5c9"),
                    ExchangerName = "STExchange",
                    ExchangeFrom = "BTC",
                    ExchangeTo = "ETH",
                    IncomeSum = 1,
                    OutcomeSum = 12.4319,
                    Amount = 390.7492,
                    MinAmount = 0.15,
                    MaxAmount = 3
                },
                new Order
                {
                    Id = Guid.Parse("29a84517-95bd-4789-9ada-0fcdc590cf2e"),
                    ExchangerId = Guid.Parse("629fd77f-7569-4daf-b800-98b1c5ffb5c9"),
                    ExchangerName = "STExchange",
                    ExchangeFrom = "BTC",
                    ExchangeTo = "XRP",
                    IncomeSum = 1,
                    OutcomeSum = 24.4319,
                    Amount = 210.7492,
                    MinAmount = 0.29,
                    MaxAmount = 9
                },
                new Order
                {
                    Id = Guid.Parse("d0f26c73-18ee-4edc-a5db-01ebc790a46b"),
                    ExchangerId = Guid.Parse("629fd77f-7569-4daf-b800-98b1c5ffb5c9"),
                    ExchangerName = "STExchange",
                    ExchangeFrom = "DAI",
                    ExchangeTo = "SHIBA",
                    IncomeSum = 1,
                    OutcomeSum = 76.4319,
                    Amount = 110.7492,
                    MinAmount = 0.19,
                    MaxAmount = 5
                },
                new Order
                {
                    Id = Guid.Parse("844b4bf2-42aa-44d6-a94c-4fc3a3a1662a"),
                    ExchangerId = Guid.Parse("af744fd4-0579-4704-b941-e07b03f9e441"),
                    ExchangerName = "OnExchange",
                    ExchangeFrom = "BTC",
                    ExchangeTo = "XRP",
                    IncomeSum = 1,
                    OutcomeSum = 32.4319,
                    Amount = 590.7492,
                    MinAmount = 0.25,
                    MaxAmount = 2
                },
                new Order
                {
                    Id = Guid.Parse("6431458b-6429-42bf-b787-bedd179e3cab"),
                    ExchangerId = Guid.Parse("af744fd4-0579-4704-b941-e07b03f9e441"),
                    ExchangerName = "OnExchange",
                    ExchangeFrom = "SOL",
                    ExchangeTo = "BNB",
                    IncomeSum = 1,
                    OutcomeSum = 34.4319,
                    Amount = 245.7492,
                    MinAmount = 0.10,
                    MaxAmount = 8
                },
                new Order
                {
                    Id = Guid.Parse("d2b5cc3a-5d12-42ba-b164-7ca1cfecab8f"),
                    ExchangerId = Guid.Parse("af744fd4-0579-4704-b941-e07b03f9e441"),
                    ExchangerName = "OnExchange",
                    ExchangeFrom = "DOT",
                    ExchangeTo = "SHIBA",
                    IncomeSum = 1,
                    OutcomeSum = 23.4319,
                    Amount = 233.7492,
                    MinAmount = 0.23,
                    MaxAmount = 7
                },
                new Order
                {
                    Id = Guid.Parse("3ad3ac21-426a-46e2-b4c1-f1cd8c8f573d"),
                    ExchangerId = Guid.Parse("d0bd707a-3a92-4ac7-b7ce-682d0f1fb314"),
                    ExchangerName = "UaExchange",
                    ExchangeFrom = "TRX",
                    ExchangeTo = "XRP",
                    IncomeSum = 1,
                    OutcomeSum = 32.4319,
                    Amount = 590.7492,
                    MinAmount = 0.25,
                    MaxAmount = 2
                },
                new Order
                {
                    Id = Guid.Parse("386a06dc-68db-477c-b0f7-d2ff8dc2a30d"),
                    ExchangerId = Guid.Parse("d0bd707a-3a92-4ac7-b7ce-682d0f1fb314"),
                    ExchangerName = "UaExchange",
                    ExchangeFrom = "ATOM",
                    ExchangeTo = "BNB",
                    IncomeSum = 1,
                    OutcomeSum = 34.4319,
                    Amount = 245.7492,
                    MinAmount = 0.10,
                    MaxAmount = 8
                },
                new Order
                {
                    Id = Guid.Parse("c05ec9b3-ac9b-4273-aa89-15ca2e88db39"),
                    ExchangerId = Guid.Parse("d0bd707a-3a92-4ac7-b7ce-682d0f1fb314"),
                    ExchangerName = "UaExchange",
                    ExchangeFrom = "LINK",
                    ExchangeTo = "NEAR",
                    IncomeSum = 1,
                    OutcomeSum = 23.4319,
                    Amount = 233.7492,
                    MinAmount = 0.23,
                    MaxAmount = 7
                }

            );

            context.SaveChanges();
            return context;
        }

        public static void Destroy(CryptoExchangeDbContext context)
        {
            context.Database.EnsureDeleted();
            context.Dispose();
        }
    }
}

