namespace CleanSpeakerApp
{
    public interface IRepository
    {
        int SaveSpeaker(Speaker speaker);
    }

    public class FakeRepository : IRepository
    {
        public int SaveSpeaker(Speaker speaker) => new System.Random().Next(1000, 9999);
    }
}
