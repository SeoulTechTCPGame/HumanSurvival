using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class StageInfo : MonoBehaviour, IPointerEnterHandler
{
    [SerializeField] int mStage;
    [SerializeField] TMP_Text mStageName;
    [SerializeField] TMP_Text mStageName1;
    [SerializeField] TMP_Text mStageExplain;
    [SerializeField] TMP_Text mStagePlayTime;
    [SerializeField] TMP_Text mStageDoubleSpeed;
    [SerializeField] TMP_Text mStageGoldCoinBonus;
    [SerializeField] TMP_Text mStageLuckBonus;
    [SerializeField] TMP_Text mStageExperienceBonus;

    string mSName;
    string mExplain;
    string mPlayTime;
    float mDoubleSpeed;
    float mGoldCoinBonus;
    float mLuckBonus;
    float mExperienceBonus;

    void Start()
    {
        switch (mStage)
        {
            case 1:
                this.mSName = "스테이지 1";
                this.mExplain = "스테이지 설명 1";
                this.mPlayTime = "30:01";
                this.mDoubleSpeed = 1;
                this.mGoldCoinBonus = 1;
                this.mLuckBonus = 1;
                this.mExperienceBonus = 1;
                break;

            case 2:
                this.mSName = "스테이지 2";
                this.mExplain = "스테이지 설명 2";
                this.mPlayTime = "30:02";
                this.mDoubleSpeed = 2;
                this.mGoldCoinBonus = 2;
                this.mLuckBonus = 2;
                this.mExperienceBonus = 2;
                break;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        mStageName.text = this.mSName;
        mStageName1.text = this.mSName;
        mStageExplain.text = this.mExplain;
        mStagePlayTime.text = "Play Time " + this.mPlayTime;
        mStageDoubleSpeed.text = "Double Speed " + this.mDoubleSpeed.ToString() + "%";
        mStageGoldCoinBonus.text = "Gold Coin Bonus " + this.mGoldCoinBonus.ToString() + "%";
        mStageLuckBonus.text = "Luck Bonus " + this.mLuckBonus.ToString() + "%";
        mStageExperienceBonus.text = "Experience Bonus " + this.mExperienceBonus.ToString() + "%";
    }
}