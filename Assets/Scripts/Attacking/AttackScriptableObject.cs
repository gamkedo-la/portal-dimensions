using UnityEngine;

[CreateAssetMenu(fileName = "Attack Configuration", menuName = "ScriptableObject/Attack Configuration")]
public class AttackScriptableObject : ScriptableObject
{
    public bool isRanged = false;
    public int damage = 5;
    public float attackRadius = 1.5f;
    public float attackDelay;

    public string attackSound;
    public string hurtSound;
    public string killedSound;
    public string healSound;

    [SerializeField] bool isEnemy = false;

    //ranged configs for sky pirates
    public Bullet bulletPrefab;
    public Vector3 bulletSpawnOffset = new Vector3(0, 1, 0);
    public LayerMask lineOfSightLayers;

    public void Attacker(IDamageable character)
    {
        if(isEnemy)
        {
            EnemyBase enemy = character as EnemyBase;
            SetUpEnemy(enemy);
        }
        else
        {
            Player player = character as Player;
            SetUpPlayer(player);
        }
    }

    public void SetUpEnemy(EnemyBase enemy)
    {
        (enemy.attackRadius.sphereCollider == null ? enemy.attackRadius.GetComponent<SphereCollider>() : 
            enemy.attackRadius.sphereCollider).radius = attackRadius;
        enemy.attackRadius.attackDelay = attackDelay;
        enemy.attackRadius.damage = damage;
        enemy.attackSound = attackSound;
        enemy.hurtSound = hurtSound;
        enemy.killedSound = killedSound;
        enemy.healSound = healSound;

        if(isRanged)
        {
            RangedAttackRadius rangedAttackRadius = enemy.attackRadius.GetComponent<RangedAttackRadius>();

            rangedAttackRadius.bulletPrefab = bulletPrefab;
            rangedAttackRadius.bulletSpawnOffset = bulletSpawnOffset;
            rangedAttackRadius.mask = lineOfSightLayers;

            rangedAttackRadius.CreateBulletPool();
        }
    }

    public void SetUpPlayer(Player player)
    {
        (player.attackRadius.sphereCollider == null ? player.attackRadius.GetComponent<SphereCollider>() :
            player.attackRadius.sphereCollider).radius = attackRadius;
        player.attackRadius.attackDelay = attackDelay;
        player.attackRadius.damage = damage;
        player.attackSound = attackSound;
        player.hurtSound = hurtSound;
        player.killedSound = killedSound;
        player.healSound = healSound;
    }

    
}
