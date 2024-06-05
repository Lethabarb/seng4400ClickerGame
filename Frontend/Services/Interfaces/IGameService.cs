namespace Frontend.Services.Interfaces
{
    public interface IGameService
    {
        public string getSessionId();
        public void setName(string name);
        public string getName();
        public Task IncrementScore();
    }
}
