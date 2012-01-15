using System.ComponentModel.DataAnnotations;

namespace NecroNetToolkit.Web.Test.Models
{
	public class Model
	{
		[Display(Name = "lol")]
		[Required]
		public string Message { get; set; }
	}
}