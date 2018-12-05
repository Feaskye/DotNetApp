using System;
using System.Collections.Generic;
using System.Web;
using System.Runtime.CompilerServices;
using System.Web.Caching;
using System.Collections;
using System.Text.RegularExpressions;
using System.Runtime.Caching;

/// <summary>
/// CacheManager 的摘要说明
/// 一般web cache的使用封装
/// </summary>
public class CacheManager
{
	public CacheManager()
	{
		//
		// TODO: 在此处添加构造函数逻辑
		//
	}


    System.Web.Caching.Cache Cache = HttpRuntime.Cache;

    public void Set(string key, object data)
    {
        Cache.Insert(key, data);
    }
    public void Set(string key, object data, DateTime absoluteExpiration, TimeSpan slidingExpiration)
    {
        Cache.Insert(key, data, null, absoluteExpiration, slidingExpiration);
    }

    public object Get(string Key)
    {
        return Cache[Key];
    }

    public T Get<T>(string key)
    {
        return (T)Cache[key];
    }

    public bool IsSet(string key)
    {
        return Cache[key] != null;
    }

    public void Remove(string Key)
    {
        if (Cache[Key] != null)
        {
            Cache.Remove(Key);
        }
    }

    public void RemoveByPattern(string pattern)
    {
        IDictionaryEnumerator enumerator = Cache.GetEnumerator();
        Regex rgx = new Regex(pattern, (RegexOptions.Singleline | (RegexOptions.Compiled | RegexOptions.IgnoreCase)));
        while (enumerator.MoveNext())
        {
            if (rgx.IsMatch(enumerator.Key.ToString()))
            {
                Cache.Remove(enumerator.Key.ToString());
            }
        }
    }

    public void Clear()
    {
        IDictionaryEnumerator enumerator = Cache.GetEnumerator();
        while (enumerator.MoveNext())
        {
            Cache.Remove(enumerator.Key.ToString());
        }
    }
}


/// <summary>
/// MomeryCache的使用
/// 引用：using System.Runtime.Caching;
/// 参考网址：http://msdn.microsoft.com/zh-cn/library/system.runtime.caching.memorycache.aspx
/// </summary>
public class MomeryCacheMgr
{
    public MomeryCacheMgr()
    { 
    
    }

     /// <summary>
    /// MemoryCache 的使用
    /// 此方法涵盖了设置与获取
    /// </summary>
    void useMemoryCache()
    {
        ObjectCache Cache = MemoryCache.Default;
        string fileContents = Cache["filecontents"] as string;
        
        if (fileContents == null)
        {
            CacheItemPolicy policy = new CacheItemPolicy();

            List<string> filePaths = new List<string>();
            filePaths.Add("F:\\cache\\example.txt");
            
            //policy.ChangeMonitors.Add(new
            //HostFileChangeMonitor(filePaths));
            policy.AbsoluteExpiration = DateTime.Now + TimeSpan.FromMinutes(60);

            // Fetch the file contents.
            fileContents = "c:\\cache\\example.txt";

            Cache.Set("filecontents", fileContents, policy);
            //Cache.Set(,);
            //Label1.Text = fileContents;
        }
    }
}


