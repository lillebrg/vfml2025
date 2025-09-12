using Microsoft.ML;
using Microsoft.ML.Data;
using Microsoft.ML.Transforms.Text;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System;
using Models;

namespace NLU
{
    public class NLURepository : INLURepository
    {
        private readonly MLContext _mlContext;
        private readonly ITransformer _trainedModel;
        private readonly PredictionEngine<NLUData, NLUDataIntentPrediction> _predictor;

        private readonly IDataView _unsupervisedData;
        private readonly ITransformer _kmeansModel;
        private readonly Dictionary<uint, string> _clusterToIntent;
        public NLURepository()
        {

            _mlContext = new MLContext();

            // Supervised pipeline
            //Brug korrekt absolut sti
            var path = "C:/Users/LilleBRG/source/repos/Machine Learning/vfml2025/NLU/Data/vet_intents.csv;";
            //var path = Path.Combine(AppContext.BaseDirectory, "Data", "vet_intents.csv");

            // Fejlbesked hvis fil ikke findes
            if (!File.Exists(path))
                throw new FileNotFoundException($"CSV-filen blev ikke fundet: {path}");

            // Load supervised træningsdata
            var data = _mlContext.Data.LoadFromTextFile<NLUData>(
                path: path,
                hasHeader: true,
                separatorChar: ',');

            var pipeline = _mlContext.Transforms.Text.FeaturizeText("Features", nameof(NLUData.Text))
                .Append(_mlContext.Transforms.Conversion.MapValueToKey("Label", nameof(NLUData.Intent)))
                .Append(_mlContext.MulticlassClassification.Trainers.SdcaMaximumEntropy("Label", "Features"))
                .Append(_mlContext.Transforms.Conversion.MapKeyToValue("PredictedLabel"));

            _trainedModel = pipeline.Fit(data);
            _predictor = _mlContext.Model.CreatePredictionEngine<NLUData, NLUDataIntentPrediction>(_trainedModel);

            // Unsupervised KMeans fallback
            _unsupervisedData = _mlContext.Data.LoadFromTextFile<NLUData>(
         path: path, hasHeader: true, separatorChar: ',');
            var kmeansPipeline = _mlContext.Transforms.Text.FeaturizeText("Features", nameof(NLUData.Text))
                .Append(_mlContext.Clustering.Trainers.KMeans("Features", numberOfClusters: 3));

            _kmeansModel = kmeansPipeline.Fit(_unsupervisedData);

            // Map cluster IDs to intent heuristically (for fallback explanation)
            _clusterToIntent = new Dictionary<uint, string>
        {
            { 0, "Physical" },
            { 1, "Mental" },
        };
        }


        private static readonly string[] FAQKeywords = ["trick", "nail", "tail", "nose"];
        private static readonly string[] EmergencyKeywords = ["dying", "chok", "puk", "sick", "act", "weird", "behaviour"];


        public NLUResult Predict(string input)
        {
            var prediction = _predictor.Predict(new NLUData { Text = input });

            if (!string.IsNullOrEmpty(prediction.PredictedIntent))
            {
                var entities = ExtractEntities(input);
                if (entities["Breed"] is not null || entities["age"] is not null)
                    return new NLUResult(prediction.PredictedIntent, entities);
                else
                    return new NLUResult("GetAnimal", null);
            }

            // Fallback for clustering
            var vectorized = _mlContext.Data.LoadFromEnumerable(new[]
            {
            new NLUData { Text = input }
            });

            var transformed = _kmeansModel.Transform(vectorized);
            var clusterColumn = transformed.GetColumn<uint>("PredictedLabel").FirstOrDefault();
            var fallbackIntent = _clusterToIntent.GetValueOrDefault(clusterColumn, "Unknown");

            return new NLUResult(fallbackIntent, ExtractEntities(input));
        }

        private Dictionary<string, string> ExtractEntities(string input)
        {
            var entities = new Dictionary<string, string>();

            // Breed
            var breedMatch = Regex.Match(input, @"(?:breed\s+is|is\s+a|is\s+an)\s+([A-Za-zæøå\s]+)", RegexOptions.IgnoreCase);
            if (breedMatch.Success)
                entities["Breed"] = breedMatch.Groups[1].Value;

            // Age
            var ageMatch = Regex.Match(input, @"(\d+)\s+(?:year)\s+old", RegexOptions.IgnoreCase);
            if (ageMatch.Success)
                entities["Age"] = breedMatch.Groups[1].Value;

            return entities;
        }

        //public NLUResult Get(string input)
        //{
        //    input = input.ToLower();
        //    var tokens = Tokenize(input);
        //    var stemmedTokens = tokens.Select(Stem).ToList();
        //    BotArea state = BotArea.Unknown;
        //    Animal animal = GetAnimal(input);
        //    if (ContainsAny(stemmedTokens, FAQKeywords))
        //        state = BotArea.FAQ;
        //    else if (ContainsAny(stemmedTokens, EmergencyKeywords))
        //        state = BotArea.Emergency;
                
        //    return new NLUResult { Animal = animal, BotState = state };
        //}

        //private static List<string> Tokenize(string input)
        //{
        //    return input
        //        .ToLower()
        //        .Split([' ', ',', '.', '?', '!', ';'], StringSplitOptions.RemoveEmptyEntries)
        //        .ToList();
        //}

        //private static string Stem(string word)
        //{
        //    if (string.IsNullOrWhiteSpace(word))
        //        return word;

        //    word = word.ToLower();

        //    if (word.EndsWith("ing"))
        //        return word.Substring(0, word.Length - 3);

        //    else if (word.EndsWith("es") && word.Length > 3)
        //    {
        //        // prevent cutting off words like "xylitos" -> "xylito"
        //        // only stem if word before 's' is a vowel/consonant that makes sense
        //        return word.Substring(0, word.Length - 2);
        //    }
        //    else if (word.EndsWith("s") && word.Length > 3)
        //    {
        //        // prevent cutting off words like "xylitos" -> "xylito"
        //        // only stem if word before 's' is a vowel/consonant that makes sense
        //        return word.Substring(0, word.Length - 1);
        //    }

        //    return word;
        //}

        //private static bool ContainsAny(IEnumerable<string> tokens, string[] keywords)
        //    => tokens.Any(t => keywords.Contains(t));
        //private static string? FindMatchedKeyword(IEnumerable<string> tokens, string[] keywords)
        //=> tokens.FirstOrDefault(t => keywords.Contains(t));
        
    }
}

