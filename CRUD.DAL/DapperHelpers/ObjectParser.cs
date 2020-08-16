namespace Crud.Dal
{
	public static class ObjectParser
	{
		/// <summary>
		/// Retrieves a Dictionary with name and value 
		/// for all object properties matching the given criteria.
		/// </summary>
		public static ObjectPropertiesContainer ParseProperties<T>(T obj)
		{
			var container = new ObjectPropertiesContainer();
			var typeName = typeof(T).Name;
			foreach (var property in typeof(T).GetProperties())
			{
			   // Skip reference types (but still include string!)
				if ((property.PropertyType.IsClass || property.PropertyType.IsInterface)
					&& (property.PropertyType != typeof(string))
					&& (property.PropertyType != typeof(byte[])))
				{
					continue;
				}
				// Skip methods without a public setter
				if (property.GetSetMethod() == null) continue;
				var name = property.Name;
				var value = typeof(T).GetProperty(property.Name).GetValue(obj, null);
				container.AddValue(name, value);
			}
			return container;
		}
	}
}
