namespace WaspProject.Model
{
    [Serializable]
    public abstract class AbstractCinemaObject
    {
        // Id для каждого объекта кинотеатра (кинотеатр или фильм)
        public int Id { get; protected set; }

        public AbstractCinemaObject(int id)
        {
            Id = id;
        }

        public override string ToString()
        {
            return $"{Id}";
        }
    }
}
