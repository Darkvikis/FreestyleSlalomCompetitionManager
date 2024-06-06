using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace FreestyleSlalomCompetitionManager.BL.Converters
{
    public class NameConverter
    {
        public static string NameWithoutDiacritics(string name)
        {
            string normalizedString = name.Normalize(NormalizationForm.FormD).Trim();
            StringBuilder stringBuilder = new();

            foreach (char c in normalizedString)
            {
                if (CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark)
                {
                    if (c == 'ł')
                    {
                        stringBuilder.Append('l');
                    }
                    else
                    {
                        stringBuilder.Append(c);
                    }
                }

            }

            return stringBuilder.ToString();
        }

        public static string CountryToShortCut(string country)
        {
            string trimmedCountry = country.Trim();
            string shortcut = trimmedCountry[..3].ToUpper();
            return shortcut;
        }
    }
}
