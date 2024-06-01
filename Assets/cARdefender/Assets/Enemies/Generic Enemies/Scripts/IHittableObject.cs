namespace cARdefender.Assets.Enemies.Generic_Enemies.Scripts
{
    /// <summary>
    /// Interface used by LaserShot_Player to deal damage. Implement in an object you want to be able to damage with the player's laser gun. 
    /// </summary>


    public interface IHittableObject
    {
        public void OnObjectHitted();
    }
}