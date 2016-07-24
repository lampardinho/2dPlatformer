using UnityEngine;

namespace Assets.Scripts
{
    public class SoundManager : SingletonBehaviour<SoundManager>
    {
        [SerializeField] private AudioClip _explosionSound;

        [SerializeField] private AudioClip _launchMissleSound;

        public void MakeLaunchMissleSound()
        {
            AudioSource.PlayClipAtPoint(_launchMissleSound, Vector2.zero);
        }

        public void MakeExplosionSound()
        {
            AudioSource.PlayClipAtPoint(_explosionSound, Vector2.zero);
        }
    }
}