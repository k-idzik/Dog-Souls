using UnityEngine;
using System.Collections;
namespace UnityStandardAssets._2D
{
    public class AxeAnimation : Weapon
    {

        public PolygonCollider2D box1;
        public PolygonCollider2D box2;
        private PolygonCollider2D localCol;


        public AxeAnimation(float cooldown) : base(cooldown)
        {
        }

        public void Awake()
        {
            localCol = gameObject.AddComponent<PolygonCollider2D>();
            localCol.isTrigger = true;
            localCol.pathCount = 0;
        }

        public void DestroySelf()
        {
            Destroy(this.gameObject);
        }

        public void SetBox1Enabled()
        {
            localCol.SetPath(0, box1.GetPath(0));
        }

        public void SetBox2Enabled()
        {
            localCol.SetPath(0, box2.GetPath(0));
        }

        public void SetBox1Disabled()
        {
            localCol.pathCount = 0;
        }

        public void SetBox2Disabled()
        {
            localCol.pathCount = 0;
        }

        public override Vector3 GetSpawnPosition(PlatformerCharacter2D character)
        {
            Vector3 position = character.transform.FindChild("AxeSpawn").transform.position;
            //if (Input.mousePosition.x > (character.cam.WorldToViewportPoint(character.gameObject.transform.position).x) * Screen.width)
            //{
            //}
            //else
            //{
            //    position.x -= (character.transform.FindChild("AxeSpawn").transform.position.x - character.transform.FindChild("Center").transform.position.x) * 2;
            //}
            return position;
        }

        public override Vector3 ControllerGetSpawnPosition(PlatformerCharacter2D character) //Weapon spawning for the controller
        {
            Vector3 position = character.transform.FindChild("AxeSpawn").transform.position;

            if (Input.GetAxis("AttackStick") < -.2) //Attacking left
            {
                position.x -= (character.transform.FindChild("AxeSpawn").transform.position.x - character.transform.FindChild("Center").transform.position.x) * 2;
            }

            return position;
        }
    }
}

