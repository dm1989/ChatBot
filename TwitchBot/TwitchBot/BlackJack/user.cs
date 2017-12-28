namespace TwitchBot
{
    class User
    {
        public string Nick { get; set; }
        public int Chips { get; set; }
        public bool Playing { get; set;} = false;
        public bool Play { get; set; } = false;
    }
}
