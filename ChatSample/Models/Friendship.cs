namespace poruchTv.Models
{
    public class Friendship
    {
        public int Id { get; set; }
        public string FirstUserName { get; set; }

        public string SecondUserName { get; set; }

        public bool IsConfirm { get; set; }
    }
}