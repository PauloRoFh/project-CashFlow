using CashFlow.Communication.Enums;
using CashFlow.Communication.Requests;
using CashFlow.Communication.Responses;

namespace CashFlow.Application.UseCases.Expenses.Register;
public class RegisterExpenseUseCase
{
    public ResponseRegisteredExpenseJson Execute(RequestRegisterExpenseJson request)
    {
        Validade(request);

        return new ResponseRegisteredExpenseJson();
    }

    private void Validade(RequestRegisterExpenseJson request)
    {
        var titleIsEmpty = string.IsNullOrWhiteSpace(request.Title);
        if (titleIsEmpty)
        {
            throw new ArgumentException("The title is required");
        }

        if (request.Amount <= 0)
        {
            throw new ArgumentException("The amount must be greater than zero");
        }

        var resultDateTime = DateTime.Compare(request.Date, DateTime.UtcNow);
        if (resultDateTime > 0)
        {
            throw new ArgumentException("The date must be less than or equal to the current date");
        }

        var paymentTypeIsValid = Enum.IsDefined(typeof(PaymentType), request.PaymentType);
        if (paymentTypeIsValid == false)
        {
            throw new ArgumentException("The payment type is invalid");
        }
    }
}
