namespace Player
{
    public class PlayerScore
    {
        private int score;
        public int Score => score;
        
        public void ChangeScore(int changeBy)
        {
            score += changeBy;
        }
    }
}