using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

// 작성자 : Rito
namespace Rito
{
    /// <summary> 가중치 랜덤 뽑기 </summary>
    public class WeightedRandomPicker<T>
    {
        /// <summary> 전체 아이템의 가중치 합 </summary>
        public double SumOfWeights
        {
            get
            {
                CalculateSumIfDirty();
                return mSumOfWeights;
            }
        }

        private System.Random mRandomInstance;
        private readonly Dictionary<T, double> mItemWeightDict;
        private readonly Dictionary<T, double> mNormalizedItemWeightDict; // 확률이 정규화된 아이템 목록

        /// <summary> 가중치 합이 계산되지 않은 상태인지 여부 </summary>
        private bool mbDirty;
        private double mSumOfWeights;

        /***********************************************************************
        *                               Constructors
        ***********************************************************************/
        #region .
        public WeightedRandomPicker()
        {
            mRandomInstance = new System.Random();
            mItemWeightDict = new Dictionary<T, double>();
            mNormalizedItemWeightDict = new Dictionary<T, double>();
            mbDirty = true;
            mSumOfWeights = 0.0;
        }
        public WeightedRandomPicker(int randomSeed)
        {
            mRandomInstance = new System.Random(randomSeed);
            mItemWeightDict = new Dictionary<T, double>();
            mNormalizedItemWeightDict = new Dictionary<T, double>();
            mbDirty = true;
            mSumOfWeights = 0.0;
        }
        #endregion
        /***********************************************************************
        *                               Add Methods
        ***********************************************************************/
        #region .
        /// <summary> 새로운 아이템-가중치 쌍 추가 </summary>
        public void Add(T item, double weight)
        {
            if (HasDuplicatedItem(item))
                return;
            CheckValidWeight(weight);

            mItemWeightDict.Add(item, weight);
            mbDirty = true;
        }
        /// <summary> 새로운 아이템-가중치 쌍들 추가 </summary>
        public void Add(params (T item, double weight)[] pairs)
        {
            foreach (var pair in pairs)
            {
                if (HasDuplicatedItem(pair.item))
                    return;
                CheckValidWeight(pair.weight);

                mItemWeightDict.Add(pair.item, pair.weight);
            }
            mbDirty = true;
        }
        #endregion
        /***********************************************************************
        *                               Public Methods
        ***********************************************************************/
        #region .
        /// <summary> 목록에서 대상 아이템 제거 </summary>
        public void Remove(T item)
        {
            CheckNotExistedItem(item);

            mItemWeightDict.Remove(item);
            mbDirty = true;
        }
        /// <summary> 대상 아이템의 가중치 수정 </summary>
        public void ModifyWeight(T item, double weight)
        {
            CheckNotExistedItem(item);
            CheckValidWeight(weight);

            mItemWeightDict[item] = weight;
            mbDirty = true;
        }
        /// <summary> 랜덤 시드 재설정 </summary>
        public void ReSeed(int seed)
        {
            mRandomInstance = new System.Random(seed);
        }
        #endregion
        /***********************************************************************
        *                               Getter Methods
        ***********************************************************************/
        #region .
        /// <summary> 랜덤 뽑기 </summary>
        public T GetRandomPick()
        {
            // 랜덤 계산
            double chance = mRandomInstance.NextDouble(); // [0.0, 1.0)
            chance *= SumOfWeights;

            return GetRandomPick(chance);
        }
        /// <summary> 직접 랜덤 값을 지정하여 뽑기 </summary>
        public T GetRandomPick(double randomValue)
        {
            if (randomValue < 0.0) randomValue = 0.0;
            if (randomValue > SumOfWeights) randomValue = SumOfWeights - 0.00000001;

            double current = 0.0;
            foreach (var pair in mItemWeightDict)
            {
                current += pair.Value;

                if (randomValue < current)
                {
                    return pair.Key;
                }
            }

            throw new Exception($"Unreachable - [Random Value : {randomValue}, Current Value : {current}]");
            //return itemPairList[itemPairList.Count - 1].item; // Last Item
        }
        /// <summary> 대상 아이템의 가중치 확인 </summary>
        public double GetWeight(T item)
        {
            return mItemWeightDict[item];
        }
        /// <summary> 대상 아이템의 정규화된 가중치 확인 </summary>
        public double GetNormalizedWeight(T item)
        {
            CalculateSumIfDirty();
            return mNormalizedItemWeightDict[item];
        }
        /// <summary> 아이템 목록 확인(읽기 전용) </summary>
        public ReadOnlyDictionary<T, double> GetItemDictReadonly()
        {
            return new ReadOnlyDictionary<T, double>(mItemWeightDict);
        }
        /// <summary> 가중치 합이 1이 되도록 정규화된 아이템 목록 확인(읽기 전용) </summary>
        public ReadOnlyDictionary<T, double> GetNormalizedItemDictReadonly()
        {
            CalculateSumIfDirty();
            return new ReadOnlyDictionary<T, double>(mNormalizedItemWeightDict);
        }
        #endregion
        /***********************************************************************
        *                               Private Methods
        ***********************************************************************/
        #region .
        /// <summary> 모든 아이템의 가중치 합 계산해놓기 </summary>
        private void CalculateSumIfDirty()
        {
            if(!mbDirty) return;
            mbDirty = false;

            mSumOfWeights = 0.0;
            foreach (var pair in mItemWeightDict)
            {
                mSumOfWeights += pair.Value;
            }

            // 정규화 딕셔너리도 업데이트
            UpdateNormalizedDict();
        }
        /// <summary> 정규화된 딕셔너리 업데이트 </summary>
        private void UpdateNormalizedDict()
        {
            mNormalizedItemWeightDict.Clear();
            foreach(var pair in mItemWeightDict)
            {
                mNormalizedItemWeightDict.Add(pair.Key, pair.Value / mSumOfWeights);
            }
        }
        /// <summary> 이미 아이템이 존재하는지 여부 검사 </summary>
        private bool HasDuplicatedItem(T item)
        {
            if (mItemWeightDict.ContainsKey(item))
                return true;
                //throw new Exception($"이미 [{item}] 아이템이 존재합니다.");
            return false;
        }
        /// <summary> 존재하지 않는 아이템인 경우 </summary>
        private void CheckNotExistedItem(T item)
        {
            if(!mItemWeightDict.ContainsKey(item))
                throw new Exception($"[{item}] 아이템이 목록에 존재하지 않습니다.");
        }
        /// <summary> 가중치 값 범위 검사(0보다 커야 함) </summary>
        private void CheckValidWeight(in double weight)
        {
            if (weight <= 0f)
                throw new Exception("가중치 값은 0보다 커야 합니다.");
        }

        #endregion
    }
}