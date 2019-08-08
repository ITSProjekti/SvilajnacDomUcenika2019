using DomUcenikaSvilajnac.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace DomUcenikaSvilajnac.Common.Services
{
    public sealed class LatUCirTransliterator : ITransliterator
    {
        static LatUCirTransliterator instance;
        private static readonly IDictionary<string, string> dic;


        public static LatUCirTransliterator Instance
        {
            get { return instance ?? (instance = new LatUCirTransliterator()); }
        }

        static LatUCirTransliterator()
        {
                dic = new Dictionary<string, string>() {
                {"A", "А"},   //A
                {"B", "Б"},   //B
                {"C", "Ц"},   //C
                {"D", "Д"},   //D
                {"E", "Е"},   //E
                {"F", "Ф"},   //F
                {"G", "Г"},   //G
                {"H", "Х"},   //H
                {"I", "И"},   //I
                {"J", "Ј"},   //J
                {"K", "К"},   //K
                {"L", "Л"},   //L
                {"M", "М"},   //M
                {"N", "Н"},   //N
                {"O", "О"},   //O
                {"P", "П"},   //P
                {"Q", "КВ"},  //Q
                {"R", "Р"},   //R
                {"S", "С"},   //S
                {"T", "Т"},   //T
                {"U", "У"},   //U
                {"V", "В"},   //V
                {"W", "В"},   //W
                {"X", "КС"},  //X
                {"Y", "" },   //Y
                {"Z", "З"},   //Z
                {"Š", "Ш"},   //Š
                {"Đ", "Ђ"},   //Đ
                {"Ž", "Ж"},   //Ž
                {"LJ", "Љ"},  //LJ
                {"Lj", "Љ"},  //Lj
                {"NJ", "Њ"},  //NJ
                {"Nj", "Њ"},  //Nj
                {"DŽ", "Џ"},  //DŽ
                {"Dž", "Џ"},  //Dž
                {"Č", "Ч"},   //Č
                {"Ć", "Ћ"},   //Ć
                {"a" ,"а"},   //a
                {"b","б" },   //b
                {"c","ц" },   //c
                {"d","д" },   //d
                {"e","е" },   //e
                {"f","ф" },   //f
                {"g","г" },   //g
                {"h","х" },   //h
                {"i", "и"},   //i
                {"j", "ј"},   //j
                {"k", "к"},   //k
                {"l", "л"},   //l
                {"m", "м"},   //m
                {"n", "н"},   //n
                {"o", "о"},   //o
                {"p", "п"},   //p
                {"q", "кв"},  //q
                {"r", "р"},   //r
                {"s", "с"},   //s
                {"t", "т"},   //t
                {"u", "у"},   //u
                {"v", "в"},   //v
                {"w", "в"},   //w
                {"x","кс"},   //x
                {"y", "" },   //y
                {"z", "з"},   //z
                {"š", "ш"},   //š
                {"đ", "ђ"},   //đ
                {"ž", "ж"},   //ž
                {"nj", "њ"},  //nj
                {"lj", "љ"},  //nj
                {"dž", "џ"},  //dž
                {"č", "ч"},   //č
                {"ć", "ћ"}    //ć
            };
        }

        public string Transliterate ( string input)
        {
            int i = 0;
            string cirilica = "";
            foreach (char karakter in input)
            {
                string cirilicnoSlovo;

                if (dic.ContainsKey(karakter.ToString()))//Provera da li karakter postoji u nasem setu
                {
                    if(input.Length != i + 1)//Ako je poslednje slovo ne proveravamo da li ide j ili dž posle njega
                    {
                        if ((karakter == 'N' && input[i + 1] == 'J') || (karakter == 'N' && input[i + 1] == 'j')) //U slucaju da N i J/j idu jedno za drugim, vraćamo "NJ" jer je ono ključ za ćirilično
                            cirilicnoSlovo = dic["NJ"];
                        else if ((karakter == 'L' && input[i + 1] == 'J') || (karakter == 'L' && input[i + 1] == 'j')) //U slucaju da L i J/j idu jedno za drugim, vraćamo "LJ" jer je ono ključ za ćirilično
                            cirilicnoSlovo = dic["LJ"];
                        else if ((karakter == 'D' && input[i + 1] == 'Ž') || (karakter == 'D' && input[i + 1] == 'ž')) //U slucaju da D i Ž/ž idu jedno za drugim, vraćamo "NJ" jer je ono ključ za ćirilično
                            cirilicnoSlovo = dic["DŽ"];
                        else if ((karakter == 'l' && input[i + 1] == 'j')) //U slucaju da l i j idu jedno za drugim, vraćamo "lj" jer je ono ključ za ćirilično
                            cirilicnoSlovo = dic["lj"];
                        else if ((karakter == 'n' && input[i + 1] == 'j')) //U slucaju da n i j idu jedno za drugim, vraćamo "nj" jer je ono ključ za ćirilično
                            cirilicnoSlovo = dic["nj"];
                        else if ((karakter == 'd' && input[i + 1] == 'ž')) //U slucaju da d i ž idu jedno za drugim, vraćamo "dž" jer je ono ključ za ćirilično
                            cirilicnoSlovo = dic["dž"];
                        else
                            cirilicnoSlovo = dic[karakter.ToString()];
                    }
                    else
                        cirilicnoSlovo = dic[karakter.ToString()];

                    if (i > 0) //Za prvo slovo ne proveravamo da li pre njega ide N,L,D..
                    {
                        if (!(cirilicnoSlovo == "Ј" && input[i - 1] == 'N') && !(cirilicnoSlovo == "ј" && input[i - 1] == 'N') //Provera da li pre J/j ide N, ako ne ide ispisujemo J/j, ako ide preskačemo
                        && !(cirilicnoSlovo == "Ј" && input[i - 1] == 'L') && !(cirilicnoSlovo == "ј" && input[i - 1] == 'L') //Provera da li pre J/j ide L, ako ne ide ispisujemo J/j, ako ide preskačemo
                        && !(cirilicnoSlovo == "Ж" && input[i - 1] == 'D') && !(cirilicnoSlovo == "ж" && input[i - 1] == 'D') //Provera da li pre Ž/ž ide D, ako ne ide ispisujemo Ž/ž, ako ide preskačemo
                        && !(cirilicnoSlovo == "ж" && input[i - 1] == 'd')  //Provera da li pre ž ide d, ako ne ide ispisujemo ž, ako ide preskačemo
                        && !(cirilicnoSlovo == "ј" && input[i - 1] == 'n')  //Provera da li pre j ide n, ako ne ide ispisujemo j, ako ide preskačemo
                        && !(cirilicnoSlovo == "ј" && input[i - 1] == 'l')) //Provera da li pre j ide l, ako ne ide ispisujemo j, ako ide preskačemo
                            cirilica += cirilicnoSlovo;
                    }
                    else
                    {
                        cirilica += cirilicnoSlovo;
                    }
                       

                }
                else
                {
                    cirilica += karakter;//Ako ne postoji ćirilični prevod ispisujemo originalan 
                }
                i++;
            }
            return cirilica;
        }
        
    }
}
