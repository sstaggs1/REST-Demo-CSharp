﻿using System;

namespace REST_Demo_CSharp
{
	public class RestException
	{
		public string status { get; set; }

		public string code { get; set; }

		public string message { get; set; }

		public string developerMessage { get; set; }

		public string extraInfo { get; set; }
	}
}

