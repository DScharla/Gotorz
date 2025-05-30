﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    public class AuthResponseDTO
    {
		[Required]
		public string Token { get; set; }
		[Required]
		public string RefreshToken { get; set; }
		[Required]
		public DateTime Expiration { get; set; }
	}
}
