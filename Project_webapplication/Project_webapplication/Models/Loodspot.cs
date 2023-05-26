using System.Collections;
using System.ComponentModel.DataAnnotations;

namespace Project_webapplication.Models;

public class Loodspot
{
    [Key] public int IdLoodspotu { get; set; }
    [Display(Name = "Adres")] public String Adres { get; set; }
    [Display(Name = "Smaki")] public ICollection<Smak> Smaki { get; set; }
}