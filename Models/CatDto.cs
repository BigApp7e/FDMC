namespace FDMC.Models
{
    public class CatDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string Breed { get; set; }
        public string? ImagePath { get; set; }

        public CatDto(int Id, string Name, int Age, string Breed, string ImagePath) {
            this.Id = Id;
            this.Name = Name;
            this.Age = Age;
            this.Breed = Breed;
            this.ImagePath = ImagePath;
        }

    }
}
