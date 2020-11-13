using System.ComponentModel.DataAnnotations;

namespace NorthwindCorp.DTO
{
  public class CategoryDto
  {
    [Key]
    public virtual int CategoryId { get; set; }

    public virtual string CategoryName { get; set; }

    public virtual string Description { get; set; }

    public virtual byte[] Picture { get; set; }
  }
}
