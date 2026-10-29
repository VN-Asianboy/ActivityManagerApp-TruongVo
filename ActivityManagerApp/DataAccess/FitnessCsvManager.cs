
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using ActivityManagerApp.Models;
using CsvHelper;
using CsvHelper.Configuration;

namespace ActivityManagerApp.DataAccess
{
    public class FitnessCsvManager
    {
        private readonly string filePath;

        public FitnessCsvManager(string filePath)
        {
            this.filePath = filePath;
        }

        private CsvConfiguration CreateConfiguration()
        {
            return new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = true
            };
        }

        public List<FitnessActivity> ReadActivities()
        {
            using (var reader = new StreamReader(filePath))
            using (var csv = new CsvHelper.CsvReader(reader, CreateConfiguration()))
            {
                csv.Context.RegisterClassMap<FitnessActivityMap>();
                return csv.GetRecords<FitnessActivity>().ToList();
            }
        }

        public void WriteActivities(List<FitnessActivity> activities)
        {
            using (var writer = new StreamWriter(filePath, false))
            using (var csv = new CsvWriter(writer, CreateConfiguration()))
            {
                csv.Context.RegisterClassMap<FitnessActivityMap>();
                csv.WriteRecords(activities);
            }
        }

        public void AddActivity(FitnessActivity activity)
        {
            List<FitnessActivity> activities = ReadActivities();
            activities.Add(activity);
            WriteActivities(activities);
        }
    }

    public sealed class FitnessActivityMap : ClassMap<FitnessActivity>
    {
        public FitnessActivityMap()
        {
            Map(x => x.DateStartTime)
                .TypeConverterOption.Format("d/M/yyyy H:mm");

            Map(x => x.Title);
            Map(x => x.Cost);
            Map(x => x.Location);
        }
    }
}
