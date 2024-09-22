namespace trpo.Lab1
{
    public class GuestBook
    {
        private List<GuestRecord> guests;

        public GuestBook()
        {
            guests = new List<GuestRecord>();
        }

        public void AddGuest(GuestRecord guest)
        {
            guests.Add(guest);
        }

        public void DisplayGuestsStayedMoreThanOneHour()
        {
            var filteredGuests = guests
                .Where(t => t.StayedMoreThanOneHour());

            Console.WriteLine("Гості, які перебували більше години:");
            foreach (var guest in filteredGuests)
            {
                Console.WriteLine(guest);
            }
            Console.WriteLine("\n");
        }

        public void SortByRoomNumber()
        {
            guests.Sort();
        }

        public void DisplayAllGuests()
        {
            Console.WriteLine("Всі гості:");
            foreach (var guest in guests)
            {
                Console.WriteLine(guest);
            }
            Console.WriteLine("\n");
        }

        public void SaveToFile(string fileName)
        {
            using (StreamWriter writer = new StreamWriter(fileName))
            {
                foreach (var guest in guests)
                {
                    writer.WriteLine($"{guest.FullName},{guest.CheckIn},{guest.CheckOut},{guest.RoomNumber}");
                }
            }
        }

        public void LoadFromFile(string fileName)
        {
            guests.Clear();
            using (StreamReader reader = new StreamReader(fileName))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    var parts = line.Split(',');
                    var fullName = parts[0];
                    var checkIn = DateTime.Parse(parts[1]);
                    var checkOut = DateTime.Parse(parts[2]);
                    var roomNumber = int.Parse(parts[3]);

                    guests.Add(new GuestRecord(fullName, checkIn, checkOut, roomNumber));
                }
            }
        }
    }
}
