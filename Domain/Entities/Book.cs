using Domain.Primitives;

namespace Domain.Entities;

public sealed class Book : Entity
{
    public string Title { get; private set; } = string.Empty;
    public string Author { get; private set; } = string.Empty;
    public int PublishedYear { get; private set; } = 0;
    public string Publisher { get; private set; } = string.Empty;
    public string ISBN { get; private set; } = string.Empty;
    public double Price { get; private set; } = 0;
    public int UnitInStock { get; private set; } = 0;
    public bool IsRemoved { get; private set; } = false;
    public DateTime RemovedAt { get; private set; } = DateTime.MinValue;

    /*One -------------------------------------------------*/
    /*Many -------------------------------------------------*/
    public ICollection<Order>? Orders { get; private set; }

    public Book() { }

    public void Update(
        string title,
        string author)
    {
        Title = title;
        Author = author;
    }

    public void Remove()
    {
        IsRemoved = true;
        RemovedAt = DateTime.UtcNow;
    }

    public class Builder
    {
        private Book _book = new Book();

        public Builder SetID(Guid id) { _book.Id = id; return this; }
        public Builder SetTitle(string title) { _book.Title = title; return this; }
        public Builder SetAuthor(string author) { _book.Author = author; return this; }
        public Builder SetPublishedYear(int year) { _book.PublishedYear = year; return this; }
        public Builder SetPublisher(string publisher) { _book.Publisher = publisher; return this; }
        public Builder SetISBN(string isbn) { _book.ISBN = isbn; return this; }
        public Builder SetPrice(double price) { _book.Price = price; return this; }
        public Builder SetUnitInStock(int unitInStock) { _book.UnitInStock = unitInStock; return this; }
        public Builder SetIsRemoved(bool isRemoved) { _book.IsRemoved = isRemoved; return this; }
        public Builder SetRemovedAt(DateTime removeAt) { _book.RemovedAt = removeAt; return this; }

        public Book Build() => _book;
    }
}
