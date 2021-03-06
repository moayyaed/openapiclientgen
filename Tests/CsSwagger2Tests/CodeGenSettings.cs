﻿using Fonlow.OpenApiClientGen.ClientTypes;

namespace SwagTests
{
	public static class CodeGenSettings
	{
		public static readonly Settings Default = new Settings()
		{
			ClientNamespace = "MyNS",
			PathPrefixToRemove = "/api",
			ContainerClassName = "Misc",
			ContainerNameStrategy = ContainerNameStrategy.None,
			ActionNameStrategy = ActionNameStrategy.Default,
			GenerateBothAsyncAndSync = false,
			UseEnsureSuccessStatusCodeEx = true,
			DataAnnotationsEnabled = true,
			DataAnnotationsToComments = true,
		};

		public static Settings WithActionNameStrategy(ActionNameStrategy ans)
		{
			return new Settings()
			{
				ClientNamespace = "MyNS",
				ContainerClassName = "Misc",
				ContainerNameStrategy = ContainerNameStrategy.None,
				ActionNameStrategy = ans,
				GenerateBothAsyncAndSync = false,
				DecorateDataModelWithSerializable = true,
				UseEnsureSuccessStatusCodeEx = true,
				DataAnnotationsEnabled = true,
				DataAnnotationsToComments = true,
				HandleHttpRequestHeaders = true,
			};
		}
	}
}
