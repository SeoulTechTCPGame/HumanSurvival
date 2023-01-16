using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

using System;
using TMPro;

public class CollectionItemInfo : MonoBehaviour, IPointerEnterHandler
{
    public int weapon;
    public Image itemImage;
    public Image thisItemIamge;
    public Sprite blackImage;
    public Sprite greenImage;
    public Sprite pinkImage;
    public Sprite purpleImage;

    public TMP_Text weaponitemName;
    public TMP_Text weaponExplain;

    string itemName;
    string explain;
    string rank;

    // Start is called before the first frame update
    void Start(){
        switch(weapon){
            case 1:
                this.itemName = "채찍[Whip]";
                this.explain = "좌우로 적을 관통해 공격합니다.(Attacks horizontally, passes through enemies.)";
                this.rank = "black";
                break;
            
            case 2:
                this.itemName = "피눈물[Blood Tear]";
                this.explain = "채찍의 진화형. 치명타 피해를 입히고 HP를 흡수합니다.(Evolved Whip. Can deal critical damage and absorb HP.)";
                this.rank = "black";
                break;

            case 3:
                this.itemName = "마법 지팡이[Magic Wand]";
                this.explain = "가장 가까운 적을 공격합니다.(Fires at the nearest enemy.)";
                this.rank = "black";
                break;

            case 4:
                this.itemName = "신성한 지팡이[Holy Wand]";
                this.explain = "마법 지팡이의 진화형. 지연 없이 발사됩니다.(Evolved Magic Wand. Fires with no delay.)";
                this.rank = "black";
                break;

            case 5:
                this.itemName = "단검[Knife]";
                this.explain = "바라보는 방향으로 단검을 투척합니다.(Fires quickly in the faced direction.)";
                this.rank = "black";
                break;

            case 6:
                this.itemName = "천개의 칼날[Thousand Edge]";
                this.explain = "단검의 진화형. 지연 없이 발사됩니다.(Evolved Knife. Fires with no delay.)";
                this.rank = "black";
                break;

            case 7:
                this.itemName = "도끼[Axe]";
                this.explain = "피해량이 높고 공격 범위가 넓습니다.(High damage, high area scaling.)";
                this.rank = "black";
                break;

            case 8:
                this.itemName = "죽음의 나선[Death Spiral]";
                this.explain = "도끼의 진화형. 적을 관통합니다.(Evolved VS_Item_Axe. Passes through enemies.)";
                this.rank = "black";
                break;

            case 9:
                this.itemName = " 십자가[Cross]";
                this.explain = "가장 가까운 적에게 날아가며 부메랑처럼 돌아옵니다.(Aims at the nearest enemy, has a boomerang effect.)";
                this.rank = "black";
                break;

            case 10:
                this.itemName = "천상의 검[Heaven Sword]";
                this.explain = "십자가의 진화형. 치명타 피해를 줍니다.(Evolved Cross. Can deal critical damage.)";
                this.rank = "black";
                break;

            case 11:
                this.itemName = "성경[King Bible]";
                this.explain = "주변을 회전하며 공격합니다.(Orbits around the character.)";
                this.rank = "black";
                break;

            case 12:
                this.itemName = "불경한 기도문[Unholy Vespers]";
                this.explain = "성경의 진화형. 사라지지 않습니다.(Evolved King Bible. Never ends.)";
                this.rank = "black";
                break;

            case 13:
                this.itemName = "불의 지팡이[Fire Wand]";
                this.explain = "무작위 방향으로 향해 발사되며 큰 피해를 줍니다.(Fires at a random enemy, deals heavy damage.)";
                this.rank = "black";
                break;

            case 14:
                this.itemName = "헬파이어[Hellfire]";
                this.explain = "불의 지팡이의 진화형. 적을 관통합니다.(Evolved Fire Wand. Passes through enemies.)";
                this.rank = "black";
                break;

            case 15:
                this.itemName = "마늘[Garlic]";
                this.explain = "범위 내의 적에게 피해를 줍니다. 적의 넉백, 빙결저항을 감소시킵니다.(Damages nearby enemies. Reduces resistance to knockback and freeze.)";
                this.rank = "black";
                break;

            case 16:
                this.itemName = "영혼 포식자[Soul Eater]";
                this.explain = "마늘의 진화형. 적 처치 시 체력을 흡수하며 체력흡수 시 피해량이 증가합니다.(Evolved Garlic. Steals hearts. Power increases when recovering HP.)";
                this.rank = "black";
                break;

            case 17:
                this.itemName = "성수[Santa Water]";
                this.explain = "피해 구역를 생성합니다.(Generates damaging zones.)";
                this.rank = "black";
                break;

            case 18:
                this.itemName = "정화자[La Borra]";
                this.explain = "성수의 진화형. 피해 구역이 점점 커지며 캐릭터를 따릅니다.(Evolved Santa Water. Damaging zones follow you and grow when they move.)";
                this.rank = "black";
                break;

            case 19:
                this.itemName = "룬 트레이서[RuneTracer]";
                this.explain = "적을 관통하며 튕겨져 나옵니다.(Passes through enemies, bounces around.)";
                this.rank = "black";
                break;

            case 20:
                this.itemName = "절멸[NO Future]";
                this.explain = "룬 트레이서 진화형. 적에게 반사 대미지를 주며 튕겨져 나올 때 폭파 피해를 줍니다.(Evolved Runetracer. Explodes when bouncing and in retaliation.)";
                this.rank = "black";
                break;

            case 21:
                this.itemName = "번개 반지[Lightning Ring]";
                this.explain = "무작위 적에게 번개를 내려칩니다.(Strikes at random enemies.)";
                this.rank = "black";
                break;

            case 22:
                this.itemName = "번개 고리[Thunder Loop]";
                this.explain = "번개 반지의 진화형. 투사체가 2회 공격합니다.(Evolved Lightning Ring. Projectiles strike twice.)";
                this.rank = "black";
                break;

            case 23:
                this.itemName = "오망성[Pentagram]";
                this.explain = "화면 안의 모든 것을 제거합니다.(Erases everything in sight.)";
                this.rank = "black";
                break;

            case 24:
                this.itemName = "매혹의 달[Gorgeous Moon]";
                this.explain = "오망성의 진화형. 추가 보석을 생성하고 처치한 적의 보석을 흡수합니다.(Evolved Pentagram. Generates extra gems and gathers all of them.)";
                this.rank = "black";
                break;

            case 25:
                this.itemName = "하얀 비둘기[Peachone]";
                this.explain = "원형의 구역을 폭격합니다.(Bombards in a circular area.)";
                this.rank = "black";
                break;

            case 26:
                this.itemName = "검은 비둘기[Ebony Wings]";
                this.explain = "원형의 구역을 폭격합니다.(Bombards in a circular area.)";
                this.rank = "black";
                break;

            case 27:
                this.itemName = "파괴자[Vandalier]";
                this.explain = "하얀 비둘기와 검은 비둘기의 진화형.(Union of Ebony Wings and Peachone.)";
                this.rank = "black";
                break;

            case 28:
                this.itemName = "피에라 데 투펠로[Phiera Der Tuphello]";
                this.explain = "고정된 네 방향으로 투사체를 빠르게 발사합니다.(Fires quickly in four fixed directions.)";
                this.rank = "black";
                break;

            case 29:
                this.itemName = "에잇 더 스페로우[Eight the Sparrow]";
                this.explain = "고정된 네 방향으로 투사체를 빠르게 발사합니다.(Fires quickly in four fixed directions.)";
                this.rank = "black";
                break;

            case 30:
                this.itemName = "피에라지[Phieraggi]";
                this.explain = "피에라 데 투펠로와 에잇 더 스패로우의 진화형. 부활횟수만큼 추가 투사체를 얻습니다.(Union of Phiera Der Tuphello and Eight The Sparrow. Scales with Revivals.)";
                this.rank = "black";
                break;

            case 31:
                this.itemName = "마녀의 고양이[Gatti Amari]";
                this.explain = "변덕이 심한 발사체를 소환합니다. 픽업과 소통할 수도 있습니다. (Summons capricious projectiles. Might interact with pickups.)";
                this.rank = "black";
                break;

            case 32:
                this.itemName = "흉포한 굶주림[Vicious Hunger]";
                this.explain = "마녀의 고양이의 진화형. 무엇이든지 금으로 바꿉니다.(Evolved Gatti Amari. Might turn anything into gold.)";
                this.rank = "black";
                break;

            case 33:
                this.itemName = "마나의 노래[Song of Mana]";
                this.explain = "수직으로 공격하며 적을 관통합니다.(Attacks vertically, passes through enemies.)";
                this.rank = "black";
                break;

            case 34:
                this.itemName = "마력의 결정체[Mannajja]";
                this.explain = "마나의 노래의 진화형. 적을 느려지게 만듭니다.(Evolved Song of Mana. Might slow enemies down.)";
                this.rank = "black";
                break;

            case 35:
                this.itemName = "검은 드릴[Shadow Pinion]";
                this.explain = "이동 시 피해를 주는 구역을 만들며, 정지 시 공격합니다.(Generates damaging zones when moving, strikes when stopping.)";
                this.rank = "black";
                break;

            case 36:
                this.itemName = "발키리 터너[Valkyrie Turner]";
                this.explain = "검은 드릴의 진화형. 더 크게, 더 길게, 더 강력하게.(Evolved Shadow Pinion. Bigger, longer, faster, stronger.)";
                this.rank = "black";
                break;

            case 37:
                this.itemName = "시곗바늘[Clock Lancet]";
                this.explain = "적들을 잠시 얼려 움직이지 못하게 합니다.(Chance to freeze enemies in time.)";
                this.rank = "black";
                break;

            case 38:
                this.itemName = "무한회랑[Infinite Corridor]";
                this.explain = "시곗바늘 진화형. 적의 체력 절반을 감소시킵니다.(Evolved Clock Lancet. Halves enemies health.)";
                this.rank = "black";
                break;

            case 39:
                this.itemName = "월계수[Laurel]";
                this.explain = "피격 피해를 막아주는 방어막을 생성합니다. 손상된 방어막은 잠시 후 재생성됩니다.(Shields from damage while active.)";
                this.rank = "black";
                break;

            case 40:
                this.itemName = "진홍의 망토[Crimson Shroud]";
                this.explain = "월계수 진화형. 받는 피해를 최대 10으로 감소시키며 보호막 감소 시 반사 피해를 줍니다.(Evolved Laurel. Caps incoming damage at 10. Retaliates when losing charges.)";
                this.rank = "black";
                break;

            case 41:
                this.itemName = "성스러운 바람[Vento Sacro]";
                this.explain = "지속적으로 움직이면 더욱 강한 피해를 줍니다. 치명타 피해를 줄 수 있습니다.(Stronger with continuous movement. Can deal critical damage.)";
                this.rank = "black";
                break;

            case 42:
                this.itemName = "붉은 장미 덩쿨[Fuwalafuwaloo]";
                this.explain = "성스러운 바람과 피눈물의 진화형. 치명타 공격시 폭발 가능성이 있습니다.(Union of Vento Sacro and Bloody Tear. Critical hits might generate explosions.)";
                this.rank = "black";
                break;

            case 43:
                this.itemName = "뼈[bone]";
                this.explain = "튕겨나오는 투사체를 던집니다.(Throws a bouncing projectile.)";
                this.rank = "black";
                break;

            case 44:
                this.itemName = "체리폭탄[cherry Bomb]";
                this.explain = "튕겨나오며 일정 확률로 폭발하는 투사체를 던집니다.(Throws a bouncing projectile that explodes after some time.)";
                this.rank = "black";
                break;

            case 45:
                this.itemName = "광산 수레[Carrello]";
                this.explain = "수레를 밀어 적을 공격합니다. 투사체의 수가 많아질수록 여러번 튕겨져 나옵니다.(Throws a bouncing projectile. Number of bounces affected by Amount.)";
                this.rank = "black";
                break;

            case 46:
                this.itemName = "천상의 꽃[Celestial Dusting]";
                this.explain = "튕겨지는 투사체를 발사합니다. 이동 시 무기의 쿨타임 감소.(Throws a bouncing projectile. Cooldown reduces when moving.)";
                this.rank = "black";
                break;

            case 47:
                this.itemName = "라 로바[La Robba]";
                this.explain = "통통 튀는 투사체를 소환합니다.(Generates bouncing projectiles.)";
                this.rank = "black";
                break;

            case 48:
                this.itemName = "위대한 기념제[Greatest Jubilee]";
                this.explain = "광원을 소환할 확률이 있습니다.(Has a chance to summon light sources)";
                this.rank = "black";
                break;

            case 49:
                this.itemName = "팔찌 1[Bracelet]";
                this.explain = "무작위 적 1명에게 3개의 투사체를 발사합니다.(Fires three projectiles at a random enemy.)";
                this.rank = "black";
                break;

            case 50:
                this.itemName = "팔찌 2[Bi-Bracelet]";
                this.explain = "무작위 적 1명에게 3개의 투사체를 발사합니다.(Fires three projectiles at a random enemy.)";
                this.rank = "black";
                break;

            case 51:
                this.itemName = "팔찌 3[Tri-Bracelet]";
                this.explain = "무작위 적 1명에게 3개의 투사체를 발사합니다.(Fires three projectiles at a random enemy.)";
                this.rank = "black";
                break;

            case 52:
                this.itemName = "사탕상자[Candybox]";
                this.explain = "잠금 해체한 기본 무기 중 원하는 무기를 하나 선택할 수 있습니다.(Allows you to choose any unlcoked base weapon.)";
                this.rank = "black";
                break;

            case 53:
                this.itemName = "슈퍼 사탕상자 2 터보[Super Candybox II Turbo]";
                this.explain = "사탕상자의 선물입니다. 고급 무기 중 선택할 수 있습니다.(Gift of Candybox. Allows to choose among a selection of advanced weapons.)";
                this.rank = "black";
                break;

            case 54:
                this.itemName = "승리의 검[Victory Sword]";
                this.explain = "가장 가까운 적에게 콤보 공격을 가합니다. 반격이 가능합니다.(Strikes with a combo attack at the nearest enemy. Retaliates.)";
                this.rank = "black";
                break;

            case 55:
                this.itemName = "주요한 해결책[Sole Solution]";
                this.explain = "승리의 검이 주는 선물입니다. 더 많은 적을 처치할수록 더 강해집니다.(Gift of Victory Sword. The more enemies are defeated, the stronger it grows.)";
                this.rank = "black";
                break;

            case 56:
                this.itemName = "잘못된 주문의 불꽃[Flames of Misspell]";
                this.explain = "불꽃의 원뿔을 방출합니다.(Emits cones of flames.)";
                this.rank = "black";
                break;

            case 57:
                this.itemName = "무슬림의 재[Ashes of Muspell]";
                this.explain = "진화한 잘못된 주문의 불꽃입니다. 더 많은 적을 처치할수록 더 강해집니다.(Evolved Flames of Misspell. The more enemies are defeated, the stronger it grows.)";
                this.rank = "black";
                break;

            case 58:
                this.itemName = "시금치[Spinach]";
                this.explain = "공격력이 10% 증가한다.";
                this.rank = "black";
                break;

            case 59:
                this.itemName = "갑옷[Armor]";
                this.explain = "방어력이 1, 반격 대미지가 10% 증가한다.";
                this.rank = "black";
                break;

            case 60:
                this.itemName = "검은 심장[Hollow Heart]";
                this.explain = "최대 체력이 20% 증가한다.";
                this.rank = "black";
                break;

            case 61:
                this.itemName = "붉은 심장[Pummarola]";
                this.explain = "매 초 0.2 의 체력이 회복된다.";
                this.rank = "black";
                break;

            case 62:
                this.itemName = "빈 책[Empty Tome]";
                this.explain = "쿨타임이 8% 감소한다.";
                this.rank = "black";
                break;

            case 63:
                this.itemName = "촛대[Candelabrador]";
                this.explain = "공격범위가 10% 증가한다.";
                this.rank = "black";
                break;

            case 64:
                this.itemName = "팔 보호대[Barcer]";
                this.explain = "투사체 속도가 10% 증가한다.";
                this.rank = "black";
                break;

            case 65:
                this.itemName = "주문속박기[Spellbinder]";
                this.explain = "지속시간이 10% 증가한다.";
                this.rank = "black";
                break;

            case 66:
                this.itemName = "복제 반지[Duplicator]";
                this.explain = "투사체 수가 1개 증가한다.";
                this.rank = "black";
                break;

            case 67:
                this.itemName = "날개[Wings]";
                this.explain = "이동속도가 10% 상승한다.";
                this.rank = "black";
                break;

            case 68:
                this.itemName = "매혹구[Attractorb]";
                this.explain = "아이템 획득 범위가 증가한다.";
                this.rank = "black";
                break;

            case 69:
                this.itemName = "클로버[Clover]";
                this.explain = "행운이 10% 증가한다.";
                this.rank = "black";
                break;

            case 70:
                this.itemName = "왕관[Corwn]";
                this.explain = "경험치 획득량이 8% 증가한다.";
                this.rank = "black";
                break;

            case 71:
                this.itemName = "돌가면[Stone Mask]";
                this.explain = "골드 획득량이 10% 증가한다.";
                this.rank = "black";
                break;

            case 72:
                this.itemName = "미치광이의 두개골[Skull O'Maniac]";
                this.explain = "저주(적의 속도, 체력, 수량, 빈도)가 10%씩 증가한다.";
                this.rank = "black";
                break;

            case 73:
                this.itemName = "티라미수[Tiragisu]";
                this.explain = "부활 횟수가 추가된다.";
                this.rank = "black";
                break;

            case 74:
                this.itemName = "토로나의 상자[Torrona's Box]";
                this.explain = "피해량, 투사체 속도, 지속시간, 공격범위가 증가한다. 9레벨에 저주가 증가한다.";
                this.rank = "black";
                break;

            case 75:
                this.itemName = "백조자리[Cygnus]";
                this.explain = "하얀 비둘기를 쌍둥이 자리 아르카나를 사용해 복제";
                this.rank = "black";
                break;

            case 76:
                this.itemName = "불사조[Zhar Ptyrtsia]";
                this.explain = "검은 비둘기를 쌍둥이 자리 아르카나를 사용해 복제";
                this.rank = "black";
                break;

            case 77:
                this.itemName = "적색근육[Red Muscle]";
                this.explain = "피에라 데 투펠로를 쌍둥이 자리 아르카나를 사용해 복제";
                this.rank = "black";
                break;

            case 78:
                this.itemName = "두번의 시간[Twice Upon a time]";
                this.explain = "에잇 더 스패로우를 쌍둥이 자리 아르카나를 사용해 복제";
                this.rank = "black";
                break;

            case 79:
                this.itemName = "무리 파괴자[Flock Destroyer]";
                this.explain = "마녀의 고양이를 쌍둥이 자리 아르카나를 사용해 복제";
                this.rank = "black";
                break;

            case 80:
                this.itemName = "은반지[Silver Ring]";
                this.explain = "지속시간과 범위 5% 증가";
                this.rank = "black";
                break;

            case 81:
                this.itemName = "금반지[Gold Ring]";
                this.explain = "저주 5% 증가";
                this.rank = "black";
                break;

            case 82:
                this.itemName = "좌 메탈리오[Metaglio Right]";
                this.explain = "어둠의 힘으로 사용자를 보호합니다.";
                this.rank = "black";
                break;

            case 83:
                this.itemName = "우 메탈리오[Metaglio Left]";
                this.explain = "어둠의 힘으로 사용자를 저주합니다.";
                this.rank = "black";
                break;

            case 84:
                this.itemName = "경험치 보석[Experience Gem]";
                this.explain = "경험치를 증가시킵니다.";
                this.rank = "green";
                break;

            case 85:
                this.itemName = "금화[Gold Coin]";
                this.explain = "획득시 1골드를 획득합니다.";
                this.rank = "green";
                break;

            case 86:
                this.itemName = "금화 주머니[Coin Bag]";
                this.explain = "획득시 10골드를 획득합니다.";
                this.rank = "green";
                break;

            case 87:
                this.itemName = "거대한 금화 주머니[Rich Coin Bag]";
                this.explain = "획득시 100골드를 획득합니다.";
                this.rank = "green";
                break;

            case 88:
                this.itemName = "묵주[Rosary]";
                this.explain = "시야 내 모든 적을 파괴합니다.";
                this.rank = "green";
                break;

            case 89:
                this.itemName = "은두자 프리타[Nduja Fritta]";
                this.explain = "바라보는 방향으로 화염을 방사합니다. 플레이어 스탯에 영향을 받습니다.";
                this.rank = "green";
                break;

            case 90:
                this.itemName = "회중시계[Orologion]";
                this.explain = "10초동안 모든 적을 얼려버립니다.";
                this.rank = "green";
                break;

            case 91:
                this.itemName = "흡입기[Vaccum]";
                this.explain = "지면의 모든 경험치 보석을 모아줍니다.";
                this.rank = "green";
                break;

            case 92:
                this.itemName = "치킨[Floor Chicken]";
                this.explain = "체력 30을 회복합니다.";
                this.rank = "green";
                break;

            case 93:
                this.itemName = "금박 클로버[Gilded Clover]";
                this.explain = "땅에 떨어진 모든 금화를 흡수하고 황금의 피버타임을 가집니다.";
                this.rank = "green";
                break;

            case 94:
                this.itemName = "작은 클로버[Little Clover]";
                this.explain = "획득시 행운이 10% 증가합니다.";
                this.rank = "green";
                break;

            case 95:
                this.itemName = "보물 상자[Treasure Chest]";
                this.explain = "골드와 무작위 파워 업을 획득합니다. 강력한 적에게서만 획득 할 수 있습니다.";
                this.rank = "green";
                break;

            case 96:
                this.itemName = "엄숙한 마도서[Grim cruniure]";
                this.explain = "일시 정지 메뉴에서 발견한 무기 진화, 조합법을 영구적으로 확인할 수 있습니다.(Permanently allows to peek at discovered weapon evolutions and unions from the pause menu.)";
                this.rank = "pink";
                break;

            case 97:
                this.itemName = "괴물 도감[Ars Gouda]";
                this.explain = "메인 메뉴에서 처치한 적의 목록을 영구적으로 확인할 수 있습니다.(Permanently allows to access the list of defeated enemies from the main menu.)";
                this.rank = "pink";
                break;

            case 98:
                this.itemName = "은하수 지도[Milky Way Map]";
                this.explain = "일시정지 화면에서 지도를 활성화하여 필드의 아이템 위치를 표시합니다.(Permanently enables the map in the pause menu.)";
                this.rank = "pink";
                break;

            case 99:
                this.itemName = "매직 뱅거[Magic Banger]";
                this.explain = "스테이지 선택에서 음악을 영구적으로 변경할 수 있습니다.(Permanently allows to change music in Stage Selection.)";
                this.rank = "pink";
                break;

            case 100:
                this.itemName = "마녀의 눈물[Sorceress' Tears]";
                this.explain = "스테이지 선택에서 게임을 영구적으로 가속할 수 있습니다.(Permanently allows to speed up time in Stage Selection.)";
                this.rank = "pink";
                break;

            case 101:
                this.itemName = "유리 가면[Glass Vizard]";
                this.explain = "모든 스테이지에서 상인을 소환합니다.(Summons the merchant in all stages.)";
                this.rank = "pink";
                break;

            case 102:
                this.itemName = "황금알[Golden Egg]";
                this.explain = "현재 플레이 중인 캐릭터의 무작위 능력치 한 가지를 영구적으로 소폭 증가시킵니다.";
                this.rank = "pink";
                break;

            case 103:
                this.itemName = "코주부 안경[Mindbender]";
                this.explain = "캐릭터의 외형을 변경 가능하게 해줍니다. 또한 게임 시작 전에 소지 가능한 최대 무기의 수량을 지정할 수 있습니다.(Permanently allows to change character appearance (where applicable) and maximum weapon loadout.)";
                this.rank = "pink";
                break;

            case 104:
                this.itemName = "노란색 신호[Yellow Sign]";
                this.explain = "모든 스테이지에서 숨겨진 아이템을 영구적으로 감지할 수 있습니다.(Permanently allows to detect hidden items in all stages.)";
                this.rank = "pink";
                break;

            case 105:
                this.itemName = "금지된 모르베인의 두루마리[Forbidden Scrolls of Morbane]";
                this.explain = "주문을 시전하고 메인 메뉴에서 비밀 목록을 확인할 수 있습니다.(Permanently allow you to cast spells and to access the list of secrets from the main menu.)";
                this.rank = "pink";
                break;

            case 106:
                this.itemName = "위대한 복음[Great Gospel]";
                this.explain = "영구적으로 무기의 레벨을 한계 이상으로 올릴 수 있습니다. 스테이지 선택에서 활성화할 수 있습니다.(Permanently allows to level up weapons beyond their limit. Can be enabled in stage selection.)";
                this.rank = "pink";
                break;

            case 107:
                this.itemName = "그라시아의 거울";
                this.explain = "영구적으로 원하는 스테이지의 더 어려운 버전을 이용할 수 있습니다.(Permanently allows to access a harder version of any stage.)";
                this.rank = "pink";
                break;

            case 108:
                this.itemName = "일곱 번째 나팔";
                this.explain = "영구적으로 모든 사신을 비활성화하고 무한으로 전투할 수 있도록 합니다.(Permanently allows to disable all reapers and fight endlessly.)";
                this.rank = "pink";
                break;

            case 109:
                this.itemName = "란도마조[Randomazzo]";
                this.explain = "아르카나의 잠금 해제 및 활성화.(Enables the unlocking and the activation of Arcanas.)";
                this.rank = "pink";
                break;

            case 110:
                this.itemName = "게임 킬러[Game Killer]";
                this.explain = "마그나 예배당에서 최종 보스 '엔더' 처치";
                this.rank = "purple";
                break;

            case 111:
                this.itemName = "쌍둥이 자리[Gemini]";
                this.explain = "푸냘라 레벨 99 달성";
                this.rank = "purple";
                break;

            case 112:
                this.itemName = "황혼의 진혼곡[Twilght Requiem]";
                this.explain = "돔마리오 레벨 99 달성";
                this.rank = "purple";
                break;

            case 113:
                this.itemName = "공주의 비련[Tragic Princess]";
                this.explain = "포르타 레벨 99 달성";
                this.rank = "purple";
                break;

            case 114:
                this.itemName = "각성[Awake]";
                this.explain = "크로치 레벨 99 달성";
                this.rank = "purple";
                break;

            case 115:
                this.itemName = "혼돈이 가득한 어두운 밤[Chaos in the Dark Night]";
                this.explain = "지오반나 레벨 99 달성";
                this.rank = "purple";
                break;

            case 116:
                this.itemName = "치유의 사라반드[Sarabande Of Healing]";
                this.explain = "란도마조 습득";
                this.rank = "purple";
                break;

            case 117:
                this.itemName = "푸른 강철의 의지[Iron Blue Will]";
                this.explain = "젠나로 레벨 99 달성";
                this.rank = "purple";
                break;

            case 118:
                this.itemName = "광기의 그루브[Mad Groove]";
                this.explain = "광기의 숲에서 31분 생존";
                this.rank = "purple";
                break;

            case 119:
                this.itemName = "신성한 혈통[Divine Bloodline]";
                this.explain = "클레리씨 수녀 레벨 99 달성";
                this.rank = "purple";
                break;

            case 120:
                this.itemName = "근원[Beginning]";
                this.explain = "안토니오 레벨 99 달성";
                this.rank = "purple";
                break;

            case 121:
                this.itemName = "진주의 왈츠[Waltz of Pearls]";
                this.explain = "이멜다 레벨 99 달성";
                this.rank = "purple";
                break;

            case 122:
                this.itemName = "한도초과[Out of Bounds]";
                this.explain = "갈로의 탑 31분 생존";
                this.rank = "purple";
                break;

            case 123:
                this.itemName = "사악한 계절[Wicken Season]";
                this.explain = "크리스틴 레벨 99 달성";
                this.rank = "purple";
                break;

            case 124:
                this.itemName = "수정 감옥[Jail of Crystal]";
                this.explain = "파스콸리나 레벨 99 달성";
                this.rank = "purple";
                break;

            case 125:
                this.itemName = "황금 디스코[Disco of Gold]";
                this.explain = "화려한 도서관 31분 생존";
                this.rank = "purple";
                break;

            case 126:
                this.itemName = "절단[Slash]";
                this.explain = "라마 레벨 99 달성";
                this.rank = "purple";
                break;

            case 127:
                this.itemName = "잃어버린 그림[Lost & Found Painting]";
                this.explain = "포페아 레벨 99 달성";
                this.rank = "purple";
                break;

            case 128:
                this.itemName = "환상의 춤[Boogaloo of Illusions]";
                this.explain = "콘체타 레벨 99 달성";
                this.rank = "purple";
                break;

            case 129:
                this.itemName = "불의 심장[Heart of Fire]";
                this.explain = "아르카 레벨 99 달성";
                this.rank = "purple";
                break;

            case 130:
                this.itemName = "고요한 옛 성역[Silent old Sanctuary]";
                this.explain = "유제품 공장에서 31분 생존";
                this.rank = "purple";
                break;

            case 131:
                this.itemName = "블러드 아스트로노미아[Blood Astronmia]";
                this.explain = "포 랏초 99 레벨 달성";
                this.rank = "purple";
                break;
        }
        
        if (!UserInfo.userItem[weapon-1]) {
            this.itemName = "???";
            this.explain = "아직 발견하지 못했습니다.";
            if(this.rank == "black"){
                thisItemIamge.GetComponent<Image>().sprite = blackImage;
            }
            else if(this.rank == "green"){
                thisItemIamge.GetComponent<Image>().sprite = greenImage;
            }
            else if(this.rank == "pink"){
                thisItemIamge.GetComponent<Image>().sprite = pinkImage;
            }
            else{
                thisItemIamge.GetComponent<Image>().sprite = purpleImage;
            }
        }
    }

    // Update is called once per frame
    void Update(){
    }

    public void OnPointerEnter(PointerEventData eventData) {
        weaponitemName.text = this.itemName;
        weaponExplain.text = this.explain;
        itemImage.GetComponent<Image>().sprite = thisItemIamge.GetComponent<Image>().sprite;
    }

}
