using UnityEngine;

namespace _Game_Assets.Scripts.Bumping
{
    public interface IBumpable
    {
        public void ReceiveBump(Vector3 direction, float force);
    }
}