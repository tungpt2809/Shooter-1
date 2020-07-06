namespace Player
{
    public interface IPlayer
    {
        float speed { get; }

        float heath { get; }

        void PlayerShot();

        void Rotation();

        void Movement();

        void UpdateHeath();
    }
}