using System.Collections.ObjectModel;

namespace WaspProject.Model
{
    [Serializable]
    public class Film : AbstractCinemaObject
    {
        // Название фильма
        public string Name { get; private set; }
        //Жанр фильма (все в enum)
        public string Genre { get;private set; }
        // Описание фильма
        public string Description { get; private set; }
        //Id кинотеатра, в котором идет данный фильм
        public int CinemaId { get; private set; }
        // Список сеансов
        public ObservableCollection<Session> Sessions { get; private set; }
        // Путь к аватрке фильма (создается автоматически)
        public string AvatarPath { get; private set; }

        public Film(int id, string name, string genre, string description, int cinemaId, ObservableCollection<Session> sessions): base(id)
        {
            Name = name;
            Genre = genre;
            Description = description;
            CinemaId = cinemaId;
            Sessions = sessions;
            AvatarPath = Patterns.GetAvatarsFilePath(id, cinemaId);
        }

        public override string ToString()
        {
            return $"{base.ToString()} {Name} {Genre} {Description} {CinemaId} {Sessions} {AvatarPath}";
        }
    }
}
