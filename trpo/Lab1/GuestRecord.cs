namespace trpo.Lab1
{
    public class GuestRecord : IComparable<GuestRecord>
    {
        public string FullName { get; set; }
        public DateTime CheckIn { get; set; }
        public DateTime CheckOut { get; set; }
        public int RoomNumber { get; set; }

        public GuestRecord() { }

        public GuestRecord(string fullName, DateTime checkIn, DateTime checkOut, int roomNumber)
        {
            if (string.IsNullOrEmpty(fullName))
                throw new ArgumentException("ПІБ не може бути порожнім.");
            if (checkOut <= checkIn)
                throw new ArgumentException("Дата виїзду повинна бути пізнішою за дату прибуття.");
            if (roomNumber <= 0)
                throw new ArgumentException("Номер кімнати повинен бути додатним числом.");

            FullName = fullName;
            CheckIn = checkIn;
            CheckOut = checkOut;
            RoomNumber = roomNumber;
        }

        public bool StayedMoreThanOneHour()
        {
            return (CheckOut - CheckIn).TotalHours > 1;
        }

        public int CompareTo(GuestRecord other)
        {
            return RoomNumber.CompareTo(other.RoomNumber);
        }

        public override string ToString()
        {
            return $"Гість: {FullName}, Кімната: {RoomNumber}, Прибув: {CheckIn}, Виїхав: {CheckOut}";
        }
    }
}
