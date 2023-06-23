using UnityEngine;

public class DestructibleObject : MonoBehaviour,IDamageable
{
    
    public enum EDestructibleType { TreasureBox, Bottle}
    public EDestructibleType Type;

    public void TakeDamage(float damage, int weaponIndex)
    {
        //position 위치에 Drop 생성
        switch (Type)
        {
            case EDestructibleType.TreasureBox:
                //보물상자 획득 시 호출할 함수
                Debug.Log("보물상자 드롭");
                break;
            case EDestructibleType.Bottle:
                
                gameObject.GetComponent<DropSystem>().OnDrop(gameObject.transform.position);
                break;
        }
        Destroy(gameObject);
    }
}