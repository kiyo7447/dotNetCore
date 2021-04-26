using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MySQLWebApp.Models
{
	[Table("student")]
	public class Student
	{
		[Key]
		[Column("id")]
		public int ID { get; set; }
		[Column("last_name", TypeName = "varchar(100)")]
		[MaxLength(100)]
		[Required(AllowEmptyStrings = false)]
		public string LastName { get; set; }
		[Column("first_mid_name", TypeName = "varchar(100)")]
		[MaxLength(100)]
		[Required(AllowEmptyStrings = false)]
		public string FirstMidName { get; set; }
		[Column("enrollment_date")]
		[Required]
		public DateTime EnrollmentDate { get; set; }
		public ICollection<Enrollment> Enrollments { get; set; }
	}
}
