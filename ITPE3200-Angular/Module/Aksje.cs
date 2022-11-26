namespace ITPE3200_Angular.Module
{
    public class Aksje
    {
        public int id { get; set; }
        //[RegularExpression(@"^[a-zA-ZæøåÆØÅ. \-]{2-20}$")] 
        public string navn { get; set; }
       // [RegularExpression(@"^[a-zA-ZæøåÆØÅ. \-]{2-50}$")]
        public int pris { get; set; }
       // [RegularExpression(@"^[0-9+kr. \-]{2-10}$")]
        public int prosent { get; set; }
        //[RegularExpression(@"^[0-9+%. \-]{1-2}$")]
    }
}
