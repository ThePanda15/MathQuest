public class LivingBeing
{
    public int hp { get { return _hp; } }
    public bool isDead { get { return hp <= 0; } }
    private int _hp = 20;

    public LivingBeing(int maxHealth)
    {
        _hp = maxHealth;
    }
    public void Damage(int hp)
    {
        _hp -= hp;
        if (_hp < 0) _hp = 0;
    }
}
