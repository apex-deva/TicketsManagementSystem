using FluentAssertions;
using TicketManagement.Domain.Tickets;
using Xunit;

namespace TicketManagement.UnitTests.Domain
{
    public class TicketTests
    {
        [Fact]
        public void CreateTicket_ShouldInitializeWithCorrectValues()
        {
            // Arrange
            var phoneNumber = "1234567890";
            var governorate = "Cairo";
            var city = "Nasr City";
            var district = "First District";

            // Act
            var ticket =  Ticket.Create(Guid.NewGuid(),phoneNumber, governorate, city, district);

            // Assert
            ticket.PhoneNumber.Should().Be(phoneNumber);
            ticket.Governorate.Should().Be(governorate);
            ticket.City.Should().Be(city);
            ticket.District.Should().Be(district);
            ticket.Status.Should().Be(TicketStatus.Open);
            ticket.CreationDateTime.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
        }

        [Fact]
        public void Handle_ShouldChangeStatusToHandled()
        {
            // Arrange
            var ticket = Ticket.Create(Guid.NewGuid(),"1234567890", "Cairo", "Nasr City", "First District");

            // Act
            ticket.Handle();

            // Assert
            ticket.Status.Should().Be(TicketStatus.Handled);
            ticket.HandledDateTime.Should().NotBeNull();
        }

        [Theory]
        [InlineData(10, TicketColorCode.Default)]
        [InlineData(20, TicketColorCode.Yellow)]
        [InlineData(35, TicketColorCode.Green)]
        [InlineData(50, TicketColorCode.Blue)]
        [InlineData(65, TicketColorCode.Red)]
        public void GetColorCode_ShouldReturnCorrectColorBasedOnTime(int minutesAgo, TicketColorCode expectedColor)
        {
            // Arrange
            var ticket = Ticket.Create(Guid.NewGuid(),"1234567890", "Cairo", "Nasr City", "First District");
            
            // Use reflection to set creation time for testing
            var creationTimeField = typeof(Ticket).GetField("<CreationDateTime>k__BackingField", 
                System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            creationTimeField?.SetValue(ticket, DateTime.UtcNow.AddMinutes(-minutesAgo));

            // Act
            var colorCode = ticket.GetColorCode();

            // Assert
            colorCode.Should().Be(expectedColor);
        }
    }
}
