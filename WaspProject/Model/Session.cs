using System.Collections.ObjectModel;
using System.Text;

namespace WaspProject.Model
{
    [Serializable]
    public class Session
    {
        // Время сеанса
        public DateTime Time { get; private set; }
        // Цена сеанса
        public int Price { get; private set; }

        public Session(DateTime time, int price)
        {
            Time = time;
            Price = price;
        }

        public override string ToString()
        {
            return $"{Time} {Price}";
        }
    }
}
