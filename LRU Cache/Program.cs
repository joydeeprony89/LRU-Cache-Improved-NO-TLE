using System;
using System.Collections.Generic;

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
    public int val;
    public int key;
    public CacheNode(int val, int key)
    {
      this.val = val;
      this.key = key;
    }
  }


  public class LRUCache
  {

    LinkedList<CacheNode> cache;
    Dictionary<int, LinkedListNode<CacheNode>> reference;
    public int Capacity { get; private set; }
    int count;
    public LRUCache(int capacity)
    {
      count = 0;
      Capacity = capacity;
      cache = new LinkedList<CacheNode>();
      reference = new Dictionary<int, LinkedListNode<CacheNode>>();
    }

    public int Get(int key)
    {
      if (reference.ContainsKey(key))
      {
        var existing = reference[key];
        cache.Remove(existing);
        cache.AddFirst(existing);
        return existing.Value.val;
      }

      return -1;
    }

    public void Put(int key, int value)
    {
      // when we call PUT, there are 3 possibility
      // 1 - Key is exist and need to perform update
      // 2 - Cache is full already
      // 3 - 1 and 2 not matched so insert into the cache

      // Scenario - 1
      if (reference.ContainsKey(key))
      {
        var existing = reference[key];
        //O(1) as we are removing using the LinkedListNode reference
        cache.Remove(existing);
        // Add it back to the top of LL
        cache.AddFirst(existing);
        // update the existing value with new value reference in Dictionary
        existing.Value.val = value;
        return;
      }

      // Scenario - 2
      if (Capacity == count)
      {
        var last = cache.Last;
        reference.Remove(last.Value.key);
        cache.RemoveLast();
        count--;
      }

      // Scenario - 3
      cache.AddFirst(new CacheNode(value, key));
      // update the existing value with new value reference in Dictionary
      reference.Add(key, cache.First);
      count++;
    }
  }

  /**
   * Your LRUCache object will be instantiated and called as such:
   * LRUCache obj = new LRUCache(capacity);
   * int param_1 = obj.Get(key);
   * obj.Put(key,value);
   */
}
