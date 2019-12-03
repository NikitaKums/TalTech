using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading;

namespace Domain
{
    public class MultiLangString : DomainEntity
    {
        private static string _defaultCulture = "en";

        [MaxLength(10240)] public string Value { get; set; } // default value when there are no translations found

        public ICollection<Translation> Translations { get; set; } = new List<Translation>();

        # region constructors

        public MultiLangString()
        {
        }

        // use current UICulture
        public MultiLangString(string value) : this(value, Thread.CurrentThread.CurrentUICulture.Name)
        {
        }


        public MultiLangString(string value, string culture) : this(value, culture, value)
        {
        }

        public MultiLangString(string value, string culture, string defaultValue)
        {
            Value = defaultValue;
            SetTranslation(value, culture);
        }

        #endregion

        public void SetTranslation(string value)
        {
            SetTranslation(value, Thread.CurrentThread.CurrentUICulture.Name);
        }

        public void SetTranslation(string value, string culture)
        {
            // use only neutral part en-US => en
            culture = culture.Substring(0, 2).ToLower();
            if (Translations == null)
            {
                Translations = new List<Translation>();
            }

            var found = Translations.FirstOrDefault(a => a.Culture == culture);
            if (found == null)
            {
                Translations.Add(new Translation()
                {
                    Value = value,
                    Culture = culture
                });
            }
            else
            {
                found.Value = value;
            }
        }


        public string Translate(string culture = "")
        {
            if (string.IsNullOrWhiteSpace(culture))
            {
                culture = Thread.CurrentThread.CurrentUICulture.Name;
            }
            culture = culture.Substring(0, 2).ToLower();

            var translation = Translations.FirstOrDefault(t => t.Culture.StartsWith(culture));

            return translation?.Value ?? Value;
        }
        
    }
}
