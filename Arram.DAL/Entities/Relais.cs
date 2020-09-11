using System.ComponentModel.DataAnnotations;
using System.Spatial;

namespace Arram.Core.DAL.Entities
{
  public class Relais : BaseEntity
	{
		[Required]
		public string Region { get; set; }
		[Required]
		public string Site { get; set; }
		[Required]
		public string Nom { get; set; }

		[Required]
		public int Altitude { get; set; }
		[Required]
		public int FreqEntree { get; set; }

		[Required]
		public int FreqSortie { get; set; }

		[Required]
		public int Bande { get; set; }

		[Required]
		public int Shift { get; set; }

		[Required]
		public string Tone { get; set; }

		[Required]
		public int Puissance { get; set; }

		[Required]
		public string QraLocator { get; set; }

		[Required]
		public decimal Latitude { get; set; }

		[Required]
		public decimal Longitude { get; set; }

		// public Geography Position { get; set; }
	}
}
