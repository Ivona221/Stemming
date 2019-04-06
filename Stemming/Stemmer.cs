using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Stemming
{
    class Stemmer
    {
        public static void StemWords()
        {
            string sentences = System.IO.File.ReadAllText(@"D:\NLP\POS\sentenseSubSplit.txt");

            var subsentences = sentences.Split('\n');

            var removeChars = new[] { '.', ',', '!', '?', ')', '(', '[', ']', '{', '}', '"', '`', '+', '-', '“', '”', '‘', '“', ';', '„', ':', '/', '\\' };

            var predlozi = new[]
            {
                "без", "во", "в", "врз", "до", "за",
                "зад", "заради", "искрај", "крај", "кај", "каде",
                "како", "кон", "крај", "меѓу", "место", "на", "над",
                "накај", "накрај", "наместо", "наспроти", "насред",
                "низ", "од", "одавде", "оданде", "отаде", "околу",
                "освен", "откај", "по", "под", "покрај", "помеѓу",
                "поради", "посред", "потем", "пред", "през",
                "преку", "при", "против", "среде", "сред",
                "според", "спроти", "спротив", "спрема", "со", "сосе", "у"
            };

            var predloziImenki = new[]
           {
                "без", "во", "в", "врз", "до", "за",
                "зад", "заради", "искрај", "крај", "кај", "каде",
                "кон", "крај", "меѓу", "место", "на", "над",
                "накај", "накрај", "наместо", "наспроти", "насред",
                "низ", "од",
                "освен", "откај", "по", "под", "покрај", "помеѓу",
                "поради", "посред", "потем", "пред", "през",
                "преку", "при", "против", "среде", "сред",
                "според", "спроти", "спротив", "спрема", "со", "сосе", "у"
            };

            var prilozi = new[]
            {
                "кога", "вечер", "утре", "лани", "денес", "доцна",
                "рано", "тогаш", "некогаш", "никогаш", "сега", "одамна",
                "некни", "после", "потоа", "зимоска", "зимава", "понекогаш",
                "оттогаш", "бргу", "дење", "ноќе",
                "каде", "близу", "далеку", "овде", "таму", "онде",
                "горе", "долу", "натаму", "наваму", "напред",
                "назад", "лево", "десно", "налево", "тука",
                "некаде", "никаде", "дома", "озгора", "оздола",
                "како", "добро", "лошо", "силно", "слабо", "така", "вака",
                "инаку", "онака", "брзо", "полека", "машки", "пријателски",
                "тешко", "смешно", "тажно", "некако", "секако",
                "јасно", "чисто", "високо", "ниско",
                "колку", "малку", "многу", "толку", "сосем", "доста",
                "неколку", "николку", "онолку", "двојно", "тројно", "веќе", "премногу", "подоцна"
            };

            var chestici = new[]
            {
                "де", "бе", "ма", "барем", "пак", "меѓутоа", "просто",
                "да",
                "не", "ни", "ниту",
                "зар", "ли", "дали",
                "само", "единствено",
                "точно", "токму", "скоро", "речиси", "рамно",
                "би", "да", "нека", "ќе",
                "исто така", "уште", "притоа",
                "по" , "нај",
                "имено", "токму", "баш",
                "било", "годе",
                "ете", "еве", "ене",
                "го", "ме", "ги", "те", "ве", "ја", "не",
                "им"
            };

            var zamenki = new[]
            {
                "јас", "ти", "тој", "таа", "тоа", "ние", "вие", "тие",
                "мене ме", "тебе те", "него го", "неа ја", "нас нè", "вас ве", "нив ги",
                "мене ми", "тебе ти", "нему му", "нејзе ù", "нам ни", "вам ви", "ним им",
                "себе се", "себе си",
                "кој", "која", "кое", "кои", "што", "чија", "чие", "чиј",
                "чии", "сечиј", "нечиј", "ничиј", "некој", "секој", "никој",
                "оваа", "овој", "ова", "овие", "оној", "онаа", "она", "оние"
            };

            var zamenkiGlagol = new[]
            {
                "јас", "ти", "тој", "таа", "тоа", "ние", "вие", "тие",
                "мене ме", "тебе те", "него го", "неа ја", "нас нè", "вас ве", "нив ги",
                "мене ми", "тебе ти", "нему му", "нејзе ù", "нам ни", "вам ви", "ним им",
                "себе се", "себе си"
            };

            var svrznici = new[]
            {
                "и", "а" , "но", "ама", "или", "да",
                "за да" , "макар што", "поради тоа што",
                "и", "ни", "ниту", "па", "та", "не само што" , "туку и",
                "а", "но", "ама", "туку", "ами", "меѓутоа",
                "само", "само што", "освен што", "единствено",
                "кога", "штом", "штотуку", "тукушто", "откако", "откога", "пред да", "дури", "додека",
                "затоа што", "зашто", "бидејќи", "дека", "оти",
                "така што", "толку што", "такви што", "така што",
                "да", "за да",
                "ако", "да", "без да", "ли",
                "иако", "макар што", "и покрај тоа што", "и да",
                "така како што", "како да", "како божем",
                "што", "кој што", "којшто", "чиј", "чијшто", "каков што", "колкав што",
                "дека", "оти", "како", "што", "да", "дали", "кој", "чиј", "кога"
            };

            var modalniZborovi = new[]
            {
                "се разбира", "значи", "нормално", "природно", "главно", "сигурно",
                "навистина", "секако", "можеби", "веројатно", "очигледно", "бездруго",
                "за жал", "за чудо", "божем", "за среќа", "за несреќа",
                "то ест", "на пример", "впрочем", "најпосле", "без сомнение", "по секоја цена",
                 "на секој начин", "односно"
            };

            var glagolSum = new[]
            {
                "бил", "беше", "биле", "бевме", "си", "е", "сме", "сте", "се"
            };

            var regexForVerbLForm1 = @"\w*л$\b";
            var regexForVerbLForm2 = @"\w*ла$\b";
            var regexForVerbLForm3 = @"\w*ле$\b";
            var regexForVerbLForm4 = @"\w*ле$\b";

            var regexForVerbNoun1 = @"\w*ние$\b";
            var regexForVerbNoun2 = @"\w*ње$\b";
            var regexForNumber = @"\w*мина$\b";

            var regexForCollectiveNouns1 = @"\w*иште$\b";
            var regexForCollectiveNouns2 = @"\w*ишта$\b";

            var regexForChlenuvanje = @"\w*(от|ов|он|та|ва|то|во|те|ве|не)$\b";
            var regexForVerbsPlural = @"\w*вме|вте$\b";

            var regexForPridavki = @"\w*(ски|ест|ен|ји|телен|ичок|узлав)$\b";
            var regexForPridavki1 = @"\w*(ски|ест)$\b";
            var regexForPridavki2 = @"\w*(ен|ји)$\b";
            var regexForPridavki3 = @"\w*ичок$\b";
            var regexForPridavki4 = @"\w*(узлав|телен)$\b";
            var regexForPridavki5 = @"\w*ска$\b";
            var regexForPridavki6 = @"\w*ската$\b";

            var regexForVerbSegashno1 = @"\w*ам$\b";
            var regexForVerbSegashno2 = @"\w*еш$\b";

            var regexForVerbSegashno3 = @"\w*еме$\b";
            var regexForVerbSegashno4 = @"\w*ете$\b";
            var regexForVerbSegashno5 = @"\w*ат$\b";

            var regexForVerbSegashno6 = @"\w*име$\b";
            var regexForVerbSegashno7 = @"\w*ите$\b";

            var regexForVerbSegashno8 = @"\w*aме$\b";
            var regexForVerbSegashno9 = @"\w*aте$\b";

            var regexForProfessions1 = @"\w*ар$\b";
            var regexForProfessions1F = @"\w*арka$\b";
            var regexForProfessions2 = @"\w*ер$\b";
            var regexForProfessions2F = @"\w*ерка$\b";
            var regexForProfessionsWork = @"\w*арство$\b";

            //var regexForPrefix1 = @"^до";
            var regexForPrefix2 = @"^нај";
            var regexForPrefix3 = @"^под";

            var regexForVerbNoun3 = @"\w*ост$\b";
            var regexForVerbNoun4 = @"\w*ство$\b";

            var regexForPluralI = @"\w*ли$\b";
            var regexForPluralI1 = @"\w*њи$\b";
            var regexForPluralI2 = @"\w*ти$\b";
            var regexForPluralI3 = @"\w*чи$\b";





            foreach (var ss in subsentences)
            {
                var subsentence = ss.Trim();

                var words = subsentence.Split(' ');

                string[] sentenceBuffer = new string[100];

                for (int i = 0; i < words.Length; i++)
                {
                    var w = words[i];
                    foreach (var rc in removeChars)
                    {
                        w = w.Replace(rc, ' ');
                    }

                    sentenceBuffer[i] = w + " ";
                }
                //string sent = "";
                //foreach (string word in sentenceBuffer)
                //{
                //    sent += word;
                //}
                //Console.WriteLine(sent);
                Stack<Dictionary<string, string>> stemmedWords = new Stack<Dictionary<string, string>>();

                for (int i = 0; i < sentenceBuffer.Length; i++)
                {

                    string current = sentenceBuffer[i];
                    if (current != "" && current != null)
                    {
                        current = current.Trim();
                        string previous = PeekPrevious(sentenceBuffer, i);
                        var stemmedWord = new Dictionary<string, string>();
                        stemmedWord.Add(current, "");
                       

                        //current
                        if (predlozi.Contains(current.ToLower()) || prilozi.Contains(current.ToLower()) ||
                            chestici.Contains(current.ToLower()) || zamenki.Contains(current.ToLower()) ||
                            svrznici.Contains(current.ToLower()) || modalniZborovi.Contains(current.ToLower()))
                        {
                            stemmedWord[current] = current;
                        }
                        //глаголот сум
                        if (glagolSum.Contains(current))
                        {
                            stemmedWord[current] = "сум";
                        }
                        else
                        {
                            MatchCollection glagolskiImenki1 = Regex.Matches(current, regexForVerbNoun1);
                            MatchCollection glagolskiImenki2 = Regex.Matches(current, regexForVerbNoun2);
                            MatchCollection glagolskiImenki3 = Regex.Matches(current, regexForVerbNoun3);
                            MatchCollection glagolskiImenki4 = Regex.Matches(current, regexForVerbNoun4);

                            MatchCollection number = Regex.Matches(current, regexForNumber);
                            MatchCollection collectiveNouns1 = Regex.Matches(current, regexForCollectiveNouns1);
                            MatchCollection collectiveNouns2 = Regex.Matches(current, regexForCollectiveNouns2);
                            MatchCollection chlenvanje = Regex.Matches(current, regexForChlenuvanje);
                            MatchCollection glagoli = Regex.Matches(current, regexForVerbsPlural);
                            MatchCollection pridavki = Regex.Matches(current, regexForPridavki);
                            MatchCollection glagolSegashno1 = Regex.Matches(current, regexForVerbSegashno1);
                            MatchCollection glagolSegashno2 = Regex.Matches(current, regexForVerbSegashno2);
                            MatchCollection glagolSegashno3 = Regex.Matches(current, regexForVerbSegashno3);
                            MatchCollection glagolSegashno4 = Regex.Matches(current, regexForVerbSegashno4);
                            MatchCollection glagolSegashno5 = Regex.Matches(current, regexForVerbSegashno5);
                            MatchCollection glagolSegashno6 = Regex.Matches(current, regexForVerbSegashno6);
                            MatchCollection glagolSegashno7 = Regex.Matches(current, regexForVerbSegashno7);
                            MatchCollection glagolSegashno8 = Regex.Matches(current, regexForVerbSegashno8);
                            MatchCollection glagolSegashno9 = Regex.Matches(current, regexForVerbSegashno9);

                            MatchCollection pridavki1 = Regex.Matches(current, regexForPridavki1);
                            MatchCollection pridavki2 = Regex.Matches(current, regexForPridavki2);
                            MatchCollection pridavki3 = Regex.Matches(current, regexForPridavki3);
                            MatchCollection pridavki4 = Regex.Matches(current, regexForPridavki4);
                            MatchCollection pridavki5 = Regex.Matches(current, regexForPridavki5);
                            MatchCollection pridavki6 = Regex.Matches(current, regexForPridavki6);

                            MatchCollection verbLForm = Regex.Matches(current, regexForVerbLForm1);
                            MatchCollection verbLForm1 = Regex.Matches(current, regexForVerbLForm2);
                            MatchCollection verbLForm2 = Regex.Matches(current, regexForVerbLForm3);
                            MatchCollection verbLForm3 = Regex.Matches(current, regexForVerbLForm4);

                            MatchCollection professions1M = Regex.Matches(current, regexForProfessions1);
                            MatchCollection professions1F = Regex.Matches(current, regexForProfessions1F);
                            MatchCollection professions2M = Regex.Matches(current, regexForProfessions2);
                            MatchCollection professions2F = Regex.Matches(current, regexForProfessions2F);
                            MatchCollection professionsWork = Regex.Matches(current, regexForProfessionsWork);

                            //MatchCollection prefix1 = Regex.Matches(current, regexForPrefix1);
                            MatchCollection prefix2 = Regex.Matches(current, regexForPrefix2);
                            MatchCollection prefix3 = Regex.Matches(current, regexForPrefix3);

                            MatchCollection plural1 = Regex.Matches(current, regexForPluralI);
                            MatchCollection plural2 = Regex.Matches(current, regexForPluralI1);
                            MatchCollection plural3 = Regex.Matches(current, regexForPluralI2);
                            MatchCollection plural4 = Regex.Matches(current, regexForPluralI3);

                            if (glagolSegashno1.Count == 1 || glagolSegashno2.Count == 1 || glagolSegashno5.Count == 1)
                            {
                                stemmedWord[current] = current.Substring(0, current.Length - 2);
                            }
                            if (glagolSegashno3.Count == 1 || glagolSegashno3.Count == 1
                                || glagolSegashno4.Count == 1)
                            {
                                stemmedWord[current] = current.Substring(0, current.Length - 3);
                            }
                            if (glagolSegashno6.Count == 1 || glagolSegashno7.Count == 1
                                || glagolSegashno8.Count == 1 || glagolSegashno9.Count == 1)
                            {
                                stemmedWord[current] = current.Substring(0, current.Length - 3);
                            }
                            if (glagolskiImenki1.Count == 1)
                            {
                                stemmedWord[current] = current.Substring(0, current.Length - 3);
                            }
                            if (glagolskiImenki2.Count == 1)
                            {
                                stemmedWord[current] = current.Substring(0, current.Length - 2);
                            }
                            if (glagolskiImenki3.Count == 1)
                            {
                                stemmedWord[current] = current.Substring(0, current.Length - 3);
                            }
                            if (glagolskiImenki4.Count == 1)
                            {
                                stemmedWord[current] = current.Substring(0, current.Length - 4);
                            }
                            if (number.Count == 1)
                            {
                                stemmedWord[current] = current.Substring(0, current.Length - 4);
                            }

                            if (collectiveNouns1.Count == 1 || collectiveNouns2.Count == 1)
                            {
                                stemmedWord[current] = current.Substring(0, current.Length - 4);
                            }      
                            if (chlenvanje.Count >= 1)
                            {
                                stemmedWord[current] = current.Substring(0, current.Length - 2);
                            }
                            if (glagoli.Count >= 1)
                            {
                                stemmedWord[current] = current.Substring(0, current.Length - 3);
                            }
                            if (pridavki1.Count >= 1)
                            {
                                stemmedWord[current] = current.Substring(0, current.Length - 3);
                            }
                            if (pridavki2.Count >= 1)
                            {
                                stemmedWord[current] = current.Substring(0, current.Length - 2);
                            }
                            if (pridavki3.Count >= 1)
                            {
                                stemmedWord[current] = current.Substring(0, current.Length - 4);
                            }
                            if (pridavki4.Count >= 1)
                            {
                                stemmedWord[current] = current.Substring(0, current.Length - 5);
                            }
                            if (verbLForm.Count >= 1)
                            {
                                stemmedWord[current] = current.Substring(0, current.Length - 1);
                            }
                            if (verbLForm1.Count >= 1)
                            {
                                stemmedWord[current] = current.Substring(0, current.Length - 2);
                            }
                            if (verbLForm2.Count >= 1)
                            {
                                stemmedWord[current] = current.Substring(0, current.Length - 2);
                            }
                            if (professions1M.Count >= 1 || professions2M.Count >= 1)
                            {
                                stemmedWord[current] = current.Substring(0, current.Length - 2);
                            }
                            if (professions1F.Count >= 1 || professions2F.Count >= 1)
                            {
                                stemmedWord[current] = current.Substring(0, current.Length - 4);
                            }
                            if (professionsWork.Count >= 1)
                            {
                                stemmedWord[current] = current.Substring(0, current.Length - 6);
                            }
                            if (prefix2.Count >= 1)
                            {
                                stemmedWord[current] = String.Concat(current.Skip(3));
                            }
                            if (prefix3.Count >= 1)
                            {
                                stemmedWord[current] = String.Concat(current.Skip(3));
                            }
                            if (pridavki5.Count >= 1)
                            {
                                stemmedWord[current] = current.Substring(0, current.Length - 3);
                            }
                            if (pridavki6.Count >= 1)
                            {
                                stemmedWord[current] = current.Substring(0, current.Length - 5);
                            }
                            if (previous != null && (current != null && current != "" && (char.IsUpper(current[0]) || IsUpper(current[0]))))
                            {
                                stemmedWord[current] = "Not stemmed";
                            }
                            //родители, писатели
                            if (plural1.Count >= 1)
                            {
                                stemmedWord[current] = current.Substring(0, current.Length - 1);
                            }
                            //коњи
                            if (plural2.Count >= 1)
                            {
                                stemmedWord[current] = current.Substring(0, current.Length - 1);
                            }
                            //прсти
                            if (plural3.Count >= 1)
                            {
                                stemmedWord[current] = current.Substring(0, current.Length - 1);
                            }
                            //гледачи
                            if (plural4.Count >= 1)
                            {
                                stemmedWord[current] = current.Substring(0, current.Length - 1);
                            }
                            if(verbLForm3.Count >= 1)
                            {
                                stemmedWord[current] = current.Substring(0, current.Length - 2);
                            }
                            

                        }
                        //Console.WriteLine(taggedWord);

                        if (current != null && stemmedWord.ContainsKey(current) && stemmedWord[current] != "")
                        {
                            stemmedWords.Push(stemmedWord);
                        }
                    }

                }
                Dictionary<string, string> allWords = new Dictionary<string, string>();
                var j = 0;
                var k = 0;
                foreach (var s in sentenceBuffer)
                {
                    if (s != "" && s != null)
                    {
                        var flag = 0;

                        foreach (var t in stemmedWords)
                        {
                            foreach (KeyValuePair<string, string> entry in t)
                            {
                                if (string.Compare(s.Trim().ToString(), entry.Key.Trim().ToString()) == 0)
                                {
                                    flag = 1;
                                    if (!allWords.ContainsKey(s))
                                    {
                                        allWords.Add(s, entry.Value);
                                    }
                                    else
                                    {
                                        allWords.Add(s + j, entry.Value);
                                        j++;
                                    }

                                    break;
                                }
                                // do something with entry.Value or entry.Key

                            }
                            if (flag == 1)
                            {
                                break;
                            }
                        }
                        if (flag == 0)
                        {
                            if (!allWords.ContainsKey(s))
                            {
                                allWords.Add(s, "Not stemmed");
                            }
                            else
                            {
                                allWords.Add(s + k, "Not stemmed");
                                k++;
                            }

                        }
                    }

                }
                using (System.IO.StreamWriter file =
                new System.IO.StreamWriter(@"D:\NLP\Stemming\stemmedWords.txt", true))
                {
                    foreach (KeyValuePair<string, string> entry in allWords)
                    {
                        // do something with entry.Value or entry.Key
                        file.WriteLine(entry.Key + " ----> " + entry.Value);
                    }
                    file.WriteLine("-----------------------------------------------------------------");

                }


            }

        }

        private static string PeekNext(string[] content, int index)
        {
            var nextIndex = index + 1;
            if (content.Length < 0) return null;
            if (nextIndex >= content.Length) return null;

            return content[nextIndex];
        }

        public static string PeekPrevious(string[] content, int index)
        {
            var prevIndex = index - 1;
            if (content.Length < 0) return null;
            if (prevIndex < 0) return null;

            return content[prevIndex];
        }

        public static bool IsUpper(char c)
        {
            var capitalLetters = new[]
            {
                'А', 'Б', 'В', 'Г', 'Д', 'Ѓ', 'Е', 'Ж', 'З', 'Ѕ', 'И', 'Ј', 'К',
                'Л', 'Љ', 'М', 'Н', 'Њ', 'О', 'П', 'Р', 'С', 'Т', 'Ќ', 'У', 'Ф', 'Х', 'Ц', 'Ч', 'Џ', 'Ш'
            };
            if (capitalLetters.Contains(c))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
