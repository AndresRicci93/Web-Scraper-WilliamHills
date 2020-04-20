using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.RegularExpressions;
using System.Threading;
namespace WilliamHills
{
    class Program
    {

        public static decimal TempQuota1 { get; private set; }

        public static decimal TempQuota2 { get; private set; }



        static void Main(string[] args)
        {
            // Console.WriteLine("---Calcio---");
            /*
          GetCalcio("http://sports.williamhill.it/bet_ita/it/betting/y/5/tm/Calcio.html");
          GetCalcio("http://sports.williamhill.it/bet_ita/it/betting/y/5/tm/1/Calcio.html");
          GetCalcio("http://sports.williamhill.it/bet_ita/it/betting/y/5/tm/2/Calcio.html");
          GetCalcio("http://sports.williamhill.it/bet_ita/it/betting/y/5/tm/3/Calcio.html");
          GetCalcio("http://sports.williamhill.it/bet_ita/it/betting/y/5/tm/4/Calcio.html");
          GetCalcio("http://sports.williamhill.it/bet_ita/it/betting/y/5/tm/5/Calcio.html");
          GetCalcio("http://sports.williamhill.it/bet_ita/it/betting/y/5/tm/6/Calcio.html");
          */
            // Console.WriteLine("---Tennis---");


            Thread.Sleep(11000);

            GetTennis("http://sports.williamhill.it/bet_ita/it/betting/y/17/mh/Tennis.html");



            Console.ReadKey();
        }


       

        async static void GetTennis(string url)
        {
            using (HttpClient client = new HttpClient())
            {
                using (HttpResponseMessage response = await client.GetAsync(url))

                {
                    using (HttpContent content = response.Content)
                    {

                        string mycontent = await content.ReadAsStringAsync();
                        string text = mycontent;

                        const string tournament_pattern = @"""type"" : {\s+""type_id""\s+: ""\d+"",\s+""type_name""\s+: ""(.+?)"",";

                        MatchCollection Tournament_Matches = Regex.Matches(text, tournament_pattern);
                    
                        
                           // foreach (Match match in Tournament_Matches)
                            //{


                           // Console.WriteLine(match.Groups[1].Value);

                           // const string grabber_pattern = @"""type"" : {\s+""type_id""\s+: ""\d+"",\s+""type_name""\s+: ""(.+?)"",";

                            const string grbbr_pattern = @":\s""(.+)"",\s+""disporder""\s+:\s"".+?""\s+},\s+""disporder""\s+:\s"".+?"",\s+""event""\s+:\s"".+?"",\s+""event_link""\s+:\s"".+?"",\s+""status""\s+:\s"".+"",\s+""raw_primary""\s+:\s.+\s+""is_us_format""\s+:\s"".+"",\s+""start_time""\s+:\s""(.+?)(\d+:\d+):\d+"",\s+.+\s+.+\s+.+\s+.+\s+.+\s+.+\s+""is_running""\s+:\s""(.+?)"",\s+""mkt_display_count""\s+:\s.+\s+""name""\s+:\s""(.+?)"",\s+.+\s+.+\s+.+\s+.+\s+.+\s+.+\s+.+\s+.+\s+.+\s+.+\s+.+\s+.+\s+.+\s+.+\s+.+\s+.+\s+.+\s+.+\s+.+\s+.+\s+.+\s+.+\s+.+\s+.+\s+.+\s+""markets""\s:\s+\[\s+{\s+"".+\s+.+\s+.+\s+.+\s+.+\s+.+\s+.+\s+.+\s+.+\s+.+\s+.+\s+.+\s+.+\s+.+\s+.+\s+.+\s+.+\s+.+\s+.+\s+.+\s+.+\s+.+\s+.+\s+.+\s+.+\s+.+\s+.+\s+.+\s+.+\s+.+\s+.+\s+.+\s+}\s+],\s+.+\s+\[\s+\],\s+.+\s+\[\s+{\s+.+\s+.+\s+""(lp_num)""\s+:\s""(.+?)"",\s+""lp_den""\s+:\s""(.+?)"",\s+.+\s+.+\s+.+\s+.+\s+.+\s+.+\s+.+\s+.+\s+.+\s+.+\s+.+\s+.+\s+.+\s+.+\s+.+\s\s+""disporder""\s+:\s""(.+)"",\s+.+\s+}\s+,\s+{\s+.+\s+.+\s+""(lp_num)""\s+:\s""(.+?)"",\s+""(lp_den)""\s+:\s""(.+?)""";

                            MatchCollection Grabber_Matches = Regex.Matches(text, grbbr_pattern);

                            foreach (Match match_g in Grabber_Matches)

                               {

                                int disporder = Convert.ToInt32(match_g.Groups[9].Value);    

                                decimal lp_num = int.Parse(match_g.Groups[7].Value);

                                decimal lp_den = int.Parse(match_g.Groups[8].Value);

                                decimal result = (lp_num / lp_den) + 1 ;


                                decimal quota_1 = Math.Round(result, 2);

                              //  decimal TempQuota;

                               decimal lp_num1 = int.Parse(match_g.Groups[11].Value);

                               decimal lp_den2 = int.Parse(match_g.Groups[13].Value);

                               decimal result1 = (lp_num1 / lp_den2) + 1;

                               decimal quota_2 = Math.Round(result1, 2);
                           
                                if (disporder == 1) {

                                 TempQuota1 = quota_1;
                                 TempQuota2 = quota_2;

                                }

                               else if (disporder == 2)
                                {

                                TempQuota1 = quota_2;
                                TempQuota2 = quota_1;

                                }
                               
                                

                         

                               Console.WriteLine(match_g.Groups[1].Value + ' ' + match_g.Groups[2].Value + ' ' + match_g.Groups[3].Value + ' ' + match_g.Groups[9].Value + ' ' + TempQuota1 + ' ' + match_g.Groups[5].Value + ' ' + TempQuota2);



                               }

                     


                    }

                }

            }
        }





    }
