using System.Collections.ObjectModel;
using System.IO;

namespace L08_Exercises.model
{
    public class User
    {
        public User(string data)
        {
            var I = data.Split(';');
            Id = I[0];
            Name = I[1];
            Age = int.Parse(I[2]);
            Score = int.Parse(I[3]);
        }

        public string Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public int Score { get; set; }

        public override string ToString()
        {
            return string.Format($"ID: {Id}, {Name}");
        }

        public static ObservableCollection<User> ReadCsvFile(string filename)
        {
            var list = new ObservableCollection<User>();
            using (var file = new StreamReader(filename))
            {
                string line;
                while ((line = file.ReadLine()) != null)
                {
                    var u = new User(line);
                    list.Add(u);
                }
            }
            return list;
        }


    }
}
