namespace HttpPatchSample.Database
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }

        public User Mother { get; set; }
        public int? MotherId { get; set; }

        public User Father { get; set; }
        public int? FatherId { get; set; }
    }
}
