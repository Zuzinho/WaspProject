namespace WaspProject.Model
{
    [Serializable]
    public class Cinema : AbstractCinemaObject
    {
        // Имя кинотеатра
        public string Name { get; private set; }
        // Адрес кинотеатра
        public string Address { get; private set; }

        public Cinema(int id, string name, string address): base(id)
        {
            Name = name;
            Address = address;
        }

        public override string ToString()
        {
            return $"{base.ToString()} {Name} {Address}";
        }
    }
}
