using Space.Front.Forms.Attributes;

namespace Space.Front.Forms.Basic
{
    public abstract class QueryModelBase
    {
        public IEnumerable<KeyValuePair<string, string>> GetParameters()
        {
            var props = GetType().GetProperties();
            var propWithAttribute = props
                .Where(u => u.CustomAttributes.Any(v => v.AttributeType == typeof(QueryAttribute)));

            if (!propWithAttribute.Any())
            {
                yield break;
            }

            var mappedProperties = propWithAttribute.Select(u => new 
            { 
                PropertyValue = u.GetValue(this),
                Attribute = u.GetCustomAttributes(typeof(QueryAttribute), false).First() as QueryAttribute 
            });

            foreach (var property in mappedProperties)
            {
                var isValueExist = property.PropertyValue != null;

                if (!isValueExist)
                    continue;

                yield return new KeyValuePair<string, string>(property.Attribute.Name, property.PropertyValue.ToString());
            }
        }
    }
}
