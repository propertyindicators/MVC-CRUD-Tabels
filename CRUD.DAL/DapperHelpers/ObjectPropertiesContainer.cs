using System.Collections.Generic;

namespace Crud.Dal
{
	public class ObjectPropertiesContainer
	{
		private readonly Dictionary<string, object> Values;

		public IEnumerable<string> ValueNames { get => Values.Keys; }

		public IDictionary<string, object> NameValuePairs { get => Values; }

		public ObjectPropertiesContainer() => Values = new Dictionary<string, object>();

		public void AddValue(string name, object value) => Values.Add(name, value);

		public void DeleteIdProp()
		{
			const string id = "Id";
			if (Values.ContainsKey(id)) Values.Remove(id);
		}
	}
}
