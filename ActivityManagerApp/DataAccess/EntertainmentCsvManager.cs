
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using ActivityManagerApp.Models;
using FileHelpers;

namespace ActivityManagerApp.DataAccess
{
    [DelimitedRecord(",")]
    public class EntertainmentCsvRecord
    {
        [FieldConverter(ConverterKind.Date, "d/M/yyyy H:mm")]
        public DateTime DateStartTime;

        public string Title;
        public decimal Cost;
        public int MinParticipants;
    }

    public class EntertainmentCsvManager
    {
        private readonly string filePath;

        public EntertainmentCsvManager(string filePath)
        {
            this.filePath = filePath;
        }

        public List<EntertainmentActivity> ReadActivities()
        {
            string[] lines = File.ReadAllLines(filePath);

            if (lines.Length <= 1)
                return new List<EntertainmentActivity>();

            var engine = new FileHelperEngine<EntertainmentCsvRecord>();

            string[] dataLines = new string[lines.Length - 1];
            Array.Copy(lines, 1, dataLines, 0, dataLines.Length);

            EntertainmentCsvRecord[] records =
                engine.ReadString(string.Join(Environment.NewLine, dataLines));

            var activities = new List<EntertainmentActivity>();

            foreach (var record in records)
            {
                activities.Add(new EntertainmentActivity
                {
                    DateStartTime = record.DateStartTime,
                    Title = record.Title,
                    Cost = record.Cost,
                    MinParticipants = record.MinParticipants
                });
            }

            return activities;
        }

        public void WriteActivities(List<EntertainmentActivity> activities)
        {
            var records = new List<EntertainmentCsvRecord>();

            foreach (var activity in activities)
            {
                records.Add(new EntertainmentCsvRecord
                {
                    DateStartTime = activity.DateStartTime,
                    Title = activity.Title,
                    Cost = activity.Cost,
                    MinParticipants = activity.MinParticipants
                });
            }

            var engine = new FileHelperEngine<EntertainmentCsvRecord>();
            engine.HeaderText =
                "DateStartTime,Title,Cost,MinParticipants";

            engine.WriteFile(filePath, records);
        }

        public void AddActivity(EntertainmentActivity activity)
        {
            List<EntertainmentActivity> activities = ReadActivities();
            activities.Add(activity);
            WriteActivities(activities);
        }
    }
}
