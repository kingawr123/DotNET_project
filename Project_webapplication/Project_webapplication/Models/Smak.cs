using System.ComponentModel.DataAnnotations;
namespace Project_webapplication.Models;

public class Smak
{
    [Key] public int IdSmaku { get; set; }
    [Display(Name = "Nazwa")] public string Nazwa { get; set; }
}