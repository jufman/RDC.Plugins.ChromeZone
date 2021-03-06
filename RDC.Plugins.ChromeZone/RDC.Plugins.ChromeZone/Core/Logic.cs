﻿using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RDC.Plugins.ChromeZone.Core.Objects;

namespace RDC.Plugins.ChromeZone.Core
{
    public static class Logic
    {
        public static int GetAttributeID(string baseString)
        {
            var ids = GetAttributeIDs(baseString);

            if (ids.Count > 0)
            {
                return ids[0];
            }

            return -1;
        }

        public static List<int> GetAttributeIDs(string baseString)
        {
            var attributeIDs = new List<int>();
            try
            {
                var RecordIDParts = baseString.Split(new[] { "**" }, StringSplitOptions.None);

                for (int i = 0; RecordIDParts.Length > i; i++)
                {
                    i++;
                    var attrID = RecordIDParts[i];
                    if (int.TryParse(attrID, out int result))
                    {
                        attributeIDs.Add(result);
                    }

                    i++;
                }
            }
            catch (Exception e)
            {
                e.ToString();
            }

            return attributeIDs;
        }

        public static string HandleFieldConversion(string fieldValue, FieldConversion conversion)
        {
            try
            {
                switch (conversion.Type)
                {
                    case "Date":
                        return DateTime.FromOADate(int.Parse(fieldValue)).AddDays(24837).ToString(conversion.Format);
                    case "Replace":
                        return fieldValue.Replace(conversion.OldValue, conversion.NewValue);
                }
            }
            catch
            {

            }

            return fieldValue;
        }

        public static bool HandleMatchFieldRules(string FieldValue, string Operator, string Value)
        {
            try
            {
                switch (Operator)
                {
                    case "=":
                        if (FieldValue == Value)
                        {
                            return true;
                        }

                        break;
                    case "!=":
                        if (FieldValue != Value)
                        {
                            return true;
                        }

                        break;
                    case ">":
                        if (int.Parse(FieldValue) > int.Parse(Value))
                        {
                            return true;
                        }

                        break;
                    case ">=":
                        if (int.Parse(FieldValue) >= int.Parse(Value))
                        {
                            return true;
                        }

                        break;
                    case "<":
                        if (int.Parse(FieldValue) < int.Parse(Value))
                        {
                            return true;
                        }

                        break;
                    case "<=":
                        if (int.Parse(FieldValue) <= int.Parse(Value))
                        {
                            return true;
                        }

                        break;
                }
            }
            catch
            {

            }

            return false;
        }


    }
}
