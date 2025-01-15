using AutoFixture;
using AutoFixture.AutoMoq;
using FluentAssertions;
using Moq;
using PosAPI.BLL.Services.Cards;
using PosAPI.DAL;
using PosAPI.DAL.Models.Cards;
using PosAPI.DAL.UnitOfWorks;
using Xunit;

namespace PosAPI.Test.Cards
{
    public class CardServiceTest
    {
        #region Variables
        private readonly CardService<PosDbContext> _cardService;
        private readonly Fixture _fixture;
        #endregion

        #region Constructor
        public CardServiceTest()
        {
            _fixture = new Fixture(); 
            _fixture.Customize(new AutoMoqCustomization());

            _cardService = _fixture.Create<CardService<PosDbContext>>();
        }
        #endregion

        #region Methods
        [Theory]
        [InlineData("Security Bank", 
                    "Debit", 
                    "Test", 
                    "1234567890", 
                    "2030-01-01", 
                    123,
                    "Unit test",
                    "1970-01-01",
                    true,
                    "Card added successfully")]
        [InlineData("BDO",
                    "Debit",
                    "Test",
                    "12345678901112131415",
                    "2030-01-01",
                    123,
                    "Unit test",
                    "1970-01-01",
                    false,
                    "Invalid card number")]
        public async Task AddCard_ShouldReturnExpectedResult(string cardBankName, 
            string cardType,
            string cardAccountName,
            string cardAccountNumber,
            string cardExpiry,
            int cardCvcNumber,
            string createdBy,
            string dateCreated,
            bool expectedOutput,
            string expectedMessage)
        {
            // Arrange
            _fixture.Freeze<Mock<IUnitOfWork<PosDbContext>>>();

            var card = new CardModel()
            {
                CardBankName = cardBankName,
                CardType = cardType,
                CardAccountName = cardAccountName,
                CardAccountNumber = cardAccountNumber,
                CardExpiry = DateTime.Parse(cardExpiry),
                CardCvcNumber = cardCvcNumber,
                CreatedBy = createdBy,
                DateCreated = DateTime.Parse(dateCreated)
            };

            var expectedResult = new Dictionary<bool, string>
            {
                { expectedOutput, expectedMessage }
            };

            // Act
            var result = await _cardService.AddCard(card);

            // Assert
            result.Should().BeEquivalentTo(expectedResult);
        }

        [Fact]
        public async Task GetCards_ShouldReturnListOfCardModelOrNull()
        {
            // Arrange
            _fixture.Freeze<Mock<IUnitOfWork<PosDbContext>>>();

            // Act
            var result = await _cardService.GetCards();

            // Assert
            if (result is null)
                result.Should().BeNull();
            else
                result.Should().BeOfType<List<CardModel>>();
        }
        #endregion
    }
}
