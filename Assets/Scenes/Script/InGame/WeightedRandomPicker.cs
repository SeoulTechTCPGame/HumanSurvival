using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

// 날짜 : 2021-03-09 AM 1:08:48
// 작성자 : Rito

/*
    [가중치 랜덤 뽑기]

    - 제네릭을 통해 아이템의 타입을 지정해 객체화하여 사용한다.
    - 중복되는 아이템이 없도록 딕셔너리로 구현하였다.
    - 가중치가 0보다 작은 경우 예외를 호출한다.

    - double SumOfWeights : 전체 아이템의 가중치 합(읽기 전용 프로퍼티)

    - void Add(T, double) : 새로운 아이템-가중치 쌍을 추가한다.
    - void Add(params (T, double)[]) : 새로운 아이템-가중치 쌍을 여러 개 추가한다.
    - void Remove(T) : 대상 아이템을 목록에서 제거한다.
    - void ModifyWeight(T, double) : 대상 아이템의 가중치를 변경한다.
    - void ReSeed(int) : 랜덤 시드를 재설정한다.

    - T GetRandomPick() : 현재 아이템 목록에서 가중치를 계산하여 랜덤으로 항목 하나를 뽑아온다.
    - T GetRandomPick(double) : 이미 계산된 확률 값을 매개변수로 넣어, 해당되는 항목 하나를 뽑아온다.
    - double GetWeight(T) : 대상 아이템의 가중치를 얻어온다.
    - double GetNormalizedWeight(T) : 대상 아이템의 정규화된 가중치를 얻어온다.

    - ReadonlyDictionary<T, double> GetItemDictReadonly() : 전체 아이템 목록을 읽기전용 컬렉션으로 받아온다.
    - ReadonlyDictionary<T, double> GetNormalizedItemDictReadonly()
      : 전체 아이템의 가중치 총합이 1이 되도록 정규화된 아이템 목록을 읽기전용 컬렉션으로 받아온다.
*/

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