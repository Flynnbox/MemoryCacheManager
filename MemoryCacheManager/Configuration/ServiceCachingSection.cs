using System;
using System.Configuration;

namespace MemoryCacheManager.Configuration
{
	public class ServiceCachingSection : ConfigurationSection
	{
		private static readonly ServiceCachingSection settings = ConfigurationManager.GetSection("serviceCachingSection") as ServiceCachingSection;

		public static ServiceCachingSection Settings
		{
			get { return settings; }
		}

		[ConfigurationProperty("enableCaching", DefaultValue = true)]
		public bool EnableCaching
		{
			get { return (bool)this["enableCaching"]; }
		}

		[ConfigurationProperty("cacheItems")]
		public CacheItemElementCollection CacheItems
		{
			get { return ((CacheItemElementCollection)(this["cacheItems"])); }
		}
	}

	public class CacheItemElement : ConfigurationElement
	{
		[ConfigurationProperty("key", IsRequired = true, IsKey = true)]
		public string Key
		{
			get { return this["key"].ToString(); }
		}

		[ConfigurationProperty("enableCaching", IsRequired = true, DefaultValue = true)]
		public bool EnableCaching
		{
			get { return (bool)this["enableCaching"]; }
		}

		[ConfigurationProperty("absoluteExpirationInSeconds", DefaultValue = 1800)]
		public int AbsoluteExpirationInSeconds
		{
			get { return (int)this["absoluteExpirationInSeconds"]; }
		}
	}

	[ConfigurationCollection(typeof(CacheItemElement))]
	public class CacheItemElementCollection : ConfigurationElementCollection
	{
		internal const string PropertyName = "cacheItem";

		public override ConfigurationElementCollectionType CollectionType
		{
			get
			{
				return ConfigurationElementCollectionType.BasicMapAlternate;
			}
		}
		protected override string ElementName
		{
			get
			{
				return PropertyName;
			}
		}

		protected override bool IsElementName(string elementName)
		{
			return elementName.Equals(PropertyName, StringComparison.InvariantCultureIgnoreCase);
		}

		public override bool IsReadOnly()
		{
			return false;
		}

		protected override ConfigurationElement CreateNewElement()
		{
			return new CacheItemElement();
		}

		protected override object GetElementKey(ConfigurationElement element)
		{
			return ((CacheItemElement)(element)).Key;
		}

		public new CacheItemElement this[string key]
		{
			get { return (CacheItemElement)this.BaseGet(key); }
		}
	}
}