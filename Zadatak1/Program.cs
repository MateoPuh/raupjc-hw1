using System;

public interface IIntegerList
{
    /// <summary>
    /// Adds an item to the collection.
    /// </summary>
    void Add(int item);

    /// <summary>
    /// Removes the first occurrence of an item from the collection.
    /// If the item was not found, method does nothing and returnes false.
    /// </summary>
    bool Remove(int item);

    /// <summary>
    /// Removes the item at the given index in the collection.
    /// Throws IndexOutOfRange exception if index out of range.
    /// </summary>
    bool RemoveAt(int index);

    /// <summary>
    /// Returns the item at the given index in the collection.
    /// Throws IndexOutOfRange exception if index out of range.
    /// </summary>
    int GetElement(int index);

    /// <summary>
    /// Returns the index of the item in the collection.
    /// If item is not found in the collection, method returns -1.
    /// </summary>
    int IndexOf(int item);

    /// <summary>
    /// Read-only property. Gets the number of items contained in the collection.
    /// </summary>
    int Count { get; }

    /// <summary>
    /// Removes all items from the collection.
    /// </summary>
    void Clear();

    /// <summary>
    /// Determines whether the collection contains a specific value.
    /// </summary>
    bool Contains(int item);

}

public class IntegerList : IIntegerList
{
    private int[] _internalStorage;
    private int count = 0;
    public int Count {
        get
        {
            return count;
        }
    }

    public IntegerList(int initialSize)
    {
        if (initialSize <= 0)
        {
            throw new ArgumentException("Size has to be greater than zero");
        }
        _internalStorage = new int[initialSize];
    }

    public IntegerList()
    {
        _internalStorage = new int[4];
    }

    public void Add(int item)
    {
        int capacity = _internalStorage.Length;

        if (count >= capacity)
        {
            int[] newIntegerList = new int[2 * capacity];

            for(int i = 0; i<capacity; i++)
            {
                newIntegerList[i] = _internalStorage[i];
             }
            newIntegerList[count++] = item;
            _internalStorage = newIntegerList;
            return;
        }

        _internalStorage[count++] = item;
    }

    public bool Remove(int item)
    {
        int index;

        for (int i=0; i<count; i++)
        {
            if(_internalStorage[i] == item)
            {
                index = i;
                return RemoveAt(index);
            }
        }

        return false;
    }

    public bool RemoveAt(int index)
    {
        if(index<0 || index>= _internalStorage.Length)
        {
            throw new IndexOutOfRangeException("Index is out of range.");
        }

        if(index >= count)
        {
            return false;
        }

        for (int i = index; i < count; i++)
        {
            _internalStorage[i] = _internalStorage[i + 1];
        }

        count--;
        return true;
    }

    public int GetElement(int index)
    {
        if(index>=0 && index <count)
        {
            return _internalStorage[index];
        }
        else
        {
            throw new IndexOutOfRangeException("Index out of range.");
        }
    }

    public int IndexOf(int item)
    {
        for(int i=0; i<count; i++)
        {
            if(_internalStorage[i] == item)
            {
                return i;
            }
        }

        return -1;
    }

    public void Clear()
    {
        count = 0;
    }

    public bool Contains(int item)
    {
        for(int i=0; i<count; i++)
        {
            if(_internalStorage[i] == item)
            {
                return true;
            }
        }

        return false;
    }
}

namespace Zadatak1
{
    class Program
    {
        static void Main(string[] args)
        {
            IntegerList listOfIntegers = new IntegerList();
            listOfIntegers.Add(1); // [1]
            listOfIntegers.Add(2); // [1 ,2]
            listOfIntegers.Add(3); // [1 ,2 ,3]
            listOfIntegers.Add(4); // [1 ,2 ,3 ,4]
            listOfIntegers.Add(5); // [1 ,2 ,3 ,4 ,5]
            listOfIntegers.RemoveAt(0); // [2 ,3 ,4 ,5]
            listOfIntegers.Remove(5); //[2 ,3 ,4]
            Console.WriteLine(listOfIntegers.Count); // 3
            Console.WriteLine(listOfIntegers.Remove(100)); // false
            Console.WriteLine(listOfIntegers.RemoveAt(5)); // false
            listOfIntegers.Clear(); // []
            Console.WriteLine(listOfIntegers.Count); // 0
            Console.ReadLine();
        }
    }
}
