using System;
using System.Collections.Generic;
using UnityEngine;

public class AchiManager : MonoBehaviour
{
    public void CalcAchieve() // TODO: 각종 게임 정보들 받아오기(회복량, 생존시간, 사용한 캐릭터 종류, 사용한 GameManager 객체, 흡입기, 회중시계, 묵주, 돌가면 발견 여부, 오브젝트 파괴 수, 발견한 치킨 수)
    {
        // UpdateAccumulateData();
        // achieve000();
        // achieve001();
        // achieve002();
        // achieve003();
        // achieve004();
        // achieve005();
        // achieve006();
        // achieve007();
        // achieve008();
        // achieve009();
        // achieve010();
        // achieve011();
        // achieve012();
        // achieve013();
        // achieve014();
        // achieve015();
        // achieve016();
        // achieve017();
        // achieve018();
        // achieve019();
        // achieve020();
        // achieve021();
        // achieve022();
        // achieve023();
        // achieve024();
        // achieve025();
        // achieve026();
        // achieve027();
        // achieve028();
        // achieve029();
        // achieve030();
        // achieve031();
        // achieve032();
        // achieve033();
        // achieve034();
        // achieve035();
    }
    public void CalcEtcAchieve() // 컬렉션 달성 개수에 따라 완료되는 업적들, 게임 종료 후 CalcAchieve -> Collection 갱신 -> CalcEtcAchieve 함수 호출하면 됨
    {

    }
    private void UpdateAccumulateData(int totalRecovery, int killCount)
    {

    }
    private void Achieve000(int level)
    {
        if (level >= 5)
        {
            // 날개
        }
    }
    private void Achieve001(int level)
    {
        if (level >= 10)
        {
            // 왕관
        }
    }
    private void Achieve002(Character character)
    {
        if (GameManager.instance.Level >= 20)
        {
            // 2스테이지 해금(화려한 도서관)
        }
    }
    private void Achieve003(float gameTime)
    {
        if (gameTime >= 60)
        {
            // 검은 심장
        }
    }
    private void Achieve004(int characterIndex, float gameTime)
    {
        if (characterIndex == (int)Enums.ECharacterType.Barbarian && gameTime >= 300)
        {
            // 붉은 심장
        }

    }
    private void Achieve005(int gameTime)
    {
        if (gameTime >= 600)
        {
            // 하얀 비둘기
        }
    }
    private void Achieve006(int weaponIndex, int weaponLevel)
    {
        if (weaponIndex == (int)Enums.EWeapon.KingBible && weaponLevel >= 4)
        {
            // 팔 보호대
        }
    }
    private void Achieve007(int weaponIndex, int weaponLevel)
    {
        if (weaponIndex == (int)Enums.EWeapon.SantaWater && weaponLevel >= 4)
        {
            // 촛대
        }
    }
    private void Achieve008(Character character, List<Weapon> weapons)
    {
        int fireWandIndexInWeapons = GameManager.instance.EquipManageSys.TransWeaponIndex[(int)Enums.EWeapon.FireWand];
        if (fireWandIndexInWeapons < 0)
            return;

        if (weapons[fireWandIndexInWeapons].WeaponLevel >= 4)
        {
            // 아르카 라돈나
        }
    }
    private void Achieve009(Character character, List<Weapon> weapons)
    {
        int lightningRingIndexInWeapons = GameManager.instance.EquipManageSys.TransWeaponIndex[(int)Enums.EWeapon.LightningRing];
        if (lightningRingIndexInWeapons < 0)
            return;

        if (weapons[lightningRingIndexInWeapons].WeaponLevel >= 4)
        {
            // 포르타 라돈나
        }
    }
    private void Achieve010(int weaponIndex, int weaponLevel)
    {
        if (weaponIndex == (int)Enums.EWeapon.MagicWand && weaponLevel >= 7)
        {
            // 복제 반지
        }
    }
    private void Achieve011(Character character, List<Weapon> weapons)
    {
        int peachoneIndexInWeapons = GameManager.instance.EquipManageSys.TransWeaponIndex[(int)Enums.EWeapon.Peachone];
        if (peachoneIndexInWeapons < 0)
            return;

        if (weapons[peachoneIndexInWeapons].WeaponLevel >= 7)
        {
            // Ebony Wings 검은 비둘기
        }
    }
    private void Achieve012(Character character, List<Weapon> weapons)
    {
        int garlicIndexInWeapons = GameManager.instance.EquipManageSys.TransWeaponIndex[(int)Enums.EWeapon.Garlic];
        if (garlicIndexInWeapons < 0)
            return;

        if (weapons[garlicIndexInWeapons].WeaponLevel >= 7)
        {
            // 포 랏초
        }
    }
    private void Achieve013(Character character, List<Accessory> accessories)
    {
        int torronaIndexInAcc = GameManager.instance.EquipManageSys.TransWeaponIndex[(int)Enums.EAccessory.TorronaBox];
        if (torronaIndexInAcc < 0)
            return;

        if (accessories[torronaIndexInAcc].AccessoryLevel >= 9)
        {
            // 전능
        }
    }
    private void Achieve014(List<Weapon> weapons)
    {
        if (weapons.Count < 6)
            return;

        foreach (Weapon weapon in weapons)
        {
            if (!weapon.IsEvoluction())
            {
                return;
            }
        }

        // 토로나의 상자
    }
    private void Achieve015(List<Weapon> weapons)
    {
        if (weapons.Count < 6)
            return;

        // 빈 책
    }
    private void Achieve016()
    {
        if (UserInfo.instance.UserDataSet.AccRecovery >= 1000)
        {
            // 시스터 클레리씨
        }
    }
    private void Achieve017(int destroyLightObjectNum)
    {
        if (destroyLightObjectNum >= 20)
        {
            // 불 지팡이
        }
    }
    private void Achieve018(int findChikenNum)
    {
        if (findChikenNum >= 5)
        {
            // 마늘
        }
    }
    private void Achieve019()
    {
        //if (작은 클로버 발견)
        //{
        //   클로버
        //}
    }
    private void Achieve020()
    {
        //if (흡입기 발견)
        //{
        //   매혹구
        //}
    }
    private void Achieve021()
    {
        //if (회중시계 발견)
        //{
        //   시곗바늘
        //}
    }
    private void Achieve022()
    {
        //if (묵주 발견)
        //{
        //   십자가
        //}
    }
    private void Achieve023()
    {
        //if (돌가면 발견)
        //{
        //   돌가면
        //}
    }
    private void Achieve024()
    {
        //if (그림 그리모어 발견)
        //{
        //   진화 목록
        //}
    }
    private void Achieve025()
    {
        //if (매직 뱅어 발견)
        //{
        //   음악 선택
        //}
    }
    private void Achieve026()
    {
        //if (은하수 지도 발견)
        //{
        //   일시정지 메뉴 지도
        //}
    }
    private void Achieve027()
    {
        //if (마녀의 눈물 발견)
        //{
        //   단축 모드
        //}
    }
    private void Achieve028(int killCount)
    {
        if (UserInfo.instance.UserDataSet.AccumulatedKill >= 5000)
        {
            // 번개 반지
        }
    }
    private void Achieve029()
    {
        if (UserInfo.instance.CompleteCollectionCount >= 50)
        {
            // 캐릭터 커스터마이징 해금
        }
    }
    private void Achieve030()
    {
        if (UserInfo.instance.CompleteCollectionCount >= 60 && UserInfo.instance.UserDataSet.Banish < 1)
        {
            UserInfo.instance.UserDataSet.Banish = 1;
        }
    }
    private void Achieve031()
    {
        if (UserInfo.instance.CompleteCollectionCount >= 70 && UserInfo.instance.UserDataSet.Banish < 2)
        {
            UserInfo.instance.UserDataSet.Banish = 2;
        }
    }
    private void Achieve032()
    {
        if (UserInfo.instance.CompleteCollectionCount >= 80 && UserInfo.instance.UserDataSet.Banish < 3)
        {
            UserInfo.instance.UserDataSet.Banish = 3;
        }
    }
    private void Achieve033()
    {
        if (UserInfo.instance.CompleteCollectionCount >= 90 && UserInfo.instance.UserDataSet.Banish < 4)
        {
            UserInfo.instance.UserDataSet.Banish = 4;
        }
    }
    private void Achieve034()
    {
        if (UserInfo.instance.CompleteCollectionCount >= 100 && UserInfo.instance.UserDataSet.Banish < 5)
        {
            UserInfo.instance.UserDataSet.Banish = 5;
        }
    }
    private void Achieve035(Character character, List<Weapon> weapons)
    {
        if (UserInfo.instance.UserDataSet.Achievements[35])
            return;

        int whipIndexInWeapons = GameManager.instance.EquipManageSys.TransWeaponIndex[(int)Enums.EWeapon.Whip];
        if (whipIndexInWeapons < 0)
            return;

        if (weapons[whipIndexInWeapons].IsEvoluction())
        {
            // 500 골드
        }
    }
    private void Achieve036(Character character, List<Weapon> weapons)
    {
        if (UserInfo.instance.UserDataSet.Achievements[36])
            return;

        int magicWandIndexInWeapons = GameManager.instance.EquipManageSys.TransWeaponIndex[(int)Enums.EWeapon.MagicWand];
        if (magicWandIndexInWeapons < 0)
            return;

        if (weapons[magicWandIndexInWeapons].IsEvoluction())
        {
            // 500 골드
        }
    }
    private void Achieve037(Character character, List<Weapon> weapons)
    {
        if (UserInfo.instance.UserDataSet.Achievements[37])
            return;

        int knifeWandIndexInWeapons = GameManager.instance.EquipManageSys.TransWeaponIndex[(int)Enums.EWeapon.Knife];
        if (knifeWandIndexInWeapons < 0)
            return;

        if (weapons[knifeWandIndexInWeapons].IsEvoluction())
        {
            // 500 골드
        }
    }
    private void Achieve038(Character character, List<Weapon> weapons)
    {
        if (UserInfo.instance.UserDataSet.Achievements[38])
            return;

        int santaWaterIndexInWeapons = GameManager.instance.EquipManageSys.TransWeaponIndex[(int)Enums.EWeapon.SantaWater];
        if (santaWaterIndexInWeapons < 0)
            return;

        if (weapons[santaWaterIndexInWeapons].IsEvoluction())
        {
            // 500 골드
        }
    }
    private void Achieve039(Character character, List<Weapon> weapons)
    {
        if (UserInfo.instance.UserDataSet.Achievements[39])
            return;

        int lightningRingWaterIndexInWeapons = GameManager.instance.EquipManageSys.TransWeaponIndex[(int)Enums.EWeapon.LightningRing];
        if (lightningRingWaterIndexInWeapons < 0)
            return;

        if (weapons[lightningRingWaterIndexInWeapons].IsEvoluction())
        {
            // 500 골드
        }
    }
    private void Achieve040(Character character, List<Weapon> weapons)
    {
        if (UserInfo.instance.UserDataSet.Achievements[40])
            return;

        int kingBibleIndexInWeapons = GameManager.instance.EquipManageSys.TransWeaponIndex[(int)Enums.EWeapon.KingBible];
        if (kingBibleIndexInWeapons < 0)
            return;

        if (weapons[kingBibleIndexInWeapons].IsEvoluction())
        {
            // 500 골드
        }
    }
    private void Achieve041(Character character, List<Weapon> weapons)
    {
        if (UserInfo.instance.UserDataSet.Achievements[41])
            return;

        int fireWandIndexInWeapons = GameManager.instance.EquipManageSys.TransWeaponIndex[(int)Enums.EWeapon.FireWand];
        if (fireWandIndexInWeapons < 0)
            return;

        if (weapons[fireWandIndexInWeapons].IsEvoluction())
        {
            // 500 골드
        }
    }
    private void Achieve042(Character character, List<Weapon> weapons)
    {
        if (UserInfo.instance.UserDataSet.Achievements[42])
            return;

        int garlicIndexInWeapons = GameManager.instance.EquipManageSys.TransWeaponIndex[(int)Enums.EWeapon.Garlic];
        if (garlicIndexInWeapons < 0)
            return;

        if (weapons[garlicIndexInWeapons].IsEvoluction())
        {
            // 500 골드
        }
    }

}