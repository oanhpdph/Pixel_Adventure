using UnityEngine;

namespace Assets._Data._Scripts.Common
{
    public class AddForce
    {
        public void Force(Rigidbody2D rigidbody2D, Vector2 Force)
        {
            rigidbody2D.AddForce(Force, ForceMode2D.Impulse);
        }
    }
}