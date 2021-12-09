using Bookinist.DAL.Context;
using Bookinist.DAL.Entities;

using MathCore.Logging;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using System;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;

namespace Bookinist.Data
{
    class DBInitializer
    {

        private readonly BookinistDb __db;
        private readonly ILogger<DBInitializer> __loger;

        public DBInitializer(BookinistDb db, ILogger<DBInitializer> loger)
        {
            this.__db = db ?? throw new ArgumentNullException(nameof(db));
            this.__loger = loger ?? throw new ArgumentNullException(nameof(loger));
        }

        public async Task InitializeAsync()
        {
            var timer = new Stopwatch();
            timer.Start();
            __loger.LogInformation("Инициализация БД...");
            //ToDo
            __loger.LogInformation("Удаление предыдушей БД...");
            await __db.Database.EnsureDeletedAsync().ConfigureAwait(false); //для отладки потом убрать 
                                                     //__db.Database.EnsureCreated(); //проверяет есть ли база и создает
            var tempTime = timer.ElapsedMilliseconds;
            __loger.LogInformation("Удаление существующей БД выполнено за {0} мc.", tempTime);
            await __db.Database.MigrateAsync(); //проверяет, создает если нет, накатывает миграции
            __loger.LogInformation("Миграция выполнена за {0} мc.", timer.ElapsedMilliseconds- tempTime);
            tempTime = timer.ElapsedMilliseconds; 

            if (await __db.Books.AnyAsync()) return;
            await InitializeCategory();
            await InitialBooks();
            await InitialBuyers();
            await InitialSellers();
            await InitialDeals();
            __loger.LogInformation("Инициализация выполнена за {0} мc.", timer.ElapsedMilliseconds - tempTime);
            timer.Stop();
        }


        private const int __catrgoriesCount = 10;
        private Category[] _categories;

        private async Task InitializeCategory()
        {
            var timer = Stopwatch.StartNew();
            __loger.LogInformation("Инициализация категорий...");

            _categories = new Category[__catrgoriesCount];
            for (int i = 0; i < __catrgoriesCount; i++)
            {
                _categories[i] = new Category()
                {
                    Name = $"Категория {i + 1}"
                };
            }
            await __db.AddRangeAsync(_categories);
            await __db.SaveChangesAsync();
            __loger.LogInformation("Инициализация категорий выполнена за {0} мc.", timer.ElapsedMilliseconds);
        }
        private const int __booksCount = 10;
        private Book[] _books;
        private async Task InitialBooks()
        {
            var timer = Stopwatch.StartNew();
            __loger.LogInformation("Инициализация книг...");

            var rnd = new Random();
            _books = new Book[__booksCount];
            _books = Enumerable.Range(1, __booksCount)
                .Select(b => new Book { Name = $" Книга {b + 1}", Category = rnd.NextItem(_categories) })
                .ToArray();
            await __db.AddRangeAsync(_books);
            await __db.SaveChangesAsync();
            
            __loger.LogInformation("Инициализация книг выполнена за {0} мc.", timer.ElapsedMilliseconds);
        }

        private const int __sellerscount = 10;
        private Seller[] _sellers;
        private async Task InitialSellers()
        {
            var timer = Stopwatch.StartNew();
            __loger.LogInformation("Инициализация Продавцов...");

            _sellers = new Seller[__sellerscount];
            _sellers = Enumerable.Range(1, __sellerscount)
                .Select(s => new Seller()
                {
                    Name = $" Продавец- Имя {s + 1}",
                    Surname = $"Продавец-фамилия {s + 1}",
                    Patronymic = $" Продавец-отчество {s + 1}"
                })
                .ToArray();
            await __db.AddRangeAsync(_sellers);
            await __db.SaveChangesAsync();
            
            __loger.LogInformation("Инициализация продавцов выполнена за {0} мc.", timer.ElapsedMilliseconds);
        }

        private const int __buyersCount = 10;
        private Buyer[] _buyers;
        private async Task InitialBuyers()
        {
            var timer = Stopwatch.StartNew();
            __loger.LogInformation("Инициализация покупателей...");
            _buyers = new Buyer[__buyersCount];
            _buyers = Enumerable.Range(1, __buyersCount)
                .Select(s => new Buyer()
                {
                    Name = $" Покупатель- Имя {s + 1}",
                    Surname = $"Покупатель-фамилия {s + 1}",
                    Patronymic = $" Покупатель-отчество {s + 1}"
                })
                .ToArray();
            await __db.AddRangeAsync(_buyers);
            await __db.SaveChangesAsync();
            __loger.LogInformation("Инициализация покупателей выполнена за {0} мc.", timer.ElapsedMilliseconds);
        }

        private const int __dealsCount = 1000;

        private async Task InitialDeals()
        {
            var timer = Stopwatch.StartNew();
            __loger.LogInformation("Инициализация сделок...");
            var rnd = new Random();
            var _deals = Enumerable.Range(1, __dealsCount)
                .Select(s => new Deal()
                {
                    Book = rnd.NextItem(_books),
                    Buyer = rnd.NextItem(_buyers),
                    Seller = rnd.NextItem(_sellers),
                    Price =(decimal) (rnd.NextDouble() * 4000 + 700)
                });
            await __db.AddRangeAsync(_deals);
            await __db.SaveChangesAsync();
            __loger.LogInformation("Инициализация Сделок выполнена за {0} мc.", timer.Elapsed.TotalSeconds);
            timer.Stop();
        }
    }
}
