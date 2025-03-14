using UnityEngine;

namespace Assets._Data._Scripts.Common
{
    public class Destruction : MonoBehaviour
    {
        [SerializeField] private GameObject _brokenObject;
        [SerializeField] private Vector2 force;
        [SerializeField] private float timeDestroy = 2f;

        private SpriteRenderer spriteRenderer;
        public AddForce addForce;

        // Use this for initialization
        void Start()
        {
            addForce = new AddForce();
            spriteRenderer = GetComponent<SpriteRenderer>();
        }

        // Update is called once per frame
        public void ObjectBroken()
        {
            Vector2 direction;

            spriteRenderer.enabled = false;
            GetComponent<Collider2D>().enabled = false;
            GameObject _instantiateObject = InstantiateObject(_brokenObject, transform.position, transform);// spawn new object
            foreach (Transform shard in _instantiateObject.transform)
            {
                direction.x = Random.Range(-1, 1);
                direction.y = Random.Range(-1, 1);
                addForce.Force(shard.gameObject.GetComponent<Rigidbody2D>(), force * direction);// add force for every shard object
            }
            DestroyObject(this.transform.gameObject);
        }
        public GameObject InstantiateObject(GameObject originalObject, Vector2 position, Transform parent)
        {
            return Instantiate(originalObject, position, Quaternion.identity, parent);
        }
        public void DestroyObject(GameObject gameObject)
        {
            Destroy(gameObject, timeDestroy);
        }
    }
}