using AndresVillarreal_Project.Models;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace AndresVillarreal_Project.Data
{

    public static class SummerGamesInitializer
    {
        public static void Seed(IApplicationBuilder applicationBuilder)
        {
            SummerGamesContext context = applicationBuilder.ApplicationServices.CreateScope()
                .ServiceProvider.GetRequiredService<SummerGamesContext>();
            try
            {
                //To randomly generate data
                Random random = new Random(10);

                context.Database.EnsureDeleted();
                context.Database.Migrate();

                if (!context.Contingents.Any())
                {
                    var contingents = new List<Contingent>
                    {
                        new Contingent { Code = "ON", Name = "Ontario"},
                        new Contingent { Code = "PE", Name = "Prince Edward Island"},
                        new Contingent { Code = "NB", Name = "New Brunswick"},
                        new Contingent { Code = "BC", Name = "British Columbia"},
                        new Contingent { Code = "NL", Name = "Newfoundland and Labrador"},
                        new Contingent { Code = "SK", Name = "Saskatchewan"},
                        new Contingent { Code = "NS", Name = "Nova Scotia"},
                        new Contingent { Code = "MB", Name = "Manitoba"},
                        new Contingent { Code = "QC", Name = "Quebec"},
                        new Contingent { Code = "YT", Name = "Yukon"},
                        new Contingent { Code = "NU", Name = "Nunavut"},
                        new Contingent { Code = "NT", Name = "Northwest Territories"},
                        new Contingent { Code = "AB", Name = "Alberta"}
                    };
                    context.Contingents.AddRange(contingents);
                    context.SaveChanges();
                }

                //Add the Sports
                if (!context.Sports.Any())
                {
                    string[] sports = new string[] { "Athletics", "Baseball", "Basketball", "Canoe Kayak", "Cycling - Road Cycling", "Cycling - Mountain Bike", "Diving", "Golf", "Lacrosse", "Rowing", "Rugby Sevens", "Sailing", "Soccer", "Softball", "Swimming", "Tennis", "Triathlon", "Volleyball - Beach", "Volleyball - Indoor", "Wrestling" };
                    string[] sportCodes = new string[] { "ATH", "BAB", "BKB", "CKY", "CYR", "CYM", "DIV", "GLF", "LAC", "ROW", "RGS", "SAL", "SOC", "SBA", "SWM", "TEN", "TRA", "VBB", "VBI", "WRS" };
                    int NumSports = sports.Length;

                    //Loop through sports and add them
                    for (int i = 0; i < NumSports; i++)
                    {
                        //Construct some details
                        Sport s = new Sport()
                        {
                            Code = sportCodes[i],
                            Name = sports[i]
                        };
                        context.Sports.Add(s);

                    }
                    context.SaveChanges();
                }

                //Create collections of the primary keys
                //Note: for Gender we will alternate
                int[] sportIDs = context.Sports.Select(a => a.ID).ToArray();
                int sportIDCount = sportIDs.Length;
                int[] contingentIDs = context.Contingents.Select(a => a.ID).ToArray();
                int contingentIDCount = contingentIDs.Length;

                //Create 5 large strings from Bacon ipsum
                string[] baconNotes = new string[] { "Bacon ipsum dolor amet meatball corned beef kevin, alcatra kielbasa biltong drumstick strip steak spare ribs swine. Pastrami shank swine leberkas bresaola, prosciutto frankfurter porchetta ham hock short ribs short loin andouille alcatra. Andouille shank meatball pig venison shankle ground round sausage kielbasa. Chicken pig meatloaf fatback leberkas venison tri-tip burgdoggen tail chuck sausage kevin shank biltong brisket.", "Sirloin shank t-bone capicola strip steak salami, hamburger kielbasa burgdoggen jerky swine andouille rump picanha. Sirloin porchetta ribeye fatback, meatball leberkas swine pancetta beef shoulder pastrami capicola salami chicken. Bacon cow corned beef pastrami venison biltong frankfurter short ribs chicken beef. Burgdoggen shank pig, ground round brisket tail beef ribs turkey spare ribs tenderloin shankle ham rump. Doner alcatra pork chop leberkas spare ribs hamburger t-bone. Boudin filet mignon bacon andouille, shankle pork t-bone landjaeger. Rump pork loin bresaola prosciutto pancetta venison, cow flank sirloin sausage.", "Porchetta pork belly swine filet mignon jowl turducken salami boudin pastrami jerky spare ribs short ribs sausage andouille. Turducken flank ribeye boudin corned beef burgdoggen. Prosciutto pancetta sirloin rump shankle ball tip filet mignon corned beef frankfurter biltong drumstick chicken swine bacon shank. Buffalo kevin andouille porchetta short ribs cow, ham hock pork belly drumstick pastrami capicola picanha venison.", "Picanha andouille salami, porchetta beef ribs t-bone drumstick. Frankfurter tail landjaeger, shank kevin pig drumstick beef bresaola cow. Corned beef pork belly tri-tip, ham drumstick hamburger swine spare ribs short loin cupim flank tongue beef filet mignon cow. Ham hock chicken turducken doner brisket. Strip steak cow beef, kielbasa leberkas swine tongue bacon burgdoggen beef ribs pork chop tenderloin.", "Kielbasa porchetta shoulder boudin, pork strip steak brisket prosciutto t-bone tail. Doner pork loin pork ribeye, drumstick brisket biltong boudin burgdoggen t-bone frankfurter. Flank burgdoggen doner, boudin porchetta andouille landjaeger ham hock capicola pork chop bacon. Landjaeger turducken ribeye leberkas pork loin corned beef. Corned beef turducken landjaeger pig bresaola t-bone bacon andouille meatball beef ribs doner. T-bone fatback cupim chuck beef ribs shank tail strip steak bacon." };
                //Affiliations
                string[] affiliations = new string[] { "Alpine Ontario", "Archery Ontario", "Athletics Ontario", "Badminton Ontario", "Baseball Ontario", "Biathlon Ontario", "Boxing Ontario", "Canoe Kayak Ontario", "Cricket Ontario", "Dive Ontario", "Field Hockey Ontario", "Golf Ontario", "Gymnastics Ontario", "Hockey Eastern Ontario", "Hockey Northwestern Ontario", "Judo Ontario", "Karate Ontario", "Kickboxing Ontario", "Muay Thai Ontario", "Ontario Amateur Wrestling Association", "Ontario Artistic Swimming", "Ontario Ball Hockey Federation", "Ontario Basketball", "Ontario Blind Sports Association", "Ontario Bobsleigh Skeleton Association", "Ontario Cerebral Palsy Sports Association", "Ontario Council of Shooters", "Ontario Curling Council", "Ontario Cycling Association", "Ontario Equestrian", "Ontario Fencing Association", "Ontario Football Alliance", "Ontario Grappling Association", "Ontario Jiu-Jitsu Association", "Ontario Lacrosse Association", "Ontario Para Network", "Ontario Sailing", "Ontario Soccer", "Ontario Table Tennis Association", "Ontario Tennis Association", "Ontario Volleyball Association", "Ontario Water Polo", "Ontario Weightlifting Association", "Racquetball Ontario", "Ringette Ontario", "Row Ontario", "Rugby Ontario", "Softball Ontario", "Special Olympics Ontario", "Squash Ontario", "Swim Ontario", "Taekwondo Ontario", "Triathlon Ontario", "Water Ski and Wakeboard Ontario", "Wushu Ontario" };
                //Names for Athletes 
                string[] firstNames = new string[] { "Lyric", "Antoinette", "Kendal", "Vivian", "Ruth", "Jamison", "Emilia", "Natalee", "Yadiel", "Jakayla", "Lukas", "Moses", "Kyler", "Karla", "Chanel", "Tyler", "Camilla", "Quintin", "Braden", "Clarence" };
                string[] lastNames = new string[] { "Watts", "Randall", "Arias", "Weber", "Stone" };//, "Carlson", "Robles", "Frederick", "Parker", "Morris", "Soto", "Bruce", "Orozco", "Boyer", "Burns", "Cobb", "Blankenship", "Houston", "Estes", "Atkins", "Miranda", "Zuniga", "Ward", "Mayo", "Costa", "Reeves", "Anthony", "Cook", "Krueger", "Crane", "Watts", "Little", "Henderson", "Bishop" };
                int firstNameCount = firstNames.Length;
                int lastNameCount = lastNames.Length;
                int affiliationCount = affiliations.Length;

                //Add the Athletes
                if (!context.Athletes.Any())
                {
                    // Birthdate for randomly produced Athletes 
                    // We will add a random number of days to the minimum date
                    DateTime startDOB = Convert.ToDateTime("1994-08-22");
                    int counter = 1; //Used to help set genders

                    foreach (string lastName in lastNames)
                    {
                        //Choose a random HashSet of 2 (Unique) first names
                        HashSet<string> selectedFirstNames = new HashSet<string>();
                        while (selectedFirstNames.Count() < 2)
                        {
                            selectedFirstNames.Add(firstNames[random.Next(firstNameCount)]);
                        }

                        foreach (string firstName in selectedFirstNames)
                        {
                            //Construct some Athlete details
                            Athlete athlete = new Athlete()
                            {
                                FirstName = firstName,
                                LastName = lastName,
                                MiddleName = lastName[1].ToString().ToUpper(),
                                AthleteCode = random.Next(1111111, 9999999).ToString(),
                                DOB = startDOB.AddDays(random.Next(60, 6500)),
                                Height = random.Next(170, 200),
                                Weight = random.Next(80, 100),
                                Gender = (counter % 2 == 0) ? "W" : "M",
                                Affiliation = affiliations[random.Next(affiliationCount)],
                                MediaInfo = baconNotes[random.Next(5)],
                                ContingentID = contingentIDs[random.Next(contingentIDCount)],
                                SportID = sportIDs[random.Next(sportIDCount)]
                            };
                            counter++;
                            context.Athletes.Add(athlete);
                            try
                            {
                                //Could be duplicate AthleteCode
                                context.SaveChanges();
                            }
                            catch (DbUpdateException)
                            {
                                //Could not insert this one so just skip it.
                                //However, we must remove it from the "cue" in
                                //the context or it will keep trying to save it
                                //and fail over and over again, preventing
                                //any more from being inserted. 
                                context.Remove(athlete);

                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.GetBaseException().Message);
            }
        }
    }
}
