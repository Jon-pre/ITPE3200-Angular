using System.ComponentModel.DataAnnotations;

namespace ITPE3200_Angular.Module
{
    public class Konto
    {
        public int id { get; set; }
       [RegularExpression(@"[a-zA-ZøæåØÆÅ. \-]{2,20}")]
        public string kontonavn { get; set; }
       [RegularExpression(@"[a-zA-ZøæåØÆÅ. \-]{2,20}")]
        public string land { get; set; }
        public int kontobalanse { get; set; }
       [RegularExpression(@"[0-9a-zA-ZøæåØÆÅ. \-]{2,20}")]
        public string brukernavn { get; set; }
       [RegularExpression(@"^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{4,}$")]
        public string passord { get; set; }
    }
}
