using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrCurrencyClient.Interfaces;
using TrDeals.Data.Infrastructure.Interfaces;
using TrDeals.Data.Models;
using TrDeals.Data.Repositories.Interfaces;
using TrDeals.Service.Services.Logic;
using TrTransactionClient.Interfaces;

namespace DealsTests
{
    [TestFixture(Description = "Тесты сервиса сделок")]
    public class DealServiceTests
    {
        #region Константы

        /// <summary>
        /// Ид пользователя
        /// </summary>
        private const string USER_ID = "ee01b062-7613-4eb5-a2aa-c614a977a51f";

        /// <summary>
        /// Ид валюты BTC
        /// </summary>
        private const string BTC = "BTC";

        /// <summary>
        /// Ид валюты ETH
        /// </summary>
        private const string ETH = "ETH";

        #endregion

        #region Поля, свойства

        /// <summary>
        /// Сервис сделок
        /// </summary>
        private readonly DealService _dealService;

        #endregion

        #region Конструктор теста

        /// <summary>
        /// Тесты сервиса сделок
        /// </summary>
        public DealServiceTests()
        {
            var currencyClient = new Mock<ICurrencyClient>();
            var transactionClient = new Mock<ITransactionClient>();
            var offerRepository = new Mock<IOfferRepository>();
            var unitOfWork = new Mock<IUnitOfWork>();

            unitOfWork.Setup(s => s.SaveChangesAsync())
                .Returns(() => SaveChangesAsync());
            currencyClient.Setup(s => s.CheckCurrencyPair(It.IsAny<string>(), It.IsAny<string>()))
                .Returns((string currencyFromId, string currencyToId) => CheckCurrencyPair(currencyFromId, currencyToId));
            transactionClient.Setup(s => s.ReserveAsync(It.IsAny<Guid>(), It.IsAny<string>(), It.IsAny<decimal>()))
                .Returns((Guid userId, string currencyId, decimal volume) => ReserveAsync(userId, currencyId, volume));
            offerRepository.Setup(s => s.GetList(It.IsAny<Guid>()))
                .Returns((Guid userId) => GetOffers(userId));
            offerRepository.Setup(s => s.RemoveRange(It.IsAny<IEnumerable<Offer>>()))
                .Callback((IEnumerable<Offer> offers) => RemoveRange(offers));
            offerRepository.Setup(s => s.Add(It.IsAny<Offer>()))
                .Callback((Offer offer) => Add(offer));
            offerRepository.Setup(s => s.GetList(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<decimal>()))
                .Returns((string currencyPairFromId, string currencyPairToId, decimal price) => GetList(currencyPairFromId, currencyPairToId, price));
            

            _dealService = new DealService(currencyClient.Object, transactionClient.Object, offerRepository.Object, unitOfWork.Object);
        }

        #endregion

        #region Тесты

        [Test(Description = "Успешное добавление предложения")]
        public void AddOfferAsync_Success()
        {
            decimal volume = 2;
            decimal price = (decimal)20;

            Assert.IsTrue(_dealService.AddOfferAsync(Guid.Parse(USER_ID), BTC, ETH, volume, price)
                .GetAwaiter().GetResult());
        }

        #endregion

        #region Moq

        /// <summary>
        /// Фэйковые предложения
        /// </summary>
        private List<Offer> _offers = new List<Offer>
        {
            new Offer
            {
                CreatedAt = DateTime.UtcNow,
                CurrencyFromId = BTC,
                CurrencyToId = ETH,
                OfferId = Guid.NewGuid(),
                Price = (decimal)20,
                UserId = Guid.Parse(USER_ID),
                Volume = 3
            },
            new Offer
            {
                CreatedAt = DateTime.UtcNow,
                CurrencyFromId = ETH,
                CurrencyToId = BTC,
                OfferId = Guid.NewGuid(),
                Price = (decimal)0.04,
                UserId = Guid.Parse(USER_ID),
                Volume = 30
            }
        };

        /// <summary>
        /// Фэйковое обновление данных
        /// </summary>
        /// <returns></returns>
        private async Task SaveChangesAsync()
        {
            await Task.Run(() => { });
        }

        /// <summary>
        /// Фэйковая проверка наличия валютной пары
        /// </summary>
        /// <returns></returns>
        private async Task<bool> CheckCurrencyPair(string currencyFromId, string currencyToId)
        {
            await Task.Run(() => { });

            return true;
        }

        /// <summary>
        /// Фэйковое добавление резерва
        /// </summary>
        /// <returns></returns>
        private async Task<bool> ReserveAsync(Guid userId, string currencyId, decimal volume)
        {
            await Task.Run(() => { });

            return true;
        }

        /// <summary>
        /// Фэйковое получение всех предложений пользователя
        /// </summary>
        /// <returns></returns>
        private async Task<List<Offer>> GetOffers(Guid userId)
        {
            await Task.Run(() => { });

            return _offers.Where(o => o.UserId == userId).ToList();
        }

        /// <summary>
        /// Фэйковое удаление
        /// </summary>
        private void RemoveRange(IEnumerable<Offer> offers)
        {
            foreach (var item in offers)
            {
                _offers.Remove(item);
            }
        }

        /// <summary>
        /// Фэйковое добавление
        /// </summary>
        /// <param name="offer"></param>
        private void Add(Offer offer)
        {
            _offers.Add(offer);
        }

        /// <summary>
        /// Фэйковое получение предложений
        /// </summary>
        /// <returns></returns>
        public async Task<List<Offer>> GetList(string currencyPairFromId, string currencyPairToId, decimal price)
        {
            await Task.Run(() => { });

            return _offers.Where(o => o.CurrencyFromId == currencyPairFromId && o.CurrencyToId == currencyPairToId
                && o.Price <= price).ToList();
        }

        #endregion
    }
}
