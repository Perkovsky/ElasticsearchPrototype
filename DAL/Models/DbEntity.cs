using System;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace DAL.Models
{
	public abstract class DbEntity
	{
		[Key]
		public int Id { get; set; }

		public int CompareTo(DbEntity entity, string propertyName)
		{
			PropertyInfo propertyType = GetType()
				.GetProperty(propertyName);

			if (propertyType == null || propertyType.PropertyType.GetInterface(nameof(IComparable)) == null)
				throw new NotSupportedException("Couldn't resolve IComparable property " + propertyName);

			var firstValue = propertyType.GetValue(this, null);
			var secondValue = propertyType.GetValue(entity, null);

			if (firstValue == null)
			{
				return secondValue == null
					? 0
					: -1;
			}

			if (secondValue == null)
				return 1;

			return ((IComparable)firstValue).CompareTo(secondValue);
		}

		public bool IsNew { get { return Id < 1; } }
	}
}
