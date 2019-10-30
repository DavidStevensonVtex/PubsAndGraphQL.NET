using System;
using System.Collections.Generic;

namespace Pubs.Types
{
    public class PubsData
    {
        public static List<Author> Authors = new List<Author>()
        {
            new Author( "172-32-1176", "White", "Johnson", "408 496-7223", "10932 Bigge Rd.", "Menlo Park", "CA", "94025", true ),
            new Author( "213-46-8915", "Green", "Marjorie", "415 986-7020", "309 63rd St. #411", "Oakland", "CA", "94618", true ),
            new Author( "238-95-7766", "Carson", "Cheryl", "415 548-7723", "589 Darwin Ln.", "Berkeley", "CA", "94705", true ),
            new Author( "267-41-2394", "O'Leary", "Michael", "408 286-2428", "22 Cleveland Av. #14", "San Jose", "CA", "95128", true ),
            new Author( "274-80-9391", "Straight", "Dean", "415 834-2919", "5420 College Av.", "Oakland", "CA", "94609", true ),
            new Author( "341-22-1782", "Smith", "Meander", "913 843-0462", "10 Mississippi Dr.", "Lawrence", "KS", "66044", false ),
            new Author( "409-56-7008", "Bennet", "Abraham", "415 658-9932", "6223 Bateman St.", "Berkeley", "CA", "94705", true ),
            new Author( "427-17-2319", "Dull", "Ann", "415 836-7128", "3410 Blonde St.", "Palo Alto", "CA", "94301", true ),
            new Author( "472-27-2349", "Gringlesby", "Burt", "707 938-6445", "PO Box 792", "Covelo", "CA", "95428", true ),
            new Author( "486-29-1786", "Locksley", "Charlene", "415 585-4620", "18 Broadway Av.", "San Francisco", "CA", "94130", true ),
            new Author( "527-72-3246", "Greene", "Morningstar", "615 297-2723", "22 Graybar House Rd.", "Nashville", "TN", "37215", false ),
            new Author( "648-92-1872", "Blotchet-Halls", "Reginald", "503 745-6402", "55 Hillsdale Bl.", "Corvallis", "OR", "97330", true ),
            new Author( "672-71-3249", "Yokomoto", "Akiko", "415 935-4228", "3 Silver Ct.", "Walnut Creek", "CA", "94595", true ),
            new Author( "712-45-1867", "del Castillo", "Innes", "615 996-8275", "2286 Cram Pl. #86", "Ann Arbor", "MI", "48105", true ),
            new Author( "722-51-5454", "DeFrance", "Michel", "219 547-9982", "3 Balding Pl.", "Gary", "IN", "46403", true ),
            new Author( "724-08-9931", "Stringer", "Dirk", "415 843-2991", "5420 Telegraph Av.", "Oakland", "CA", "94609", false ),
            new Author( "724-80-9391", "MacFeather", "Stearns", "415 354-7128", "44 Upland Hts.", "Oakland", "CA", "94612", true ),
            new Author( "756-30-7391", "Karsen", "Livia", "415 534-9219", "5720 McAuley St.", "Oakland", "CA", "94609", true ),
            new Author( "807-91-6654", "Panteley", "Sylvia", "301 946-8853", "1956 Arlington Pl.", "Rockville", "MD", "20853", true ),
            new Author( "846-92-7186", "Hunter", "Sheryl", "415 836-7128", "3410 Blonde St.", "Palo Alto", "CA", "94301", true ),
            new Author( "893-72-1158", "McBadden", "Heather", "707 448-4982", "301 Putnam", "Vacaville", "CA", "95688", false ),
            new Author( "899-46-2035", "Ringer", "Anne", "801 826-0752", "67 Seventh Av.", "Salt Lake City", "UT", "84152", true ),
            new Author( "998-72-3567", "Ringer", "Albert", "801 826-0752", "67 Seventh Av.", "Salt Lake City", "UT", "84152", true )
        };

