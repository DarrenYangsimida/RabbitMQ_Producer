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
        private readonly List<IDatabase> DBS;

        /// <summary>
        /// Redis 连接初始化
        /// </summary>
        /// <param name="dbCount">需要连接的 Redis DB 总数</param>
        public RedisHelper(int dbCount = 1)
        {
            Redis = ConnectionMultiplexer.Connect("localhost:6379");
            DBS = new List<IDatabase>();
            if (dbCount > 1)
            {
                for (int i = 0; i < dbCount; i++)
                {
                    DBS.Add(Redis.GetDatabase(i));
                }
            }
            else
            {
                DBS.Add(Redis.GetDatabase(0));
            }
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
        /// <param name="dbNum">需要操作的数据库索引，默认第一个 DB0</param>
        /// <returns></returns>
        public bool SetStringValue(string key, string value, int dbNum = 0)
        {
            return DBS[dbNum].StringSet(key, value);
        }

        /// <summary>
        /// 新增 / 修改 - List （单条数据）
        /// </summary>
        /// <param name="key"></param>
        /// <param name="index"></param>
        /// <param name="value"></param>
        /// <param name="dbNum">需要操作的数据库索引，默认第一个 DB0</param>
        /// <returns></returns>
        public bool SetListValue(string key, int index, string value, int dbNum = 0)
        {
            try
            {
                DBS[dbNum].ListSetByIndex(key, index, value);
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
        /// <param name="dbNum">需要操作的数据库索引，默认第一个 DB0</param>
        /// <returns></returns>
        public bool SetListValue(string key, RedisValue[] values, int dbNum = 0)
        {
            try
            {
                _ = DBS[dbNum].ListRightPush(key, values);
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
        /// <param name="dbNum">需要操作的数据库索引，默认第一个 DB0</param>
        /// <returns></returns>
        public string GetValue(string key, int dbNum = 0)
        {
            return DBS[dbNum].StringGet(key);
        }

        /// <summary>
        /// 查询 - Model 对象
        /// </summary>
        /// <param name="key"></param>
        /// <param name="dbNum">需要操作的数据库索引，默认第一个 DB0</param>
        /// <returns></returns>
        public T GetModelByIndex<T>(string key, int index, int dbNum = 0)
        {
            try
            {
                var jsonVal = DBS[dbNum].ListGetByIndex(key, index);
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
        /// <param name="dbNum">需要操作的数据库索引，默认第一个 DB0</param>
        /// <returns></returns>
        public List<T> GetList<T>(string key, int startIndex, int endIndex, int dbNum = 0)
        {
            try
            {
                var data = DBS[dbNum].ListRange(key, startIndex, endIndex);
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
        /// <param name="dbNum">需要操作的数据库索引，默认第一个 DB0</param>
        /// <returns></returns>
        public bool DeleteByKey(string key, int dbNum = 0)
        {
            return DBS[dbNum].KeyDelete(key);
        }
    }
}
