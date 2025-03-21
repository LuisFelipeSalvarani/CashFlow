namespace CashFlow.Exception.ExeceptionsBase;

public abstract class CashFlowException : SystemException
{
    protected CashFlowException(string message) : base(message)
    {

    }
}