        public static List<TitleAuthor> TitleAuthors = new List<TitleAuthor>()
        {
            new TitleAuthor( "172-32-1176", "PS3333", 1, 100 ),
            new TitleAuthor( "213-46-8915", "BU1032", 2, 40 ),
            new TitleAuthor( "213-46-8915", "BU2075", 1, 100 ),
            new TitleAuthor( "238-95-7766", "PC1035", 1, 100 ),
            new TitleAuthor( "267-41-2394", "BU1111", 2, 40 ),
            new TitleAuthor( "267-41-2394", "TC7777", 2, 30 ),
            new TitleAuthor( "274-80-9391", "BU7832", 1, 100 ),
            new TitleAuthor( "409-56-7008", "BU1032", 1, 60 ),
            new TitleAuthor( "427-17-2319", "PC8888", 1, 50 ),
            new TitleAuthor( "472-27-2349", "TC7777", 3, 30 ),
            new TitleAuthor( "486-29-1786", "PC9999", 1, 100 ),
            new TitleAuthor( "486-29-1786", "PS7777", 1, 100 ),
            new TitleAuthor( "648-92-1872", "TC4203", 1, 100 ),
            new TitleAuthor( "672-71-3249", "TC7777", 1, 40 ),
            new TitleAuthor( "712-45-1867", "MC2222", 1, 100 ),
            new TitleAuthor( "722-51-5454", "MC3021", 1, 75 ),
            new TitleAuthor( "724-80-9391", "BU1111", 1, 60 ),
            new TitleAuthor( "724-80-9391", "PS1372", 2, 25 ),
            new TitleAuthor( "756-30-7391", "PS1372", 1, 75 ),
            new TitleAuthor( "807-91-6654", "TC3218", 1, 100 ),
            new TitleAuthor( "846-92-7186", "PC8888", 2, 50 ),
            new TitleAuthor( "899-46-2035", "MC3021", 2, 25 ),
            new TitleAuthor( "899-46-2035", "PS2091", 2, 50 ),
            new TitleAuthor( "998-72-3567", "PS2091", 1, 50 ),
            new TitleAuthor( "998-72-3567", "PS2106", 1, 100 )
        };

