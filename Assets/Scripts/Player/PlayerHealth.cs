namespace Player
{
    public class PlayerHealth 
    {
        private int _lives;

        public int Lives => _lives;

        public PlayerHealth(int initialLives)
        {
            _lives = initialLives;
        }
        
        public void LoseLife()
        {
            _lives--;
        }

        public void GainLife()
        {
            _lives++;
        }
    }
}