using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Abstractions
{
	public class ModelMetadata
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public Guid Id { get; set; }
		[DataType(DataType.Date)]
		public DateTime Created { get; set; }
		[DataType(DataType.Date)]
		public DateTime Modified { get; set; }
		public bool Status { get; set; }
	}
}
