using System;
using System.Collections.Generic;
using System.Linq;

namespace LRU_Cache
{
  class Program
  {
    static void Main(string[] args)
    {
      LRUCache sol = new LRUCache(2);
      sol.Put(1, 0);
      sol.Put(2, 2);
      Console.WriteLine(sol.Get(1));
      sol.Put(3, 3);
      Console.WriteLine(sol.Get(2));
      sol.Put(4, 4);
      Console.WriteLine(sol.Get(1));
      Console.WriteLine(sol.Get(3));
      Console.WriteLine(sol.Get(4));
    }
  }

  public class CacheNode
  {
    public int value;
    public int key;
    public CacheNode(int val, int key)
    {
      this.value = val;
      this.key = key;
    }
  }

  public class LRUCache
  {

    LinkedList<CacheNode> cache;
    Dictionary<int, LinkedListNode<CacheNode>> reference;
    int Capacity;
    int Count;
    public LRUCache(int capacity)
    {
      Capacity = capacity;
      Count = 0;
      cache = new LinkedList<CacheNode>();
      reference = new Dictionary<int, LinkedListNode<CacheNode>>();
    }

    public int Get(int key)
    {
      if (reference.ContainsKey(key))
      {
        var exist = reference[key];
        cache.Remove(exist);
        cache.AddFirst(exist);
        return exist.Value.value;
      }

      return -1;
    }

    public void Put(int key, int value)
    {
      if (reference.ContainsKey(key))
      {
        var exist = reference[key];
        cache.Remove(exist);
        exist.Value.value = value;
        cache.AddFirst(exist);
        return;
      }

      if (Capacity == Count)
      {
        var lru = cache.Last;
        cache.RemoveLast();
        reference.Remove(lru.Value.key);
        Count--;
      }

      var node = new CacheNode(value, key);
      var added = cache.AddFirst(node);
      reference.Add(key, added);
      Count++;
    }
  }
}
