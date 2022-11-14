using System.Collections.Generic;

public interface IStorage
{
    public List<Product> ProductsList { get; }
    public int Capacity { get; }
    public bool IsFull { get; }
    public bool IsEmpty { get; }

}
