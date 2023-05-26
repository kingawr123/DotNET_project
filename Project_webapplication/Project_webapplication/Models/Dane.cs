using System.ComponentModel.DataAnnotations;
namespace Project_webapplication.Models;

public class Dane
{
    [Key]
    public int Id { get; set; }
    public String Opis { get; set; }

}