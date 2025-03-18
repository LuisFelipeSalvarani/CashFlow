using CashFlow.Application.UseCases.Expenses.Register;
using CommonTestUtilities.Requests;

namespace Validators.Tests.Expenses.Register;

public class RegisterExpenseValidatorTests
{
    [Fact]
    public void Success()
    {
        // Arrange
        var validator = new RegisterExpenseValidator();
        var request = RequestRegisterExpenseJsonBuilder.Build();

        // Act
        var response = validator.Validate(request);

        // Assert
        Assert.True(response.IsValid);
    }
}
