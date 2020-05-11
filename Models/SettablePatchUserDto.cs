namespace HttpPatchSample.Models
{
    
    public class SettablePatchUserDto
    {
        public string Name { get; set; }

        public Settable<int> Age { get; set; }
        
        public Settable<int> MotherId { get; set; }

        public Settable<int> FatherId { get; set; }
    }
}