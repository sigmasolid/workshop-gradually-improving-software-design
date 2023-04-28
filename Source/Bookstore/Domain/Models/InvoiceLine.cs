using Bookstore.Domain.Common;

namespace Bookstore.Domain.Models;

public abstract class InvoiceLine
{
    public Guid Id { get; private set; } = Guid.Empty;
    public string Label { get; private set; } = string.Empty;
    public int Quantity { get; private set; } = 1;
    public Money Price
    { 
        get => this.Currency.Amount(this.Amount);
        private set => (this.Amount, this.Currency) = (value.Amount, value.Currency);
    }

    private decimal Amount { get; set; } = 0;       // Used by EF Core
    private Currency Currency { get; set; }         // Used by EF Core

    protected InvoiceLine() { }      // Used by EF Core

    protected InvoiceLine(Guid id, string label, int quantity, Money price) =>
        (Label, Id, Quantity, Price) = (label, id, quantity, price);

    public void Increment(int quantity, Money price)
    {
        Quantity += quantity;
        Price += price;
    }
}