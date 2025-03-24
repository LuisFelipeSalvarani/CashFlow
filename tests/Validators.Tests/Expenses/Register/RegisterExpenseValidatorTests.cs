using CashFlow.Application.UseCases.Expenses;
using CashFlow.Communication.Enums;
using CashFlow.Exception;
using CommonTestUtilities.Requests;
using Shouldly;

namespace Validators.Tests.Expenses.Register;

public class RegisterExpenseValidatorTests
{
    [Fact]
    public void Success()
    {
        // Arrange
        var validator = new ExpenseValidator();
        var request = RequestRegisterExpenseJsonBuilder.Build();

        // Act
        var response = validator.Validate(request);

        // Assert
        response.IsValid.ShouldBeTrue();
    }

    [Theory]
    [InlineData("")]
    [InlineData("        ")]
    [InlineData(null)]
    public void Error_Title_Empty(string title)
    {
        var validator = new ExpenseValidator();
        var request = RequestRegisterExpenseJsonBuilder.Build();
        request.Title = title;

        var response = validator.Validate(request);

        response.IsValid.ShouldBeFalse();
        response.Errors.ShouldSatisfyAllConditions(() => response.Errors.ShouldHaveSingleItem(), () => response.Errors.ShouldContain(e => e.ErrorMessage.Equals(ResourceErrorMessages.TITLE_REQUIRED)));
    }

    [Fact]
    public void Error_Date_Future()
    {
        var validator = new ExpenseValidator();
        var request = RequestRegisterExpenseJsonBuilder.Build();
        request.Date = DateTime.UtcNow.AddDays(1);

        var response = validator.Validate(request);

        response.IsValid.ShouldBeFalse();
        response.Errors.ShouldSatisfyAllConditions(() => response.Errors.ShouldHaveSingleItem(), () => response.Errors.ShouldContain(e => e.ErrorMessage.Equals(ResourceErrorMessages.EXPENSE_CANNOT_FOR_THE_FUTURE)));
    }

    [Fact]
    public void Error_Payment_Type_Invalid()
    {
        var validator = new ExpenseValidator();
        var request = RequestRegisterExpenseJsonBuilder.Build();
        request.PaymentType = (PaymentType)700;

        var response = validator.Validate(request);

        response.IsValid.ShouldBeFalse();
        response.Errors.ShouldSatisfyAllConditions(() => response.Errors.ShouldHaveSingleItem(), () => response.Errors.ShouldContain(e => e.ErrorMessage.Equals(ResourceErrorMessages.PAYMENT_TYPE_INVALID)));
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    [InlineData(-2)]
    [InlineData(-7)]
    public void Error_Amount_Invalid(decimal amount)
    {
        var validator = new ExpenseValidator();
        var request = RequestRegisterExpenseJsonBuilder.Build();
        request.Amount = amount;

        var response = validator.Validate(request);

        response.IsValid.ShouldBeFalse();
        response.Errors.ShouldSatisfyAllConditions(() => response.Errors.ShouldHaveSingleItem(), () => response.Errors.ShouldContain(e => e.ErrorMessage.Equals(ResourceErrorMessages.AMOUNT_MUST_BE_GREATER_THEN_ZERO)));
    }
}
