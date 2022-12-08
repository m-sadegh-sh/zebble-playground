﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models
{
	public class Category
	{
		public Guid Id { get; set; }
		public string Name { get; set; }

		public override string ToString() => Name;
	}
}
