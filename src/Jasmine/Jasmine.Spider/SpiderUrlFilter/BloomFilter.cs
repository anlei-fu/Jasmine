using System;
using System.Collections;
using System.Collections.Generic;

namespace Jasmine.Spider.SpiderUrlFilter
{
    public class BloomFilter<T>
    {
        /// <summary>
        /// 随机生成器
        /// </summary>
        private Random _random;
        /// <summary>
        /// 位数组
        /// </summary>
        private BitArray _bitArray;
        /// <summary>
        /// 初始化bloom滤波器并设置hash散列的最佳数目
        /// </summary>
        /// <param name="bitSize">布隆过滤器的大小(m)</param>
        /// <param name="setSize">集合的大小 (n)</param>
        public BloomFilter(int bitSize, int setSize, int numberOfHashes = 0)
        {
            BitSize = bitSize;
            _bitArray = new BitArray(bitSize);
            SetSize = setSize;
            //等于0计算最佳哈希个数
            NumberOfHashes = numberOfHashes == 0 ? (int)Math.Ceiling((BitSize / SetSize) * Math.Log(2.0)) : numberOfHashes;
        }
        /// <summary>
        /// 错误率
        /// </summary>
        public double FalsePositiveProbability => Math.Pow((1 - Math.Exp(-NumberOfHashes * SetSize / (double)BitSize)), NumberOfHashes);
        /// <summary>
        /// 哈希函数的个数
        /// </summary>
        public int NumberOfHashes { get; private set; }
        /// <summary>
        /// 集合大小
        /// </summary>
        public int SetSize { get; private set; }
        /// <summary>
        /// 位数组位数
        /// </summary>
        public int BitSize { get; private set; }
        /// <summary>
        /// Unique 添加
        /// </summary>
        public bool Add(T item)
        {
            if (Contains(item))
                return false;

            _random = new Random(item.GetHashCode());

            for (int i = 0; i < NumberOfHashes; i++)
                _bitArray[_random.Next(BitSize)] = true;

            return true;
        }
        public void Add(IEnumerable<T> collection)
        {
            foreach (var item in collection)
                Add(item);
        }
        /// <summary>
        /// 包含
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool Contains(T item)
        {
            _random = new Random(item.GetHashCode());

            for (int i = 0; i < NumberOfHashes; i++)
            {
                if (!_bitArray[_random.Next(BitSize)])
                    return false;
            }

            return true;
        }
        /// <summary>
        /// 包含任何一个
        /// </summary>
        /// <param name="collection"></param>
        /// <returns></returns>
        public bool ContainsAny(IEnumerable<T> collection)
        {
            foreach (T item in collection)
            {
                if (Contains(item))
                    return true;
            }

            return false;
        }
        /// <summary>
        /// 包含全部
        /// </summary>
        /// <param name="collection"></param>
        /// <returns></returns>
        public bool ContainsAll(IEnumerable<T> collection)
        {
            foreach (T item in collection)
            {
                if (!Contains(item))
                    return false;
            }

            return true;
        }
    }

}
