using Newtonsoft.Json;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProducerApp.AppUtils
{
    public class RedisHelper
    {
        private readonly ConnectionMultiplexer Redis;
        private readonly IDatabase DB;

        public RedisHelper()
        {
            Redis = ConnectionMultiplexer.Connect("localhost:6379");
            DB = Redis.GetDatabase(1);
        }

        /// <summary>
        /// 关闭 Redis 连接
        /// </summary>
        public void Dispose()
        {
            Redis.Close();
            Redis.Dispose();
        }

        /// <summary>
        /// 新增 / 修改 - String
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool SetStringValue(string key, string value)
        {
            return DB.StringSet(key, value);
        }

        /// <summary>
        /// 新增 / 修改 - List （单条数据）
        /// </summary>
        /// <param name="key"></param>
        /// <param name="index"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool SetListValue(string key, int index, string value)
        {
            try
            {
                DB.ListSetByIndex(key, index, value);
                return true;
            }
            catch (Exception ex)
            {
                _ = ex.Message;
                return false;
            }
        }

        /// <summary>
        /// 新增 - List （多条数据）
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool SetListValue(string key, List<string> value)
        {
            try
            {
                value.ForEach(item =>
                {
                    _ = DB.ListRightPush(key, item);
                });
                return true;
            }
            catch (Exception ex)
            {
                _ = ex.Message;
                return false;
            }
        }

        /// <summary>
        /// 查询 - String
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public string GetValue(string key)
        {
            return DB.StringGet(key);
        }

        /// <summary>
        /// 查询 - Model 对象
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public T GetModelByIndex<T>(string key, int index)
        {
            try
            {
                var jsonVal = DB.ListGetByIndex(key, index);
                return JsonConvert.DeserializeObject<T>(jsonVal);
            }
            catch (Exception ex)
            {
                _ = ex.Message;
                return Activator.CreateInstance<T>();
            }
        }

        /// <summary>
        /// 查询 - List<Model>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="startIndex"></param>
        /// <param name="endIndex"></param>
        /// <returns></returns>
        public List<T> GetList<T>(string key, int startIndex, int endIndex)
        {
            try
            {
                var data = DB.ListRange(key, startIndex, endIndex);
                var result = new List<T>();
                foreach (RedisValue val in data)
                {
                    var model = JsonConvert.DeserializeObject<T>(val);
                    if (model != null)
                    {
                        result.Add(model);
                    }
                }
                return result;
            }
            catch (Exception ex)
            {
                _ = ex.Message;
                return new List<T>();
            }
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool DeleteByKey(string key)
        {
            return DB.KeyDelete(key);
        }
    }
}
