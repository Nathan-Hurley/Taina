using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Taina.Data.Models
{
	public enum GenderType
	{
		[Display(Name = "Prefer Not To Say")]
		PreferNotToSay,
		Male,
		Female
	}
}
