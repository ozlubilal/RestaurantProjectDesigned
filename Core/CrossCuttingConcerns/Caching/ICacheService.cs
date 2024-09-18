using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.CrossCuttingConcerns.Caching;

public interface ICacheManager
{
    T Get<T>(string key);
    void Add<T>(string key, T data, int duration);
    bool IsAdd(string key);
    void Remove(string key);
    void RemoveByPattern(string pattern);
}
