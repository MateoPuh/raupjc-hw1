using System;
using System.Collections;
using System.Collections.Generic;

public interface IGenericList<X> : IEnumerable<X>
{
    /// <summary >
    /// Adds an item to the collection .
    /// </ summary >
    void Add(X item);

    /// <summary >
    /// Removes the first occurrence of an item from the collection .
    /// If the item was not found , method does nothing .
    /// </ summary >
    bool Remove(X item);

    /// <summary >
    /// Removes the item at the given index in the collection .
    /// </ summary >
    bool RemoveAt(int index);

    /// <summary>
    /// Returns the item at the given index in the collection .
    /// </ summary >
    X GetElement(int index);

    /// <summary >
    /// Returns the index of the item in the collection .
    /// If item is not found in the collection , method returns -1.
    /// </ summary >
    int IndexOf(X item);

    /// <summary >
    /// Readonly property . Gets the number of items contained in the collection.
    /// /// </ summary >
    int Count { get; }

    /// <summary >
    /// Removes all items from the collection .
    /// </ summary >
    void Clear();

    /// <summary >
    /// Determines whether the collection contains a specific value .
    /// </ summary >
    bool Contains(X item);
}

public class GenericList<X> : IGenericList<X>
{
    private int count=0;
    public int Count
    {
        get
        {
            return count;
        }
    }
    private X[] _internalStorage;


    public GenericList(int initialSize)
    {
        if (initialSize <= 0)
        {
            throw new ArgumentException("Size has to be greater than zero");
        }
        _internalStorage = new X[initialSize];
    }

    public GenericList()
    {
        _internalStorage = new X[4];
    }


    public void Add(X item)
    {
        int capacity = _internalStorage.Length;

        if (count >= capacity)
        {
            X[] newIntegerList = new X[2 * capacity];

            for (int i = 0; i < capacity; i++)
            {
                newIntegerList[i] = _internalStorage[i];
            }
            newIntegerList[count++] = item;
            _internalStorage = newIntegerList;
            return;
        }

        _internalStorage[count++] = item;
    }

    public void Clear()
    {
        count = 0;
    }

    public bool Contains(X item)
    {
        for (int i = 0; i < count; i++)
        {
            if (_internalStorage[i].Equals(item))
            {
                return true;
            }
        }

        return false;
    }

    public X GetElement(int index)
    {
        if (index >= 0 && index < count)
        {
            return _internalStorage[index];
        }
        else
        {
            throw new IndexOutOfRangeException("Index out of range.");
        }
    }

    public int IndexOf(X item)
    {
        for (int i = 0; i < count; i++)
        {
            if (_internalStorage[i].Equals(item))
            {
                return i;
            }
        }

        return -1;
    }

    public bool Remove(X item)
    {
        int index;

        for (int i = 0; i < count; i++)
        {
            if (_internalStorage[i].Equals(item))
            {
                index = i;
                return RemoveAt(index);
            }
        }

        return false;
    }

    public bool RemoveAt(int index)
    {
        if(index < 0 || index >= _internalStorage.Length)
        {
            throw new IndexOutOfRangeException("Index is out of range.");
        }

        if (index >= count)
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

    public IEnumerator<X> GetEnumerator()
    {
        return new GenericListEnumerator<X>(this);
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}

public class GenericListEnumerator<T> : IEnumerator<T>
{
    private int index=-1;
    private GenericList<T> genericList;

    public GenericListEnumerator(GenericList<T> genericList)
    {
        this.genericList = genericList;
    }

    object IEnumerator.Current
    {
        get
        {
            return Current;
        }
    }

    public bool MoveNext()
    {
        index++;
        return (index < genericList.Count);
    }

    public void Reset()
    {
        index = 0;
    }

    public void Dispose()
    {
    }

    public T Current
    {
        get
        {
            try
            {
                return genericList.GetElement(index);
            }
            catch (IndexOutOfRangeException)
            {
                throw new IndexOutOfRangeException("Index out of range");
            }
        }
    }

}

namespace Zadatak3
{
    class Program
    {
        static void Main(string[] args)
        {
            GenericList<int> genList = new GenericList<int>();
            genList.Add(1);
            genList.Add(2);
            genList.Add(3);

            foreach (int i in genList)
            {
                Console.WriteLine(i);
            }
        }
    }
}
