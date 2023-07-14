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
               
                break;
            case EDestructibleType.Bottle:
                gameObject.GetComponent<DropSystem>().OnDrop(gameObject.transform.position);
                break;
        }
        Destroy(gameObject);
    }
}