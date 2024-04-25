using System;

namespace _11labbib
{
    public class Rat
    {
        public int Id { get; set; }
        public int STId { get; set; }
        public int SBId { get; set; }
        public int Ratt { get; set; }
        public string Zad { get; set; }
        public DateTime Date { get; set; }
        public int PRId { get; set; } 

        public Rat()
        { }

        public Rat(int STId, int SBId, int Ratt, string Zad, DateTime Date, int PRId)
        {
            this.STId = STId;
            this.SBId = SBId;
            this.Ratt = Ratt;
            this.Zad = Zad;
            this.Date = Date;
            this.PRId = PRId;
        }
    }
}
