using System.ComponentModel.DataAnnotations;

namespace EasyWeb.Demo.Dtos.Request;

public class LeadRequestDto
{
    [Required]
    public string Name { get; set; }
    
    [Required]
    public string Surname { get; set; }   
}