using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp;
using Newtonsoft.Json;


namespace BikeAppTest
{
    public class BikePreference
    {
        public string[] bikes { get; set; }

    }
    class Program
    {

        static List<BikePreference> BikeCombination = new List<BikePreference>();



        static void Main(string[] args)
        {

            ShowBikeTreands();
            Console.ReadKey();
        }

        static void ShowBikeTreands()
        {
            var client = new RestClient("https://trekhiringassignments.blob.core.windows.net/interview/bikes.json");
            var request = new RestRequest(Method.GET);

            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("content-type", "application/json");



            IRestResponse response = client.Execute(request);

            BikeCombination = JsonConvert.DeserializeObject<List<BikePreference>>(response.Content);


            Dictionary<string, int> BikeFamilies = new Dictionary<string, int>();
            foreach (BikePreference bikefamily in BikeCombination)
            {
                string bikecombination = string.Join(",", bikefamily.bikes);
                if (BikeFamilies.Keys.Contains(bikecombination))
                {
                    BikeFamilies[bikecombination] = BikeFamilies[bikecombination] + 1;
                }
                else
                {
                    BikeFamilies.Add(bikecombination, 1);
                }

            }


            var sortlist = from bikefam in BikeFamilies
                           orderby bikefam.Value descending
                           select bikefam;



            List<KeyValuePair<string, int>> myList = BikeFamilies.ToList();
            int counter = 0;

            Console.WriteLine("Top 20 Trending Bike Families");
            foreach (var kvp in sortlist)
            {
                if (counter == 19)
                {
                    break;

                }
                Console.WriteLine("Bike Famly " + kvp.Key + " Number Of Bikes " + kvp.Value);
                counter++;
            }



        }



    }
}
