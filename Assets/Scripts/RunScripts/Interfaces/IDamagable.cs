using System;

namespace Assets.Scripts.RunScripts.Interfaces
{
    public interface IDamagable
    {
        public void TakeDMG(int dmg);
        public void Death();
    }
}
