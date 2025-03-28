using System;

namespace Assets.Scripts.RunScripts.Interfaces
{
    public interface IDamagable
    {
        public void TakeDMG();
        public void Death();

        event EventHandler death;
    }
}
