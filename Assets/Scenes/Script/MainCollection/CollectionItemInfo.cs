using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

using System;
using TMPro;

public class CollectionItemInfo : MonoBehaviour, IPointerEnterHandler
{
    [SerializeField] int mItemIndex;
    [SerializeField] Image mItemImage;
    [SerializeField] Image mThisItemIamge;
    [SerializeField] Sprite mBlackImage;
    [SerializeField] Sprite mGreenImage;
    [SerializeField] Sprite mPinkImage;
    [SerializeField] Sprite mPurpleImage;

    [SerializeField] TMP_Text mWeaponItemName;
    [SerializeField] TMP_Text mWeaponExplain;

    string mItemName;
    string mExplain;
    string mRank;

    // Start is called before the first frame update
    void Start(){
        switch(mItemIndex){
            case 1:
                this.mItemName = "채찍\n[Whip]";
                this.mExplain = "좌우로 적을 관통해 공격합니다.\n(Attacks horizontally, passes through enemies.)";
                this.mRank = "black";
                break;
            
            case 2:
                this.mItemName = "피눈물\n[Blood Tear]";
                this.mExplain = "채찍의 진화형. 치명타 피해를 입히고 HP를 흡수합니다.\n(Evolved Whip. Can deal critical damage and absorb HP.)";
                this.mRank = "black";
                break;

            case 3:
                this.mItemName = "마법 지팡이\n[Magic Wand]";
                this.mExplain = "가장 가까운 적을 공격합니다.\n(Fires at the nearest enemy.)";
                this.mRank = "black";
                break;

            case 4:
                this.mItemName = "신성한 지팡이\n[Holy Wand]";
                this.mExplain = "마법 지팡이의 진화형. 지연 없이 발사됩니다.\n(Evolved Magic Wand. Fires with no delay.)";
                this.mRank = "black";
                break;

            case 5:
                this.mItemName = "단검\n[Knife]";
                this.mExplain = "바라보는 방향으로 단검을 투척합니다.\n(Fires quickly in the faced direction.)";
                this.mRank = "black";
                break;

            case 6:
                this.mItemName = "천개의 칼날\n[Thousand Edge]";
                this.mExplain = "단검의 진화형. 지연 없이 발사됩니다.\n(Evolved Knife. Fires with no delay.)";
                this.mRank = "black";
                break;

            case 7:
                this.mItemName = "성경\n[King Bible]";
                this.mExplain = "주변을 회전하며 공격합니다.\n(Orbits around the character.)";
                this.mRank = "black";
                break;

            case 8:
                this.mItemName = "불경한 기도문\n[Unholy Vespers]";
                this.mExplain = "성경의 진화형. 사라지지 않습니다.\n(Evolved King Bible. Never ends.)";
                this.mRank = "black";
                break;

            case 9:
                this.mItemName = "불의 지팡이\n[Fire Wand]";
                this.mExplain = "무작위 방향으로 향해 발사되며 큰 피해를 줍니다.\n(Fires at a random enemy, deals heavy damage.)";
                this.mRank = "black";
                break;

            case 10:
                this.mItemName = "헬파이어\n[Hellfire]";
                this.mExplain = "불의 지팡이의 진화형. 적을 관통합니다.\n(Evolved Fire Wand. Passes through enemies.)";
                this.mRank = "black";
                break;

            case 11:
                this.mItemName = "마늘\n[Garlic]";
                this.mExplain = "범위 내의 적에게 피해를 줍니다. 적의 넉백, 빙결저항을 감소시킵니다.\n(Damages nearby enemies. Reduces resistance to knockback and freeze.)";
                this.mRank = "black";
                break;

            case 12:
                this.mItemName = "영혼 포식자\n[Soul Eater]";
                this.mExplain = "마늘의 진화형. 적 처치 시 체력을 흡수하며 체력흡수 시 피해량이 증가합니다.\n(Evolved Garlic. Steals hearts. Power increases when recovering HP.)";
                this.mRank = "black";
                break;

            case 13:
                this.mItemName = "성수\n[Santa Water]";
                this.mExplain = "피해 구역를 생성합니다.\n(Generates damaging zones.)";
                this.mRank = "black";
                break;

            case 14:
                this.mItemName = "정화자\n[La Borra]";
                this.mExplain = "성수의 진화형. 피해 구역이 점점 커지며 캐릭터를 따릅니다.\n(Evolved Santa Water. Damaging zones follow you and grow when they move.)";
                this.mRank = "black";
                break;

            case 15:
                this.mItemName = "번개 반지\n[Lightning Ring]";
                this.mExplain = "무작위 적에게 번개를 내려칩니다.\n(Strikes at random enemies.)";
                this.mRank = "black";
                break;

            case 16:
                this.mItemName = "번개 고리\n[Thunder Loop]";
                this.mExplain = "번개 반지의 진화형. 투사체가 2회 공격합니다.\n(Evolved Lightning Ring. Projectiles strike twice.)";
                this.mRank = "black";
                break;

            case 17:
                this.mItemName = "하얀 비둘기\n[Peachone]";
                this.mExplain = "원형의 구역을 폭격합니다.\n(Bombards in a circular area.)";
                this.mRank = "black";
                break;

            case 18:
                this.mItemName = "검은 비둘기\n[Ebony Wings]";
                this.mExplain = "원형의 구역을 폭격합니다.\n(Bombards in a circular area.)";
                this.mRank = "black";
                break;

            case 19:
                this.mItemName = "파괴자\n[Vandalier]";
                this.mExplain = "하얀 비둘기와 검은 비둘기의 진화형.\n(Union of Ebony Wings and Peachone.)";
                this.mRank = "black";
                break;

            case 20:
                this.mItemName = "시곗바늘\n[Clock Lancet]";
                this.mExplain = "적들을 잠시 얼려 움직이지 못하게 합니다.\n(Chance to freeze enemies in time.)";
                this.mRank = "black";
                break;

            case 21:
                this.mItemName = "시금치\n[Spinach]";
                this.mExplain = "공격력이 10% 증가한다.";
                this.mRank = "black";
                break;

            case 22:
                this.mItemName = "갑옷\n[Armor]";
                this.mExplain = "방어력이 1, 반격 대미지가 10% 증가한다.";
                this.mRank = "black";
                break;

            case 23:
                this.mItemName = "검은 심장\n[Hollow Heart]";
                this.mExplain = "최대 체력이 20% 증가한다.";
                this.mRank = "black";
                break;

            case 24:
                this.mItemName = "붉은 심장[\nPummarola]";
                this.mExplain = "매 초 0.2 의 체력이 회복된다.";
                this.mRank = "black";
                break;

            case 25:
                this.mItemName = "빈 책\n[Empty Tome]";
                this.mExplain = "쿨타임이 8% 감소한다.";
                this.mRank = "black";
                break;

            case 26:
                this.mItemName = "촛대\n[Candelabrador]";
                this.mExplain = "공격범위가 10% 증가한다.";
                this.mRank = "black";
                break;

            case 27:
                this.mItemName = "팔 보호대\n[Barcer]";
                this.mExplain = "투사체 속도가 10% 증가한다.";
                this.mRank = "black";
                break;

            case 28:
                this.mItemName = "복제 반지\n[Duplicator]";
                this.mExplain = "투사체 수가 1개 증가한다.";
                this.mRank = "black";
                break;

            case 29:
                this.mItemName = "날개\n[Wings]";
                this.mExplain = "이동속도가 10% 상승한다.";
                this.mRank = "black";
                break;

            case 30:
                this.mItemName = "매혹구\n[Attractorb]";
                this.mExplain = "아이템 획득 범위가 증가한다.";
                this.mRank = "black";
                break;

            case 31:
                this.mItemName = "클로버\n[Clover]";
                this.mExplain = "행운이 10% 증가한다.";
                this.mRank = "black";
                break;

            case 32:
                this.mItemName = "왕관\n[Corwn]";
                this.mExplain = "경험치 획득량이 8% 증가한다.";
                this.mRank = "black";
                break;

            case 33:
                this.mItemName = "돌가면\n[Stone Mask]";
                this.mExplain = "골드 획득량이 10% 증가한다.";
                this.mRank = "black";
                break;

            case 34:
                this.mItemName = "토로나의 상자\n[Torrona's Box]";
                this.mExplain = "피해량, 투사체 속도, 지속시간, 공격범위가 증가한다. 9레벨에 저주가 증가한다.";
                this.mRank = "black";
                break;

            case 35:
                this.mItemName = "경험치 보석\n[Experience Gem]";
                this.mExplain = "경험치를 증가시킵니다.";
                this.mRank = "green";
                break;

            case 36:
                this.mItemName = "금화\n[Gold Coin]";
                this.mExplain = "획득시 1골드를 획득합니다.";
                this.mRank = "green";
                break;

            case 37:
                this.mItemName = "금화 주머니\n[Coin Bag]";
                this.mExplain = "획득시 10골드를 획득합니다.";
                this.mRank = "green";
                break;

            case 38:
                this.mItemName = "거대한 금화 주머니\n[Rich Coin Bag]";
                this.mExplain = "획득시 100골드를 획득합니다.";
                this.mRank = "green";
                break;

            case 39:
                this.mItemName = "묵주\n[Rosary]";
                this.mExplain = "시야 내 모든 적을 파괴합니다.";
                this.mRank = "green";
                break;

            case 40:
                this.mItemName = "은두자 프리타\n[Nduja Fritta]";
                this.mExplain = "바라보는 방향으로 화염을 방사합니다. 플레이어 스탯에 영향을 받습니다.";
                this.mRank = "green";
                break;

            case 41:
                this.mItemName = "회중시계\n[Orologion]";
                this.mExplain = "10초동안 모든 적을 얼려버립니다.";
                this.mRank = "green";
                break;

            case 42:
                this.mItemName = "흡입기\n[Vaccum]";
                this.mExplain = "지면의 모든 경험치 보석을 모아줍니다.";
                this.mRank = "green";
                break;

            case 43:
                this.mItemName = "치킨\n[Floor Chicken]";
                this.mExplain = "체력 30을 회복합니다.";
                this.mRank = "green";
                break;

            case 44:
                this.mItemName = "금박 클로버\n[Gilded Clover]";
                this.mExplain = "땅에 떨어진 모든 금화를 흡수하고 황금의 피버타임을 가집니다.";
                this.mRank = "green";
                break;

            case 45:
                this.mItemName = "작은 클로버\n[Little Clover]";
                this.mExplain = "획득시 행운이 10% 증가합니다.";
                this.mRank = "green";
                break;

            case 46:
                this.mItemName = "보물 상자\n[Treasure Chest]";
                this.mExplain = "골드와 무작위 파워 업을 획득합니다. 강력한 적에게서만 획득 할 수 있습니다.";
                this.mRank = "green";
                break;

            case 47:
                this.mItemName = "엄숙한 마도서\n[Grim cruniure]";
                this.mExplain = "일시 정지 메뉴에서 발견한 무기 진화, 조합법을 영구적으로 확인할 수 있습니다.";
                break;

            case 48:
                this.mItemName = "은하수 지도\n[Milky Way Map]";
                this.mExplain = "일시정지 화면에서 지도를 활성화하여 필드의 아이템 위치를 표시합니다.\n(Permanently enables the map in the pause menu.)";
                this.mRank = "pink";
                break;

            case 49:
                this.mItemName = "매직 뱅거\n[Magic Banger]";
                this.mExplain = "스테이지 선택에서 음악을 영구적으로 변경할 수 있습니다.\n(Permanently allows to change music in Stage Selection.)";
                this.mRank = "pink";
                break;

            case 50:
                this.mItemName = "마녀의 눈물\n[Sorceress' Tears]";
                this.mExplain = "스테이지 선택에서 게임을 영구적으로 가속할 수 있습니다.\n(Permanently allows to speed up time in Stage Selection.)";
                this.mRank = "pink";
                break;

            case 51:
                this.mItemName = "코주부 안경\n[Mindbender]";
                this.mExplain = "캐릭터의 외형을 변경 가능하게 해줍니다. 또한 게임 시작 전에 소지 가능한 최대 무기의 수량을 지정할 수 있습니다.";
                this.mRank = "pink";
                break;

            case 52:
                this.mItemName = "공주의 비련\n[Tragic Princess]";
                this.mExplain = "포르타 레벨 99 달성";
                this.mRank = "purple";
                break;

            case 53:
                this.mItemName = "푸른 강철의 의지\n[Iron Blue Will]";
                this.mExplain = "젠나로 레벨 99 달성";
                this.mRank = "purple";
                break;

            case 54:
                this.mItemName = "광기의 그루브\n[Mad Groove]";
                this.mExplain = "광기의 숲에서 31분 생존";
                this.mRank = "purple";
                break;

            case 55:
                this.mItemName = "신성한 혈통\n[Divine Bloodline]";
                this.mExplain = "클레리씨 수녀 레벨 99 달성";
                this.mRank = "purple";
                break;

            case 56:
                this.mItemName = "근원\n[Beginning]";
                this.mExplain = "안토니오 레벨 99 달성";
                this.mRank = "purple";
                break;

            case 57:
                this.mItemName = "진주의 왈츠\n[Waltz of Pearls]";
                this.mExplain = "이멜다 레벨 99 달성";
                this.mRank = "purple";
                break;

            case 58:
                this.mItemName = "황금 디스코\n[Disco of Gold]";
                this.mExplain = "화려한 도서관 31분 생존";
                this.mRank = "purple";
                break;

            case 59:
                this.mItemName = "불의 심장\n[Heart of Fire]";
                this.mExplain = "아르카 레벨 99 달성";
                this.mRank = "purple";
                break;

            case 60:
                this.mItemName = "블러드 아스트로노미아\n[Blood Astronmia]";
                this.mExplain = "포 랏초 99 레벨 달성";
                this.mRank = "purple";
                break;
        }
        
        if (!UserInfo.instance.UserDataSet.Collection[mItemIndex]) {
            this.mItemName = "???";
            this.mExplain = "아직 발견하지 못했습니다.";
            if(this.mRank == "black"){
                mThisItemIamge.GetComponent<Image>().sprite = mBlackImage;
            }
            else if(this.mRank == "green"){
                mThisItemIamge.GetComponent<Image>().sprite = mGreenImage;
            }
            else if(this.mRank == "pink"){
                mThisItemIamge.GetComponent<Image>().sprite = mPinkImage;
            }
            else{
                mThisItemIamge.GetComponent<Image>().sprite = mPurpleImage;
            }
        }
    }
    public void OnPointerEnter(PointerEventData eventData) {
        mWeaponItemName.text = this.mItemName;
        mWeaponExplain.text = this.mExplain;
        mItemImage.GetComponent<Image>().sprite = mThisItemIamge.GetComponent<Image>().sprite;
    }

}
