namespace PromisedEigong.ModSystem;
#nullable disable
using System.Collections.Generic;

public class DualStatePool<T>
{
    public HashSet<T> AllItemsSet { get; } = new();
    public HashSet<T> ActiveItemsSet { get; } = new();
    public Stack<T> InactiveItemsPool { get; } = new();
        
    public T Fetch ()
    {
        if (InactiveItemsPool.Count == 0) 
            return default;
            
        T item = InactiveItemsPool.Pop();
        ActiveItemsSet.Add(item);
        return item;
    }

    public void InsertAsActive (T item)
    {
        AllItemsSet.Add(item);
        ActiveItemsSet.Add(item);
    }
        
    public void InsertAsInactive (T item)
    {
        AllItemsSet.Add(item);
        ActiveItemsSet.Remove(item);
        InactiveItemsPool.Push(item);
    }
}