        public static List<Title> Titles = new List<Title>()
        {
            new Title( "BU1032", "The Busy Executive's Database Guide", "business", "1389", 19.99m, 5000.00m, 10, 4095, "An overview of available database systems with emphasis on common business applications. Illustrated.", new DateTime( 1991, 6, 12) ),
            new Title( "BU1111", "Cooking with Computers: Surreptitious Balance Sheets", "business", "1389", 11.95m, 5000.00m, 10, 3876, "Helpful hints on how to use your electronic resources to the best advantage.", new DateTime( 1991, 6, 9) ),
            new Title( "BU2075", "You Can Combat Computer Stress!", "business", "0736", 2.99m, 10125.00m, 24, 18722, "The latest medical and psychological techniques for living with the electronic office. Easy-to-understand explanations.", new DateTime( 1991, 6, 30) ),
            new Title( "BU7832", "Straight Talk About Computers", "business", "1389", 19.99m, 5000.00m, 10, 4095, "Annotated analysis of what computers can do for you: a no-hype guide for the critical user.", new DateTime( 1991, 6, 22) ),
            new Title( "MC2222", "Silicon Valley Gastronomic Treats", "mod_cook", "0877", 19.99m, 0.00m, 12, 2032, "Favorite recipes for quick, easy, and elegant meals.", new DateTime( 1991, 6, 9) ),
            new Title( "MC3021", "The Gourmet Microwave", "mod_cook", "0877", 2.99m, 15000.00m, 24, 22246, "Traditional French gourmet recipes adapted for modern microwave cooking.", new DateTime( 1991, 6, 18) ),
            new Title( "MC3026", "The Psychology of Computer Cooking", "UNDECIDED", "0877", null, null, null, null, null, new DateTime( 2019, 10, 12) ),
            new Title( "PC1035", "But Is It User Friendly?", "popular_comp", "1389", 22.95m, 7000.00m, 16, 8780, "A survey of software for the naive user, focusing on the 'friendliness' of each.", new DateTime( 1991, 6, 30) ),
            new Title( "PC8888", "Secrets of Silicon Valley", "popular_comp", "1389", 20.00m, 8000.00m, 10, 4095, "Muckraking reporting on the world's largest computer hardware and software manufacturers.", new DateTime( 1994, 6, 12) ),
            new Title( "PC9999", "Net Etiquette", "popular_comp", "1389", null, null, null, null, "A must-read for computer conferencing.", new DateTime( 2019, 10, 12) ),
            new Title( "PS1372", "Computer Phobic AND Non-Phobic Individuals: Behavior Variations", "psychology", "0877", 21.59m, 7000.00m, 10, 375, "A must for the specialist, this book examines the difference between those who hate and fear computers and those who don't.", new DateTime( 1991, 10, 21) ),
            new Title( "PS2091", "Is Anger the Enemy?", "psychology", "0736", 10.95m, 2275.00m, 12, 2045, "Carefully researched study of the effects of strong emotions on the body. Metabolic charts included.", new DateTime( 1991, 6, 15) ),
            new Title( "PS2106", "Life Without Fear", "psychology", "0736", 7.00m, 6000.00m, 10, 111, "New exercise, meditation, and nutritional techniques that can reduce the shock of daily interactions. Popular audience. Sample menus included, exercise video available separately.", new DateTime( 1991, 10, 5) ),
            new Title( "PS3333", "Prolonged Data Deprivation: Four Case Studies", "psychology", "0736", 19.99m, 2000.00m, 10, 4072, "What happens when the data runs dry?  Searching evaluations of information-shortage effects.", new DateTime( 1991, 6, 12) ),
            new Title( "PS7777", "Emotional Security: A New Algorithm", "psychology", "0736", 7.99m, 4000.00m, 10, 3336, "Protecting yourself and your loved ones from undue emotional stress in the modern world. Use of computer and nutritional aids emphasized.", new DateTime( 1991, 6, 12) ),
            new Title( "TC3218", "Onions, Leeks, and Garlic: Cooking Secrets of the Mediterranean", "trad_cook", "0877", 20.95m, 7000.00m, 10, 375, "Profusely illustrated in color, this makes a wonderful gift book for a cuisine-oriented friend.", new DateTime( 1991, 10, 21) ),
            new Title( "TC4203", "Fifty Years in Buckingham Palace Kitchens", "trad_cook", "0877", 11.95m, 4000.00m, 14, 15096, "More anecdotes from the Queen's favorite cook describing life among English royalty. Recipes, techniques, tender vignettes.", new DateTime( 1991, 6, 12) ),
            new Title( "TC7777", "Sushi, Anyone?", "trad_cook", "0877", 14.99m, 8000.00m, 10, 4095, "Detailed instructions on how to make authentic Japanese sushi in your spare time.", new DateTime( 1991, 6, 12) )
        };

        public static List<Publisher> Publishers = new List<Publisher>()
        {
            new Publisher( "0736", "New Moon Books", "Boston", "MA", "USA" ),
            new Publisher( "0877", "Binnet & Hardley", "Washington", "DC", "USA" ),
            new Publisher( "1389", "Algodata Infosystems", "Berkeley", "CA", "USA" ),
            new Publisher( "1622", "Five Lakes Publishing", "Chicago", "IL", "USA" ),
            new Publisher( "1756", "Ramona Publishers", "Dallas", "TX", "USA" ),
            new Publisher( "9901", "GGG&G", "Munich", "nu", "Germany" ),
            new Publisher( "9952", "Scootney Books", "New York", "NY", "USA" ),
            new Publisher( "9999", "Lucerne Publishing", "Paris", "nu", "France" ),
        };
    }
}
