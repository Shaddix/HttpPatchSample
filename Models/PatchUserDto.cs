namespace HttpPatchSample.Models
{
    public class PatchUserDto : PatchDtoBase
    {
        public string Name { get; set; }
        public int Age { get; set; }

        public int? MotherId { get; set; }

        public int? FatherId { get; set; }
    }
}